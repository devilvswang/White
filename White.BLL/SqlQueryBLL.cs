using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using White.DAL;

namespace White.BLL
{
    public class SqlQueryBLL<T> where T:class,new()
    {
        #region 数据层操作对象 # SqlQueryDAL<T> dal
        /// <summary>
        /// 数据层操作对象
        /// </summary>
        protected SqlQueryDAL<T> dal ;
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
            return dal.GetModel(where, parameter);
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
            return dal.GetModel(order);
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
            return dal.GetModel(where, order, parameter);
        }
        #endregion


        #region 2.0 查询记录条数 +int GetCount()
        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <returns>返回条数</returns>
        public int GetCount()
        {
            return dal.GetCount();
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
            return dal.GetCount(where, parameter);
        }
        #endregion


        #region 3.0 查询所有数据 +List<T> GetList()
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>返回泛型List集合</returns>
        public List<T> GetList()
        {
            return dal.GetList();
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
            return dal.GetList(order);
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
            return dal.GetList(where, parameter);
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
            return dal.GetList(where, order, parameter);
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
            return dal.GetPagedList(pageIndex, pageSize, order);
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
            return dal.GetPagedList(pageIndex, pageSize, where, order, parameter);
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
            return dal.GetSum(select, where, parameter);
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
            return dal.GetMin(select, where, parameter);
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
            return dal.GetMax(select, where, parameter);
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
            return dal.GetDistinctList<T>(select, where, parameter);
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
            return dal.GetDistinctList<T>(select, where, order, parameter);
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
            return dal.SqlQuery<T>(sql, parameter);
        }
        #endregion
    }
}
