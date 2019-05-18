using System;
using System.Diagnostics;

namespace LibraryExtentions
{
    public class MethodHelper
    {
        public static string GetMethodName(int indexFrame = 1)
        {
            var st = new StackTrace();
            //for (int i = 0; i < st.FrameCount; i++)
            //{
            //    $"{i}: {st.GetFrame(i).GetMethod().Name}".LogToDebug();
            //}
            return st.GetFrame(indexFrame).GetMethod().Name;
        }

        public static string GetMethodsAsyncName()
        {
            var st = new StackTrace();
            //for (int i = 0; i < st.FrameCount; i++)
            //{
            //    $"{i}: {st.GetFrame(i).GetMethod().Name}".LogToDebug();
            //}
            return st.GetFrame(3).GetMethod().Name;
        }

        public static T CatchFunction<T>(Func<T> func) => _catchFunction<T>(func);
        public static T CatchFunction<T, P1>(Func<P1, T> func, P1 p1) => _catchFunction<T>(func, p1);
        public static T CatchFunction<T, P1, P2>(Func<P1, P2, T> func, P1 p1, P2 p2) => _catchFunction<T>(func, p1, p2);
        public static T CatchFunction<T, P1, P2, P3>(Func<P1, P2, P3, T> func, P1 p1, P2 p2, P3 p3) => _catchFunction<T>(func, p1, p2, p3);
        public static T CatchFunction<T, P1, P2, P3, P4>(Func<P1, P2, P3, P4, T> func, P1 p1, P2 p2, P3 p3, P4 p4) => _catchFunction<T>(func, p1, p2, p3, p4);
        public static T CatchFunction<T, P1, P2, P3, P4, P5>(Func<P1, P2, P3, P4, P5, T> func, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5) => _catchFunction<T>(func, p1, p2, p3, p4, p5);
        public static T CatchFunction<T, P1, P2, P3, P4, P5, P6>(Func<P1, P2, P3, P4, P5, P6, T> func, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6) => _catchFunction<T>(func, p1, p2, p3, p4, p5, p6);
        public static T CatchFunction<T, P1, P2, P3, P4, P5, P6, P7>(Func<P1, P2, P3, P4, P5, P6, P7, T> func, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7) => _catchFunction<T>(func, p1, p2, p3, p4, p5, p6, p7);
        public static T CatchFunction<T, P1, P2, P3, P4, P5, P6, P7, P8>(Func<P1, P2, P3, P4, P5, P6, P7, P8, T> func, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8) => _catchFunction<T>(func, p1, p2, p3, p4, p5, p6, p7, p8);

        protected static T _catchFunction<T>(dynamic func, params dynamic[] data)
        {
            if (data?.Length > 8)
            {
                throw new NotSupportedException($"data have length = {data?.Length.ToString() ?? "NULL"}. length must <= 8.");
            }
            try
            {
                switch (data?.Length)
                {
                    case null:
                    case 0:
                        return func.Invoke();
                    case 1:
                        return func.Invoke(data[0]);
                    case 2:
                        return func.Invoke(data[0], data[1]);
                    case 3:
                        return func.Invoke(data[0], data[1], data[2]);
                    case 4:
                        return func.Invoke(data[0], data[1], data[2], data[3]);
                    case 5:
                        return func.Invoke(data[0], data[1], data[2], data[3], data[4]);
                    case 6:
                        return func.Invoke(data[0], data[1], data[2], data[3], data[4], data[5]);
                    case 7:
                        return func.Invoke(data[0], data[1], data[2], data[3], data[4], data[5], data[6]);
                    case 8:
                        return func.Invoke(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7]);
                    default:
                        throw new NotSupportedException($"data have length = {data?.Length.ToString() ?? "NULL"}. length must <= 8.");
                }
            }
            catch (Exception ex)
            {
                ex.LogToDebug();
                ex.LogToFile();
            }
            return default(T);
        }
    }
}
