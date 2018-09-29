using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using White.Base;
using White.BLL;
using White.Model;

namespace White.Admin.Controllers
{

    [AdminActionFilter(Order = 1)]
    [PermissionActionFilter(Order = 2)]
    [ActionLogActionFilter(Order = 3)]
    public class PermissionController : BaseController
    {
        #region 当前登录系统管理员用户 #User_Info LoginUser
        /// <summary>
        /// 当前登录系统管理员用户
        /// </summary>
        public User_Info LoginUser { get { return System.Web.HttpContext.Current.Session["LoginUser"] as User_Info; } }
        #endregion     


        #region 1.0 获取包含权限URL的当前登录系统用户 + User_Info GetLoginUser()
        /// <summary>
        /// 获取包含权限URL的当前登录系统用户
        /// </summary>
        /// <returns></returns>
        public User_Info GetLoginUser()
        {
            LoginUser.PermissionUrl = WebCache.GetPermissionUrlCache(LoginUser);
            if (LoginUser.PermissionUrl == null)
            {
                //查出用户所属角色的权限
                var role = new RoleBLL().GetModel(i => i.ID == LoginUser.RoleID);

                if (role != null)
                {
                    LoginUser.PermissionUrl = new ResourceBLL().GetPermissionURL(role.ResourceID);
                }

                LoginUser.PermissionUrl = LoginUser.PermissionUrl ?? "";

                //将url权限字符串加入到缓存中
                WebCache.SetPermissionUrlCache(LoginUser, LoginUser.PermissionUrl);
            }
            return LoginUser;
        }
        #endregion

        #region 2.0 当前登录用户是否拥有此URL的权限 + bool IsHasPermission(string url)
        /// <summary>
        /// 当前登录用户是否拥有此URL的权限
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool IsHasPermission(string url)
        {
            var loginUser = GetLoginUser();

            return loginUser.IsAdmin || loginUser.PermissionUrl.Contains(url.ToLower());
        }
        #endregion
    }
}