using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using White.BLL;


namespace Demo.Controllers
{
    public class ThreadingController : Controller
    {
        // GET: Threading
        public ActionResult Index()
        {
            Simple.Main();

            return View();
        }
    }
}