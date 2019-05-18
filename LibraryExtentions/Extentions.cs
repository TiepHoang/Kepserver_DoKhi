using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryExtentions
{
    public static class Extentions
    {
        public static T CastTo<T>(this object obj, T anonymous)
        {
            return (T)(obj);
        }

        public static int? GetIndexFirst<T>(this IEnumerable<T> source, Func<T, bool> funcToCheck)
        {
            int count = source.Count();
            for (int i = 0; i < count; i++)
            {
                if (funcToCheck(source.ElementAt(i)))
                {
                    return i;
                }
            }
            return null;
        }

        public static string GetPropertyName<T, Y>(this Expression<Func<T, Y>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                var member = (MemberExpression)expression.Body;
                return member.Member.Name;
            }
            if (expression.Body is MethodCallExpression)
            {
                return ((MethodCallExpression)expression.Body).Method.Name;
            }
            return ((expression.Body as UnaryExpression).Operand as MemberExpression).Member.Name;
        }

        public static Type GetPropertyType<T, Y>(this Expression<Func<T, Y>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                var member = (MemberExpression)expression.Body;
                return ((PropertyInfo)member.Member).PropertyType;
            }
            return ((PropertyInfo)((expression.Body as UnaryExpression).Operand as MemberExpression).Member).PropertyType;
        }

        public static bool IsNumericType(this Type o)
        {
            switch (Type.GetTypeCode(o))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static string Bindata(this string stringFormat, params object[] value)
        {
            return string.Format(stringFormat, value);
        }

        public static string Display(this object value, string format = "#,##0")
        {
            return string.Format("{0:" + format + "}", value);
        }

        public static Y TryGetValue<T, Y>(this T source, Expression<Func<T, Y>> valueMember)
        {
            if (source != null)
            {
                return (Y)source.GetType().GetProperty(valueMember.GetPropertyName()).GetValue(source);
            }
            return default(Y);
        }

        public static string TryGetMessage(this Exception ex)
        {
            string message = ex.Message;
            if (ex.InnerException != null)
            {
                message += ", InnerException: {0}".Bindata(ex.InnerException.TryGetValue(q => q.Message));
            }
            return message;
        }

        public static string GetName<T>(this T source, Expression<Func<T, object>> valueMember)
        {
            return valueMember.GetPropertyName();
        }

        public static Y CopyDeep<T, Y>(this Y target, T source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var pro_target = target.GetType().GetProperties().ToList();
            foreach (var item in source.GetType().GetProperties())
            {
                var math = pro_target.FirstOrDefault(q => q.Name == item.Name);
                if (math != null)
                {
                    if (math.CanWrite && math.PropertyType == item.PropertyType)
                    {
                        math.SetValue(target, item.GetValue(source));
                    }
                    pro_target.Remove(math);
                }
            }
            return target;
        }

        public static Y CopyDeepOnlyValue<T, Y>(this Y target, T source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var pro_target = target.GetType().GetProperties().ToList();
            foreach (var item in source.GetType().GetProperties())
            {
                var math = pro_target.FirstOrDefault(q => q.Name == item.Name);
                if (math != null)
                {
                    if (math.CanWrite && math.PropertyType == item.PropertyType && math.PropertyType.Namespace == "System")
                    {
                        math.SetValue(target, item.GetValue(source));
                    }
                    pro_target.Remove(math);
                }
            }
            return target;
        }

        public static DateTime OnlyDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static TimeSpan OnlyTime(this DateTime dateTime)
        {
            return new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
        }

        static GregorianCalendar _gc = new GregorianCalendar();

        public static int GetWeekOfMonth(this DateTime time)
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            return time.GetWeekOfYear() - first.GetWeekOfYear() + 1;
        }

        public static int GetWeekOfYear(this DateTime time)
        {
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }

    public class FileAndFolderExtention
    {

        /// <summary>
        /// Create Directory if not exit
        /// </summary>
        /// <param name="pathDir"></param>
        /// <returns></returns>
        public static bool CreateDirectory(string pathDir)
        {
            if (Directory.Exists(pathDir) == false)
            {
                Directory.CreateDirectory(pathDir);
                return true;
            }
            return false;
        }

        public static bool DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
                return true;
            }
            return false;
        }

        public static bool DeleteFolder(string pathLocal)
        {
            if (Directory.Exists(pathLocal))
            {
                ToEmpty(pathLocal);
                Directory.Delete(pathLocal);
                return true;
            }
            return false;
        }

        public static bool MoveOrReplaceFile(string file_src, string file_new)
        {
            if (File.Exists(file_src))
            {
                if (File.Exists(file_new))
                {
                    File.Delete(file_new);
                }
                File.Move(file_src, file_new);
                return true;
            }
            return false;
        }

        public static bool ToEmpty(DirectoryInfo directory)
        {
            if (directory.Exists)
            {
                foreach (var item in directory.GetFiles())
                {
                    item.Delete();
                }
                foreach (var item in directory.GetDirectories())
                {
                    ToEmpty(item);
                    Directory.Delete(item.FullName);
                }
                return true;
            }
            return false;
        }

        public static bool ToEmpty(string path_directory)
        {
            return ToEmpty(new DirectoryInfo(path_directory));
        }
    }

    public class Network
    {
        public static bool IsAvailable { get { return NetworkInterface.GetIsNetworkAvailable(); } }
    }
}
