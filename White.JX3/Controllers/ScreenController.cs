using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using White.Base;
using White.BLL;
using White.Common;
using White.Model;

namespace White.JX3.Controllers
{
    public class ScreenController : BaseController
    {
        #region 1.0 截图展示页面视图 + ActionResult Index()
        /// <summary>
        /// 截图展示页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PageIndex = 1;
            PageSize = 6;

            var predicate = PredicateBuilder.True<ScreenShot>().And(i => i.IsValid == true);

            var BLL = new ScreenShotBLL();
            var list = BLL.GetPagedList(PageIndex, PageSize, predicate, i => i.ID, false);
            var rowCount = BLL.GetCount(predicate); ;
            var pageCount = Convert.ToInt32(Math.Ceiling(rowCount * 1.0 / PageSize));

            ViewBag.Data = new
            {
                PageCount = pageCount
            }.ToExpando();

            return View(list);
        }
        #endregion

        #region 1.1 获取图片分页列表 + JsonResult GetPagedPicList(int page)
        /// <summary>
        /// 获取图片分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public JsonResult GetPagedPicList(int page)
        {
            PageIndex = page;
            PageSize = 6;

            var json = new JsonModel();
            var predicate = PredicateBuilder.True<ScreenShot>().And(i => i.IsValid == true);

            var BLL = new ScreenShotBLL();
            var list = BLL.GetPagedList(PageIndex, PageSize, predicate, i => i.ID, false);

            json.Status = "success";
            json.Data = list;

            return Json(json);
        }
        #endregion
    }
}