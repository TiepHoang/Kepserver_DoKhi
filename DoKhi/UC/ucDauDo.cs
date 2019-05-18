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
    public partial class ucDauDo : UserControl
    {
        public DiemDoObject _diemDoObject { get; private set; }

        public ucDauDo()
        {
            InitializeComponent();
        }

        public override void Refresh()
        {
            pictureBox_status.Image = _diemDoObject.OPC_Qualiti == ThanMaoKhe.ModuleOPC.eQualiti.OPCQualityGood ? Properties.Resources.dot_green : Properties.Resources.dot_red;
            textBox_value.Text = string.Format(textBox_value.Tag + "", _diemDoObject.sGiaTri, _diemDoObject.DonVi);
            base.Refresh();
        }


        public void SetGiaTri(long? value, DateTime time, eQualiti qualiti)
        {
            _diemDoObject.SetGiaTri(value, time, qualiti);
            Refresh();
        }

        public void Setup(DiemDoObject diemDo)
        {
            _diemDoObject = diemDo;
            _diemDoObject.Setup(diemDo.TypeDiemDo, diemDo.OPC_Address, diemDo.Name);
            iLabel_name.BinText(_diemDoObject.Name);
            this.toolTip1.SetToolTip(this.iLabel_name, diemDo.OPC_Address);
            Refresh();
        }

        private void iButton_viewdothi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xem đồ thị - update sau!");
        }
    }
}
