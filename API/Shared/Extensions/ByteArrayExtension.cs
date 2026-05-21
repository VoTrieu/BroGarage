using System.Text;

namespace BroGarage.API.Shared.Extensions
{
    public static class ByteArrayExtension
    {
        public static string GetString(this byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
