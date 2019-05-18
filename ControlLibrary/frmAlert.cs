using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class frmAlert : Form
    {
        public class ThongBao
        {
            public string Message { get; set; }
            public bool IsDialog { get; set; }
            public int SecondDisplay { get; set; }
            public eTypeMessage eTypeMessage { get; set; }
        }

        public enum eTypeMessage
        {
            ThongBao, XacNhan
        }

        public ThongBao mThongBao { get; set; } = new ThongBao();


        public frmAlert()
        {
            InitializeComponent();
        }

        private void frmAlert_Load(object sender, EventArgs e)
        {
            if (mThongBao != null)
            {
                this.label_messgase.BinText(mThongBao.Message);
                timer_close.Enabled = !mThongBao.IsDialog;
                switch (mThongBao.eTypeMessage)
                {
                    case eTypeMessage.ThongBao:
                        this.Text = "Thông báo";
                        this.iButton_cancle.Text = "Cancle";
                        break;
                    case eTypeMessage.XacNhan:
                        this.Text = "Xác nhận";
                        this.iButton_cancle.BinText(mThongBao.SecondDisplay);
                        break;
                    default:
                        throw new NotSupportedException(mThongBao.eTypeMessage.ToString());
                }
            }
        }

        private void timer_close_Tick(object sender, EventArgs e)
        {
            if (mThongBao == null)
            {
                timer_close.Stop();
            }
            else
            {
                mThongBao.SecondDisplay--;
                this.iButton_cancle.BinText(mThongBao.SecondDisplay);
                if (mThongBao.SecondDisplay <= 0)
                {
                    DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
        }

        private void iButton_cancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void iButton_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
