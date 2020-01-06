using System;

namespace Microliu.Utils
{
    /// <summary>
    /// 时间日期处理类
    /// </summary>
    public class DateTimeHelper
    {

        #region 返回本年有多少天
        /// <summary>返回本年有多少天</summary>
        /// <param name="iYear">年份</param>
        /// <returns>本年的天数</returns>
        public static int GetDaysOfYear(int iYear)
        {
            int cnt = 0;
            if (IsRuYear(iYear))
            {
                //闰年多 1 天 即：2 月为 29 天
                cnt = 366;

            }
            else
            {
                //--非闰年少1天 即：2 月为 28 天
                cnt = 365;
            }
            return cnt;
        }
        #endregion

        #region 返回本年有多少天
        /// <summary>本年有多少天</summary>
        /// <param name="dt">日期</param>
        /// <returns>本天在当年的天数</returns>
        public static int GetDaysOfYear(DateTime idt)
        {
            int n;

            //取得传入参数的年份部分，用来判断是否是闰年

            n = idt.Year;
            if (IsRuYear(n))
            {
                //闰年多 1 天 即：2 月为 29 天
                return 366;
            }
            else
            {
                //--非闰年少1天 即：2 月为 28 天
                return 365;
            }

        }
        #endregion

        #region 返回本月有多少天
        /// <summary>本月有多少天</summary>
        /// <param name="iYear">年</param>
        /// <param name="Month">月</param>
        /// <returns>天数</returns>
        public static int GetDaysOfMonth(int iYear, int Month)
        {
            int days = 0;
            switch (Month)
            {
                case 1:
                    days = 31;
                    break;
                case 2:
                    if (IsRuYear(iYear))
                    {
                        //闰年多 1 天 即：2 月为 29 天
                        days = 29;
                    }
                    else
                    {
                        //--非闰年少1天 即：2 月为 28 天
                        days = 28;
                    }

                    break;
                case 3:
                    days = 31;
                    break;
                case 4:
                    days = 30;
                    break;
                case 5:
                    days = 31;
                    break;
                case 6:
                    days = 30;
                    break;
                case 7:
                    days = 31;
                    break;
                case 8:
                    days = 31;
                    break;
                case 9:
                    days = 30;
                    break;
                case 10:
                    days = 31;
                    break;
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
            }

            return days;
        }
        #endregion

        #region 返回本月有多少天
        /// <summary>本月有多少天</summary>
        /// <param name="dt">日期</param>
        /// <returns>天数</returns>
        public static int GetDaysOfMonth(DateTime dt)
        {
            //--------------------------------//
            //--从dt中取得当前的年，月信息  --//
            //--------------------------------//
            int year, month, days = 0;
            year = dt.Year;
            month = dt.Month;

            //--利用年月信息，得到当前月的天数信息。
            switch (month)
            {
                case 1:
                    days = 31;
                    break;
                case 2:
                    if (IsRuYear(year))
                    {
                        //闰年多 1 天 即：2 月为 29 天
                        days = 29;
                    }
                    else
                    {
                        //--非闰年少1天 即：2 月为 28 天
                        days = 28;
                    }

                    break;
                case 3:
                    days = 31;
                    break;
                case 4:
                    days = 30;
                    break;
                case 5:
                    days = 31;
                    break;
                case 6:
                    days = 30;
                    break;
                case 7:
                    days = 31;
                    break;
                case 8:
                    days = 31;
                    break;
                case 9:
                    days = 30;
                    break;
                case 10:
                    days = 31;
                    break;
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
            }

            return days;

        }
        #endregion

        #region 返回当前日期的星期名称
        /// <summary>返回当前日期的星期名称</summary>
        /// <param name="dt">日期</param>
        /// <returns>星期名称</returns>
        public static string GetWeekNameOfDay(DateTime idt)
        {
            string dt, week = "";

            dt = idt.DayOfWeek.ToString();
            switch (dt)
            {
                case "Mondy":
                    week = "星期一";
                    break;
                case "Tuesday":
                    week = "星期二";
                    break;
                case "Wednesday":
                    week = "星期三";
                    break;
                case "Thursday":
                    week = "星期四";
                    break;
                case "Friday":
                    week = "星期五";
                    break;
                case "Saturday":
                    week = "星期六";
                    break;
                case "Sunday":
                    week = "星期日";
                    break;

            }
            return week;
        }
        #endregion

        #region 返回当前日期的星期编号
        /// <summary>返回当前日期的星期编号</summary>
        /// <param name="dt">日期</param>
        /// <returns>星期数字编号</returns>
        public static string GetWeekNumberOfDay(DateTime idt)
        {
            string dt, week = "";

            dt = idt.DayOfWeek.ToString();
            switch (dt)
            {
                case "Mondy":
                    week = "1";
                    break;
                case "Tuesday":
                    week = "2";
                    break;
                case "Wednesday":
                    week = "3";
                    break;
                case "Thursday":
                    week = "4";
                    break;
                case "Friday":
                    week = "5";
                    break;
                case "Saturday":
                    week = "6";
                    break;
                case "Sunday":
                    week = "7";
                    break;

            }

            return week;


        }
        #endregion

        #region 判断当前日期所属的年份是否是闰年，私有函数
        /// <summary>判断当前日期所属的年份是否是闰年，私有函数</summary>
        /// <param name="dt">日期</param>
        /// <returns>是闰年：True ，不是闰年：False</returns>
        private static bool IsRuYear(DateTime idt)
        {
            //形式参数为日期类型 
            //例如：2003-12-12
            int n;
            n = idt.Year;

            if ((n % 400 == 0) || (n % 4 == 0 && n % 100 != 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判断当前年份是否是闰年，私有函数
        /// <summary>判断当前年份是否是闰年，私有函数</summary>
        /// <param name="dt">年份</param>
        /// <returns>是闰年：True ，不是闰年：False</returns>
        private static bool IsRuYear(int iYear)
        {
            //形式参数为年份
            //例如：2003
            int n;
            n = iYear;

            if ((n % 400 == 0) || (n % 4 == 0 && n % 100 != 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 将输入的字符串转化为日期。如果字符串的格式非法，则返回当前日期
        /// <summary>
        /// 将输入的字符串转化为日期。如果字符串的格式非法，则返回当前日期。
        /// </summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>日期对象</returns>
        public static DateTime ConvertStringToDate(string strInput)
        {
            DateTime oDateTime;

            try
            {
                oDateTime = DateTime.Parse(strInput);
            }
            catch (Exception)
            {
                oDateTime = DateTime.Today;
            }

            return oDateTime;
        }
        #endregion

        #region 将日期对象转化为格式字符串
        /// <summary>
        /// 将日期对象转化为格式字符串
        /// </summary>
        /// <param name="oDateTime">日期对象</param>
        /// <param name="strFormat">
        /// 格式：
        ///        "SHORTDATE"===短日期
        ///        "LONGDATE"==长日期
        ///        其它====自定义格式
        /// </param>
        /// <returns>日期字符串</returns>
        public static string ConvertDateToString(DateTime oDateTime, string strFormat)
        {
            string strDate = "";

            try
            {
                switch (strFormat.ToUpper())
                {
                    case "SHORTDATE":
                        strDate = oDateTime.ToShortDateString();
                        break;
                    case "LONGDATE":
                        strDate = oDateTime.ToLongDateString();
                        break;
                    default:
                        strDate = oDateTime.ToString(strFormat);
                        break;
                }
            }
            catch (Exception)
            {
                strDate = oDateTime.ToShortDateString();
            }

            return strDate;
        }
        #endregion

        #region 判断是否为合法日期，必须大于1800年1月1日
        /// <summary>
        /// 判断是否为合法日期，必须大于1800年1月1日
        /// </summary>
        /// <param name="strDate">输入日期字符串</param>
        /// <returns>True/False</returns>
        public static bool IsDateTime(string strDate)
        {
            try
            {
                DateTime oDate = DateTime.Parse(strDate);
                if (oDate.CompareTo(DateTime.Parse("1800-1-1")) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 获取两个日期之间的差值 可返回年 月 日 小时 分钟 秒
        /// <summary>
        /// 获取两个日期之间的差值
        /// </summary>
        /// <param name="howtocompare">比较的方式可为：year month day hour minute second</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>时间差</returns>
        public static double DateDiff(string howtocompare, DateTime startDate, DateTime endDate)
        {
            double diff = 0;
            try
            {
                TimeSpan TS = new TimeSpan(endDate.Ticks - startDate.Ticks);

                switch (howtocompare.ToLower())
                {
                    case "year":
                        diff = Convert.ToDouble(TS.TotalDays / 365);
                        break;
                    case "month":
                        diff = Convert.ToDouble((TS.TotalDays / 365) * 12);
                        break;
                    case "day":
                        diff = Convert.ToDouble(TS.TotalDays);
                        break;
                    case "hour":
                        diff = Convert.ToDouble(TS.TotalHours);
                        break;
                    case "minute":
                        diff = Convert.ToDouble(TS.TotalMinutes);
                        break;
                    case "second":
                        diff = Convert.ToDouble(TS.TotalSeconds);
                        break;
                }
            }
            catch (Exception)
            {
                diff = 0;
            }
            return diff;
        }
        #endregion

        #region 计算两个日期之间相差的工作日天数
        ///  <summary>
        ///  计算两个日期之间相差的工作日天数
        ///  </summary>
        ///  <param  name="dtStart">开始日期</param>
        ///  <param  name="dtEnd">结束日期</param>
        ///  <param  name="Flag">是否除去周六，周日</param>
        ///  <returns>Int</returns>
        public static int CalculateWorkingDays(DateTime dtStart, DateTime dtEnd, bool Flag)
        {
            int count = 0;
            for (DateTime dtTemp = dtStart; dtTemp < dtEnd; dtTemp = dtTemp.AddDays(1))
            {
                if (Flag)
                {
                    if (dtTemp.DayOfWeek != DayOfWeek.Saturday && dtTemp.DayOfWeek != DayOfWeek.Sunday)
                    {
                        count++;
                    }
                }
                else
                {
                    count++;
                }
            }
            return count;
        }
        #endregion

        #region 秒数转日期
        /// <summary>
        /// 秒数转日期
        /// </summary>
        /// <param name="Value">秒数</param>
        /// <returns>日期</returns>
        public static DateTime GetGMTDateTime(int Value)
        {
            //秒数转时间日期
            //GMT时间从2000年1月1日开始，先把它作为赋为初值
            long Year = 2000, Month = 1, Day = 1;
            long Hour = 0, Min = 0, Sec = 0;
            //临时变量
            long iYear = 0, iDay = 0;
            long iHour = 0, iMin = 0, iSec = 0;
            //计算文件创建的年份
            iYear = Value / (365 * 24 * 60 * 60);
            Year = Year + iYear;
            //计算文件除创建整年份以外还有多少天
            iDay = (Value % (365 * 24 * 60 * 60)) / (24 * 60 * 60);
            //把闰年的年份数计算出来
            int RInt = 0;
            for (int i = 2000; i < Year; i++)
            {
                if ((i % 4) == 0)
                    RInt = RInt + 1;
            }
            //计算文件创建的时间(几时)
            iHour = ((Value % (365 * 24 * 60 * 60)) % (24 * 60 * 60)) / (60 * 60);
            Hour = Hour + iHour;
            //计算文件创建的时间(几分)
            iMin = (((Value % (365 * 24 * 60 * 60)) % (24 * 60 * 60)) % (60 * 60)) / 60;
            Min = Min + iMin;
            //计算文件创建的时间(几秒)
            iSec = (((Value % (365 * 24 * 60 * 60)) % (24 * 60 * 60)) % (60 * 60)) % 60;
            Sec = Sec + iSec;
            DateTime t = new DateTime((int)Year, (int)Month, (int)Day, (int)Hour, (int)Min, (int)Sec);
            DateTime t1 = t.AddDays(iDay - RInt);
            return t1;
        }

        #endregion

        #region 计算两个日期的差，返回(year:month:day:secode)
        public static DateTime GetValueByTwo(DateTime startTime, DateTime endTime)
        {
            //两个待计算的时间
            //DateTime startTime = DateTime.Parse("2017-07-05 22:29:10.643");//之前的时间
            //DateTime endTime = DateTime.Now;//当前时间
            //计算
            double minutes = DateTimeHelper.DateDiff("minute", startTime, endTime);//相差的秒数
            DateTime retTime = DateTimeHelper.GetGMTDateTime(Convert.ToInt32(minutes * 60));//分钟转为结果时间
            return retTime;
        }
        #endregion

        #region 秒数转换为 hh:mm
        /// <summary>
        /// 秒数转换为 hh:mm
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string GetHourMinDatetime(int second)
        {
            int mins = second / 60;
            //string[] temps = (mins / 60).ToString().Split('.');
            //int hours = Convert.ToInt32(temps[0]);
            double t = (mins / 60);
            int hours = Convert.ToInt32(Math.Floor(t));
            int min = Convert.ToInt32(mins % 60);
            string time = (hours < 10 ? "0" + hours : hours + "") + " 小时 " + ((min < 10.0) ? ("0" + min.ToString()) : min.ToString()) + " 分钟";// ":00";

            return time;
        }
        #endregion

        #region 根据两个时间，返回相差，格式（小时:分钟）

        /// <summary>
        /// 根据两个时间，返回相差，格式（小时:分钟）
        /// </summary>
        /// <returns></returns>
        public static string GetUseTime(DateTime startTime, DateTime endTime)
        {
            double second = DateTimeHelper.DateDiff("second", startTime, endTime);//相差的秒数
            string ret = DateTimeHelper.GetHourMinDatetime(Convert.ToInt32(second));
            return ret;
        }
        #endregion

        #region 根据两个秒数，返回差值，格式（小时:分钟）
        /// <summary>
        /// 根据两个秒数，返回差值，格式（小时:分钟）
        /// </summary>
        /// <param name="startSecond">最小秒</param>
        /// <param name="endSecond">最大秒</param>
        /// <returns></returns>
        public static string GetLeaveTime(int startSecond, int endSecond)
        {
            int spanSecond = endSecond - startSecond;
            if (spanSecond >= 0)
            {
                return DateTimeHelper.GetHourMinDatetime(spanSecond);
            }
            else
            {
                return null;//时间格式有误
            }
        }
        #endregion

        #region 整理日期，只取时分
        public static string GetHM(DateTime dt)
        {
            return dt.Hour + ":" + (dt.Minute < 10 ? "0" + dt.Minute.ToString() : dt.Minute.ToString());
        }
        #endregion

        #region 返回：年月日时分秒
        public static string GetString()
        {
            return "";
        }
        #endregion

    }
}
