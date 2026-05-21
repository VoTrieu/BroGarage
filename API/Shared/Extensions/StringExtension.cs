using System.Globalization;
using System.Text;

namespace BroGarage.API.Shared.Extensions
{
    public static class StringExtension
    {
        public static DateTime? ToDateTime(this string dateTime, string format = "dd/MM/yyyy")
        {
            if (DateTime.TryParseExact(dateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value))
            {
                return value;
            }
            return default;
        }

        public static byte[] GetByteArray(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
    }
}
