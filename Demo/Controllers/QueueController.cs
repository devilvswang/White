using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Messaging;
using RabbitMQ.Client;
using System.Text;
using System.Web.Helpers;

namespace Demo.Controllers
{
    public class QueueController : Controller
    {

        public ActionResult Index()
        {




            return View();
        }

        public JsonResult Producer(string value)
        {
            try
            {
                var qName = "wnTest";
                var exchangeName = "fanoutchange1";
                var exchangeType = "fanout";
                var routingKey = "*";
                var uri = "http://localhost:15672";

                var factory = new ConnectionFactory()
                {
                    UserName = "guest",
                    Password = "guest",
                    Endpoint = new AmqpTcpEndpoint(uri)
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        //设置交换器类型
                        channel.ExchangeDeclare(exchangeName, exchangeType);
                        //声明一个队列，设置队列是否持久化，排他性，与自动删除
                        channel.QueueDeclare(qName, true, false, false, null);
                        //绑定消息队列，交换器，routingkey
                        channel.QueueBind(qName, exchangeName, routingKey);

                        var properties = channel.CreateBasicProperties();
                        //队列持久化
                        properties.Persistent = true;

                        var body = Encoding.UTF8.GetBytes("Hello Laowang");

                        //发送消息
                        channel.BasicPublish(exchangeName, routingKey, properties, body);

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { });
        }
    }
}