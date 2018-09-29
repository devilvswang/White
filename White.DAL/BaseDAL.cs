using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace White.DAL
{
    public class BaseDAL<T> where T : class,new()
    {
        #region 0.0 EF 上下文对象 #DbContext db
        /// <summary>
        /// EF 上下文对象
        /// </summary>
        protected DbContext db;
        #endregion

        #region 0.1 构造函数， 用于创建EF上下文对象 +BaseDAL(string context)
        /// <summary>
        /// 构造函数， 用于创建EF上下文对象
        /// </summary>
        /// <param name="context">指定创建哪个上下文对象</param>
        public BaseDAL(string context)
        {
            db = DBContextFactory.GetDBContext(context);
        } 
        #endregion


        #region 1.0 添加数据 +int Add(T model)
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model">要添加的实体对象</param>
        /// <returns>返回受影响行数</returns>
        public int Add(T model)
        {
            db.Set<T>().Add(model);
            return db.SaveChanges();
        }
        #endregion

        #region 1.1 添加List集合数据 +int Add(List<T> list)
        /// <summary>
        /// 添加List集合数据
        /// </summary>
        /// <param name="model">要添加的实体对象集合</param>
        /// <returns>返回受影响行数</returns>
        public int Add(List<T> list)
        {
            foreach (var model in list)
            {
                db.Set<T>().Add(model);
            }            
            return db.SaveChanges();
        }
        #endregion

        #region 1.2 将要新增的数据附加到EF上下文中 +void Attach(T model)
        /// <summary>
        /// 将要新增的数据附加到EF上下文中
        /// </summary>
        /// <param name="model">要添加的实体对象</param>
        public void Attach(T model)
        {
            db.Set<T>().Add(model);
        }
        #endregion

        #region 1.3 将要新增的数据集合附加到EF上下文中 +void Attach(List<T> list)
        /// <summary>
        /// 将要新增的数据集合附加到EF上下文中
        /// </summary>
        /// <param name="list">要添加的实体对象集合</param>
        public void Attach(List<T> list)
        {
            foreach (var model in list)
            {
                db.Set<T>().Add(model);
            }
        }
        #endregion


        #region 2.0 删除数据 +int Delete(T model)
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="model">要删除的实体对象，主键必须要有数据</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(T model)
        {
            db.Set<T>().Attach(model);
            db.Set<T>().Remove(model);
            return db.SaveChanges();
        }
        #endregion

        #region 2.1 删除List集合数据 +int Delete(List<T> list)
        /// <summary>
        /// 删除List集合数据<T>
        /// </summary>
        /// <param name="model">要删除的List集合，主键必须要有数据</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(List<T> list)
        {
            foreach (var model in list)
            {
                db.Set<T>().Attach(model);
                db.Set<T>().Remove(model);
            }
            return db.SaveChanges();
        }
        #endregion

        #region 2.2 根据条件批量删除数据 + int Delete(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 根据条件批量删除数据
        /// </summary>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <returns></returns>
        public int Delete(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).Delete();
        }
        #endregion

        #region 2.3 从EF上下文中移除数据 +void Remove(T model)
        /// <summary>
        /// 从EF上下文中移除数据
        /// </summary>
        /// <param name="model">要移除的实体对象，主键必须要有数据</param>
        public void Remove(T model)
        {
            db.Set<T>().Attach(model);
            db.Set<T>().Remove(model);
        }
        #endregion

        #region 2.4 从EF上下文中称除List集合数据 +void Remove(List<T> list)
        /// <summary>
        /// 从EF上下文中称除List集合数据<T>
        /// </summary>
        /// <param name="model">要称除的List集合，主键必须要有数据</param>
        public void Remove(List<T> list)
        {
            foreach (var model in list)
            {
                db.Set<T>().Attach(model);
                db.Set<T>().Remove(model);
            }
        }
        #endregion


        #region 3.0 修改数据 +int Update(T model, params string[] strs)
        /// <summary>
        /// 修改数据 根据指定的列名
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="proNames">要修改的字段</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(T model, params string[] proNames)
        {
            //关闭实体验证
            db.Configuration.ValidateOnSaveEnabled = false;

            DbEntityEntry entry = db.Entry<T>(model);
            entry.State = System.Data.Entity.EntityState.Unchanged;


            foreach (string proName in proNames)
            {
                entry.Property(proName).IsModified = true;
            }
            return db.SaveChanges();
        }
        #endregion

        #region 3.1 批量修改数据 +int Update(List<T> list, params string[] proNames)
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="list">要修改的List数据集合</param>
        /// <param name="proNames">要修改的字段</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(List<T> list, params string[] proNames)
        {
            //关闭实体验证
            db.Configuration.ValidateOnSaveEnabled = false;

            foreach (var model in list)
            {
                DbEntityEntry entry = db.Entry<T>(model);
                entry.State = System.Data.Entity.EntityState.Unchanged;

                foreach (string proName in proNames)
                {
                    entry.Property(proName).IsModified = true;
                }
            }

            return db.SaveChanges();
        }
        #endregion

        #region 3.2 根据条件批量更新数据 + int Update(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> updateLambda)
        /// <summary>
        /// 根据条件批量更新数据
        /// </summary>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <param name="updateLambda">更新内容Lambda表达式</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(Expression<Func<T, bool>> whereLambda, Expression<Func<T, T>> updateLambda)
        {
            return db.Set<T>().Where(whereLambda).Update(updateLambda);
        }
        #endregion


        #region 4.0 保存对代理类的修改 +int SaveChange(T model)
        /// <summary>
        /// 保存对代理类的修改，必须是已经从数据库获取数据的代理类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveChange()
        {
            return db.SaveChanges();
        }
        #endregion

        #region 4.1 关闭EF实体跟踪 +void CloseDetectChanges()
        /// <summary>
        /// 关闭EF实体跟踪
        /// 保存上下文更改时，由于EF会对附加到上下文的实体对象进行验证，造成性能极差，关闭EF实体跟踪提升性能
        /// </summary>
        public void CloseDetectChanges()
        {
            db.Configuration.AutoDetectChangesEnabled = false;
        } 
        #endregion


        #region 5.0 根据条件查询对象 +T GetModel(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 根据条件查询对象
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <returns>返回实体对象</returns>
        public T GetModel(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).FirstOrDefault();
        }
        #endregion

        #region 5.1 根据排序条件获取第一个对象 +T GetModel<Tkey>(Expression<Func<T, Tkey>> orderLambda, bool isAsc)
        /// <summary>
        /// 根据排序条件获取第一个对象
        /// </summary>
        /// <typeparam name="Tkey">排序字段</typeparam>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public T GetModel<Tkey>(Expression<Func<T, Tkey>> orderLambda, bool isAsc)
        {
            return isAsc
                ? db.Set<T>().OrderBy(orderLambda).FirstOrDefault()
                : db.Set<T>().OrderByDescending(orderLambda).FirstOrDefault();
        } 
        #endregion

        #region 5.2 根据条件获取排序后第一个对象 + T GetModel<Tkey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> orderLambda, bool isAsc)
        /// <summary>
        /// 根据条件获取排序后第一个对象
        /// </summary>
        /// <typeparam name="Tkey">排序字段</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public T GetModel<Tkey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> orderLambda, bool isAsc)
        {
            return isAsc
                ? db.Set<T>().Where(whereLambda).OrderBy(orderLambda).FirstOrDefault()
                : db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).FirstOrDefault();
        } 
        #endregion


        #region 6.0 查询记录条数 +int GetCount()
        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <returns>返回条数</returns>
        public int GetCount()
        {
            return db.Set<T>().Count();
        }
        #endregion

        #region 6.1 根据条件查询记录条数 +int GetCount(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 根据条件查询记录条数
        /// </summary>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <returns>返回条数</returns>
        public int GetCount(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).Count();
        }
        #endregion


        #region 7.0 查询所有数据 +List<T> GetList()
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>返回泛型List集合</returns>
        public List<T> GetList()
        {
            return db.Set<T>().ToList();
        }
        #endregion

        #region 7.1 查询排序后所有数据 +List<T> GetList<Tkey>(Expression<Func<T, Tkey>> orderLambda, bool isAsc)
        /// <summary>
        /// 查询排序后所有数据
        /// </summary>
        /// <typeparam name="Tkey">排序列名</typeparam>
        /// <param name="orderLambda">排序Lambda表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>返回排序后的泛型List集合</returns>
        public List<T> GetList<Tkey>(Expression<Func<T, Tkey>> orderLambda, bool isAsc)
        {
            return
                isAsc ?
                db.Set<T>().OrderBy(orderLambda).ToList()
                :
                db.Set<T>().OrderByDescending(orderLambda).ToList();
        }
        #endregion

        #region 7.2 根据条件查询所有数据 +List<T> GetList(Expression<Func<T, bool>> whereLambda)
        /// <summary>
        /// 根据条件查询所有数据
        /// </summary>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <returns>返回泛型List集合</returns>
        public List<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).ToList();
        }
        #endregion

        #region 7.3 根据条件查询排序后的所有数据 +List<T> GetList<Tkey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> orderLambda, bool isAsc)
        /// <summary>
        /// 根据条件查询排序后的所有数据
        /// </summary>
        /// <typeparam name="Tkey">排序字段</typeparam>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <param name="orderLambda">排序Lambda表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>返回排序后的泛型List集合</returns>
        public List<T> GetList<Tkey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> orderLambda, bool isAsc)
        {
            return
                isAsc ?
                db.Set<T>().Where(whereLambda).OrderBy(orderLambda).ToList()
                :
                db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).ToList();
        }
        #endregion

        #region 7.4 根据条件查询排序后的所有数据 +List<T> GetList<Tkey1,Tkey2>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey1>> orderLambda1, bool isAsc1, Expression<Func<T, Tkey2>> orderLambda2, bool isAsc2)
        /// <summary>
        /// 根据条件查询排序后的所有数据
        /// </summary>
        /// <typeparam name="Tkey">排序字段</typeparam>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <param name="orderLambda1">排序Lambda表达式1</param>
        /// <param name="isAsc1">是否升序1</param>
        /// <param name="orderLambda2">排序Lambda表达式2</param>
        /// <param name="isAsc2">是否升序2</param>
        /// <returns>返回排序后的泛型List集合</returns>
        public List<T> GetList<Tkey1,Tkey2>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey1>> orderLambda1, bool isAsc1, Expression<Func<T, Tkey2>> orderLambda2, bool isAsc2)
        {
            var iQuery = db.Set<T>().Where(whereLambda);

            var iOrderQuery = isAsc1 ? iQuery.OrderBy(orderLambda1) : iQuery.OrderByDescending(orderLambda1);

            iOrderQuery = isAsc2 ? iOrderQuery.ThenBy(orderLambda2) : iOrderQuery.ThenByDescending(orderLambda2);

            return iOrderQuery.ToList();
        }
        #endregion

                
        #region 8.0 查询排序后的分页数据  +List<T> GetPagedList<Tkey>(int pageIndex, int pageSize, Expression<Func<T, Tkey>> orderLambda, bool isAsc)
        /// <summary>
        /// 查询排序后的分页数据
        /// </summary>
        /// <typeparam name="Tkey">排序字段</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="orderLambda">排序Lambda表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>返回排序后的分页泛型List集合</returns>
        public List<T> GetPagedList<Tkey>(int pageIndex, int pageSize, Expression<Func<T, Tkey>> orderLambda, bool isAsc = true)
        {
            return
                isAsc ?
                db.Set<T>().OrderBy(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
                :
                db.Set<T>().OrderByDescending(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        #endregion

        #region 8.1 根据条件查询排序分页数据 +List<T> GetPagedList<Tkey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda,Expression<Func<T,Tkey>> orderLambda, bool isAsc)
        /// <summary>
        /// 根据条件查询排序分页数据
        /// </summary>
        /// <typeparam name="Tkey">排序字段</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <param name="orderLambda">排序Lambda表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>返回排序后的泛型List集合</returns>
        public List<T> GetPagedList<Tkey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> orderLambda, bool isAsc)
        {
            return
                isAsc ?
                db.Set<T>().Where(whereLambda).OrderBy(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
                :
                db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        #endregion

        #region 8.2 根据条件查询多条件排序分页数据 +List<T> GetPagedList<Tkey1,Tkey2>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> orderLambda1, bool isAsc1, Expression<Func<T, Tkey>> orderLambda2, bool isAsc2)
        /// <summary>
        /// 根据条件查询多条件排序分页数据
        /// </summary>
        /// <typeparam name="Tkey">排序字段</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <param name="orderLambda1">排序Lambda表达式1</param>
        /// <param name="isAsc1">是否升序1</param>
        /// <param name="orderLambda2">排序Lambda表达式2</param>
        /// <param name="isAsc2">是否升序2</param>
        /// <returns>返回排序后的泛型List集合</returns>
        public List<T> GetPagedList<Tkey1,Tkey2>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey1>> orderLambda1, bool isAsc1, Expression<Func<T, Tkey2>> orderLambda2, bool isAsc2)
        {
            var iQuery = db.Set<T>().Where(whereLambda);

            var iOrderQuery = isAsc1 ? iQuery.OrderBy(orderLambda1) : iQuery.OrderByDescending(orderLambda1);

            iOrderQuery = isAsc2 ? iOrderQuery.ThenBy(orderLambda2) : iOrderQuery.ThenByDescending(orderLambda2);

            return iOrderQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        #endregion


        #region 9.0 根据条件获某一列的SUM值 +decimal GetSum(Expression<Func<T, bool>> whereLambda, Expression<Func<T, decimal?>> selectLambda)
        /// <summary>
        /// 根据条件获某一列的SUM值
        /// </summary>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <param name="selectLambda">查询某列Lambda表达式</param>
        /// <returns>返回SUM值</returns>
        public decimal GetSum(Expression<Func<T, bool>> whereLambda, Expression<Func<T, decimal?>> selectLambda)
        {
            return (decimal)(db.Set<T>().Where(whereLambda).Sum(selectLambda) ?? 0);
        }
        #endregion

        #region 9.1 根据条件获某一列的MIN值 +decimal GetMin(Expression<Func<T, bool>> whereLambda, Expression<Func<T, decimal?>> selectLambda)
        /// <summary>
        /// 根据条件获某一列的MIN值
        /// </summary>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <param name="selectLambda">查询某列Lambda表达式</param>
        /// <returns>返回MIN值</returns>
        public decimal? GetMin(Expression<Func<T, bool>> whereLambda, Expression<Func<T, decimal?>> selectLambda)
        {
            return db.Set<T>().Where(whereLambda).Min(selectLambda);
        }
        #endregion

        #region 9.2 根据条件获某一列的MAX值 +decimal GetMax(Expression<Func<T, bool>> whereLambda, Expression<Func<T, decimal?>> selectLambda)
        /// <summary>
        /// 根据条件获某一列的MAX值
        /// </summary>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <param name="selectLambda">查询某列Lambda表达式</param>
        /// <returns>返回MAX值</returns>
        public decimal? GetMax(Expression<Func<T, bool>> whereLambda, Expression<Func<T, decimal?>> selectLambda)
        {
            return db.Set<T>().Where(whereLambda).Max(selectLambda);
        }
        #endregion

        #region 9.3 根据条件查询去除重复数据列的集合 +List<Tkey> GetDistinctList<Tkey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> selectLambda)
        /// <summary>
        /// 根据条件查询去除重复数据列的集合
        /// </summary>
        /// <typeparam name="Tkey">要去除重复数据的列名</typeparam>
        /// <param name="whereLambda">条件lamdba表达式</param>
        /// <param name="selectLambda">去除重复列lambda表达式</param>
        /// <returns>返回List集合</returns>
        public List<Tkey> GetDistinctList<Tkey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> selectLambda)
        {
            return db.Set<T>().Where(whereLambda).Select(selectLambda).Distinct().ToList();
        }
        #endregion


        #region 10.0 获取IQueryable查询接口 + IQueryable<T> GetIQueryable()
        /// <summary>
        /// 获取IQueryable查询接口
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetIQueryable()
        {
            return db.Set<T>().AsNoTracking().AsQueryable();
        }
        #endregion


        //执行原生SQL语句

        #region 1.0 对数据库执行给定的 DDL/DML 命令 +int ExecuteSqlCommand(string sql, params object[] parameter)
        /// <summary>
        /// 对数据库执行给定的 DDL/DML 命令
        /// </summary>
        /// <param name="sql">sql命令</param>
        /// <param name="parameter">参数</param>
        /// <returns>返回数据库执行结果</returns>
        public int ExecuteSqlCommand(string sql, params object[] parameter)
        {
            return db.Database.ExecuteSqlCommand(sql, parameter);
        }
        #endregion

        #region 1.1 执行原生SQL查询返回泛型List集合 +List<T> ExecuteSqlList(string sql, params object[] parameter)
        /// <summary>
        /// 执行原生SQL查询返回泛型List集合
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数</param>
        /// <returns>返回泛型List信</returns>
        public List<T> ExecuteSqlList(string sql, params object[] parameter)
        {
            return db.Database.SqlQuery<T>(sql, parameter).ToList();
        }
        #endregion
        #region 1.1.1执行原生SQL查询返回泛型List集合 (泛型参数由方法决定) + List<Tenty> ExecuteSqlList<Tenty>(string sql, params object[] parameter)
        /// <summary>
        /// 执行原生SQL查询返回泛型List集合 (泛型参数由方法决定)
        /// </summary>
        /// <typeparam name="Tenty"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<Tenty> ExecuteSqlList<Tenty>(string sql, params object[] parameter)
        {
            return db.Database.SqlQuery<Tenty>(sql, parameter).ToList();
        }
        #endregion
        /// <summary>
        /// 执行原生SQL查询返回泛型int
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数</param>
        /// <returns>返回泛型List信</returns>
        public List<int> ExecuteSqlInt(string sql, params object[] parameter)
        {
            return db.Database.SqlQuery<int>(sql, parameter).ToList();
        }

        #region 1.2 根据Oracle中序列名，查出下一序列值 +int GetNextVal(string sequenceName)
        /// <summary>
        /// 根据Oracle中序列名，查出下一序列值
        /// </summary>
        /// <param name="sequenceName"></param>
        /// <returns></returns>
        public int GetNextVal(string sequenceName)
        {
            string sql = string.Format("select {0}.Nextval from dual", sequenceName);

            return db.Database.SqlQuery<int>(sql).FirstOrDefault();
        } 
        #endregion

    }
}
