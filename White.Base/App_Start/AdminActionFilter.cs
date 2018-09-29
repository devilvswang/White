using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using White.BLL;
using White.Common;
using White.Model;

namespace White.Base
{
    public class AdminActionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var LoginUser = filterContext.HttpContext.Session["LoginUser"] as User_Info;

            if (LoginUser == null)
            {
                HttpCookie cookie = filterContext.HttpContext.Request.Cookies["LoginUserID"];
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    //票据对象加密后的用户ID
                    string encryptUserId = SecurityHelper.DecryptTicketString(cookie.Value);

                    if (!string.IsNullOrEmpty(encryptUserId))
                    {
                        int loginUserId = Convert.ToInt32(encryptUserId);

                        LoginUser = new User_InfoBLL().GetModel(u => u.ID == loginUserId);

                        filterContext.HttpContext.Session["LoginUser"] = LoginUser;
                    }
                }
            }

            if (LoginUser == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Login");
            }
        }
    }


    /// <summary>
    /// 程序运行出错过滤器
    /// </summary>
    public class MyHandleErrorAttribute : HandleErrorAttribute
    {
        #region 程序运行出错过滤方法，将错误信息保存到数据库中 +void OnException(ExceptionContext filterContext)
        /// <summary>
        /// 程序运行出错过滤方法，将错误信息保存到数据库中
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            var loginUser = filterContext.HttpContext.Session["LoginUser"] as User_Info;

            new White.BLL.Error_LogBLL().Add(new Error_Log()
            {
                Url = HttpContext.Current.Request.Url.ToString(),
                HttpMethod = HttpContext.Current.Request.HttpMethod,
                Params = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Form.ToString()),
                Message = filterContext.Exception.ToString(),
                IsRead = false,
                CreateUserID = loginUser != null ? loginUser.ID : 0,
                CreateUserName = loginUser != null ? loginUser.Realname : "",
                CreateDate = DateTime.Now
            });

            base.OnException(filterContext);
        }
        #endregion
    }


    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class PermissionActionFilter : ActionFilterAttribute
    {
        #region 权限过滤方法 + void OnActionExecuting(ActionExecutingContext filterContext)
        /// <summary>
        /// 权限过滤方法
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var LoginUser = filterContext.HttpContext.Session["LoginUser"] as User_Info;

            if (LoginUser.IsAdmin)
            {
                return;  //管理员拥有所有权限
            }

            var hasPermissionUrl = WebCache.GetPermissionUrlCache(LoginUser);

            if (hasPermissionUrl == null)
            {
                //查出用户所属角色的权限
                var role = new RoleBLL().GetModel(i => i.ID == LoginUser.RoleID);

                if (role != null)
                {
                    hasPermissionUrl = new ResourceBLL().GetPermissionURL(role.ResourceID);
                }

                hasPermissionUrl = hasPermissionUrl ?? "";

                //将url权限字符串加入到缓存中
                WebCache.SetPermissionUrlCache(LoginUser, hasPermissionUrl);
            }

            string requestUrl = ("/" + filterContext.RouteData.Values["controller"] + "/" + filterContext.RouteData.Values["action"]).ToLower();

            if (!hasPermissionUrl.Contains(requestUrl))
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    JsonResult jsonResult = new JsonResult();
                    jsonResult.Data = new JsonModel() { Status = "error", Message = "抱歉，您貌似没有此操作的权限！" };
                    filterContext.Result = jsonResult;
                    return;
                }

                filterContext.Result = new RedirectResult("/Admin/NotPermission");
            }
        }
        #endregion
    }


    public class ActionLogActionFilter : ActionFilterAttribute
    {
        #region 记录用户操作日志方法，只针对POST请求，将数据库保存至数据库中 + override void OnActionExecuting(ActionExecutingContext filterContext)
        /// <summary>
        /// 记录用户操作日志方法，只针对POST请求，将数据库保存至数据库中
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var loginUser = filterContext.HttpContext.Session["LoginUser"] as User_Info;

            if (filterContext.HttpContext.Request.HttpMethod == "POST" || filterContext.HttpContext.Request.IsAjaxRequest())
            {
                new Action_LogBLL().Add(new Action_Log()
                {
                    Url = HttpContext.Current.Request.Url.ToString(),
                    Params = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Form.ToString()),
                    IP = HttpContext.Current.Request.UserHostAddress,
                    UserAgent = HttpContext.Current.Request.UserAgent,
                    CreateDate = DateTime.Now,
                    CreateUserID = loginUser.ID,
                    CreateUserName = loginUser.Realname
                });
            }
        }
        #endregion
    }
}
