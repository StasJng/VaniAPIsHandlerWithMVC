using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MP.Lib.LibUtility
{
    public class SecurityMethod
    {
        public static string RandomString(int length)
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int strlen = str.Length;
            Random rnd = new Random();
            string retVal = String.Empty;

            for (int i = 0; i < length; i++)
                retVal += str[rnd.Next(strlen)];

            return retVal;
        }

        public static string RandomStringNumber(int length)
        {
            string str = "0123456789";
            int strlen = str.Length;
            Random rnd = new Random();
            string retVal = String.Empty;

            for (int i = 0; i < length; i++)
                retVal += str[rnd.Next(strlen)];

            return retVal;
        }

        public static string MD5Encrypt(string plainText)
        {
            byte[] data, output;
            UTF8Encoding encoder = new UTF8Encoding();
            MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider();

            data = encoder.GetBytes(plainText);
            output = hasher.ComputeHash(data);

            return BitConverter.ToString(output).Replace("-", string.Empty).ToLower();
        }
        public static byte[] CreateDefaultMD5(string plainText)
        {
            byte[] data, output;
            UTF8Encoding encoder = new UTF8Encoding();
            MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider();

            data = encoder.GetBytes(plainText);
            output = hasher.ComputeHash(data);

            return output;
        }
        public static string RandomPassword()
        {
            string retVal = String.Empty;
            Random rd = new Random(DateTime.Now.Millisecond);
            for (int i = 1; i < 10; i++)
            {
                retVal += rd.Next(0, 9);
            }
            return retVal;
        }

        #region HMACSHA
        public static string HMACSHA256ToHex(string secretKey, string partnerCode, string strData)
        {
            return HMACSHA256ToHex(HMACSHA1(secretKey, partnerCode), strData).ToLower();
        }
        public static string HMACSHA256ToHex(string secretKey, string strData)
        {
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            byte[] data = Encoding.UTF8.GetBytes(strData);
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                byte[] hashValue = hmac.ComputeHash(data);
                DesSecurity des = new DesSecurity();
                string strkey = des.Byte2Hex(hashValue);
                return strkey;
            }

        }
        public static byte[] HMACSHA256(byte[] secretKey, string strData)
        {
            byte[] data = Encoding.UTF8.GetBytes(strData);
            using (HMACSHA256 hmac = new HMACSHA256(secretKey))
            {
                byte[] hashValue = hmac.ComputeHash(data);
                return hashValue;
            }
        }


        public static string HMACSHA1(string secretKey, string strData)//byte[] HMACSHA1(string secretKey, string strData)
        {
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            using (HMACSHA1 hmac = new HMACSHA1(key))
            {
                byte[] data = Encoding.UTF8.GetBytes(strData);
                byte[] result;
                //SHA1 sha = new SHA1CryptoServiceProvider();
                // This is one implementation of the abstract class SHA1.
                result = hmac.ComputeHash(data);
                DesSecurity des = new DesSecurity();
                string strkey = des.Byte2Hex(result).ToLower(); //BitConverter.ToString(result);
                return strkey;
            }
        }
        #endregion
    }
}
