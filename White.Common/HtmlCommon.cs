using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace White.Common
{
    /// <summary>
    /// HTML辅助类
    /// </summary>
    public static class HtmlCommon
    { 
        #region 获取分页的页码HTML +string GetPagedHtml(int page, int pages, string url, string param)
        /// <summary>
        /// 获取分页的页码HTML
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="pages">共多少页</param>
        /// <param name="url">链接地址</param>
        /// <param name="param">参数(以&字符开始)</param>
        /// <returns></returns>
        public static string GetPagedHtml(int page, int pages, string url, string param)
        {
            if (pages <= 1)
            {
                return "";
            }
            int pageNum = 5;    //最多显示多少个页码
            page = page < 1 ? 1 : page;     //当前页面小于1 则为1
            page = page > pages ? pages : page;     //当前页大于总页数 则为总页数
            pages = pages < page ? page : pages;    //页数小当前页 则为当前页

            //计算开始页
            int start = page - pageNum / 2;
            start = start < 1 ? 1 : start;

            //计算结束页
            int end = page + pageNum / 2;
            end = end > pages ? pages : end;

            //当前显示的页码个数不够最大页码数，在进行左右调整
            int curPageNum = end - start + 1;
            //左调整
            if (curPageNum < pageNum && start > 1)
            {
                start = start - (pageNum - curPageNum);
                start = start < 1 ? 1 : start;
                curPageNum = end - start + 1;
            }
            //右边调整
            if (curPageNum < pageNum && end < pages)
            {
                end = end + (pageNum - curPageNum);
                end = end > pages ? pages : end;
            }

            StringBuilder pageHtml = new StringBuilder();
            
            //生成首页及上一页按钮
            if (page == 1)
            {
                pageHtml.Append("<li><a class='btn btn-sm' disabled='disabled' title='首页'>首页</a></li>");
                pageHtml.Append("<li><a class='btn btn-sm' disabled='disabled' title='上一页'>上一页</a></li>");
            }
            else
            {
                pageHtml.AppendFormat("<li><a class='btn-sm btn' title='首页' href='{0}?page=1{1}'>首页</a></li>", url, param);
                pageHtml.AppendFormat("<li><a class='btn-sm btn' title='上一页' href='{0}?page={1}{2}'>上一页</a></li>", url, page > 1 ? page - 1 : 1, param);
            }
            

            for (int i = start; i <= end; i++)
            {
                if (i == page)
                {
                    pageHtml.AppendFormat("<li class='active'><a class='btn btn-sm btn-alt'>{0}</a></li>", i);

                }
                else
                {
                    pageHtml.AppendFormat("<li><a class='btn-sm btn' href='{0}?page={1}{2}'>{1}</a></li>", url, i, param);
                }
            }

            //生成下一页及尾页按钮
            if (page >= pages)
            {
                pageHtml.Append("<li><a class='btn btn-sm' disabled='disabled' title='下一页'>下一页</a></li>");
                pageHtml.Append("<li><a class='btn btn-sm' disabled='disabled' title='尾页'>尾页</a></li>");
            }
            else
            {
                pageHtml.AppendFormat("<li><a class='btn-sm btn' title='下一页' href='{0}?page={1}{2}'>下一页</a></li>", url, page < end ? page + 1 : end, param);
                pageHtml.AppendFormat("<li><a class='btn-sm btn' title='尾页' href='{0}?page={1}{2}'>尾页</a></li>", url, pages, param);
            }

            return pageHtml.ToString();
        }
        #endregion

        #region 移除HTML标签 +string RemoveHTML(string str)
        /// <summary>
        /// 移除HTML标签
        /// </summary>
        /// <param name="str">要移除HTML标签的字符串</param>
        /// <returns>移除HTML标签后的字符串</returns>
        public static string RemoveHTML(string str)
        {
            return Regex.Replace(str, @"<.*?>", "", RegexOptions.IgnoreCase);

        }
        #endregion

        #region 获取分页的页码HTML +string GetPagedHtml(int page, int pages, string url, string param)
        /// <summary>
        /// 获取分页的页码HTML
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="pages">共多少页</param>
        /// <param name="url">链接地址</param>
        /// <param name="param">参数(以&字符开始)</param>
        /// <returns></returns>
        public static string GetPagedHtmlPC(int page, int pages, string url, string param)
        {
            if (pages <= 1)
            {
                return "";
            }
            int pageNum = 5;    //最多显示多少个页码
            page = page < 1 ? 1 : page;     //当前页面小于1 则为1
            page = page > pages ? pages : page;     //当前页大于总页数 则为总页数
            pages = pages < page ? page : pages;    //页数小当前页 则为当前页

            //计算开始页
            int start = page - pageNum / 2;
            start = start < 1 ? 1 : start;

            //计算结束页
            int end = page + pageNum / 2;
            end = end > pages ? pages : end;

            //当前显示的页码个数不够最大页码数，在进行左右调整
            int curPageNum = end - start + 1;
            //左调整
            if (curPageNum < pageNum && start > 1)
            {
                start = start - (pageNum - curPageNum);
                start = start < 1 ? 1 : start;
                curPageNum = end - start + 1;
            }
            //右边调整
            if (curPageNum < pageNum && end < pages)
            {
                end = end + (pageNum - curPageNum);
                end = end > pages ? pages : end;
            }

            StringBuilder pageHtml = new StringBuilder();

            //生成首页及上一页按钮
            if (page == 1)
            {
                //pageHtml.Append("<a class='pn' title='首页'>&lt</a>");
                pageHtml.Append("<a class='pn disabled' title='上一页'>&lt</a>");
            }
            else
            {
                //pageHtml.AppendFormat("<a  class='pn' title='首页' href='{0}?page=1{1}'>lt</a>", url, param);
                pageHtml.AppendFormat("<a  class='pn' title='上一页' href='{0}?page={1}{2}'>&lt</a>", url, page > 1 ? page - 1 : 1, param);
            }


            for (int i = start; i <= end; i++)
            {
                if (i == page)
                {
                    pageHtml.AppendFormat("<a class='pn active' class='active'>{0}</a>", i);

                }
                else
                {
                    pageHtml.AppendFormat("<a class='pn' href='{0}?page={1}{2}'>{1}</a>", url, i, param);
                }
            }

            //生成下一页及尾页按钮
            if (page >= pages)
            {
                pageHtml.Append("<a class='pn disabled' title='下一页'>&gt</a>");
                //pageHtml.Append("<a title='尾页'>尾页</a>");
            }
            else
            {
                pageHtml.AppendFormat("<a class='pn' title='下一页' href='{0}?page={1}{2}'>&gt</a>", url, page < end ? page + 1 : end, param);
                //pageHtml.AppendFormat("<a title='尾页' href='{0}?page={1}{2}'>尾页</a>", url, pages, param);
            }

            return pageHtml.ToString();
        }
        #endregion
    }
}
