using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace White.Base
{
    public class BaseController : Controller
    {
        #region 分页中当前页码（默认为1）#int PageIndex
        /// <summary>
        /// 分页中当前页码（默认为1）
        /// </summary>
        private int _pageIndex = 1;
        protected int PageIndex { get { return _pageIndex; } set { _pageIndex = value; } }

        #endregion

        #region 分页中页容量大小（默认为10）#int PageSize
        /// <summary>
        /// 分页中页容量大小（默认为10）
        /// </summary>
        private int _pageSize = 10;
        protected int PageSize { get { return _pageSize; } set { _pageSize = value; } }
        #endregion

        #region 上传图片保存路径 # string UploadImagePath
        /// <summary>
        /// 上传图片保存路径
        /// </summary>
        protected string UploadImagePath
        {
            get
            {
                return (ConfigurationManager.AppSettings["UploadImgPath"] ?? "") + DateTime.Now.ToString("yyyy/MM/dd/");
            }
        }
        #endregion

        #region 获得当前页面客户端的IP + string GetIP()
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns></returns>
        public string GetIP()
        {
            var IP = string.Empty;

            IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(IP))
            {
                IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(IP))
            {
                IP = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(IP))
            {
                return "127.0.0.1";
            }

            return IP;
        }
        #endregion

        #region WebUrl 网站服务器地址 # string WebUrl
        /// <summary>
        /// WebUrl 网站服务器地址
        /// </summary>
        public string WebUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["WebUrl"] ?? "";
            }
        }
        #endregion

        #region PayUrl 支付地址 # string PayUrl
        /// <summary>
        /// PayUrl 支付地址
        /// </summary>
        public string PayUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["PayUrl"] ?? "";
            }
        }
        #endregion

        #region 从缓存中获取皮肤类名
        /// <summary>
        /// 从缓存中获取皮肤类名
        /// </summary>
        /// <returns></returns>
        public static string SkinClass()
        {
            var skin = "";

            var cookie = System.Web.HttpContext.Current.Request.Cookies["SkinClass"];

            if (cookie == null)
            {
                skin = "skin-blur-ocean";
            }
            else
            {
                skin = cookie.Value;
            }

            return skin;
        } 
        #endregion
    }
}