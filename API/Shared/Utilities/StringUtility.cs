using System.Text;
using System.Text.RegularExpressions;

namespace BroGarage.API.Shared.Utilities
{
    public class StringUtility
    {
        public static string ConvertToUnSign(string input)
        {
            if (input == null)
            {
                return "";
            }
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.Contains('?', StringComparison.CurrentCulture))
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        public static string Random(int length)
        {
            char[] _base62chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

            Random _random = new();

            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                sb.Append(_base62chars[_random.Next(62)]);
            }

            return sb.ToString();
        }

        public static string RandomFromSource(char[] source, int length)
        {
            Random _random = new();

            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                sb.Append(source[_random.Next(62)]);
            }

            return sb.ToString();
        }

        public static byte[] GetByteArrayFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string GetStringFromByteArray(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
