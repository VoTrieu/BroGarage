using System.Security.Cryptography;
using System.Text;

namespace BroGarage.API.Shared.Utilities
{
    public class CryptographyUtility
    {
        public static string TripleDESEncrypt(string key, string msg)
        {
            byte[] results;
            UTF8Encoding UTF8 = new();
            var md5 = MD5.Create();
            var bytes = md5.ComputeHash(UTF8.GetBytes(key));
            var tripleDes = TripleDES.Create();
            tripleDes.Key = bytes;
            tripleDes.Mode = CipherMode.ECB;
            tripleDes.Padding = PaddingMode.PKCS7;
            byte[] data = UTF8.GetBytes(msg);
            try
            {
                var encryptor = tripleDes.CreateEncryptor();
                results = encryptor.TransformFinalBlock(data, 0, data.Length);
            }
            finally
            {
                tripleDes.Clear();
                md5.Clear();
            }
            return Convert.ToBase64String(results);
        }

        public static string TripleDESDescrypt(string key, string msg)
        {
            byte[] results;
            UTF8Encoding UTF8 = new();
            var md5 = MD5.Create();
            var bytes = md5.ComputeHash(UTF8.GetBytes(key));
            var tripleDes = TripleDES.Create();
            tripleDes.Key = bytes;
            tripleDes.Mode = CipherMode.ECB;
            tripleDes.Padding = PaddingMode.PKCS7;
            byte[] data = Convert.FromBase64String(msg);
            try
            {
                var descryptor = tripleDes.CreateDecryptor();
                results = descryptor.TransformFinalBlock(data, 0, data.Length);
            }
            finally
            {
                tripleDes.Clear();
                md5.Clear();
            }
            return UTF8.GetString(results);
        }

        public static string HashSHA256(string rawData)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string HashMD5(string input)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
