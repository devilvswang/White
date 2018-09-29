using White.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace White.Base
{
    public class WebCache
    {
        private static string permissionUrlCacheName = "PermissionUrl";
        private static string topMenuCacheName = "TopTreeMenuHtml";
        private static string leftMenuCacheName = "LeftTreeMenuHtml";

        #region 1.0 从缓存中获取URL权限 + static string GetPermissionUrlCache(User_Info loginUser)
        /// <summary>
        /// 从缓存中获取URL权限
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetPermissionUrlCache(User_Info loginUser)
        {
            return HttpRuntime.Cache.Get(permissionUrlCacheName + loginUser.ID) as string;
        }
        #endregion

        #region 1.1 将URL权限加入缓存中 + static void SetPermissionUrlCache(User_Info loginUser, string permissionUrl)
        /// <summary>
        /// 将URL权限加入缓存中
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="permissionUrl"></param>
        public static void SetPermissionUrlCache(User_Info loginUser, string permissionUrl)
        {
            HttpRuntime.Cache.Insert(permissionUrlCacheName + loginUser.ID, permissionUrl);
        }
        #endregion

        #region 1.2 移除URL权限缓存 + static void RemovePermissionUrlCache(User_Info loginUser)
        /// <summary>
        /// 移除URL权限缓存
        /// </summary>
        /// <param name="loginUser"></param>
        public static void RemovePermissionUrlCache(User_Info loginUser)
        {
            if (GetPermissionUrlCache(loginUser) != null)
            {
                HttpRuntime.Cache.Remove(permissionUrlCacheName + loginUser.ID);
            }
        }
        #endregion


        #region 2.0 从缓存中获取上侧菜单HTML + static string GetTopMenuCache(User_Info loginUser)
        /// <summary>
        /// 从缓存中获取上侧菜单HTML
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetTopMenuCache(User_Info loginUser)
        {
            return HttpRuntime.Cache.Get(topMenuCacheName + loginUser.ID) as string;
        }
        #endregion

        #region 2.1 将上侧菜单HTML加入缓存中 + static void SetTopMenuCache(User_Info loginUser, string topMenuHtml)
        /// <summary>
        /// 将上侧菜单HTML加入缓存中
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="topMenuHtml"></param>
        public static void SetTopMenuCache(User_Info loginUser, string topMenuHtml)
        {
            HttpRuntime.Cache.Insert(topMenuCacheName + loginUser.ID, topMenuHtml);
        }
        #endregion

        #region 2.2 移除上侧菜单HTML缓存 + static void RemoveTopMenuCache(User_Info loginUser)
        /// <summary>
        /// 移除上侧菜单HTML缓存
        /// </summary>
        /// <param name="loginUser"></param>
        public static void RemoveTopMenuCache(User_Info loginUser)
        {
            if (GetTopMenuCache(loginUser) != null)
            {
                HttpRuntime.Cache.Remove(topMenuCacheName + loginUser.ID);
            }
        }
        #endregion

        

        #region 3.0 从缓存中获取左侧菜单HTML + static string GetLeftMenuCache(User_Info loginUser)
        /// <summary>
        /// 从缓存中获取左侧菜单HTML
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetLeftMenuCache(User_Info loginUser)
        {
            return HttpRuntime.Cache.Get(leftMenuCacheName + loginUser.ID) as string;
        }
        #endregion

        #region 3.1 将左侧菜单HTML加入缓存中 + static void SetLeftMenuCache(User_Info loginUser, string leftMenuHtml)
        /// <summary>
        /// 将左侧菜单HTML加入缓存中
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="leftMenuHtml"></param>
        public static void SetLeftMenuCache(User_Info loginUser, string leftMenuHtml)
        {
            HttpRuntime.Cache.Insert(leftMenuCacheName + loginUser.ID, leftMenuHtml);
        }
        #endregion

        #region 3.2 移除左侧菜单HTML缓存 + static void RemoveLeftMenuCache(User_Info loginUser)
        /// <summary>
        /// 移除左侧菜单HTML缓存
        /// </summary>
        /// <param name="loginUser"></param>
        public static void RemoveLeftMenuCache(User_Info loginUser)
        {
            if (GetLeftMenuCache(loginUser) != null)
            {
                HttpRuntime.Cache.Remove(leftMenuCacheName + loginUser.ID);
            }
        }
        #endregion
    }
}
