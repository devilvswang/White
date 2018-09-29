using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Common
{
    public class WebHelper
    {
        #region 1.0 浏览器类型，0-PC，1-手机，2-微信 + static int GetUserAgentType(string userAgent)
        /// <summary>
        /// 浏览器类型，0-PC，1-手机，2-微信
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static int GetUserAgentType(string userAgent)
        {
            if (userAgent.ToLower().Contains("micromessenger"))
            {
                return 2; //微信
            }


            string[] keywords = { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "MQQBrowser" };
            //排除Window 桌面系统 和 苹果桌面系统 
            if (!userAgent.Contains("Windows NT") && !userAgent.Contains("Macintosh"))
            {
                foreach (string item in keywords)
                {
                    if (userAgent.Contains(item))
                    {
                        return 1; //手机
                    }
                }
            }

            return 0; //默认PC 
        } 
        #endregion
    }
}
