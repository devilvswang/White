using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace White.WebApi.Controllers
{
    public class UploadController : ApiController
    {
        [HttpPost]
        public string UploadFile()
        {
            string result = string.Empty;
            try
            {
                //HttpFileCollection files = HttpContext.Current.Request.Files;

                HttpRequest request = System.Web.HttpContext.Current.Request;
                HttpFileCollection fileCollection = request.Files;
                // 判断是否有文件
                if (fileCollection.Count > 0)
                {
                    // 获取文件
                    HttpPostedFile httpPostedFile = fileCollection[0];

                    result = FileUpload(httpPostedFile);
                }
            }
            catch (Exception)
            {
                result = "上传失败";
            }
            return result;
        }


        /// <summary>
        /// 上传图片方法
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string FileUpload(HttpPostedFile file)
        {
            var fileurl = string.Empty;

            if (file != null)
            {
                var filePath = $"/Upload/{DateTime.Now.ToString("yyyy-MM-dd/")}";

                if (!Directory.Exists(HttpContext.Current.Request.MapPath(filePath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Request.MapPath(filePath));
                }

                var ext_name = file.FileName.Substring(file.FileName.IndexOf('.'));

                fileurl = filePath + Guid.NewGuid().ToString().Replace("-", "") + $"{ext_name}";

                file.SaveAs(HttpContext.Current.Request.MapPath(fileurl));
            }
            else
            {
                return null;
            }

            return fileurl;
        }
    }
}