using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using White.Admin.Controllers;
using White.Common;
using White.Model;
using White.BLL;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace White.Admin.Areas.JX3.Controllers
{
    public class ScreenController : PermissionController
    {
        #region 1.0 截图列表 + ActionResult Index(int? page)
        /// <summary>
        /// 截图列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int? page)
        {
            PageIndex = page ?? 1;
            PageSize = 10;

            var predicate = PredicateBuilder.True<ScreenShot>().And(i => i.IsValid == true);

            var BLL = new ScreenShotBLL();
            var list = BLL.GetPagedList(PageIndex, PageSize, predicate, i => i.ID, false);

            var rowCount = BLL.GetCount(predicate); ;
            var pageCount = Convert.ToInt32(Math.Ceiling(rowCount * 1.0 / PageSize));
            var pagedHtml = HtmlCommon.GetPagedHtml(PageIndex, pageCount, "",
                string.Format("&keyword={0}", ""));

            ViewBag.Data = new
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                PageCount = pageCount,
                PagedHtml = pagedHtml,
                RowCount = rowCount
            }.ToExpando();

            return View(list);
        }
        #endregion

        #region 1.1 添加/修改截图视图 + ActionResult Edit()
        /// <summary>
        /// 添加/修改截图视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            return View(new ScreenShotBLL().GetModel(i => i.IsValid == true && i.ID == id) ?? new ScreenShot());
        }
        #endregion

        #region 1.2 添加/修改截图视图 + JsonResult Edit()
        /// <summary>
        /// 添加/修改截图视图
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(ScreenShot model, string picBase)
        {
            var json = new JsonModel();
            var screenBLL = new ScreenShotBLL();

            #region 上传图片
            if (!string.IsNullOrEmpty(picBase))
            {
                picBase = picBase.Substring(picBase.IndexOf(',') + 1);

                //正常图片路径
                var bigPicPath = UploadImagePath + "Pic/";
                //缩略图路径
                var nailPicPath = UploadImagePath + "ThubmnailPic/";

                if (!Directory.Exists(Server.MapPath(bigPicPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(bigPicPath));
                }
                if (!Directory.Exists(Server.MapPath(nailPicPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(nailPicPath));
                }

                model.ImageUrl = bigPicPath + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                model.Thumbnail = nailPicPath + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";

                byte[] arr = Convert.FromBase64String(picBase);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                #region 压缩图片

                //用指定的大小和格式初始化Bitmap类的新实例
                Bitmap smallPic = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
                //从指定的Image对象创建新Graphics对象
                Graphics graphics = Graphics.FromImage(smallPic);
                //清除整个绘图面并以透明背景色填充
                graphics.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片对象
                graphics.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                #endregion

                //上传正常图片
                bmp.Save(Server.MapPath(model.ImageUrl), ImageFormat.Jpeg);
                //上传缩略图
                smallPic.Save(Server.MapPath(model.Thumbnail), ImageFormat.Jpeg);

                ms.Close();
            }

            #endregion

            if (model.ID > 0)
            {
                var srceenInfo = screenBLL.GetModel(i => i.IsValid == true && i.ID == model.ID);

                srceenInfo.Title = model.Title;
                srceenInfo.SubTitle = model.SubTitle;
                srceenInfo.ImageUrl = model.ImageUrl;
                srceenInfo.Thumbnail = model.Thumbnail;
                srceenInfo.Describe = model.Describe;
                srceenInfo.UpdateDate = DateTime.Now;
                srceenInfo.UpdateUserID = LoginUser.ID;

                if (screenBLL.SaveChange() > 0)
                {
                    json.Status = "success";
                    json.Message = "编辑成功！";
                }
                else
                {
                    json.Message = "编辑失败，请稍后重试！";
                }
            }
            else
            {
                model.IsValid = true;
                model.CreateDate = DateTime.Now;
                model.CreateUserID = LoginUser.ID;

                if (screenBLL.Add(model) > 0)
                {
                    json.Status = "success";
                    json.Message = "添加成功！";
                }
                else
                {
                    json.Message = "添加失败，请稍后重试！";
                }
            }

            return Json(json);
        }
        #endregion

        #region 1.3 删除截图方法 + JsonResult ScreenDel(int id)
        /// <summary>
        /// 删除截图方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ScreenDel(int id)
        {
            var json = new JsonModel();

            var BLL = new ScreenShotBLL();

            var screen = BLL.GetModel(i => i.ID == id);

            screen.IsValid = false;

            if (BLL.SaveChange() > 0)
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