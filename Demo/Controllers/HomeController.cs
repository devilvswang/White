using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var sb = new StringBuilder();

            for (int i = 1; i <= 30; i++)
            {
                sb.AppendFormat("{0}<br/>", Guid.NewGuid());
            }

            ViewBag.Text = sb.ToString();
            return View();
        }
    }
}