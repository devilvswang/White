using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace White.DAL
{
    public class SqlQueryDAL<T> where T : class, new()
    {
        #region EF 上下文对象 # DbContext db
        /// <summary>
        /// EF 上下文对象
        /// </summary>
        protected DbContext db = null;
        #endregion

        #region 查询语句，指定查询某一张表/视图 # string TableName
        /// <summary>
        /// 查询语句，指定查询某一张表/视图
        /// </summary>
        protected string TableName { get; set; }
        #endregion

        #region 构造函数 + SqlQueryDAL(string dbContext, string tableName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="tableName"></param>
        public SqlQueryDAL(string dbContext, string tableName)
        {
            db = DBContextFactory.GetDBContext(dbContext);
            TableName = tableName;
        }
        #endregion



        #region 1.0 根据条件查询对象 +T GetModel(string where, params object[] parameter)
        /// <summary>
        /// 根据条件查询对象
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="parameter">参数数组</param>
        /// <returns>返回实体对象</returns>
        public T GetModel(string where, params object[] parameter)
        {
            return db.Database.SqlQuery<T>(string.Format("select * from {0} {1}", TableName, where), parameter).FirstOrDefault();
        }
        #endregion

        #region 1.1 根据排序条件获取第一个对象 +T GetModel(string order)
        /// <summary>
        /// 根据排序条件获取第一个对象
        /// </summary>
        /// <typeparam name="order">ordery条件</typeparam>
        /// <returns></returns>
        public T GetModel(string order)
        {
            return db.Database.SqlQuery<T>(string.Format("select * from {0} {1}", TableName, order)).FirstOrDefault();
        }
        #endregion

        #region 1.2 根据条件获取排序后第一个对象 + T GetModel(string where, string order, params object[] parameter)
        /// <summary>
        /// 根据条件获取排序后第一个对象
        /// </summary>
        /// <typeparam name="where">where条件</typeparam>
        /// <param name="order">order条件</param>
        /// <param name="parameter">参数数组</param>
        /// <returns></returns>
        public T GetModel(string where, string order, params object[] parameter)
        {
            return db.Database.SqlQuery<T>(string.Format("select * from {0} {1} {2}", TableName, where, order), parameter).FirstOrDefault();
        }
        #endregion


        #region 2.0 查询记录条数 +int GetCount()
        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <returns>返回条数</returns>
        public int GetCount()
        {
            return db.Database.SqlQuery<int>(string.Format("select count(*) from {0} as t", TableName)).FirstOrDefault();
        }
        #endregion

        #region 2.1 根据条件查询记录条数 +int GetCount(string where,params object[] parameter)
        /// <summary>
        /// 根据条件查询记录条数
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="parameter">参数数组</param>
        /// <returns>返回条数</returns>
        public int GetCount(string where, params object[] parameter)
        {
            return db.Database.SqlQuery<int>(string.Format("select count(*) from {0} {1}", TableName, where), parameter).FirstOrDefault();
        }
        #endregion


        #region 3.0 查询所有数据 +List<T> GetList()
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>返回泛型List集合</returns>
        public List<T> GetList()
        {
            return db.Database.SqlQuery<T>(string.Format("select * from {0} t", TableName)).ToList();
        }
        #endregion

        #region 3.1 查询排序后所有数据 +List<T> GetList(string order)
        /// <summary>
        /// 查询排序后所有数据
        /// </summary>
        /// <param name="order">order条件</param>
        /// <returns>返回排序后的泛型List集合</returns>
        public List<T> GetList(string order)
        {
            return
                db.Database.SqlQuery<T>(string.Format("select * from {0} {1}", TableName, order)).ToList();
        }
        #endregion

        #region 3.2 根据条件查询所有数据 +List<T> GetList(string where,params object[] parameter)
        /// <summary>
        /// 根据条件查询所有数据
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="parameter">参数数组</param>
        /// <returns>返回泛型List集合</returns>
        public List<T> GetList(string where, params object[] parameter)
        {
            return db.Database.SqlQuery<T>(string.Format("select * from {0} {1}", TableName, where), parameter).ToList();
        }
        #endregion

        #region 3.3 根据条件查询排序后的所有数据 +List<T> GetList(string where, string order, params object[] parameter)
        /// <summary>
        /// 根据条件查询排序后的所有数据
        /// </summary>
        /// <typeparam name="where">where条件</typeparam>
        /// <param name="order">order条件</param>
        /// <param name="parameter">参数数组</param>
        /// <returns>返回排序后的泛型List集合</returns>
        public List<T> GetList(string where, string order, params object[] parameter)
        {
            return
                db.Database.SqlQuery<T>(string.Format("select * from {0} {1} {2}", TableName, where, order), parameter).ToList();
        }
        #endregion


        #region 4.0 查询排序后的分页数据  +List<T> GetPagedList(int pageIndex, int pageSize, string order)
        /// <summary>
        /// 查询排序后的分页数据
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="order">order条件</param>
        /// <returns>返回排序后的分页泛型List集合</returns>
        public List<T> GetPagedList(int pageIndex, int pageSize, string order)
        {
            string sql = string.Format(@"select * from (
                                          select t.*,row_number() over(order by {3}) row_number from {0} t
                                        ) t
                                        where t.row_number>{1} and t.row_number<={2}", TableName, (pageIndex - 1) * pageSize, pageIndex * pageSize, order);
            return db.Database.SqlQuery<T>(sql).ToList();
        }
        #endregion

        #region 4.1 根据条件查询排序分页数据 +List<T> GetPagedList(int pageIndex, int pageSize, string where, string order, params object[] parameter)
        /// <summary>
        /// 根据条件查询排序分页数据
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="where">where条件</param>
        /// <param name="order">order条件</param>
        /// <param name="parameter">参数数组</param>
        /// <returns>返回排序后的泛型List集合</returns>
        public List<T> GetPagedList(int pageIndex, int pageSize, string where, string order, params object[] parameter)
        {
            string sql = string.Format(@"select * from (
                                          select t.*,row_number() over({4}) row_number from {0} t {3}
                                        ) t
                                        where t.row_number>{1} and t.row_number<={2}", TableName, (pageIndex - 1) * pageSize, pageIndex * pageSize, where, order);
            return db.Database.SqlQuery<T>(sql, parameter).ToList();
        }
        #endregion




        #region 5.0 根据条件获某一列的SUM值 +decimal GetSum(string select, string where, params object[] parameter)
        /// <summary>
        /// 根据条件获某一列的SUM值
        /// </summary>
        /// <param name="select">查询字段</param>
        /// <param name="where">where条件</param>        
        /// <param name="parameter">参数数组</param>
        /// <returns>返回SUM值</returns>
        public int GetSum(string select, string where, params object[] parameter)
        {
            return db.Database.SqlQuery<int>(string.Format("select sum({2}) from ({0})t {1}", TableName, where, select), parameter).FirstOrDefault();
        }
        #endregion

        #region 5.1 根据条件获某一列的MIN值 +decimal GetMin(string select, string where, params object[] parameter)
        /// <summary>
        /// 根据条件获某一列的MIN值
        /// </summary>
        /// <param name="select">查询字段</param>
        /// <param name="where">where条件</param>        
        /// <param name="parameter">参数数组</param>
        /// <returns>返回MIN值</returns>
        public decimal? GetMin(string select, string where, params object[] parameter)
        {
            return db.Database.SqlQuery<decimal>(string.Format("select min({2}) from {0} {1}", TableName, where, select), parameter).FirstOrDefault();
        }
        #endregion

        #region 5.2 根据条件获某一列的MAX值 +decimal GetMax(string select, string where, params object[] parameter)
        /// <summary>
        /// 根据条件获某一列的MAX值
        /// </summary>
        /// <param name="select">查询字段</param>
        /// <param name="where">where条件</param>        
        /// <param name="parameter">参数数组</param>
        /// <returns>返回MAX值</returns>
        public decimal? GetMax(string select, string where, params object[] parameter)
        {
            return db.Database.SqlQuery<decimal>(string.Format("select max({2}) from {0} {1}", TableName, where, select), parameter).FirstOrDefault();
        }
        #endregion

        #region 5.3 根据条件查询去除重复数据列的集合 +List<T> GetDistinctList<T>(string select, string where, params object[] parameter)
        /// <summary>
        /// 根据条件查询去除重复数据列的集合
        /// </summary>
        /// <param name="select">查询字段</param>
        /// <param name="where">where条件</param>        
        /// <param name="parameter">参数数组</param>
        /// <returns>返回List集合</returns>
        public List<T> GetDistinctList<T>(string select, string where, params object[] parameter)
        {
            return db.Database.SqlQuery<T>(string.Format("select distinct({2}) from {0} a {1}", TableName, where, select), parameter).ToList();
        }
        #endregion

        #region 5.4 根据条件查询去除重复数据列的集合 +List<T> GetDistinctList<T>(string select, string where, string order, params object[] parameter)
        /// <summary>
        /// 根据条件查询去除重复数据列的集合
        /// </summary>
        /// <param name="select">查询字段</param>
        /// <param name="where">where条件</param>
        /// <param name="order">order条件</param>
        /// <param name="parameter">参数数组</param>
        /// <returns>返回List集合</returns>
        public List<T> GetDistinctList<T>(string select, string where, string order, params object[] parameter)
        {
            return db.Database.SqlQuery<T>(string.Format("select distinct({3}) from {0} {1} {2}", TableName, where, order, select), parameter).ToList();
        }
        #endregion


        #region 6.0 查询原生SQL返回List集合 + List<T> SqlQuery<T>(string sql, params object[] parameter)
        /// <summary>
        /// 查询原生SQL返回List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<T> SqlQuery<T>(string sql, params object[] parameter)
        {
            return db.Database.SqlQuery<T>(sql, parameter).ToList();
        }
        #endregion
    }
}
