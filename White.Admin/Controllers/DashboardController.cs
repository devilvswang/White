using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using White.BLL;
using White.Common;
using White.Model;

namespace White.Admin.Controllers
{
    public class DashboardController : PermissionController
    {
        #region 2.0 系统角色管理视图 + ActionResult Role()
        /// <summary>
        /// 系统角色管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Role()
        {
            return View(new RoleBLL().GetList(i => i.ID, true));
        }
        #endregion

        #region 2.1 添加/修改系统角色视图 + ActionResult RoleEdit(int? id)
        /// <summary>
        /// 添加/修改系统角色视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RoleEdit(int? id)
        {
            var role = new RoleBLL().GetModel(i => i.ID == id) ?? new Role();

            string resourceTree = "";
            if (!string.IsNullOrEmpty(role.ResourceID))
            {
                var resourceIdArray = role.ResourceID.Split(',');
                var resourceIdList = new List<int>();
                foreach (var resourceId in resourceIdArray)
                {
                    resourceIdList.Add(Convert.ToInt32(resourceId));
                }

                resourceTree = new ResourceBLL().GetTreeByEdit(resourceIdList);
            }
            else
            {
                resourceTree = new ResourceBLL().GetTreeByEdit(null);
            }

            ViewBag.Data = new
            {
                ResourceTree = resourceTree
            }.ToExpando();

            return View(role);
        }
        #endregion

        #region 2.2 添加/修改系统角色方法 + JsonResult RoleEdit(Role model)
        /// <summary>
        /// 添加/修改系统角色方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RoleEdit(Role model)
        {
            var json = new JsonModel();

            var roleBLL = new RoleBLL();

            model.ResourceID = Request.Form["RESOURCESID"];

            if (model.ID > 0)
            {

                model.UpdateDate = DateTime.Now;
                model.UpdateUserID = model.UpdateLoginID = LoginUser.ID;

                if (roleBLL.Update(model, "Name", "ResourceID", "UpdateDate", "UpdateUserID", "UpdateLoginID") > 0)
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
                model.CreateUserID = model.CreateLoginID = LoginUser.ID;

                if (roleBLL.Add(model) > 0)
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

        #region 2.3 删除系统角色方法 + JsonResult RoleDel(int id)
        /// <summary>
        /// 删除系统角色方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RoleDel(int id)
        {
            var json = new JsonModel();

            if (new RoleBLL().Delete(new Role() { ID = id }) > 0)
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


        #region 2.0 菜单管理视图 + ActionResult Resource()
        /// <summary>
        /// 菜单管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Resource()
        {
            ViewBag.Data = new
            {
                TreeHtml = new ResourceBLL().GetTree()
            }.ToExpando();

            return View();
        }
        #endregion

        #region 2.1 菜单添加/编辑视图 + ActionResult ResourceEdit(int? id)
        /// <summary>
        /// 菜单添加/编辑视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ResourceEdit(int? id)
        {
            var resouceBLL = new ResourceBLL();

            var model = resouceBLL.GetModel(i => i.ID == id) ?? new Resource();

            var parentName = "";

            if (id != null)
            {
                var parentResource = resouceBLL.GetModel(i => i.ID == model.ParentID);
                parentName = parentResource != null ? parentResource.Name : "无";
            }

            ViewBag.Data = new
            {
                ParentName = parentName
            }.ToExpando();

            return View(model);
        }
        #endregion

        #region 2.2 菜单添加/编辑方法 + JsonResult ResourceEdit(Resource model)
        /// <summary>
        /// 菜单添加/编辑方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ResourceEdit(Resource model)
        {
            var json = new JsonModel();
            var resourceBLL = new ResourceBLL();

            if (model.ID > 0)
            {

                model.UpdateDate = DateTime.Now;
                model.UpdateUserID = model.UpdateLoginID = LoginUser.ID;

                if (resourceBLL.Update(model, "ParentID", "Name", "Url", "IsMenu", "CSS", "UpdateDate", "UpdateUserID", "UpdateLoginID") > 0)
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
                model.CreateUserID = model.CreateLoginID = LoginUser.ID;

                if (resourceBLL.Add(model) > 0)
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

        #region 2.3 获取菜单树结构HTML方法 + JsonResult GetResourceTree()
        /// <summary>
        /// 获取菜单树结构HTML方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult GetResourceTree()
        {
            return Content(new ResourceBLL().GetChoseTree());
        }
        #endregion

        #region 2.4 获取排序功能菜单列表HTML方法 + ContentResult GetResourceOrder()
        /// <summary>
        /// 获取排序功能菜单列表HTML方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult GetResourceOrder()
        {
            return Content(new ResourceBLL().GetTreeByOrder());
        }
        #endregion

        #region 2.5 删除功能菜单方法 + JsonResult ResourceDel(int id)
        /// <summary>
        /// 删除功能菜单方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ResourceDel(int id)
        {
            var json = new JsonModel();

            var resourceBLL = new ResourceBLL();

            if (resourceBLL.GetCount(i => i.ParentID == id) > 0)
            {
                json.Message = "抱歉，当前菜单还有未删除的子级菜单，不能直接删除！";
                return Json(json);
            }

            if (resourceBLL.Delete(new Resource() { ID = id }) > 0)
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

        #region 2.6 执行排序功能菜单列表方法 + JsonResult ResourceOrder()
        /// <summary>
        /// 执行排序功能菜单列表方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ResourceOrder()
        {
            string resourcesid = Request.Form["resource"];
            string[] resourcesidArray = resourcesid.Split(',');

            //待修改排序菜单的List集合
            var resourceList = new List<Resource>();
            foreach (var resourceid in resourcesidArray)
            {
                int id = Convert.ToInt32(resourceid);
                int ordernum = Convert.ToInt16(Request.Form["order_" + id]);

                resourceList.Add(new Resource() { ID = id, OrderNum = ordernum });
            }

            JsonModel json = new JsonModel();
            if (new ResourceBLL().Update(resourceList, "OrderNum") > 0)
            {
                json.Status = "success";
                json.Message = "菜单排序成功，重新登录系统后生效！";
            }
            else
            {
                json.Status = "error";
                json.Message = "菜单排序失败，请稍后重试！";
            }

            return Json(json);
        }
        #endregion


        #region 3.0 用户管理视图 + ActionResult User()
        /// <summary>
        /// 用户管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult User(int? page, string keyword, bool? isAdmin, int? roleId)
        {
            PageIndex = page ?? 1;

            var predicate = PredicateBuilder.True<V_User_Info>().And(i => i.IsValid == true);

            #region 查询
            if (!string.IsNullOrEmpty(keyword))
            {
                predicate = predicate.And(i => i.Username.Contains(keyword) || i.Realname.Contains(keyword) || i.Mobile.Contains(keyword));
            }
            if (isAdmin != null)
            {
                predicate = predicate.And(i => i.IsAdmin == isAdmin);
            }
            if (roleId != null)
            {
                predicate = predicate.And(i => i.RoleID == roleId);
            }

            #endregion


            var userBLL = new V_User_InfoBLL();
            var list = userBLL.GetPagedList(PageIndex, PageSize, predicate, i => i.ID, false);
            var rowCount = userBLL.GetCount(predicate);
            var pageCount = Convert.ToInt32(Math.Ceiling(rowCount * 1.0 / PageSize));
            var pagedHtml = HtmlCommon.GetPagedHtml(PageIndex, pageCount, "",
                string.Format("&keyword={0}&isAdmin={1}&roleId={2}", keyword, isAdmin, roleId));

            ViewBag.Data = new
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                PageCount = pageCount,
                PagedHtml = pagedHtml,
                RowCount = rowCount,
                keyword = keyword,
                IsAdmin = isAdmin.ToString().ToLower(),
                RoleId = roleId,

                RoleList = new RoleBLL().GetList(i => i.ID, true)
            }.ToExpando();

            return View(list);
        }
        #endregion

        #region 3.1 添加/修改系统用户视图 + ActionResult UserEdit(int? id)
        /// <summary>
        /// 添加/修改系统用户视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserEdit(int? id)
        {
            var model = new User_InfoBLL().GetModel(i => i.ID == id) ?? new User_Info();

            ViewBag.Data = new
            {
                RoleList = new RoleBLL().GetList(i => i.ID, true)
            }.ToExpando();

            return View(model);
        }
        #endregion

        #region 3.2 添加/修改系统用户方法 + JsonResult UserEdit(User_Info model)
        /// <summary>
        /// 添加/修改系统用户方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UserEdit(User_Info model)
        {
            var json = new JsonModel();

            var userBLL = new User_InfoBLL();

            if (userBLL.GetCount(i => i.ID != model.ID && i.Username == model.Username.Trim() && i.IsValid == true) > 0)
            {
                json.Message = "抱歉，此用户名已被使用！";
                return Json(json);
            }

            if (model.ID > 0)
            {
                var user = userBLL.GetModel(i => i.ID == model.ID);

                user.BarePass = model.Password != "********" ? model.Password : user.BarePass;
                user.Password = model.Password != "********" ? SecurityHelper.GetMD5(model.Password) : user.Password;
                user.Realname = model.Realname;
                user.IsAdmin = model.Username.ToLower() == "admin" ? true : model.IsAdmin;
                user.RoleID = model.RoleID;
                user.Mobile = model.Mobile;
                user.UserAddress = model.UserAddress;
                user.UpdateDate = DateTime.Now;

                if (userBLL.SaveChange() > 0)
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
                model.Password = SecurityHelper.GetMD5(model.Password);
                model.IsValid = true;
                model.CreateDate = DateTime.Now;
                model.CreateUserID = LoginUser.ID;

                if (userBLL.Add(model) > 0)
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

        #region 3.3 删除系统用户方法 + JsonResult UserDel(int id)
        /// <summary>
        /// 删除系统用户方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UserDel(int id)
        {
            var json = new JsonModel();

            var userBLL = new User_InfoBLL();

            var user = userBLL.GetModel(i => i.ID == id);

            user.IsValid = false;

            if (userBLL.SaveChange() > 0)
            {
                json.Status = "success";
                json.Message = "删除成功！";
            }
            else
            {
                json.Message = "抱歉，操作失败，请稍后重试！";
            }

            return Json(json);
        }
        #endregion
    }
}