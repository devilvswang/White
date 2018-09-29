using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using White.Model;


namespace White.DAL
{
    /// <summary>
    /// 获取EF上下文对象，简单工厂
    /// </summary>
    public class DBContextFactory
    {
        /// <summary>
        /// 创建EF上下文对象，在线程中共享一个上下文对象
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDBContext(string context)
        {

            DbContext dbcontext = CallContext.GetData(context) as DbContext;

            if (dbcontext == null)
            {
                switch (context)
                {
                    case "AdminContext":
                        dbcontext = new AdminDBEntities();
                        break;
                    case "JX3Context":
                        dbcontext = new JX3Entities();
                        break;
                }
                dbcontext.Configuration.ValidateOnSaveEnabled = false;
                dbcontext.Database.CommandTimeout = 360;
                CallContext.SetData(context, dbcontext);
            }
            return dbcontext;
        }
    }
}
