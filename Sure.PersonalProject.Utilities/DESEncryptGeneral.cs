using System;
using System.Text;

namespace Sure.PersonalProject.Utilities
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: DESEncryptGeneral DES加解密
    -----------------------------------------------------------------------*/
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// DESEncrypt 加密
    /// </summary>
    public class DESEncryptGeneral
    {
        /// <summary>
        /// 加解密秘钥
        /// </summary>
        private static readonly string sKey = "senlin.huang";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text">需加密的字符串</param>
        /// <returns>已加密的字符串</returns>
        public static string Encrypt(string Text)
        {
            return DESEncryptGeneral.Encrypt(Text, sKey);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text">需解密字符串</param>
        /// <returns>已解密字符串</returns>
        public static string Decrypt(string Text)
        {
            if (!string.IsNullOrEmpty(Text))
                return DESEncryptGeneral.Decrypt(Text, sKey);
            return string.Empty;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text">需加密的字符串</param>
        /// <param name="sKey">需加密的字符串秘钥</param>
        /// <returns>已加密的字符串</returns>
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(Text);
            cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in memoryStream.ToArray())
                stringBuilder.AppendFormat("{0:X2}", (object)num);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text">需加密的字符串</param>
        /// <param name="sKey">需加密的字符串秘钥</param>
        /// <returns>已加密的字符串</returns>
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            int length = Text.Length / 2;
            byte[] buffer = new byte[length];
            for (int index = 0; index < length; ++index)
            {
                int num = Convert.ToInt32(Text.Substring(index * 2, 2), 16);
                buffer[index] = (byte)num;
            }
            cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.Default.GetString(memoryStream.ToArray());
        }

        /// <summary>
        /// Md5
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>已加密的字符串</returns>
        private static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
