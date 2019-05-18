using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanMaoKhe.ModuleOPC;

namespace DoKhi.Models
{
    public partial class DiemDoObject
    {
        [Editable(false)]
        public eTypeDiemDo TypeDiemDo { get; set; }

        [Editable(false)]
        public long? OPC_GiaTri { get; set; }

        [Editable(false)]
        public double? GiaTri { get; set; }

        [Editable(false)]
        public string sGiaTri { get; set; }

        [Editable(false)]
        public string DonVi { get; set; }

        [Editable(false)]
        public DateTime UpdateTime { get; set; }

        [Editable(false)]
        public eQualiti OPC_Qualiti { get; set; }

        [Editable(false)]
        public int countBad_GiaTri { get; set; }

        private const int MAX_COUNT = 5;

        public void SetGiaTri(long? value, DateTime time, eQualiti qualiti = eQualiti.OPCQualityGood)
        {
            if (qualiti == eQualiti.OPCQualityGood)
            {
                countBad_GiaTri = 0;
                OPC_GiaTri = value;
            }
            else
            {
                if (countBad_GiaTri <= MAX_COUNT)
                {
                    countBad_GiaTri++;
                    if (countBad_GiaTri > MAX_COUNT)
                    {
                        OPC_GiaTri = null;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            OPC_Qualiti = qualiti;
            UpdateTime = time;

            switch (TypeDiemDo)
            {
                case eTypeDiemDo.CH4:
                    GiaTri = OPC_GiaTri * 1F / 100;
                    break;
                case eTypeDiemDo.O2:
                    GiaTri = OPC_GiaTri * 1F / 10;
                    break;
                case eTypeDiemDo.CO:
                case eTypeDiemDo.H2:
                case eTypeDiemDo.NhietDo:
                case eTypeDiemDo.Gio:
                case eTypeDiemDo.Notset:
                    GiaTri = OPC_GiaTri;
                    break;
                default:
                    throw new NotSupportedException(TypeDiemDo.ToString());
            }
            sGiaTri = GiaTri.HasValue ? GiaTri.Value.ToString("#,##0.###") : "---";
        }

        public void Setup(eTypeDiemDo eTypeDiemDo, string address, string name)
        {
            this.TypeDiemDo = eTypeDiemDo;
            this.OPC_Address = address;
            this.Name = name;
            switch (TypeDiemDo)
            {
                case eTypeDiemDo.Notset:
                    DonVi = "";
                    break;
                case eTypeDiemDo.CH4:
                    DonVi = StaticDonVi.CH4;
                    break;
                case eTypeDiemDo.CO:
                    DonVi = StaticDonVi.CO;
                    break;
                case eTypeDiemDo.O2:
                    DonVi = StaticDonVi.O2;
                    break;
                case eTypeDiemDo.H2:
                    DonVi = StaticDonVi.H2;
                    break;
                case eTypeDiemDo.NhietDo:
                    DonVi = StaticDonVi.NhietDo;
                    break;
                case eTypeDiemDo.Gio:
                    DonVi = StaticDonVi.Gio;
                    break;
                default:
                    throw new NotSupportedException(TypeDiemDo.ToString());
            }
            SetGiaTri(this.OPC_GiaTri, this.UpdateTime, this.OPC_Qualiti);
        }
    }

    public enum eTypeDiemDo
    {
        Notset = 0,
        CH4 = 1,
        CO = 2,
        O2 = 3,
        H2 = 4,
        NhietDo = 5,
        Gio = 6,
    }
}
