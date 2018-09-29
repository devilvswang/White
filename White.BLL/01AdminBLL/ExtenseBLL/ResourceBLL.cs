using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using White.DAL;
using White.Common;
using White.Model;

namespace White.BLL
{
    public partial class ResourceBLL
    {
        #region 1.0 生成功能菜单列表HTML + string GetTree()
        /// <summary>
        /// 生成功能菜单列表HTML
        /// </summary>
        /// <returns></returns>
        public string GetTree()
        {
            var list = GetParentList(null, false);

            var sb = new StringBuilder();

            foreach (var model in list)
            {
                sb.Append("<li>");

                //生成功能模块列表
                sb.AppendFormat(@"<div>
                                        <i class='{0}' title='点击折叠列表'></i>
                                        <span title='{1}'><i class='{4}'></i> {2}</span>
                                        <span><a href='/Dashboard/ResourceEdit/{3}'>编辑</a><a href='javascript:;' onclick='Del(this,{3})'>删除</a></span>
                                    </div>",
                                model.ChildrenResourceList != null && model.ChildrenResourceList.Count > 0 ? "fa fa-plus-square-o blue" : "fa fa-minus-square-o icon-hide",
                                model.Url, model.Name, model.ID,
                                model.IsMenu ? "fa fa-bars" : "fa fa-chain");

                if (model.ChildrenResourceList != null && model.ChildrenResourceList.Count > 0)
                {
                    GetTreeChildren(sb, model);
                }

                sb.Append("</li>");
            }

            return sb.ToString();
        }
        #endregion

        #region 1.1 生成子功能菜单列表HTML - void GetTreeChildren(StringBuilder sb, Resource model)
        /// <summary>
        /// 生成子功能菜单列表HTML
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="model"></param>
        private void GetTreeChildren(StringBuilder sb, Resource model)
        {
            sb.Append("<ul style='display:none'>");
            foreach (var children in model.ChildrenResourceList)
            {
                sb.Append("<li>");

                //生成子功能模块列表
                sb.AppendFormat(@"<div>
                                    <i class='{0}' title='点击折叠列表'></i>
                                    <span title='{1}'><i class='{4}'></i> {2}</span>
                                    <span><a href='/Dashboard/ResourceEdit/{3}'>编辑</a><a href='javascript:;' onclick='Del(this,{3})'>删除</a></span>
                                </div>",
                                children.ChildrenResourceList != null && children.ChildrenResourceList.Count > 0 ? "fa fa-plus-square-o blue" : "fa fa-minus-square-o icon-hide",
                                children.Url, children.Name, children.ID, children.IsMenu ? "fa fa-bars" : "fa fa-chain");

                if (children.ChildrenResourceList != null && children.ChildrenResourceList.Count > 0)
                {
                    GetTreeChildren(sb, children);
                }

                sb.Append("</li>");
            }
            sb.Append("</ul>");
        }
        #endregion


        #region 2.0 生成排序功能菜单列表HTML + string GetTreeByOrder()
        /// <summary>
        /// 生成排序功能菜单列表HTML
        /// </summary>
        /// <returns></returns>
        public string GetTreeByOrder()
        {
            StringBuilder sb = new StringBuilder();

            var list = GetParentList(null, false);

            int index = 1;
            foreach (var model in list)
            {
                sb.Append("<li>");

                //生成功能模块列表
                sb.AppendFormat(@"<div>
                                    <input type='hidden' name='resource' value='{0}'/>
                                    <input type='hidden' name='order_{0}' value=''/>
                                    <span title='{1}'>{2}. <i class='{4}'></i>{3}</span>
                                    <span><a href='javascript:;' onclick='setUp(this)'>上升</a><a href='javascript:;' onclick='setDown(this)'>下降</a></span>
                                </div>", model.ID, model.Url, index, model.Name, model.IsMenu ? "fa fa-bars" : "fa fa-chain");

                if (model.ChildrenResourceList != null && model.ChildrenResourceList.Count > 0)
                {
                    GetTreeChildrenByOrder(sb, model);
                }

                sb.Append("</li>");
                index++;
            }

            return sb.ToString();
        }
        #endregion

        #region 2.1 生成排序子功能菜单列表HTML - void GetTreeChildrenByOrder(StringBuilder sb, Resource model)
        /// <summary>
        /// 生成排序子功能菜单列表HTML
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="model"></param>
        private void GetTreeChildrenByOrder(StringBuilder sb, Resource model)
        {
            sb.Append("<ul>");

            int index = 1;

            foreach (var item in model.ChildrenResourceList)
            {
                sb.Append("<li>");

                //生成子功能模块列表
                sb.AppendFormat(@"<div>
                                        <input type='hidden' name='resource' value='{0}'/>
                                        <input type='hidden' name='order_{0}' value=''/>
                                        <span title='{1}'>{2}. <i class='{4}'></i>{3}</span>
                                        <span><a href='javascript:;' onclick='setUp(this)'>上升</a><a href='javascript:;' onclick='setDown(this)'>下降</a></span>
                                    </div>", item.ID, item.Url, index, item.Name, item.IsMenu ? "fa fa-bars" : "fa fa-chain");

                if (item.ChildrenResourceList != null && item.ChildrenResourceList.Count > 0)
                {
                    GetTreeChildrenByOrder(sb, item);
                }

                sb.Append("</li>");
                index++;
            }
            sb.Append("</ul>");
        }
        #endregion


        #region 3.0 生成选择菜单列表HTML + string GetChoseTree()
        /// <summary>
        /// 生成选择菜单列表HTML
        /// </summary>
        /// <returns></returns>
        public string GetChoseTree()
        {
            StringBuilder sb = new StringBuilder();

            var list = GetParentList(null, false);

            sb.Append("<input type='radio' id='ResourceID_0' name='RESOURCEID' value=''/> <label for='ResourceID_0'>一级菜单</label>");

            foreach (var model in list)
            {
                sb.Append("<li>");

                sb.AppendFormat("<input type='radio' id='ResourceID_{0}' name='RESOURCEID' value='{0}'/> <label for='ResourceID_{0}'><i class='{2}'></i> {1}</label>",
                        model.ID,
                        model.Name,
                        model.IsMenu ? "fa fa-bars" : "fa fa-chain");

                if (model.ChildrenResourceList != null && model.ChildrenResourceList.Count > 0)
                    GetChildrenChoseTree(sb, model);

                sb.Append("</li>");

            }

            return sb.ToString();
        }
        #endregion

        #region 3.1 生成选择子菜单列表HTML - void GetChildrenChoseTree(StringBuilder sb, Resource model)
        /// <summary>
        /// 生成选择子菜单列表HTML
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="model"></param>
        private void GetChildrenChoseTree(StringBuilder sb, Resource model)
        {
            sb.Append("<ul>");
            foreach (var item in model.ChildrenResourceList)
            {
                sb.Append("<li>");

                sb.AppendFormat("<input type='radio' id='ResourceID_{0}' name='RESOURCEID' value='{0}'/> <label for='ResourceID_{0}'><i class='{3}'></i> {2}</label>",
                        item.ID,
                        item.ParentID,
                        item.Name,
                        item.IsMenu ? "fa fa-bars" : "fa fa-chain");

                if (item.ChildrenResourceList != null && item.ChildrenResourceList.Count > 0)
                    GetChildrenChoseTree(sb, item);

                sb.Append("</li>");
            }
            sb.Append("</ul>");
        }
        #endregion


        #region 4.0 生成功能菜单编辑复选框HTML + string GetTreeByEdit(List<int> resourceIdList)
        /// <summary>
        /// 生成功能菜单编辑复选框HTML
        /// </summary>
        /// <param name="resourceIdList"></param>
        /// <returns></returns>
        public string GetTreeByEdit(List<int> resourceIdList)
        {
            var sb = new StringBuilder();

            var list = GetParentList(null, false);

            foreach (var model in list)
            {
                sb.Append("<li>");
                //生成可勾选权限树
                if (resourceIdList != null && resourceIdList.Count > 0)
                {
                    var hasPermission = resourceIdList.Contains(model.ID);
                    sb.AppendFormat(@"<label class='mt-checkbox mt-checkbox-outline'> 
                                        <input type='checkbox' id='ResourceID_{0}' name='RESOURCESID' value='{0}' {1}/> 
                                        <div class='{2}'><i class='{3}'></i> {4}</div> 
                                        <span></span>
                                    </label>",
                            model.ID,
                            hasPermission ? "checked" : "",
                            hasPermission ? "highlight" : "",
                            model.IsMenu ? "fa fa-bars" : "fa fa-chain",
                            model.Name);
                }
                else
                {
                    sb.AppendFormat(@"<label class='mt-checkbox mt-checkbox-outline'>
                                        <input type='checkbox' id='ResourceID_{0}' name='RESOURCESID' value='{0}'/> 
                                        <div class=''><i class='{2}'></i> {1}</div>
                                        <span></span>
                                    </label>",
                            model.ID,
                            model.Name,
                            model.IsMenu ? "fa fa-bars" : "fa fa-chain");
                }
                if (model.ChildrenResourceList != null && model.ChildrenResourceList.Count > 0)
                    GetTreeChildrenByEdit(sb, model, resourceIdList);

                sb.Append("</li>");
            }

            return sb.ToString();
        }
        #endregion

        #region 4.1 生成子功能菜单编辑复选框HTML - void GetTreeChildrenByEdit(StringBuilder sb, Resource model, List<int> resourceIdList)
        /// <summary>
        /// 生成子功能菜单编辑复选框HTML
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="model"></param>
        /// <param name="resourceIdList"></param>
        private void GetTreeChildrenByEdit(StringBuilder sb, Resource model, List<int> resourceIdList)
        {
            sb.Append("<ul>");
            foreach (var item in model.ChildrenResourceList)
            {
                sb.Append("<li>");
                //生成可勾选权限树
                if (resourceIdList != null && resourceIdList.Count > 0)
                {
                    var hasPermission = resourceIdList.Contains(item.ID);
                    sb.AppendFormat(@"<label class='mt-checkbox mt-checkbox-outline'> 
                                        <input type='checkbox' id='ResourceID_{0}' name='RESOURCESID' parent-id='ResourceID_{1}' value='{0}' {2}/> 
                                        <div class='{3}'><i class='{4}'></i> {5}</div> 
                                        <span></span>
                                    </label>",
                            item.ID,
                            item.ParentID,
                            hasPermission ? "checked" : "",
                            hasPermission ? "highlight" : "",
                            item.IsMenu ? "fa fa-bars" : "fa fa-chain",
                            item.Name);
                }
                else
                {
                    sb.AppendFormat(@"<label class='mt-checkbox mt-checkbox-outline'>
                                        <input type='checkbox' id='ResourceID_{0}' name='RESOURCESID' parent-id='ResourceID_{1}' value='{0}'/> 
                                        <div class=''><i class='{3}'></i> {2}</div>
                                        <span></span>
                                    </label>",
                           item.ID,
                           item.ParentID,
                           item.Name,
                           item.IsMenu ? "fa fa-bars" : "fa fa-chain");
                }
                if (item.ChildrenResourceList != null && item.ChildrenResourceList.Count > 0)
                    GetTreeChildrenByEdit(sb, item, resourceIdList);

                sb.Append("</li>");
            }
            sb.Append("</ul>");
        }
        #endregion


        #region 5.0 【已弃用】生成上侧导航功能菜单HTML + string GetTopTreeMenu(List<int> resourceIdList)
        /// <summary>
        /// 【已弃用】生成上侧导航功能菜单HTML
        /// </summary>
        /// <param name="resourceIdList"></param>
        /// <returns></returns>
        public string GetTopTreeMenu(List<int> resourceIdList)
        {
            var predicate = PredicateBuilder.True<Resource>().And(i => i.ParentID == null);

            if (resourceIdList != null && resourceIdList.Count > 0)
            {
                predicate = predicate.And(i => resourceIdList.Contains(i.ID));
            }

            var sb = new StringBuilder();

            var list = dal.GetList(predicate, i => i.OrderNum, true);

            foreach (var model in list)
            {
                sb.Append("<li>");
                sb.AppendFormat("<a href='{0}' data-toggle='ParentId_{1}'><i class='{2}'></i> {3} <span class=''></span></a>",
                                model.Url != null ? model.Url : "javascript:;", model.ID, model.CSS, model.Name);
                sb.Append("</li>");
            }

            return sb.ToString();
        }
        #endregion


        #region 5.3 生成左侧导航功能菜单HTML + string GetLeftTreeMenu(List<int> resourceIdList)
        /// <summary>
        /// 生成左侧导航功能菜单HTML
        /// </summary>
        /// <param name="resourceIdList"></param>
        /// <returns></returns>
        public string GetLeftTreeMenu(List<int> resourceIdList)
        {
            var sb = new StringBuilder();

            var list = GetParentList(resourceIdList, true);

            int index = 0;

            foreach (var model in list)
            {
                bool isHasChildren = model.ChildrenResourceList.Count > 0;

                sb.AppendFormat("<li class='{0}'>", isHasChildren ? "dropdown" : "");

                sb.AppendFormat(@"<a href='{0}'>
                                    <i class='{1}' style='font-size:30px;'></i>
                                    <span class='menu-item'>{2}</span>
                                </a>",
                                model.Url ?? "javascript:;",
                                model.CSS,
                                model.Name);

                if (model.ChildrenResourceList != null && model.ChildrenResourceList.Count > 0)
                {
                    GetLeftTreeMenuChildren(sb, model);
                }
                sb.Append("</li>");
            }

            return sb.ToString();
        }
        #endregion

        #region 5.4 生成左侧子导航功能菜单HTML - void GetLeftTreeMenuChildren(StringBuilder sb, Resource resource)
        /// <summary>
        /// 生成左侧子导航功能菜单HTML
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="resource"></param>
        private void GetLeftTreeMenuChildren(StringBuilder sb, Resource resource)
        {
            sb.Append("<ul class='list-unstyled menu-item'>");

            foreach (var item in resource.ChildrenResourceList)
            {
                sb.Append("<li>");

                bool isHasChildren = item.ChildrenResourceList.Count > 0;

                sb.AppendFormat("<a href='{0}'>{1}</a>",
                                    item.Url ?? "javascript:;", item.Name);

                if (item.ChildrenResourceList != null && item.ChildrenResourceList.Count > 0)
                {
                    GetLeftTreeMenuChildren(sb, item);
                }

                sb.Append("</li>");
            }
            sb.Append("</ul>");
        }
        #endregion


        //-------------------------------查询数据库-------------------------------

        #region 0.0 获取功能菜单集合 + List<Resource> GetParentList(List<int> resourceIdList, bool isMenu)
        /// <summary>
        /// 获取功能菜单集合
        /// </summary>
        /// <param name="resourceIdList"></param>
        /// <param name="isMenu"></param>
        /// <returns></returns>
        private List<Resource> GetParentList(List<int> resourceIdList, bool isMenu)
        {
            var parentPredicate = PredicateBuilder.True<Resource>().And(i => i.ParentID == null);
            var childrenPredicate = PredicateBuilder.True<Resource>().And(i => i.ParentID != null);

            if (resourceIdList != null && resourceIdList.Count > 0)
            {
                parentPredicate = parentPredicate.And(i => resourceIdList.Contains(i.ID));
                childrenPredicate = childrenPredicate.And(i => resourceIdList.Contains(i.ID));
            }
            if (isMenu)
            {
                parentPredicate = parentPredicate.And(i => i.IsMenu == true);
                childrenPredicate = childrenPredicate.And(i => i.IsMenu == true);
            }

            var parentList = dal.GetList(parentPredicate, i => i.OrderNum, true);
            var childrenList = dal.GetList(childrenPredicate, i => i.OrderNum, true);

            var childrenDict = new Dictionary<int, List<Resource>>();

            foreach (var children in childrenList)
            {
                if (childrenDict.ContainsKey((int)children.ParentID))
                {
                    childrenDict[(int)children.ParentID].Add(children);
                }
                else
                {
                    childrenDict[(int)children.ParentID] = new List<Resource>() { children };
                }
            }

            foreach (var model in parentList)
            {
                GetChildrenList(model, childrenDict, resourceIdList, isMenu);
            }

            return parentList;
        }
        #endregion

        #region 0.1 递归出子功能菜单集合 + void GetChildrenList(Sys_Resource model, Dictionary<int, List<Sys_Resource>> childrenDict, List<int> resourceIdList, bool isMenu)
        /// <summary>
        /// 递归出子功能菜单集合
        /// </summary>
        /// <param name="model"></param>
        /// <param name="childrenDict"></param>
        /// <param name="resourceIdList"></param>
        /// <param name="isMenu"></param>
        private void GetChildrenList(Resource model, Dictionary<int, List<Resource>> childrenDict, List<int> resourceIdList, bool isMenu)
        {

            model.ChildrenResourceList = childrenDict.ContainsKey(model.ID) ? childrenDict[model.ID] : new List<Resource>();

            if (model.ChildrenResourceList.Count > 0)
            {
                foreach (var children in model.ChildrenResourceList)
                {
                    GetChildrenList(children, childrenDict, resourceIdList, isMenu);
                }
            }
        }
        #endregion

        #region 0.2 根据指定权限，获取有权限的URL字符串以','分隔 + string GetPermissionURL(string resourceId)
        /// <summary>
        /// 根据指定权限，获取有权限的URL字符串以','分隔
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public string GetPermissionURL(string resourceId)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(resourceId))
            {
                var resourceList = dal.ExecuteSqlList("select * from Resource where ID in (" + resourceId.Trim(',') + ")");

                foreach (var resource in resourceList)
                {
                    sb.Append(resource.Url + ",");
                }
            }

            return sb.ToString().Trim(',').ToLower();
        }
        #endregion


    }
}
