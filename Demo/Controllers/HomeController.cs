using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Net;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region AES加解密
            //var key = "8caf1b0dd8924804";
            //var vi = "f35f800d95a865c0";

            //var enStr = AesEncrypt("wang", key, vi);

            //ViewBag.AESStr = enStr;
            //ViewBag.DeStr = AesDecrypt(enStr, key, vi); 
            #endregion

            Threading();

            return View();
        }
        #endregion

        #region Json
        /// <summary>
        /// Json
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowJson()
        {
            var dict = new Dictionary<string, object>();
            var jStr = "";
            var jStrList = "";
            //var dictModelList = new List<DictModel>();

            //var str = @"[
            //    {""映射111111"":""这是第一个内容""},
            //    {""映射888888"":""这是第二个内容""}
            //    ]";

            //var keyMappingList = new List<Core_WebService_KeyMapping>();

            //keyMappingList.Add(new Core_WebService_KeyMapping
            //{
            //    ID = 1,
            //    PMConnGuid = "d4257e71-9419-4b9e-88a9-6f6abe64436b",
            //    KeyID = 12,
            //    ReturnKey = "映射111111",
            //    MessageKey = "111111"
            //});
            //keyMappingList.Add(new Core_WebService_KeyMapping
            //{
            //    ID = 2,
            //    PMConnGuid = "f4d06e08-cb80-4bf3-a6ef-de80ca086f4e",
            //    KeyID = 11,
            //    ReturnKey = "映射888888",
            //    MessageKey = "888888"
            //});

            //try
            //{
            //    JArray ja = (JArray)JsonConvert.DeserializeObject(str);

            //    if (ja.Count > 0 && ja != null)
            //    {
            //        foreach (JObject items in ja)
            //        {
            //            foreach (var item in items)
            //            {
            //                var key = item.Key;
            //                var tempMapping = keyMappingList.Where(i => i.ReturnKey == item.Key).FirstOrDefault();

            //                if (tempMapping != null)
            //                {
            //                    key = tempMapping.MessageKey;
            //                }

            //                if (!dict.ContainsKey(key))
            //                {
            //                    dict.Add(key, item.Value.ToString());
            //                    dictModelList.Add(new DictModel { Key = key, Value = item.Value.ToString() });
            //                }
            //            }
            //        }
            //    }

            //    jStr = JsonConvert.SerializeObject(dict);
            //    jStrList = JsonConvert.SerializeObject(dictModelList);
            //}
            //catch (Exception ex)
            //{
            //    return Content(ex.Message);
            //}




            //ViewBag.Json = str;

            ViewBag.Dict = dict;
            ViewBag.JStr = jStr;
            ViewBag.JStrList = jStrList;

            return View();
        }
        #endregion

        #region WCF
        /// <summary>
        /// WCF
        /// </summary>
        /// <returns></returns>
        public ActionResult WCF()
        {
            //FieldBatchServiceClient client = new FieldBatchServiceClient();
            var url = "http://localhost:8081/FieldBatchService.svc";
            var ReqResult = "";

            string method = "BatchSetAllUserFieldCache";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + method);
            request.Method = "POST";
            Stream requestStram = request.GetRequestStream();
            requestStram.Close();
            HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            ReqResult = reader.ReadToEnd();
            Console.WriteLine(ReqResult);


            ViewBag.Str1 = ReqResult;
            //ViewBag.Str2 = client.UpdateAllUserPartFieldCache();
            //ViewBag.Str3 = client.CheckUserCacheExist();

            return View();
        }

        #endregion

        #region VueDemo
        /// <summary>
        /// Vue
        /// </summary>
        /// <returns></returns>
        public ActionResult Vue()
        {
            return View();
        } 
        #endregion



        #region Json字符串解析


        // 从一个对象信息生成Json串
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }
        // 从一个Json串生成对象信息
        public static object JsonToObject(string jsonString, object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }

        #endregion

        #region 线程

        private static int poolFlag = 0;//标记
        private const int amountThread = 10;//线程总量
        private const int maxThread = 3;//可执行线程最大数量
        private static Mutex muxConsole = new Mutex();


        [HttpPost]
        public void Threading()
        {
            for (int i = 0; i < amountThread; i++)
            {
                // 创建指定数量的线程
                // 是线程调用Run方法
                // 启动线程
                Thread trd = new Thread(new ThreadStart(Run));
                trd.Name = "线程" + i;
                trd.Start();
            }
        }

        public void Run()
        {
            muxConsole.WaitOne();  //阻塞队列
            Interlocked.Increment(ref poolFlag);  //标记+1
            if (poolFlag != maxThread)             //判断是否等于上限
                muxConsole.ReleaseMutex();     //如果此线程达不到可执行线程上限,则继续开通,让后面的线程进来
            System.Diagnostics.Debug.WriteLine("{0} 正在运行....../n", Thread.CurrentThread.Name);
            Thread.Sleep(5000);                                                                                             //模拟执行
            System.Diagnostics.Debug.WriteLine("{0} 已经中止....../n", Thread.CurrentThread.Name);

            //标记-1
            Interlocked.Decrement(ref poolFlag);
        }

        #endregion

        #region AES加解密

        #region AES加密 + string AesEncrypt(string value, string key, string iv = "")
        //AES加密
        public static string AesEncrypt(string value, string key, string iv = "")
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (key == null) throw new Exception("未将对象引用设置到对象的实例。");
            if (key.Length < 16) throw new Exception("指定的密钥长度不能少于16位。");
            if (key.Length > 32) throw new Exception("指定的密钥长度不能多于32位。");
            if (key.Length != 16 && key.Length != 24 && key.Length != 32) throw new Exception("指定的密钥长度不明确。");
            if (!string.IsNullOrEmpty(iv))
            {
                if (iv.Length < 16) throw new Exception("指定的向量长度不能少于16位。");
            }

            var _keyByte = Encoding.UTF8.GetBytes(key);
            var _valueByte = Encoding.UTF8.GetBytes(value);
            using (var aes = new RijndaelManaged())
            {
                aes.IV = !string.IsNullOrEmpty(iv) ? Encoding.UTF8.GetBytes(iv) : Encoding.UTF8.GetBytes(key.Substring(0, 16));
                aes.Key = _keyByte;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                var cryptoTransform = aes.CreateEncryptor();
                var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }
        #endregion

        #region AES解密 + string AesDecrypt(string value, string key, string iv = "")
        //AES解密
        public static string AesDecrypt(string value, string key, string iv = "")
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (key == null) throw new Exception("未将对象引用设置到对象的实例。");
            if (key.Length < 16) throw new Exception("指定的密钥长度不能少于16位。");
            if (key.Length > 32) throw new Exception("指定的密钥长度不能多于32位。");
            if (key.Length != 16 && key.Length != 24 && key.Length != 32) throw new Exception("指定的密钥长度不明确。");
            if (!string.IsNullOrEmpty(iv))
            {
                if (iv.Length < 16) throw new Exception("指定的向量长度不能少于16位。");
            }

            var _keyByte = Encoding.UTF8.GetBytes(key);
            var _valueByte = Convert.FromBase64String(value);
            using (var aes = new RijndaelManaged())
            {
                aes.IV = !string.IsNullOrEmpty(iv) ? Encoding.UTF8.GetBytes(iv) : Encoding.UTF8.GetBytes(key.Substring(0, 16));
                aes.Key = _keyByte;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                var cryptoTransform = aes.CreateDecryptor();
                var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);
                return Encoding.UTF8.GetString(resultArray);
            }

        }
        #endregion 

        #endregion
    }
}