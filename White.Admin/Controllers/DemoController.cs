using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using White.Model;

namespace White.Admin.Controllers
{
    public class DemoController : PermissionController
    {
        public ActionResult ImageTest()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }


        public JsonResult UploadFile()
        {
            var json = new JsonModel();
            var file = Request.Files["imageFile"];

            if (file != null)
            {
                var filePath = UploadImagePath;

                if (!Directory.Exists(Server.MapPath(filePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(filePath));
                }

                var url = filePath + DateTime.Now.Ticks + new Random().Next(100, 999) + ".jpg";
                file.SaveAs(Server.MapPath(url));

                json.Status = "success";
            }

            return Json(json);
        }
    }
}