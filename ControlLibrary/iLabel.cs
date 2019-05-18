using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public class iLabel : Label, IControlNhapNhay
    {
        public iLabel() : base()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // iLabel
            // 
            this.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Tag = "{0}";
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ResumeLayout(false);

        }
        public override bool AutoSize { get => false; set => base.AutoSize = false; }

        public void BinText(params object[] values)
        {
            this.FormatTextFromTag(values);
        }

        #region implement NhapNhay


        bool _enableNhapNhay;
        public bool EnableNhapNhay
        {
            get => _enableNhapNhay;
            set
            {
                _enableNhapNhay = value;
                if (_enableNhapNhay == false)
                {
                    this.ForeColor = DefaultForeColor;
                }
            }
        }

        public Color ColorNhapNhay { get; set; } = Color.Red;

        TimerNhapNhay _timerNhapNhay;
        public TimerNhapNhay TimerNhapNhay
        {
            get => _timerNhapNhay;
            set
            {
                _timerNhapNhay?.Remove(this);
                _timerNhapNhay = value;
                _timerNhapNhay?.Add(this);
            }
        }

        public void NhapNhay(bool colorDefault)
        {
            this.ForeColor = colorDefault ? DefaultForeColor : ColorNhapNhay;
            this.Refresh();
        }


        #endregion
    }
}
