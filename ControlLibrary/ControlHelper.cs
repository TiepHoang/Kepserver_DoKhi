using LibraryExtentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public static class ControlHelper
    {
        public static void Invoke(this Control control, Action action)
        {
            try
            {
                if (control.IsDisposed == false && control.Disposing == false)
                {
                    if (control.InvokeRequired)
                    {
                        control.Invoke(new MethodInvoker(action));
                    }
                    else
                    {
                        action.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogToDebug();
                ex.LogToFile();
            }
        }

        public static void FormatTextFromTag(this Control control, params object[] values)
        {
            if (string.IsNullOrEmpty(control.Tag + "") == false)
            {
                control.Text = string.Format(control.Tag + "", values);
            }
            else
            {
                throw new Exception("Tag of control is null");
            }
        }

        public static DialogResult ThongBao(this string message)
        {
            //return MessageBox.Show(message, "THAN MẠO KHÊ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var frm = new frmAlert()
            {
                mThongBao = new frmAlert.ThongBao
                {
                    eTypeMessage = frmAlert.eTypeMessage.ThongBao,
                    IsDialog = true,
                    Message = message,
                }
            };
            return frm.ShowDialog();
        }

        public static DialogResult XacNhan(this string message, int SecondDisplay = 5)
        {
            //return MessageBox.Show(message, "XÁC NHẬN - THAN MẠO KHÊ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            var frm = new frmAlert()
            {
                mThongBao = new frmAlert.ThongBao
                {
                    eTypeMessage = frmAlert.eTypeMessage.XacNhan,
                    IsDialog = false,
                    Message = message,
                    SecondDisplay = SecondDisplay
                }
            };
            return frm.ShowDialog();
        }
    }
}
