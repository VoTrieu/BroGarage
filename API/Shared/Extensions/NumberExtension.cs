using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroGarage.API.Shared.Extensions
{
    public static class NumberExtension
    {
        const string defaultCul = "en-US";

        public static string ToNumberString(this decimal num, string cul = defaultCul, byte dec = 0)
        {
            return num.ToString($"N{dec}", new CultureInfo(cul));
        }

        public static string ToNumberString(this double num, string cul = defaultCul, byte dec = 0)
        {
            return num.ToString($"N{dec}", new CultureInfo(cul));
        }
        public static string ToNumberString(this float num, string cul = defaultCul, byte dec = 0)
        {
            return num.ToString($"N{dec}", new CultureInfo(cul));
        }

        public static string ToNumberString(this int num, string cul = defaultCul, byte dec = 0)
        {
            return num.ToString($"N{dec}", new CultureInfo(cul));
        }

        public static string ToNumberString(this short num, string cul = defaultCul, byte dec = 0)
        {
            return num.ToString($"N{dec}", new CultureInfo(cul));
        }

        public static string ToNumberString(this long num, string cul = defaultCul, byte dec = 0)
        {
            return num.ToString($"N{dec}", new CultureInfo(cul));
        }
    }
}
