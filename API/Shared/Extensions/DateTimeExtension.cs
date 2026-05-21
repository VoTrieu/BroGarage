using BroGarage.API.Shared.Enums;
using System.Globalization;

namespace BroGarage.API.Shared.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToDate(this DateTime dateTime)
        {
            return dateTime.ToString(DateTimeEnum.DateFormat, CultureInfo.InvariantCulture);
        }

        public static string ToTime(this TimeSpan dateTime)
        {
            return dateTime.ToString(DateTimeEnum.TimeFormat, CultureInfo.InvariantCulture);
        }

        private static readonly DateTime FromDate = new(1970, 1, 1);

        public static long ToTimestamp(this DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.Subtract(FromDate).TotalSeconds);
        }
    }
}
