using LibraryExtentions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public class iButton : Button, IControlNhapNhay
    {
        #region implement NhapNhay


        bool _isNhapNhay;
        public bool EnableNhapNhay
        {
            get => _isNhapNhay;
            set
            {
                _isNhapNhay = value;
                if (_isNhapNhay == false)
                {
                    this.BackColor = DefaultBackColor;
                    this.ForeColor = DefaultForeColor;
                }
            }
        }

        public void BinText(params object[] values)
        {
            this.FormatTextFromTag(values);
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

        public void NhapNhay(bool flagdefault)
        {
            this.BackColor = flagdefault ? Button.DefaultBackColor : this.ColorNhapNhay;
            this.ForeColor = flagdefault ? this.ColorNhapNhay : Button.DefaultBackColor;
            this.Refresh();
        }


        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // iButton
            // 
            this.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Tag = "{0}";
            this.ResumeLayout(false);

        }

        public iButton() : base()
        {
            InitializeComponent();
        }
    }
}
