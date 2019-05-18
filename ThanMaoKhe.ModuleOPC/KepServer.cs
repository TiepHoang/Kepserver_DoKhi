using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanMaoKhe.ModuleOPC
{
    public class KepServer : IDisposable
    {
        public ILogServer Log { get; set; }
        public Action<string, eKepserverStatus> ActionServerShutDown { get; }

        private OPCServer _OPCServer;
        private OPCGroup _OPCGroup;
        private eKepserverStatus _Status;

        private HashSet<OPCValue> _lstOPC;
        private object RequestedDataTypes;
        private Array ServerHandles;
        private Array ClientHandles;
        private Array ItemOPCs;

        public string NameKepserver { get; private set; }
        public object AccessPaths { get; private set; }

        public delegate void StatusChangeHandle(eKepserverStatus status);
        public event StatusChangeHandle StatusChange;

        public delegate void EndUpdateViewHandle();
        public event EndUpdateViewHandle EndUpdateView;

        public eKepserverStatus Status
        {
            get => _Status;
            set
            {
                _Status = value;
                StatusChange?.Invoke(Status);
            }
        }

        public KepServer(ILogServer Log, Action<string, eKepserverStatus> ActionServerShutDown)
        {
            this.Log = Log;
            this.ActionServerShutDown = ActionServerShutDown;
            Status = eKepserverStatus.OFF;
        }

        public void Dispose()
        {
            _OPCServer?.Disconnect();
            Status = eKepserverStatus.OFF;
        }

        public bool IsRuningEvent => Status == eKepserverStatus.ON;

        public void Resume()
        {
            Status = eKepserverStatus.ON;
        }
        public void Pause()
        {
            Status = eKepserverStatus.PAUSE;
        }

        public async Task<bool> ConfigAsync(string nameKepServer, List<OPCValue> lstOPC)
        {
            try
            {
                _log($"config nameKepServer = {nameKepServer}");
                this.NameKepserver = nameKepServer;
                _OPCServer?.Disconnect();

                _OPCServer = new OPCServer();
                _OPCServer.ServerShutDown += _OPCServer_ServerShutDown;
                _OPCServer.GetOPCServers();
                _OPCServer.Connect(this.NameKepserver);

                _OPCGroup = _OPCServer.OPCGroups.Add("Group1");
                _OPCGroup.DeadBand = 0;
                _OPCGroup.UpdateRate = 100;
                _OPCGroup.IsSubscribed = true;
                _OPCGroup.IsActive = true;

                _OPCGroup.DataChange += _OPCGroup_DataChange;
                _OPCGroup.AsyncCancelComplete += _OPCGroup_AsyncCancelComplete;
                _OPCGroup.AsyncReadComplete += _OPCGroup_AsyncReadComplete;
                _OPCGroup.AsyncWriteComplete += _OPCGroup_AsyncWriteComplete;

                int NumItems = lstOPC.Count + 1; //bỏ qua index = 0.
                ServerHandles = Array.CreateInstance(typeof(Int32), NumItems);
                Array Errors = Array.CreateInstance(typeof(Int32), NumItems);
                RequestedDataTypes = Array.CreateInstance(typeof(Int16), NumItems);
                AccessPaths = Array.CreateInstance(typeof(string), NumItems);
                ItemOPCs = Array.CreateInstance(typeof(string), NumItems);
                ClientHandles = Array.CreateInstance(typeof(Int32), NumItems);

                this._lstOPC = new HashSet<OPCValue>();
                int indexInList = 0;
                for (int i = 1; i <= lstOPC.Count; i++)
                {
                    var opc = lstOPC[indexInList];
                    opc.Qualiti = eQualiti.OPCQualityBad;
                    opc.Time = DateTime.Now;
                    opc.IndexInList = indexInList;

                    _lstOPC.Add(opc);
                    ItemOPCs.SetValue(opc.Name, i);
                    _log($"listening [{opc.Name}]");
                    ClientHandles.SetValue(opc.IndexInList, i);
                    indexInList++;
                }
                if (_lstOPC.Count > 0)
                {
                    _OPCGroup.OPCItems.AddItems(_lstOPC.Count, ItemOPCs, ClientHandles, out ServerHandles, out Errors, RequestedDataTypes, AccessPaths);
                }

                Status = eKepserverStatus.ON;
                _log("Kepserver oke.");
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _log(ex);
                Dispose();
            }
            Status = eKepserverStatus.OFF;
            return false;
        }
        private void _OPCGroup_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            throw new NotImplementedException();
        }

        private void _OPCGroup_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            throw new NotImplementedException();
        }

        private void _OPCGroup_AsyncCancelComplete(int CancelID)
        {
            throw new NotImplementedException();
        }

        private void _OPCServer_ServerShutDown(string Reason)
        {
            _log("ServerShutDown");
            Status = eKepserverStatus.OFF;

            ActionServerShutDown?.Invoke(Reason, Status);
        }

        public async Task Reread()
        {
            _log("0. rereading...");
            try
            {
                Array Errors = Array.CreateInstance(typeof(Int32), _lstOPC.Count + 1);
                Array Values = Array.CreateInstance(typeof(Int32), _lstOPC.Count + 1);
                object value = null;
                _log("1. SyncRead begin...");
                _OPCGroup.SyncRead((short)OPCDataSource.OPCDevice, _lstOPC.Count, ServerHandles, out Errors, out Values, out object Qualities, out object TimeStamps);
                _log("2. SyncRead end.");

                for (int i = 1; i < Values.Length; i++)
                {
                    try
                    {
                        var quality = (eQualiti)((short)(Qualities as Array).GetValue(i));
                        var time = (DateTime)((TimeStamps as Array).GetValue(i));
                        var opc = _lstOPC.ElementAt(i - 1);
                        opc.Qualiti = quality;
                        opc.Time = time;
                        value = Values.GetValue(i);
                        opc.Value = opc.ConvertValue?.Invoke(value) ?? value;
                        _callUpdateEntity(opc);
                    }
                    catch (Exception ex)
                    {
                        _log(ex);
                    }
                }
                _log("3. reread success!");
                _callEndUpdateView();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _log(ex);
                _log("3. reread error!");
            }
        }

        public bool OPCWrite(string OPCname, object value, ref string messageError)
        {
            try
            {
                var opcValue = _lstOPC.FirstOrDefault(q => q.Name == OPCname);
                if (opcValue == null)
                {
                    messageError = $"OPC {OPCname} chưa được nạp vào phần mềm!";
                }
                else
                {
                    int ServerHandle = (int)ServerHandles.GetValue(opcValue.IndexInList + 1);
                    var opc = _OPCGroup.OPCItems.GetOPCItem(ServerHandle);
                    opc.Write(value);
                    return true;
                }
            }
            catch (Exception ex)
            {
                messageError = $"Lỗi phần mềm! {ex.Message}";
                _log(ex);
            }
            return false;
        }

        public bool OPCWrite(IEnumerable<OPCValue> opcs, ref string messageError)
        {
            try
            {
                int NumItem = opcs.Count() + 1;
                if (NumItem > ServerHandles.Length)
                {
                    throw new Exception("opcs vượt quá số lượng!");
                }
                else
                {
                    var opcValues = from o in _lstOPC
                                    join item in opcs
                                    on o.Name equals item.Name
                                    select new OPCValue
                                    {
                                        IndexInList = o.IndexInList,
                                        KeyOPC = o.KeyOPC,
                                        Name = o.Name,
                                        Qualiti = o.Qualiti,
                                        Time = o.Time,
                                        Value = item.Value,
                                    };
                    Array Value = Array.CreateInstance(typeof(object), NumItem);
                    Array ServerHandle = Array.CreateInstance(typeof(Int32), NumItem);
                    Array Error = Array.CreateInstance(typeof(Int32), NumItem);
                    NumItem = 0;
                    foreach (var item in opcValues)
                    {
                        Value.SetValue(item.Value, ++NumItem);
                        ServerHandle.SetValue(ServerHandles.GetValue(item.IndexInList + 1), NumItem);
                    }
                    _OPCGroup.SyncWrite(NumItem, ServerHandle, Value, out Error);
                    return true;
                }
            }
            catch (Exception ex)
            {
                messageError = $"Lỗi phần mềm! {ex.Message}";
                _log(ex);
            }
            return false;
        }

        private void _OPCGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            int client = 0;
            object value = null;
            object time = null;
            object Qualiti = null;
            for (int i = 1; i <= NumItems; i++)
            {
                client = (int)ClientHandles.GetValue(i);
                var opc = _lstOPC.ElementAt(client);
                if (opc == null)
                {
                    _log($"Không tìm thấy OPCItem Index = {client}", true);
                }
                else
                {
                    time = TimeStamps.GetValue(i);
                    Qualiti = Qualities.GetValue(i);

                    opc.Time = (DateTime?)TimeStamps.GetValue(i);
                    opc.Qualiti = (eQualiti)Qualiti;
                    opc.Time = (DateTime?)time;
                    value = ItemValues.GetValue(i);
                    opc.Value = opc.ConvertValue?.Invoke(value) ?? value;

                    _callUpdateEntity(opc);
                }
            }
            _callEndUpdateView();
        }

        private void _callEndUpdateView()
        {
            if (IsRuningEvent)
            {
                EndUpdateView?.Invoke();
            }
        }

        public delegate void UpdateEntityHandle(OPCValue value);
        public event UpdateEntityHandle UpdateEntity;

        private void _callUpdateEntity(OPCValue value)
        {
            if (IsRuningEvent)
            {
                UpdateEntity?.Invoke(value);
            }
        }

        public static List<string> GetListKepServer()
        {
            object o_server = new OPCServer().GetOPCServers();
            var servers = (Array)o_server;
            List<string> lst_nameServer = new List<string>();
            foreach (var item in servers)
            {
                lst_nameServer.Add(item + "");
            }
            return lst_nameServer;
        }

        private void _log(object message, bool flag_error = false)
        {
            Log?.Log(message, flag_error);
        }

        private void _log(Exception exception)
        {
            Log?.Log(exception);
        }

#if DEBUG
        public Random rd_test = new Random();
        public void test_randomData()
        {
            bool setnull = rd_test.Next(0, 10) == 10;
            foreach (var item in _lstOPC)
            {
                if (item.Name.Contains(".AI") || item.Name.Contains(".GioiHan") || item.Name.Contains(".PLC_Chay") || item.Name.Contains(".ThoiGian_CatDien_TuDong"))
                {
                    item.Value = setnull ? (int?)null : rd_test.Next(0, 32000);
                }
                else if (item.Name.Contains(".DO") || item.Name.Contains(".DI"))
                {
                    item.Value = setnull ? null : item.Value == false;
                }
                else if (item.Name.Contains(".DI_QUAT"))
                {
                    item.Value = setnull ? (bool?)null : rd_test.Next(0, 1) == 1;
                }
                else
                {
                    continue;
                }

                item.Time = DateTime.Now;
                item.Qualiti = eQualiti.OPCQualityGood;
                _callUpdateEntity(item);
            }
            _callEndUpdateView();
        }

        public void test2()
        {
            bool setnull = rd_test.Next(0, 5) == 10;
            eQualiti qualiti = rd_test.Next(0, 20) == 0 ? eQualiti.OPCQualityBad : eQualiti.OPCQualityGood;

            foreach (var item in _lstOPC)
            {
                item.Value = setnull ? (int?)null : rd_test.Next(0, 32000);
                item.Qualiti = qualiti;
                item.Time = DateTime.Now;
                _callUpdateEntity(item);
            }
            _callEndUpdateView();
        }
#endif
    }

    public interface ILogServer : IDisposable
    {
        void Log(object message, bool flag_error = false);
        void Log(Exception exception);
    }

    public class OPCValue
    {
        public string Name { get; set; }
        public int KeyOPC { get; set; }
        public dynamic Value { get; set; }
        public eQualiti Qualiti { get; internal set; }
        public DateTime? Time { get; internal set; }
        public Func<object, dynamic> ConvertValue { get; set; }

        internal int IndexInList { get; set; }

        public override string ToString()
        {
            return $"Time={Time}\t Qualiti={Qualiti}\t KeyOPC={KeyOPC}\t({Name}={Value})";
        }
    }
}
