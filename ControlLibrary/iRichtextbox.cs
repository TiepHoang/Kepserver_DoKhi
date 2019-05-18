using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public interface ILog
    {
        void WriteLog(object message, bool error = false);
        void WriteLog(Exception exception);
    }

    public class iRichTextBox : RichTextBox, ILog
    {
        public void WriteLog(object message, bool error = false)
        {
            this.Invoke(() =>
            {
                if (this.IsDisposed) return;
                if (this.TextLength > 50000)
                {
                    this.Clear();
                }
                string textLog = $"\n{DateTime.Now}>>{message}";
                this.AppendText(textLog);

                int start = this.TextLength - textLog.Length;
                this.Select(start <= 0 ? 0 : start, start <= 0 ? this.TextLength : textLog.Length);
                this.SelectionColor = error ? Color.Red : Color.Green;

                this.SelectionStart = this.TextLength;
                this.ScrollToCaret();
            });
        }

        public void WriteLog(Exception exception)
        {
            WriteLog(exception, true);
        }
    }
}
