using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace White.Common
{
    /// <summary>
    /// 安全辅助类
    /// </summary>
    public class SecurityHelper
    {

        #region 1.0 使用 票据对象 将 用户数据 加密成字符串（默认3小时有效） +static string EncryptTicketString(string info)
        /// <summary>
        /// 使用 票据对象 将 用户数据 加密成字符串（默认3小时有效）
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string EncryptTicketString(string info)
        {
            //1.1 将用户数据 存入 票据对象
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "AdminUser", DateTime.Now, DateTime.Now.AddHours(3), false, info);

            //1.2 将票据对象 加密成字符串（可逆）
            return FormsAuthentication.Encrypt(ticket);
        }
        #endregion

        #region 1.1 使用票据对象将用户数据加密成字符串（传入有效期日期） + static string EncryptTicketString(string info, DateTime expireDate)
        /// <summary>
        /// 使用票据对象将用户数据加密成字符串（传入有效期日期）
        /// </summary>
        /// <param name="info"></param>
        /// <param name="expireDate"></param>
        /// <returns></returns>
        public static string EncryptTicketString(string info, DateTime expireDate)
        {
            //1.1 将用户数据 存入 票据对象
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "AdminUser", DateTime.Now, expireDate, false, info);

            //1.2 将票据对象 加密成字符串（可逆）
            return FormsAuthentication.Encrypt(ticket);
        } 
        #endregion


        #region 2.0 使用票据对象解密加密后字符串，失效返回null  +static string DecryptTicketString(string cryptograph)
        /// <summary>
        /// 使用票据对象解密加密后字符串，失效返回null
        /// </summary>
        /// <param name="cryptograph">加密字符串</param>
        /// <returns></returns>
        public static string DecryptTicketString(string cryptograph)
        {
            try
            {
                //1.1 将 加密字符串 解密成 票据对象
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cryptograph);

                //1.2 将票据里的 用户数据 返回
                return ticket.Expired ? null : ticket.UserData;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion
        

        #region 3.0 计算字符串的MD5值 +static string GetMD5(string input)
        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5(string input)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(input);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        }
        #endregion


        #region 4.0 计算字符串的SHA1值 + static string SHA1(string str)
        /// <summary>
        /// 计算字符串的SHA1值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SHA1(string str)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "");
            return str_sha1_out;
        } 
        #endregion

    }
}
