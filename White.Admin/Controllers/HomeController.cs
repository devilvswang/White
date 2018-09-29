using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace White.Admin.Controllers
{
    public class HomeController : PermissionController
    {

        public ActionResult Index()
        {
            return View();
        }


    }
}