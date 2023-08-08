using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace White.WinForm
{
    public partial class Form1 : Form
    {
        private long _appId = 3502931170604154880;
        private string _appSignKey = "ohRKgsDejTnbVPAZcfgB";
        private string _root = "https://mama.dxy.net/api/partner-gateway/open";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_timestamp_Click(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            txt_timestamp.Text = Convert.ToInt64(ts.TotalSeconds).ToString();
        }


        private void btn_sign_Click(object sender, EventArgs e)
        {
            txt_sign.Text = $"appId={_appId}&appSignKey={_appSignKey}&data={JsonConvert.SerializeObject(new { keyword = "疫苗" })}&nonce={txt_nonce.Text}&timestamp={txt_timestamp.Text}";
        }


        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }

        private void btn_nonce_Click(object sender, EventArgs e)
        {
            txt_nonce.Text = Guid.NewGuid().ToString("n");
        }

        private void btn_cryp_Click(object sender, EventArgs e)
        {
            txt_cryp.Text = SHA1(txt_sign.Text, Encoding.UTF8).ToLower();
        }

        public async Task<T> Post<T>(string action, object obj)
        {
            var guid = System.Guid.NewGuid().ToString("n").Replace("-", "");

            string uri = $"{_root}{action}";

            HttpClientHandler handler = new HttpClientHandler
            {
                UseDefaultCredentials = false,
            };
            HttpClient httpClient = new HttpClient(handler);


            string jsonContent = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/x-www-form-urlencoded");

            content.Headers.Remove("Content-Type"); //application/json; charset=utf-8
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            HttpResponseMessage response = httpClient.PostAsync(uri, content).Result;



            response.EnsureSuccessStatusCode();

            string resp = await response.Content.ReadAsStringAsync();

            var rtn = JsonConvert.DeserializeObject<T>(resp);

            return rtn;
        }

        private void btn_request_Click(object sender, EventArgs e)
        {
            var options = new JsonSerializerOptions();
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All);

            txt_request.Text = Post<vo_dingxiang_result>("/commodity/standard-commodity/list",
                new
                {
                    appId = _appId,
                    sign = txt_cryp.Text,
                    timestamp = txt_timestamp.Text,
                    data = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        keyword = "疫苗"
                    }, options),
                    nonce = txt_nonce.Text
                }).Result.ToString();
        }


        public class vo_dingxiang_result
        {
            public bool success { get; set; }
            public int errorCode { get; set; }
            public string message { get; set; }
        }

    }
}
