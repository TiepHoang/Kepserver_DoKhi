using DoKhi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThanMaoKhe.ModuleOPC;

namespace DoKhi.UC
{
    public partial class ucDaThongSo : UserControl
    {
        public DaThongSoObject _daThongSoObject { get; private set; }

        public ucDaThongSo()
        {
            InitializeComponent();
        }

        public override void Refresh()
        {
            pictureBox_status.Image = _daThongSoObject.OPC_Qualiti == ThanMaoKhe.ModuleOPC.eQualiti.OPCQualityGood ? Properties.Resources.dot_green : Properties.Resources.dot_red;
            textBox_co.Text = _daThongSoObject.sCO;
            textBox_ch4.Text = _daThongSoObject.sCH4;
            textBox_o2.Text = _daThongSoObject.sO2;
            textBox_h2.Text = _daThongSoObject.sH2;

            base.Refresh();
        }

        public void SetGiaTri_CH4(long? v_CH4, DateTime time, eQualiti qualiti = eQualiti.OPCQualityGood)
        {
            _daThongSoObject.SetGiaTri_CH4(v_CH4, time, qualiti);
            Refresh();
        }

        public void SetGiaTri_O2(long? v_O2, DateTime time, eQualiti qualiti = eQualiti.OPCQualityGood)
        {
            _daThongSoObject.SetGiaTri_O2(v_O2, time, qualiti);
            Refresh();
        }

        public void SetGiaTri_H2(long? v_H2, DateTime time, eQualiti qualiti = eQualiti.OPCQualityGood)
        {
            _daThongSoObject.SetGiaTri_H2(v_H2, time, qualiti);
            Refresh();
        }

        public void SetGiaTri_CO(long? v_CO, DateTime time, eQualiti qualiti = eQualiti.OPCQualityGood)
        {
            _daThongSoObject.SetGiaTri_CO(v_CO, time, qualiti);
            Refresh();
        }

        public void Setup(DaThongSoObject daThongSo)
        {
            _daThongSoObject = daThongSo;
            _daThongSoObject.Setup(_daThongSoObject.OPC_Address, _daThongSoObject.Name);
            iLabel_name.Text = _daThongSoObject.Name;
            toolTip1.SetToolTip(iLabel_name, _daThongSoObject.OPC_Address);
            Refresh();
        }

        private void iButton_viewdothi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xem đồ thị - update sau!");
        }
    }
}
