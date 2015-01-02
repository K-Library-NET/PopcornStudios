using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Common.Unity
{
    /// <summary>
    /// 时间戳辅助类
    /// </summary>
    public class TimeStampUtility
    {
        /// <summary>
        /// 时间戳转换成日期
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime FromTimeStamp(long timestamp)
        {
            if (timestamp <= 0)
                return GetDefaultDateTime();

            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestamp.ToString() + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);

            return dtResult;
        }

        /// <summary>
        /// 取得时间戳计时的起始时间long值
        /// </summary>
        /// <returns></returns>
        public static long GetDefaultTimeStamp()
        {
            return ToTimeStamp(GetDefaultDateTime());
        }

        /// <summary>
        /// 取得时间戳计时的起始时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDefaultDateTime()
        {
            return new DateTime(1970, 1, 1).ToLocalTime();
        }

        /// <summary>
        /// 日期转换成时间戳
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static long ToTimeStamp(DateTime datetime)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            //DateTime dtNow = DateTime.Parse(DateTime.Now.ToString());
            TimeSpan toNow = datetime.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = "0000000" + timeStamp;
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);

            return long.Parse(timeStamp);
        }
    }
}
