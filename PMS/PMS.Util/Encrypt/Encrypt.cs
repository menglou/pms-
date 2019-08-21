using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;


namespace PMS.Util
{
   public  class Encrypt
    {
        //// 创建Key
        //public static string GenerateKey()
        //{
        //    DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
        //    return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        //}

        ///// <summary>
        ///// MD5加密
        ///// </summary>
        ///// <param name="pToEncrypt"></param>
        ///// <param name="sKey"></param>
        ///// <returns></returns>
        //public static string MD5Encrypt( string pToEncrypt, string sKey )
        //{
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
        //    des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();
        //    StringBuilder ret = new StringBuilder();
        //    foreach (byte b in ms.ToArray())
        //    {
        //        ret.AppendFormat("{0:X2}", b);
        //    }
        //    ret.ToString();
        //    return ret.ToString();
        //}

        ///// <summary>
        ///// MD5解密
        ///// </summary>
        ///// <param name="pToDecrypt"></param>
        ///// <param name="sKey"></param>
        ///// <returns></returns>
        //public static string MD5Decrypt(string pToDecrypt, string sKey)
        //{
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        //    byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
        //    for (int x = 0; x < pToDecrypt.Length / 2; x++)
        //    {
        //        int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
        //        inputByteArray[x] = (byte)i;
        //    }

        //    des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();

        //    StringBuilder ret = new StringBuilder();

        //    return System.Text.Encoding.Default.GetString(ms.ToArray());
        //}

        /// <summary>
        /// 加密盐
        /// </summary>
        private static readonly string saltString = "{xiketang}";
        
        /// <summary>
        /// 64位加密（md5加密是不可逆的，故无法解密）
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string MD5Encrypt64(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password + saltString);
            byte[] hash = md5.ComputeHash(bytes);

            StringBuilder stringResult = new StringBuilder();

            foreach (byte b in hash)
            {
                stringResult.Append(b.ToString("X"));
            }

            return stringResult.ToString();
        }
        
    }
}
