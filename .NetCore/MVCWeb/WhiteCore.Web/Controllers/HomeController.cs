using System;
using WhiteCore.Web.Models;
using WhiteCore.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WhiteCore.Web.Controllers
{
    public class HomeController : Controller
    {
        public MySqlRepository Repository { get; }
        private MsSqlRepository MsRepository { get; }

        public HomeController(MsSqlRepository msRepository)
        {
            this.MsRepository = msRepository;
        }



        public IActionResult Index()
        {
            return Content("Hello WN");
        }

        public IActionResult Add(UserInfoEntity user)
        {

            var message = MsRepository.Add(user) > 0 ? "success" : "failed";
            return Json(new { Message = message, User = user });
        }


        public IActionResult Time()
        {
            ViewBag.NowTime = DateTime.Now;

            ViewData["aa"] = 123;

            return View();
        }


    }
}
