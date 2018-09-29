using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace White.Model
{
    /// <summary>
    /// Json对象模型
    /// </summary>
    public class JsonModel
    {
        public JsonModel()
        {
            Status = "error";
        }

        /// <summary>
        /// 状态（success:成功，error:失败）
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据对象
        /// </summary>
        public object Data { get; set; }
    }
}
