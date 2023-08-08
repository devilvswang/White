using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using White.Model;
using White.BLL;

namespace White.JX3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        #region 1.0 首页 + ActionResult Index()
        public ActionResult Index()
        {
            return View();
        }
        #endregion


        #region 2.0 Vue列表 + ActionResult VueList()
        public ActionResult VueList()
        {
            return View();
        }
        #endregion

        #region 2.1 列表 + JsonResult Get()
        [HttpGet]
        //列表
        public JsonResult Get()
        {
            var json = new JsonModel();

            json.Status = "success";
            json.Data = new UserBLL().GetList();

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 2.2 添加 + JsonResult Add(User model)
        [HttpPost]
        //添加
        public JsonResult Add(User model)
        {
            var json = new JsonModel();
            json.Status = "success";

            model.IsValid = true;
            model.CreateDate = DateTime.Now;

            new UserBLL().Add(model);
            new UserBLL().SaveChange();
            json.Data = new UserBLL().GetList();

            return Json(json);
        }
        #endregion

        #region 2.3 修改 + JsonResult Update(User model)
        [HttpPost]
        //修改
        public JsonResult Update(User model)
        {
            var json = new JsonModel();
            json.Status = "success";

            var userBLL = new UserBLL();

            var user = userBLL.GetModel(i => i.ID == model.ID);

            user.Name = model.Name;
            user.Tel = model.Tel;
            user.Mail = model.Mail;

            userBLL.SaveChange();

            json.Data = new UserBLL().GetList();

            return Json(json);
        }
        #endregion

        #region 2.4 删除 + JsonResult Delete(int id)
        [HttpPost]
        //删除
        public JsonResult Delete(int id)
        {
            var json = new JsonModel();
            json.Status = "success";

            var userBLL = new UserBLL();

            userBLL.Delete(new User { ID = id });

            json.Data = new UserBLL().GetList();

            return Json(json);
        }
        #endregion


        #region 3.0 测试Nginx + ActionResult Nginx()
        public ActionResult Nginx()
        {

            ViewBag.Data = new
            {

            }.ToExpando();

            return View();
        }
        #endregion
    }
}