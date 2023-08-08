using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using White.BLL;
using Newtonsoft.Json;
using Redis;
using System.Threading;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;

namespace Demo.Controllers
{
    public class ThreadingController : Controller
    {
        private static object o = new object();

        private static List<int> list = new List<int>();

        private int count = 0;

        // GET: Threading
        public ActionResult Index()
        {
            //Simple.Main();

            #region 多线程执行

            //var t1 = Task.Factory.StartNew(() =>
            //{
            //    AddNumber();
            //});

            //var t2 = Task.Factory.StartNew(() =>
            //{
            //    AddNumber();
            //});

            //var t3 = Task.Factory.StartNew(() =>
            //{
            //    AddNumber();
            //});

            //Task.WaitAll(t1, t2, t3);


            #endregion

            RedisHelper.Set("aa", "111");

            count = Convert.ToInt32(RedisHelper.Get<string>("aa"));

            ViewData["Count"] = count;

            

            return View();
        }

        private void AddNumber()
        {
            Parallel.For(0, 200, i =>
            {
                //互斥锁
                lock (o)
                {
                    count++;
                }


            });
        }

        private void MonitorMethod()
        {
            try
            {
                Monitor.Enter(o);
                //Todo
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Monitor.Exit(o);
            }

        }


    }
}