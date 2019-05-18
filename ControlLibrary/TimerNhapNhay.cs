using LibraryExtentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ControlLibrary
{
    public class TimerNhapNhay : Timer
    {
        private void InitializeComponent()
        {
            // 
            // TimerNhapNhay
            // 
            this.Interval = 1000;
            this.Tick += new System.EventHandler(this.TimerNhapNhay_Tick);

        }

        public TimerNhapNhay() : base()
        {
            InitializeComponent();
        }

        public TimerNhapNhay(IContainer container) : base(container)
        {
            InitializeComponent();
        }


        volatile bool _flagColor;
        private void TimerNhapNhay_Tick(object sender, EventArgs e)
        {
            _flagColor = !_flagColor;
            foreach (var item in lstControlNhapNhay)
            {
                if (item.EnableNhapNhay)
                {
                    item.NhapNhay(_flagColor);
                }
            }
        }

        public void Add(IControlNhapNhay nhapNhay)
        {
            if (lstControlNhapNhay.Contains(nhapNhay) == false)
            {
                lstControlNhapNhay.Add(nhapNhay);
            }
        }

        public void Remove(IControlNhapNhay nhapNhay)
        {
            if (lstControlNhapNhay.Contains(nhapNhay))
            {
                lstControlNhapNhay.Remove(nhapNhay);
            }
        }

        List<IControlNhapNhay> lstControlNhapNhay = new List<IControlNhapNhay>();
    }

    public interface IControlNhapNhay
    {
        bool EnableNhapNhay { get; set; }
        void NhapNhay(bool colorDefault);
        Color ColorNhapNhay { get; set; }
        TimerNhapNhay TimerNhapNhay { get; set; }
    }
}
