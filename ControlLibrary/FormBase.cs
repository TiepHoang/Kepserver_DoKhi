using LibraryExtentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class FormBase : Form
    {
        [Browsable(false)]
        public virtual bool APP_DEBUG => ConfigHelper.GetConfig("APP_DEBUG", true);
        [Browsable(false)]
        public virtual string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;
        [Browsable(false)]
        public virtual ILog BaseLog => APP_DEBUG ? iRichTextBox_log : null;
        [Browsable(false)]
        public virtual string PathFileLog => $@"{BaseDirectory}/log/{DateTime.Now.ToString("yyyy/dd.MM")}.txt";

        public virtual void _log(object message, bool error = false)
        {
            BaseLog?.WriteLog(message, error);
            if (error)
            {
                FileAndFolderExtention.CreateDirectory(Path.GetDirectoryName(PathFileLog));
                message.LogToFile(PathFileLog);
            }
        }

        public virtual void _log(Exception exception)
        {
            _log(exception, true);
        }

        public virtual void _log(IEnumerable<object> enumerable, string separator = "\n")
        {
            _log(string.Join(separator, enumerable), false);
        }

        public delegate void OnDispose();
        public event OnDispose DisposeForm;

        public FormBase()
        {
            InitializeComponent();
        }

        public virtual void FormBase_Load(object sender, EventArgs e)
        {
            this.panelbase_log.Visible = APP_DEBUG;
        }
    }
}
