using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanMaoKhe.ModuleOPC
{
    public class dbModule
    {
        public static List<OPCServerObject> GetOPCServer()
        {
            return new OPCServerRepository().GetListDiemDoActive();
        }

        public static List<BangTenThongSoObject> GetBangTenThongs(int[] ids = null)
        {
            return new BangTenThongSoRepository().GetListActiveDiemDoJoin(ids);
        }

        public static IEnumerable<DataThongSoChartJson> GetDataCharts(int[] id, DateTime startDate, DateTime? endDate = null)
        {
            var data = new DataBangTenThongSoRepository().GetAll(id, startDate, endDate);
            return data.Select(q => new DataThongSoChartJson
            {
                IDThongSo = q.IDThongSo ?? 0,
                timestamp = q.UpdateDate,
                value = q.AI,
            });
        }
    }
}
