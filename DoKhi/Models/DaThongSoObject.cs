using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanMaoKhe.ModuleOPC;

namespace DoKhi.Models
{
    public partial class DaThongSoObject
    {
        [Editable(false)]
        public long? OPC_H2 { get; set; }
        [Editable(false)]
        public long? OPC_O2 { get; set; }
        [Editable(false)]
        public long? OPC_CH4 { get; set; }
        [Editable(false)]
        public long? OPC_CO { get; set; }

        [Editable(false)]
        public double? H2 { get; set; }
        [Editable(false)]
        public double? O2 { get; set; }
        [Editable(false)]
        public double? CH4 { get; set; }
        [Editable(false)]
        public double? CO { get; set; }

        [Editable(false)]
        public string sH2 { get; set; }
        [Editable(false)]
        public string sO2 { get; set; }
        [Editable(false)]
        public string sCH4 { get; set; }
        [Editable(false)]
        public string sCO { get; set; }

        [Editable(false)]
        public string address_H2 { get; set; }
        [Editable(false)]
        public string address_O2 { get; set; }
        [Editable(false)]
        public string address_CH4 { get; set; }
        [Editable(false)]
        public string address_CO { get; set; }

        [Editable(false)]
        public int countBad_H2 { get; set; }
        [Editable(false)]
        public int countBad_O2 { get; set; }
        [Editable(false)]
        public int countBad_CH4 { get; set; }
        [Editable(false)]
        public int countBad_CO { get; set; }

        [Editable(false)]
        public DateTime UpdateTime { get; set; }

        [Editable(false)]
        public eQualiti OPC_Qualiti { get; set; }

        private const int MAX_COUNT = 5;

        public void SetGiaTri_H2(long? v_H2, DateTime time, eQualiti qualiti = eQualiti.OPCQualityGood)
        {
            if (qualiti == eQualiti.OPCQualityGood)
            {
                countBad_H2 = 0;

                OPC_H2 = v_H2;
            }
            else
            {
                if (countBad_H2 <= MAX_COUNT)
                {
                    countBad_H2++;
                    if (countBad_H2 > MAX_COUNT)
                    {
                        OPC_H2 = null;
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
            H2 = OPC_H2;
            sH2 = H2.HasValue ? H2.Value.ToString($"#,##0.### {StaticDonVi.H2}") : "---";
            OPC_Qualiti = qualiti;
            UpdateTime = time;
        }

        public void SetGiaTri_O2(long? v_O2, DateTime time, eQualiti qualiti = eQualiti.OPCQualityGood)
        {
            if (qualiti == eQualiti.OPCQualityGood)
            {
                countBad_O2 = 0;

                OPC_O2 = v_O2;
            }
            else
            {
                if (countBad_O2 <= MAX_COUNT)
                {
                    countBad_O2++;
                    if (countBad_O2 > MAX_COUNT)
                    {
                        OPC_H2 = null;
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
            O2 = OPC_O2 * 1F / 10;
            sO2 = O2.HasValue ? O2.Value.ToString($"#,##0.### {StaticDonVi.O2}") : "---";
            OPC_Qualiti = qualiti;
            UpdateTime = time;
        }

        public void SetGiaTri_CH4(long? v_CH4, DateTime time, eQualiti qualiti = eQualiti.OPCQualityGood)
        {
            if (qualiti == eQualiti.OPCQualityGood)
            {
                countBad_CH4 = 0;
                OPC_CH4 = v_CH4;
            }
            else
            {
                if (countBad_CH4 <= MAX_COUNT)
                {
                    countBad_CH4++;
                    if (countBad_CH4 > MAX_COUNT)
                    {
                        OPC_H2 = null;
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
            CH4 = OPC_CH4 * 1F / 100;
            sCH4 = CH4.HasValue ? CH4.Value.ToString($"#,##0.### {StaticDonVi.CH4}") : "---";
            OPC_Qualiti = qualiti;
            UpdateTime = time;
        }

        public void SetGiaTri_CO(long? v_CO, DateTime time, eQualiti qualiti = eQualiti.OPCQualityGood)
        {
            if (qualiti == eQualiti.OPCQualityGood)
            {
                countBad_CO = 0;
                OPC_CO = v_CO;
            }
            else
            {
                if (countBad_CO <= MAX_COUNT)
                {
                    countBad_CO++;
                    if (countBad_CO > MAX_COUNT)
                    {
                        OPC_H2 = null;
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
            CO = OPC_CO;
            sCO = CO.HasValue ? CO.Value.ToString($"#,##0.### {StaticDonVi.CO}") : "---";
            OPC_Qualiti = qualiti;
            UpdateTime = time;
        }

        public void Setup(string address, string name)
        {
            this.OPC_Address = address;
            this.Name = name;

            address_CH4 = this.OPC_Address + ".CH4";
            address_CO = this.OPC_Address + ".CO";
            address_H2 = this.OPC_Address + ".H2";
            address_O2 = this.OPC_Address + ".O2";
        }
    }
}
