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
    public partial class RelationBLL
    {
        public List<RelationTree> relationJson = new List<RelationTree>();

        #region 1.0 生成关系列表HTML + string GetTree()
        /// <summary>
        /// 生成关系列表HTML
        /// </summary>
        /// <returns></returns>
        public string GetTree()
        {
            var list = GetParentList();

            var sb = new StringBuilder();

            foreach (var model in list)
            {
                sb.Append("<li>");

                //生成关系列表
                sb.AppendFormat(@"<div>
                                        <i class='{0}' title='点击折叠列表'></i>
                                        <span title='{1}'><i class='{4}'></i> {2}</span>
                                        <span><a href='/JX3/Relation/Edit/{3}'>编辑</a><a href='javascript:;' onclick='Del(this,{3})'>删除</a></span>
                                    </div>",
                                model.ChildrenRelationList != null && model.ChildrenRelationList.Count > 0 ? "fa fa-plus-square-o blue" : "fa fa-minus-square-o icon-hide",
                                "", model.Name, model.ID,
                                "");

                if (model.ChildrenRelationList != null && model.ChildrenRelationList.Count > 0)
                {
                    GetTreeChildren(sb, model);
                }

                sb.Append("</li>");
            }

            return sb.ToString();
        }
        #endregion

        #region 1.1 生成子关系列表HTML - void GetTreeChildren(StringBuilder sb, Relation model)
        /// <summary>
        /// 生成子关系列表HTML
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="model"></param>
        private void GetTreeChildren(StringBuilder sb, Relation model)
        {
            sb.Append("<ul style='display:none'>");
            foreach (var children in model.ChildrenRelationList)
            {
                sb.Append("<li>");

                //生成子关系列表
                sb.AppendFormat(@"<div>
                                    <i class='{0}' title='点击折叠列表'></i>
                                    <span title='{1}'><i class='{4}'></i> {2}</span>
                                    <span><a href='/JX3/Relation/Edit/{3}'>编辑</a><a href='javascript:;' onclick='Del(this,{3})'>删除</a></span>
                                </div>",
                                children.ChildrenRelationList != null && children.ChildrenRelationList.Count > 0 ? "fa fa-plus-square-o blue" : "fa fa-minus-square-o icon-hide",
                                "", children.Name, children.ID, "");

                if (children.ChildrenRelationList != null && children.ChildrenRelationList.Count > 0)
                {
                    GetTreeChildren(sb, children);
                }

                sb.Append("</li>");
            }
            sb.Append("</ul>");
        }
        #endregion


        #region 2.0 生成排序关系列表HTML + string GetTreeByOrder()
        /// <summary>
        /// 生成排序关系列表HTML
        /// </summary>
        /// <returns></returns>
        public string GetTreeByOrder()
        {
            StringBuilder sb = new StringBuilder();

            var list = GetParentList();

            int index = 1;
            foreach (var model in list)
            {
                sb.Append("<li>");

                //生成功能模块列表
                sb.AppendFormat(@"<div>
                                    <input type='hidden' name='relation' value='{0}'/>
                                    <input type='hidden' name='order_{0}' value=''/>
                                    <span title='{1}'>{2}. <i class='{4}'></i>{3}</span>
                                    <span><a href='javascript:;' onclick='setUp(this)'>上升</a><a href='javascript:;' onclick='setDown(this)'>下降</a></span>
                                </div>", model.ID, "", index, model.Name, "");

                if (model.ChildrenRelationList != null && model.ChildrenRelationList.Count > 0)
                {
                    GetTreeChildrenByOrder(sb, model);
                }

                sb.Append("</li>");
                index++;
            }

            return sb.ToString();
        }
        #endregion

        #region 2.1 生成排序子关系列表HTML - void GetTreeChildrenByOrder(StringBuilder sb, Relation model)
        /// <summary>
        /// 生成排序子关系列表HTML
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="model"></param>
        private void GetTreeChildrenByOrder(StringBuilder sb, Relation model)
        {
            sb.Append("<ul>");

            int index = 1;

            foreach (var item in model.ChildrenRelationList)
            {
                sb.Append("<li>");

                //生成子关系列表
                sb.AppendFormat(@"<div>
                                        <input type='hidden' name='relation' value='{0}'/>
                                        <input type='hidden' name='order_{0}' value=''/>
                                        <span title='{1}'>{2}. <i class='{4}'></i>{3}</span>
                                        <span><a href='javascript:;' onclick='setUp(this)'>上升</a><a href='javascript:;' onclick='setDown(this)'>下降</a></span>
                                    </div>", item.ID, "", index, item.Name, "");

                if (item.ChildrenRelationList != null && item.ChildrenRelationList.Count > 0)
                {
                    GetTreeChildrenByOrder(sb, item);
                }

                sb.Append("</li>");
                index++;
            }
            sb.Append("</ul>");
        }
        #endregion


        #region 3.0 生成选择人员列表HTML + string GetChoseTree()
        /// <summary>
        /// 生成选择人员列表HTML
        /// </summary>
        /// <returns></returns>
        public string GetChoseTree()
        {
            StringBuilder sb = new StringBuilder();

            var list = GetParentList();

            sb.Append("<input type='radio' id='RelationID_0' name='RELATIONID' value=''/> <label for='RelationID_0'>创始人</label>");

            foreach (var model in list)
            {
                sb.Append("<li>");

                sb.AppendFormat("<input type='radio' id='RelationID_{0}' name='RELATIONID' value='{0}'/> <label for='RelationID_{0}'><i class='{2}'></i> {1}</label>",
                        model.ID,
                        model.Name,
                        "");

                if (model.ChildrenRelationList != null && model.ChildrenRelationList.Count > 0)
                    GetChildrenChoseTree(sb, model);

                sb.Append("</li>");

            }

            return sb.ToString();
        }
        #endregion

        #region 3.1 生成选择子人员列表HTML - void GetChildrenChoseTree(StringBuilder sb, Relation model)
        /// <summary>
        /// 生成选择子人员列表HTML
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="model"></param>
        private void GetChildrenChoseTree(StringBuilder sb, Relation model)
        {
            sb.Append("<ul>");
            foreach (var item in model.ChildrenRelationList)
            {
                sb.Append("<li>");

                sb.AppendFormat("<input type='radio' id='RelationID_{0}' name='RELATIONID' value='{0}'/> <label for='RelationID_{0}'><i class='{3}'></i> {2}</label>",
                        item.ID,
                        item.ParentID,
                        item.Name,
                        "");

                if (item.ChildrenRelationList != null && item.ChildrenRelationList.Count > 0)
                    GetChildrenChoseTree(sb, item);

                sb.Append("</li>");
            }
            sb.Append("</ul>");
        }
        #endregion


        #region 4.0 生成关系列表树状图Json + string GetTreeJson()
        /// <summary>
        /// 生成关系列表树状图Json
        /// </summary>
        /// <returns></returns>
        public RelationTree GetTreeJson()
        {
            var list = GetParentList();

            var sb = new StringBuilder();

            sb.Append("{");

            foreach (var model in list)
            {
                //生成关系列表
                sb.AppendFormat("\"name\":\"{0}\",\"title\":\"{1}\",\"children\":[", model.Name, model.Describe);

                if (model.ChildrenRelationList != null && model.ChildrenRelationList.Count > 0)
                {
                    GetTreeChildrenJson(sb, model);
                }

                sb.Append("]");                
            }

            sb.Append("}");

            //foreach (var model in list)
            //{
            //    var treeModel = new RelationTree
            //    {
            //        name = model.Name,
            //        title = model.Describe,
            //        children = new List<RelationTree>()
            //    };

            //    relationJson.Add(treeModel);

            //    if (model.ChildrenRelationList != null && model.ChildrenRelationList.Count > 0)
            //    {
            //        GetTreeChildrenJson(treeModel, model);
            //    }
            //}

            return Newtonsoft.Json.JsonConvert.DeserializeObject<RelationTree>(sb.ToString());
        }
        #endregion

        #region 4.1 生成子关系列表树状图Json - void GetTreeChildrenJson(RelationTree treeModel, Relation model)
        /// <summary>
        /// 生成子关系列表树状图Json
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="model"></param>
        private void GetTreeChildrenJson(StringBuilder sb, Relation model)
        {
            //foreach (var children in model.ChildrenRelationList)
            //{
            //    var treeChildModel = new RelationTree
            //    {
            //        name = children.Name,
            //        title = children.Describe,
            //        children = new List<RelationTree>()
            //    };

            //    relationJson.Last().children.Add(treeChildModel);

            //    if (children.ChildrenRelationList != null && children.ChildrenRelationList.Count > 0)
            //    {
            //        GetTreeChildrenJson(treeChildModel, children);
            //    }
            //}

           

            foreach (var children in model.ChildrenRelationList)
            {
                sb.Append("{");

                //生成子关系列表
                sb.AppendFormat("\"name\":\"{0}\",\"title\":\"{1}\",\"children\":[", children.Name, children.Describe);

                if (children.ChildrenRelationList != null && children.ChildrenRelationList.Count > 0)
                {
                    GetTreeChildrenJson(sb, children);
                }

                sb.Append("]");

                sb.Append("}" + (model.ChildrenRelationList.IndexOf(children) == model.ChildrenRelationList.Count - 1 ? "" : ","));
            }

           
        }
        #endregion


        //-------------------------------查询数据库-------------------------------

        #region 0.0 获取关系集合 + List<Relation> GetParentList()
        /// <summary>
        /// 获取关系集合
        /// </summary>
        /// <returns></returns>
        private List<Relation> GetParentList()
        {
            var parentPredicate = PredicateBuilder.True<Relation>().And(i => i.ParentID == null);
            var childrenPredicate = PredicateBuilder.True<Relation>().And(i => i.ParentID != null);

            var parentList = dal.GetList(parentPredicate, i => i.OrderNum, true);
            var childrenList = dal.GetList(childrenPredicate, i => i.OrderNum, true);

            var childrenDict = new Dictionary<int, List<Relation>>();

            foreach (var children in childrenList)
            {
                if (childrenDict.ContainsKey((int)children.ParentID))
                {
                    childrenDict[(int)children.ParentID].Add(children);
                }
                else
                {
                    childrenDict[(int)children.ParentID] = new List<Relation>() { children };
                }
            }

            foreach (var model in parentList)
            {
                GetChildrenList(model, childrenDict);
            }

            return parentList;
        }
        #endregion

        #region 0.1 递归出子关系集合 + void GetChildrenList(Relation model, Dictionary<int, List<Relation>> childrenDict)
        /// <summary>
        /// 递归出子关系集合
        /// </summary>
        /// <param name="model"></param>
        /// <param name="childrenDict"></param>
        private void GetChildrenList(Relation model, Dictionary<int, List<Relation>> childrenDict)
        {

            model.ChildrenRelationList = childrenDict.ContainsKey(model.ID) ? childrenDict[model.ID] : new List<Relation>();

            if (model.ChildrenRelationList.Count > 0)
            {
                foreach (var children in model.ChildrenRelationList)
                {
                    GetChildrenList(children, childrenDict);
                }
            }
        }
        #endregion

    }
}
