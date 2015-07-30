using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xen.Common.Services.Logging;
using Xen.Entity;
using Xen.Helpers;

namespace SMEasy.Common
{
    public class RandomPassword
    {
        public static string EncryptPassword(string password)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);
            HashAlgorithm hash = new SHA256Managed();
            byte[] computedHash = hash.ComputeHash(plainTextBytes);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var computedHashItem in computedHash)
            {
                stringBuilder.Append(computedHashItem.ToString("X2"));
            }
            return stringBuilder.ToString();
        }

        public static string GenerateEncryptedRandomPassword()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            Random r = new Random();
            builder.Append(RandomString(4, true));
            builder.Append(r.Next(1000, 9999));
            builder.Append(RandomString(2, false));
            return EncryptPassword(builder.ToString());
        }

        public static string GenerateRandomPassword()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            Random r = new Random();
            builder.Append(RandomString(4, true));
            builder.Append(r.Next(1000, 9999));
            builder.Append(RandomString(2, false));

            return builder.ToString();
        }

        private static string RandomString(int size, bool lowerCase)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            Random random = new Random();

            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            if (lowerCase)
                return builder.ToString().ToLower();

            return builder.ToString();
        }


        #region Encryption for Employee Unique Id

        public static string Encrypt(string value, string key = "")
        {
            TripleDESCryptoServiceProvider desCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            byte[] byteHash, byteBuff;
            string strTempKey = key;

            byteHash = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            hashMD5 = null;
            desCrypto.Key = byteHash;
            desCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = ASCIIEncoding.ASCII.GetBytes(value);
            return Convert.ToBase64String(desCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        }

        public static string Decrypt(string value, string key = "")
        {
            TripleDESCryptoServiceProvider crypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            byte[] byteHash, byteBuff;
            string strTempKey = key;

            byteHash = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            hashMD5 = null;
            crypto.Key = byteHash;
            crypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = Convert.FromBase64String(value);
            string strDecrypted = ASCIIEncoding.ASCII.GetString(crypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            crypto = null;

            return strDecrypted;
        }

        #endregion


    }
}
