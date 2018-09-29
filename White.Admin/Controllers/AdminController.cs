using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using White.Base;
using White.BLL;
using White.Common;
using White.Model;

namespace White.Admin.Controllers
{
    public class AdminController : Controller
    {
        protected User_Info LoginUser { get { return new PermissionController().LoginUser; } }


        #region 1.0 用户登录视图 + ActionResult Login()
        /// <summary>
        /// 用户登录视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        #endregion

        #region 1.1 用户登录方法 + JsonResult Login(string username, string password)
        /// <summary>
        /// 用户登录方法
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            JsonModel json = new JsonModel();

            password = SecurityHelper.GetMD5(password);

            var userBLL = new User_InfoBLL();
            var loginUser = userBLL.GetModel(i => i.Username == username && i.Password == password && i.IsValid == true);

            if (loginUser != null)
            {
                Session["LoginUser"] = loginUser;
                HttpCookie cookie = new HttpCookie("LoginUserID");
                cookie.Value = SecurityHelper.EncryptTicketString(loginUser.ID.ToString(), DateTime.Now.AddDays(1));
                Response.Cookies.Add(cookie);

                loginUser.LastLoginDate = DateTime.Now;
                userBLL.SaveChange();

                json.Status = "success";
                json.Message = "登录成功！";

                var role = new RoleBLL().GetModel(i => i.ID == LoginUser.RoleID);
                if (role != null)
                {
                    var resource = new ResourceBLL().GetModel(i => role.ResourceID.Contains(i.ID.ToString()) && i.IsMenu == true && i.Url != null, i => i.ID, true);
                    json.Data = resource != null ? resource.Url : "/Home/Index";
                }
                else
                {
                    json.Data = "/Home/Index";
                }
            }
            else
            {
                json.Message = "用户名或密码错误！";
            }

            return Json(json);
        }
        #endregion

        #region 2.0 错误页视图 + ActionResult Error()
        /// <summary>
        /// 错误页视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        }
        #endregion 



        #region 以下Action需要登录权限

        #region 1.0 设置背景Cooike方法 + ActionResult SetSkin()
        /// <summary>
        /// 设置背景Cooike方法
        /// </summary>
        [AdminActionFilter]
        [HttpPost]
        public ActionResult SetSkin(string skin)
        {
            HttpCookie cookie = new HttpCookie("SkinClass");
            cookie.Value = skin;
            Response.Cookies.Add(cookie);

            return Json("");
        }
        #endregion

        #region 1.1 公共部分视图 + ActionResult CommonBar()
        /// <summary>
        /// 公共部分视图
        /// </summary>
        /// <returns></returns>
        [AdminActionFilter]
        public ActionResult CommonBar()
        {

            ViewBag.Skin = "skin-blur-kiwi";

            return View();
        }
        #endregion


        #region 2.0 获取左侧导航树HTML方法 + ActionResult LeftTreeMenu()
        /// <summary>
        /// 获取左侧导航树HTML方法
        /// </summary>
        /// <returns></returns>
        [AdminActionFilter]
        public ActionResult LeftTreeMenu()
        {
            var leftTreeMenuHtml = WebCache.GetLeftMenuCache(LoginUser);

            if (leftTreeMenuHtml == null)
            {
                if (LoginUser.IsAdmin)
                {
                    leftTreeMenuHtml = new ResourceBLL().GetLeftTreeMenu(null);
                }
                else
                {
                    var role = new RoleBLL().GetModel(i => i.ID == LoginUser.RoleID);
                    if (role != null && !string.IsNullOrEmpty(role.ResourceID))
                    {
                        var resourceIdArray = role.ResourceID.Split(',');
                        var resourceIdList = new List<int>();
                        foreach (var resourceId in resourceIdArray)
                        {
                            resourceIdList.Add(Convert.ToInt32(resourceId));
                        }

                        leftTreeMenuHtml = new ResourceBLL().GetLeftTreeMenu(resourceIdList);
                    }
                }
                WebCache.SetLeftMenuCache(LoginUser, leftTreeMenuHtml ?? "");
            }

            return Content(leftTreeMenuHtml);
        }
        #endregion


        #region 3.0 修改密码视图 + ActionResult EditPwd()
        /// <summary>
        /// 修改密码视图
        /// </summary>
        /// <returns></returns>
        [AdminActionFilter]
        public ActionResult EditPwd()
        {
            return View();
        }
        #endregion

        #region 3.1 修改密码方法 + JsonResult EditPwd(string oldPassword, string newPassword)
        /// <summary>
        /// 修改密码方法
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminActionFilter]
        public JsonResult EditPwd(string oldPassword, string newPassword)
        {
            JsonModel json = new JsonModel();

            oldPassword = SecurityHelper.GetMD5(oldPassword);
            newPassword = SecurityHelper.GetMD5(newPassword);

            if (LoginUser.Password != oldPassword)
            {
                json.Message = "抱歉，原密码输入不正确！";
                return Json(json);
            }

            var userBLL = new User_InfoBLL();
            var user = userBLL.GetModel(i => i.ID == LoginUser.ID);
            user.Password = newPassword;
            userBLL.SaveChange();

            json.Status = "success";
            json.Message = "密码修改成功，请重新登录！";

            Session["LoginUser"] = null;
            HttpCookie cookie = new HttpCookie("LoginUserID");
            cookie.Value = "";
            Response.Cookies.Add(cookie);

            return Json(json);
        }
        #endregion


        #region 4.0 退出登录方法 + ActionResult Logout()
        /// <summary>
        /// 退出登录方法
        /// </summary>
        [AdminActionFilter]
        public ActionResult Logout()
        {
            //清除权限URL缓存
            WebCache.RemovePermissionUrlCache(LoginUser);
            //清除导航树缓存
            WebCache.RemoveTopMenuCache(LoginUser);
            WebCache.RemoveLeftMenuCache(LoginUser);

            Session["LoginUser"] = null;
            HttpCookie cookie = new HttpCookie("LoginUserID");
            cookie.Value = "";
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);

            //HttpCookie skin = new HttpCookie("SkinClass");
            //skin.Value = "";
            //skin.Expires = DateTime.Now.AddDays(-1);
            //Response.Cookies.Add(skin);

            return Redirect("/Admin/Login");
        }
        #endregion


        #region 5.0 无权限操作视图 + ActionResult NotPermission()
        /// <summary>
        /// 无权限操作视图
        /// </summary>
        /// <returns></returns>
        [AdminActionFilter]
        public ActionResult NotPermission()
        {
            return View();
        }
        #endregion

        #endregion
    }
}