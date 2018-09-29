using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using White.Model;
using White.BLL;

namespace White.JX3.Controllers
{
    public class RelationController : Controller
    {

        #region 1.0 师门关系视图 + ActionResult Index()
        /// <summary>
        /// 师门关系视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 1.1 师门关系树状图数据获取方法 + JsonResult GetTreeData()
        /// <summary>
        /// 师门关系树状图数据获取方法
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTreeData()
        {
            var json = new JsonModel();
            var relationBLL = new RelationBLL();

            json.Status = "success";
            json.Data = relationBLL.GetTreeJson();


            return Json(json);
        }
        #endregion
    }
}