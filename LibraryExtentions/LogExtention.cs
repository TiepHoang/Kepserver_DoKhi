using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LibraryExtentions
{
    public static class LogExtention
    {
        public static void LogToDebug(this object message)
        {
            Debug.WriteLine($"\n[{DateTime.Now}][{Namespace}]>> {message}");
        }

        public static string FolderLog => $"{AppDomain.CurrentDomain.BaseDirectory}\\log";
        public static string PathFileLogDefault => $"{FolderLog}\\{DateTime.Now.Year}\\{DateTime.Now.ToString("MM")}\\{DateTime.Now.ToString("dd")}.txt";

        /// <summary>
        /// Log to file. return path file writed.
        /// if path == null => create file in folder default: App_Data or log.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="pathFile"></param>
        /// <returns></returns>
        public static string LogToFile(this object message, string pathFile = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pathFile))
                {
                    pathFile = PathFileLogDefault;
                }
                FileAndFolderExtention.CreateDirectory(Path.GetDirectoryName(pathFile));
                using (StreamWriter writer = new StreamWriter(pathFile, true))
                {
                    writer.WriteLine($"{DateTime.Now}>>{message}");
                    writer.Close();
                }
                return pathFile;
            }
            catch (Exception ex)
            {
                ex.LogToDebug();
            }
            return null;
        }

        static string Namespace => MethodBase.GetCurrentMethod().ReflectedType.Namespace;

        static string LOGEVENT => ConfigHelper.GetConfig("LOGEVENT", Namespace);

        public static void LogToEvent(this object message, bool isError = false)
        {
            try
            {
                if (EventLog.SourceExists(LOGEVENT) == false)
                {
                    EventLog.CreateEventSource(LOGEVENT, LOGEVENT);
                }
                if (EventLog.SourceExists(LOGEVENT))
                {
                    using (EventLog log = new EventLog())
                    {
                        log.Source = LOGEVENT;
                        log.WriteEntry(message.ToString(), isError ? EventLogEntryType.Error : EventLogEntryType.Information);
                    }
                }
                else
                {
                    $"Not found Source log {LOGEVENT}".LogToDebug();
                }
            }
            catch (SecurityException security)
            {
                security.LogToDebug();
                "thiết lập application ở chế độ admin. create app.manifest!".LogToDebug();
            }
            catch (Exception ex)
            {
                ex.LogToDebug();
            }
        }
    }
}
