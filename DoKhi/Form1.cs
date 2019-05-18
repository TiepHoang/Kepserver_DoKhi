using ControlLibrary;
using DoKhi.Models;
using DoKhi.UC;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using ThanMaoKhe.ModuleOPC;

namespace DoKhi
{
    public partial class Form1 : FormBase, ILogServer
    {
        private Dictionary<int, ucDauDo> _lstUC_diemdo;
        private Dictionary<int, ucDaThongSo> _lstUC_dathongso;


        private IEnumerable<DiemDoObject> _diemdos => _lstUC_diemdo.Values.Select(q => q._diemDoObject);
        private IEnumerable<DaThongSoObject> _dathongsos => _lstUC_dathongso.Values.Select(q => q._daThongSoObject);
        private KepServer _kepServer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _loadDiemDo();

            _loadKepserver();

            //chart.Series = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Title = "Series 1",
            //        Values = new ChartValues<double> {4, 6, 5, 2, 7},
            //        Fill = Brushes.Transparent,
            //        LineSmoothness = 0,
            //        PointGeometrySize = 8
            //    },
            //};

            //chart.AxisX.Add(new Axis
            //{
            //    Title = "Month",
            //    Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" },
            //    MinRange = 1000
            //});

            //chart.AxisY.Add(new Axis
            //{
            //    Title = "Sales",
            //    LabelFormatter = value => value.ToString("C")
            //});

            //chart.LegendLocation = LegendLocation.Right;

            //InitChart();
            //_testChart();

            //var mapper = Mappers.Xy<DiemDoObject>()
            //    .X(model => model.UpdateTime.Ticks)   //use DateTime.Ticks as X
            //    .Y(model => model.GiaTri ?? 0);           //use the value property as Y

            ////lets save the mapper globally.
            //Charting.For<DiemDoObject>(mapper);

            ////the ChartValues property will store our values array
            //ChartValues = new ChartValues<DiemDoObject>();
            //chart.Series = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Values = ChartValues,
            //        PointGeometrySize = 18,
            //        StrokeThickness = 4
            //    }
            //};
            //chart.AxisX.Add(new Axis
            //{
            //    DisableAnimations = true,
            //    LabelFormatter = value => new System.DateTime((long)value).ToString("mm:ss"),
            //    Separator = new Separator
            //    {
            //        Step = TimeSpan.FromSeconds(1).Ticks
            //    }
            //});

            //SetAxisLimits(System.DateTime.Now);

            ////The next code simulates data changes every 500 ms
            //Timer = new Timer
            //{
            //    Interval = 500
            //};
            //Timer.Tick += TimerOnTick;
            //R = new Random();
            //Timer.Start();
        }

        public ChartValues<DiemDoObject> ChartValues { get; set; }
        public Timer Timer { get; set; }
        public Random R { get; set; }

        private void SetAxisLimits(System.DateTime now)
        {
            chart.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 100ms ahead
            chart.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(8).Ticks; //we only care about the last 8 seconds
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            var now = System.DateTime.Now;

            ChartValues.Add(new DiemDoObject
            {
                UpdateTime = now,
                GiaTri = R.Next(0, 10)
            });

            SetAxisLimits(now);

            //lets only use the last 30 values
            if (ChartValues.Count > 30) ChartValues.RemoveAt(0);
        }

        private void _testChart()
        {
            HashSet<DiemDoObject> diemDos = new HashSet<DiemDoObject>();
            var rd = new Random();
            var now = DateTime.Now.Ticks;
            for (int i = 0; i < 100; i++)
            {

                var item = new DiemDoObject
                {
                    UpdateTime = new DateTime(now + i * 10),
                    GiaTri = rd.Next() % 32000
                };
                diemDos.Add(item);
            }
            //chart.Series[0].Values = new ChartValues();
        }

        private SolidColorBrush Foreground_tocdo = Brushes.DodgerBlue;

        //https://lvcharts.net/App/examples/v1/wf/Constant%20Changes
        public void InitChart()
        {
            //tạo mapper
            var mapperDiemDo = Mappers.Xy<DiemDoObject>()
                .X(model => model.UpdateTime.Ticks)
                .Y(model => model.GiaTri ?? 0);
            //bin mapper to charting
            Charting.For<DiemDoObject>(mapperDiemDo);

            chart.Series.Add(new LineSeries
            {
                Title = "Tốc độ (m/s)",
                Stroke = Foreground_tocdo,
                Foreground = Foreground_tocdo,
                ScalesYAt = 1,
                Fill = Brushes.Transparent,
                LineSmoothness = 1,
            });

            var axis_x = new Axis
            {
                LabelFormatter = val => new System.DateTime((long)val).ToString("HH:mm:ss"),
                DisableAnimations = true,
                Separator = new Separator
                {
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 8 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                },
                Foreground = Brushes.Green,
                FontSize = 15
            };
            chart.AxisX.Add(axis_x);

            chart.AxisY.Add(new Axis
            {
                LabelFormatter = val => val.ToString("#,##0.00"),
                DisableAnimations = true,
                Position = AxisPosition.RightTop,
                Title = chart.Series[0].Title,
                Separator = new Separator
                {
                    Step = 1,
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 }),
                    Stroke = Foreground_tocdo
                },
                Foreground = Foreground_tocdo,
                FontSize = 17,
            });

            chart.LegendLocation = LegendLocation.Top;
        }


        private void _dispose()
        {
            _kepServer?.Dispose();
            _kepServer = null;
        }

        private Func<object, dynamic> _convertValue_long = (v) => Convert.ToInt32(v);

        private async void _loadKepserver()
        {
            _kepServer?.Dispose();

            _kepServer = new KepServer(this, (s, e) =>
            {
                string message = "KEPServer v4 đã tắt! " + s;
                message.ThongBao();
            });

            _kepServer.EndUpdateView += _kepServer_EndUpdateView;
            _kepServer.StatusChange += _kepServer_StatusChange;
            _kepServer.UpdateEntity += _kepServer_UpdateEntity;

            DateTime now = DateTime.Now;

            List<OPCValue> lstOPC = (from x in _diemdos
                                     select new OPCValue
                                     {
                                         ConvertValue = _convertValue_long,
                                         KeyOPC = x.ID,
                                         Name = x.OPC_Address,
                                     }).ToList();
            foreach (var x in _dathongsos)
            {
                x.Setup(x.OPC_Address, x.Name);
                lstOPC.Add(new OPCValue
                {
                    ConvertValue = _convertValue_long,
                    KeyOPC = x.ID,
                    Name = x.address_CH4,
                });
                lstOPC.Add(new OPCValue
                {
                    ConvertValue = _convertValue_long,
                    KeyOPC = x.ID,
                    Name = x.address_CO,
                });
                lstOPC.Add(new OPCValue
                {
                    ConvertValue = _convertValue_long,
                    KeyOPC = x.ID,
                    Name = x.address_H2,
                });
                lstOPC.Add(new OPCValue
                {
                    ConvertValue = _convertValue_long,
                    KeyOPC = x.ID,
                    Name = x.address_O2,
                });
            }

            string nameKepServer = "KEPware.KEPServerEx.V4";
            await _kepServer.ConfigAsync(nameKepServer, lstOPC);
        }

        private void _kepServer_UpdateEntity(OPCValue value)
        {
            _log(value);
            var contain = _lstUC_diemdo.ContainsKey(value.KeyOPC);
            contain &= value.Name.Contains("Server_ThanMaoKhe");
            if (contain)
            {
                _lstUC_diemdo[value.KeyOPC].SetGiaTri(value.Value, value.Time ?? DateTime.Now, value.Qualiti);
            }
            else
            {
                contain = _lstUC_dathongso.ContainsKey(value.KeyOPC);
                if (contain)
                {
                    var dathongso = _lstUC_dathongso[value.KeyOPC];
                    if (value.Name.Contains(".CH4"))
                    {
                        dathongso.SetGiaTri_CH4(value.Value, value.Time ?? DateTime.Now, value.Qualiti);
                    }
                    else if (value.Name.Contains(".H2"))
                    {
                        dathongso.SetGiaTri_H2(value.Value, value.Time ?? DateTime.Now, value.Qualiti);
                    }
                    else if (value.Name.Contains(".O2"))
                    {
                        dathongso.SetGiaTri_O2(value.Value, value.Time ?? DateTime.Now, value.Qualiti);
                    }
                    else if (value.Name.Contains(".CO"))
                    {
                        dathongso.SetGiaTri_CO(value.Value, value.Time ?? DateTime.Now, value.Qualiti);
                    }
                    else
                    {
                        _log($"not found dathongso Name {value.Name}!", true);
                    }
                }
                else
                {
                    _log($"not found dathongso {value.Name}!", true);
                }
            }
        }

        private void _kepServer_StatusChange(eKepserverStatus status)
        {
            System.Drawing.Image img = null;
            string text = status.ToString();

            switch (status)
            {
                case eKepserverStatus.OFF:
                    img = Properties.Resources.dot_red;
                    break;
                case eKepserverStatus.ON:
                    img = Properties.Resources.dot_green;
                    break;
                case eKepserverStatus.PAUSE:
                    img = Properties.Resources.dot_gray;
                    break;
                default:
                    throw new NotSupportedException(text);
            }

            pictureBox_statusKepserver.Image = img;
            iLabel_statusKepserver.FormatTextFromTag(text);
        }

        private void _kepServer_EndUpdateView()
        {
            this.Refresh();
        }

        private void _loadDiemDo()
        {
            var _diemdos = new DiemDoRepository().GetList()?.ToList() ?? new List<DiemDoObject>();
            var _dathongsos = new DaThongSoRepository().GetList()?.ToList() ?? new List<DaThongSoObject>();

            _log($"load {_diemdos.Count} điểm đo!");
            _log($"load {_dathongsos.Count} đa thông số!");

            flowLayoutPanel_dsdiemdo.Controls.Clear();

            _lstUC_diemdo = new Dictionary<int, ucDauDo>();
            _lstUC_dathongso = new Dictionary<int, ucDaThongSo>();

            foreach (var item in _diemdos)
            {
                ucDauDo ucDauDo_tmp = new ucDauDo();
                flowLayoutPanel_dsdiemdo.Controls.Add(ucDauDo_tmp);

                eTypeDiemDo eTypeDiemDo = (eTypeDiemDo)(item.Type ?? 0);
                item.TypeDiemDo = eTypeDiemDo;
                ucDauDo_tmp.Setup(item);
                ucDauDo_tmp.Margin = new Padding(10);
                _lstUC_diemdo.Add(item.ID, ucDauDo_tmp);
            }

            foreach (var item in _dathongsos)
            {
                ucDaThongSo ucDaThongSo_tmp = new ucDaThongSo();
                flowLayoutPanel_dsdiemdo.Controls.Add(ucDaThongSo_tmp);

                ucDaThongSo_tmp.Setup(item);
                ucDaThongSo_tmp.Margin = new Padding(10);
                _lstUC_dathongso.Add(item.ID, ucDaThongSo_tmp);
            }
        }

        public void Log(object message, bool flag_error = false)
        {
            _log(message, flag_error);
        }

        public void Log(Exception exception)
        {
            _log(exception);
        }

        private void iButton1_Click(object sender, EventArgs e)
        {
            _kepServer.test2();
        }
    }
}
