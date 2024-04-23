using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.utils
{
    public class MD5
    {
        private static TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        private static MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        private static string myKey = "mi2018pueblo";

        public static string EncryptText(string p_text)
        {
            string result = "";

            Byte[] buff = Encoding.UTF8.GetBytes(p_text);
            hashmd5 = new MD5CryptoServiceProvider();
            Byte[] bytHash = hashmd5.ComputeHash(buff);
            hashmd5.Clear();

            for (var i = 0; i < bytHash.Length; i++)
            {
                result = result + bytHash[i].ToString("x").PadLeft(2, '0');
            }


            return result;
        }

    }
}
