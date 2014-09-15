//=========================================================================
// CopyRight (C) 2005-2014 温州市捷点信息技术有限公司 All Rights Reserved.
//=========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;
using HD.Framework.Helper;

namespace HD.Framework.Utils
{
    /// <summary>
    /// 加密解密函数
    /// </summary>
    public class Encrypt
    {
        /// <summary>
        /// 加密因子
        /// </summary>
        private const string Factor = "www.cnvp.com";

        #region "MD5加密"
        /// <summary>
        /// MD5加密值
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <returns></returns>
        public static string Md5(string str)
        {
            return Md5_Encrypt(str);
        }
        /// <summary>
        /// MD5加密值
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <returns></returns>
        public static string Md5_Encrypt(string str)
        {
            return Md5_Encrypt(str, Factor);
        }

        /// <summary>
        /// MD5加密值
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Md5_Encrypt(string str, string key)
        {
            string strEncrypt = string.Format("{0}@{1}", str, key);
            for (int i = 0; i < 2; i++)
            {
                var hashPasswordForStoringInConfigFile = FormsAuthentication.HashPasswordForStoringInConfigFile(strEncrypt, "MD5");
                if (hashPasswordForStoringInConfigFile != null)
                    strEncrypt = hashPasswordForStoringInConfigFile.ToUpper();
            }
            return strEncrypt;
        }
        #endregion
        #region "DES加密"
        /// <summary>
        /// DES可逆加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <returns></returns>
        public static string DES_Encrypt(string str)
        {
            return DES_Encrypt(str, Factor);
        }
        /// <summary> 
        /// DES可逆加密
        /// </summary> 
        /// <param name="str">加密字符</param> 
        /// <param name="key">加密因子</param> 
        /// <returns></returns> 
        public static string DES_Encrypt(string str, string key)
        {
            var des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(str);
            var hashPasswordForStoringInConfigFile = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5");
            if (hashPasswordForStoringInConfigFile != null)
                des.Key = Encoding.ASCII.GetBytes(hashPasswordForStoringInConfigFile.Substring(0, 8));
            var passwordForStoringInConfigFile = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5");
            if (passwordForStoringInConfigFile != null)
                des.IV = ASCIIEncoding.ASCII.GetBytes(passwordForStoringInConfigFile.Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary>
        /// DES可逆解密
        /// </summary>
        /// <param name="Str">加密字符</param>
        /// <returns></returns>
        public static string DES_Decrypt(string Str)
        {
            return DES_Decrypt(Str, Factor);
        }
        /// <summary> 
        /// DES可逆解密
        /// </summary> 
        /// <param name="Str">加密字符</param> 
        /// <param name="Key">解密因子</param> 
        /// <returns></returns> 
        public static string DES_Decrypt(string Str, string Key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Str.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Str.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Key, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion
        #region "RAS加密"
        /// <summary>
        /// 支持最大127个明文长度
        /// </summary>
        private const int DWKEYSIZE = 1024;
        /// <summary>
        /// 创建密钥对
        /// </summary>
        /// <returns></returns>
        public static void GetRASKey(ref string publicKey,ref string privateKey)
        {
            if (publicKey == null) throw new ArgumentNullException("publicKey");
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            //声明一个指定大小的RSA容器
            var rsaProvider = new RSACryptoServiceProvider(DWKEYSIZE);
            //取得RSA容易里的各种参数
            var p = rsaProvider.ExportParameters(true);

            publicKey = ComponentKey(p.Exponent, p.Modulus);
            privateKey = ComponentKey(p.D, p.Modulus);
        }

        /// <summary>
        /// 检查明文的有效性
        /// </summary>
        /// <returns></returns>
        private static bool CheckSourceValidate(string str)
        {
            return (DWKEYSIZE / 8 - 11) >= str.Length;
        }
        #region "解析密匙"
        /// <summary>
        /// 组合成密匙字符串
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        private static string ComponentKey(byte[] b1, byte[] b2)
        {
            List<byte> list = new List<byte>();
            //在前端加上第一个数组的长度值 这样今后可以根据这个值分别取出来两个数组
            list.Add((byte)b1.Length);
            list.AddRange(b1);
            list.AddRange(b2);
            byte[] b = list.ToArray();
            return Convert.ToBase64String(b);
        }
        /// <summary>
        /// 解析密匙
        /// </summary>
        /// <param name="key">密匙</param>
        /// <param name="b1">RSA的相应参数1</param>
        /// <param name="b2">RSA的相应参数2</param>
        private static void ResolveKey(string key, out byte[] b1, out byte[] b2)
        {
            //从base64字符串 解析成原来的字节数组
            byte[] b = Convert.FromBase64String(key);
            //初始化参数的数组长度
            b1 = new byte[b[0]];
            b2 = new byte[b.Length - b[0] - 1];
            //将相应位置是值放进相应的数组
            for (int n = 1, i = 0, j = 0; n < b.Length; n++)
            {
                if (n <= b[0])
                {
                    b1[i++] = b[n];
                }
                else
                {
                    b2[j++] = b[n];
                }
            }
        }
        #endregion
        #region "加密解密"
        /// <summary>
        /// RSA可逆加密
        /// </summary>
        /// <param name="Str">加密字符</param>
        /// <param name="Key">加密因子</param>
        /// <returns></returns>
        public static string RSA_Encrypt(string Str, string Key)
        {
            string encryptString = string.Empty;
            byte[] d;
            byte[] n;
            try
            {
                if (!CheckSourceValidate(Str))
                {
                    string LogContent = string.Format("明文字符：{0}，错误原因：{1}", Str, "明文长度超出最大长度。");
                    LogHelper.WriteLog(LogContent);
                }
                //解析这个密钥
                ResolveKey(Key, out d, out n);
                BigInteger biN = new BigInteger(n);
                BigInteger biD = new BigInteger(d);
                return RSA_Encrypt(Str, biD, biN);
            }
            catch(Exception ex)
            {
                string LogContent = string.Format("明文字符：{0}，错误原因：{1}", Str, ex.ToString());
                LogHelper.WriteLog(LogContent);
            }
            return null;
        }

        /// <summary>
        /// RSA可逆解密
        /// </summary>
        /// <param name="Str">密文字符</param>
        /// <param name="Key">解密因子</param>
        /// <returns></returns>
        public static string RSA_Decrypt(string Str, string Key)
        {
            byte[] e;
            byte[] n;
            try
            {
                ResolveKey(Key, out e, out n);
                BigInteger biE = new BigInteger(e);
                BigInteger biN = new BigInteger(n);
                return RSA_Decrypt(Str, biE, biN);
            }
            catch (Exception ex)
            {
                string LogContent = string.Format("密文字符：{0}，错误原因：{1}", Str, ex.ToString());
                LogHelper.WriteLog(LogContent);
            }
            return null;
        }
        /// <summary>
        /// 用指定的密匙加密 
        /// </summary>
        /// <param name="source">明文</param>
        /// <param name="d">可以是RSACryptoServiceProvider生成的D</param>
        /// <param name="n">可以是RSACryptoServiceProvider生成的Modulus</param>
        /// <returns>返回密文</returns>
        private static string RSA_Encrypt(string source, BigInteger d, BigInteger n)
        {
            int len = source.Length;
            int len1 = 0;
            int blockLen = 0;
            if ((len % 128) == 0)
                len1 = len / 128;
            else
                len1 = len / 128 + 1;
            string block = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < len1; i++)
            {
                if (len >= 128)
                    blockLen = 128;
                else
                    blockLen = len;
                block = source.Substring(i * 128, blockLen);
                byte[] oText = System.Text.Encoding.Default.GetBytes(block);
                BigInteger biText = new BigInteger(oText);
                BigInteger biEnText = biText.modPow(d, n);
                string temp = biEnText.ToHexString();
                result.Append(temp).Append("@");
                len -= blockLen;
            }
            return result.ToString().TrimEnd('@');
        }

        /// <summary>
        /// 用指定的密匙加密 
        /// </summary>
        /// <param name="source">密文</param>
        /// <param name="e">可以是RSACryptoServiceProvider生成的Exponent</param>
        /// <param name="n">可以是RSACryptoServiceProvider生成的Modulus</param>
        /// <returns>返回明文</returns>
        private static string RSA_Decrypt(string encryptString, BigInteger e, BigInteger n)
        {
            StringBuilder result = new StringBuilder();
            string[] strarr1 = encryptString.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strarr1.Length; i++)
            {
                string block = strarr1[i];
                BigInteger biText = new BigInteger(block, 16);
                BigInteger biEnText = biText.modPow(e, n);
                string temp = System.Text.Encoding.Default.GetString(biEnText.getBytes());
                result.Append(temp);
            }
            return result.ToString();
        }
        #endregion
        #endregion
    }
}