using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xen.Helpers
{
    public static class StringHelper
    {
        public static string Trim(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            return Regex.Replace(value, "^[ \t\r\n]+|[ \t\r\n]+$", "");
        }

        public static string TrimSpace(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            return Regex.Replace(value, @"\s", "");
        }

        public static bool IsNumeric(this string value)
        {
            float output;
            return float.TryParse(value, out output);
        }

        public static byte[] GetBytes(string value)
        {
            byte[] bytes = new byte[value.Length * sizeof(char)];
            System.Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}
