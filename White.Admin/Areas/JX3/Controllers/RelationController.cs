using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using White.Admin.Controllers;
using White.Model;
using White.BLL;

namespace White.Admin.Areas.JX3.Controllers
{
    public class RelationController : PermissionController
    {
        #region 1.0 关系管理视图 + ActionResult Index()
        /// <summary>
        /// 关系管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Data = new
            {
                TreeHtml = new RelationBLL().GetTree()
            }.ToExpando();

            return View();
        }
        #endregion

        #region 1.1 关系添加/编辑视图 + ActionResult Edit(int? id)
        /// <summary>
        /// 关系添加/编辑视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            var relationBLL = new RelationBLL();

            var model = relationBLL.GetModel(i => i.ID == id) ?? new Relation();

            var parentName = "";

            if (id != null)
            {
                var parentRelation = relationBLL.GetModel(i => i.ID == model.ParentID);
                parentName = parentRelation != null ? parentRelation.Name : "无";
            }

            ViewBag.Data = new
            {
                ParentName = parentName
            }.ToExpando();

            return View(model);
        }
        #endregion

        #region 1.2 关系添加/编辑方法 + JsonResult Edit(Relation model)
        /// <summary>
        /// 关系添加/编辑方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(Relation model)
        {
            var json = new JsonModel();
            var relationBLL = new RelationBLL();

            if (model.ID > 0)
            {

                model.UpdateDate = DateTime.Now;
                model.UpdateUserID = LoginUser.ID;

                if (relationBLL.Update(model, "ParentID", "Name", "Describe", "UpdateDate", "UpdateUserID") > 0)
                {
                    json.Status = "success";
                    json.Message = "修改成功！";
                }
                else
                {
                    json.Message = "抱歉，修改失败，请稍后重试！";
                }
            }
            else
            {
                model.CreateDate = DateTime.Now;
                model.CreateUserID = LoginUser.ID;

                if (relationBLL.Add(model) > 0)
                {
                    json.Status = "success";
                    json.Message = "添加成功！";
                }
                else
                {
                    json.Message = "抱歉，添加失败，请稍后重试！";
                }
            }


            return Json(json);
        }
        #endregion

        #region 1.3 获取关系树结构HTML方法 + JsonResult GetTree()
        /// <summary>
        /// 获取关系树结构HTML方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult GetTree()
        {
            return Content(new RelationBLL().GetChoseTree());
        }
        #endregion

        #region 1.4 获取排序关系列表HTML方法 + ContentResult GetOrder()
        /// <summary>
        /// 获取排序关系列表HTML方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult GetOrder()
        {
            return Content(new RelationBLL().GetTreeByOrder());
        }
        #endregion

        #region 1.5 删除关系方法 + JsonResult Del(int id)
        /// <summary>
        /// 删除关系方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Del(int id)
        {
            var json = new JsonModel();

            var relationBLL = new RelationBLL();

            if (relationBLL.GetCount(i => i.ParentID == id) > 0)
            {
                json.Message = "抱歉，当前关系还有未删除的子级人员，不能直接删除！";
                return Json(json);
            }

            if (relationBLL.Delete(new Relation() { ID = id }) > 0)
            {
                json.Status = "success";
                json.Message = "删除成功！";
            }
            else
            {
                json.Message = "抱歉，删除失败，请稍后重试！";
            }

            return Json(json);
        }
        #endregion

        #region 1.6 执行排序关系列表方法 + JsonResult Order()
        /// <summary>
        /// 执行排序关系列表方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Order()
        {
            string relationsid = Request.Form["relation"];
            string[] relationsidArray = relationsid.Split(',');

            //待修改排序关系的List集合
            var relationList = new List<Relation>();
            foreach (var relationid in relationsidArray)
            {
                int id = Convert.ToInt32(relationid);
                int ordernum = Convert.ToInt16(Request.Form["order_" + id]);

                relationList.Add(new Relation() { ID = id, OrderNum = ordernum });
            }

            JsonModel json = new JsonModel();
            if (new RelationBLL().Update(relationList, "OrderNum") > 0)
            {
                json.Status = "success";
                json.Message = "人员排序成功！";
            }
            else
            {
                json.Status = "error";
                json.Message = "人员排序失败，请稍后重试！";
            }

            return Json(json);
        }
        #endregion
    }
}