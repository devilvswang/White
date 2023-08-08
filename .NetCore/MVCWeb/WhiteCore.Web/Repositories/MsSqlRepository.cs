﻿using System;
using System.Collections.Generic;
using System.Linq;
using WhiteCore.Web.Models;

namespace WhiteCore.Web.Repositories
{
    public class MsSqlRepository
    {
        private MsSqlDbContext DbContext { get; }

        public MsSqlRepository(MsSqlDbContext dbContext)
        {
            //在构造函数中注入DbContext
            this.DbContext = dbContext;
        }

        //添加
        public int Add(UserInfoEntity user)
        {
            using (DbContext)
            {
                //由于我们在UserEntity.Id配置了自增列的Attribute，EF执行完成后会自动把自增列的值赋值给user.Id
                DbContext.UserInfo.Add(user);
                return DbContext.SaveChanges();
            }

        }

        //删除
        public int Delete(int id)
        {
            using (DbContext)
            {
                var userFromContext = DbContext.UserInfo.FirstOrDefault(u => u.ID == id);
                DbContext.UserInfo.Remove(userFromContext);
                return DbContext.SaveChanges();
            }
        }

        //更新
        public int Update(UserInfoEntity user)
        {
            using (DbContext)
            {
                var userFromContext = DbContext.UserInfo.FirstOrDefault(u => u.ID == user.ID);
                userFromContext.Name = user.Name;
                userFromContext.Age = user.Age;
                return DbContext.SaveChanges();
            }
        }

        //查询
        public UserInfoEntity QueryById(int id)
        {
            using (DbContext)
            {
                return DbContext.UserInfo.FirstOrDefault(u => u.ID == id);
            }
        }

        //查询集合
        public List<UserInfoEntity> QueryByAge(int age)
        {
            using (DbContext)
            {
                return DbContext.UserInfo.Where(u => u.Age == age).ToList();
            }
        }

        //查看指定列
        public List<string> QueryNameByAge(int age)
        {
            using (DbContext)
            {
                return DbContext.UserInfo.Where(u => u.Age == age).Select(u => u.Name).ToList();
            }
        }

        //分页查询
        public List<UserInfoEntity> QueryUserPaging(int pageSize, int page)
        {
            using (DbContext)
            {
                return DbContext.UserInfo.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
        }

        //事务：将年龄<0的用户修改年龄为0
        public int FixAge()
        {
            using (DbContext)
            {
                using (var transaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userListFromContext = DbContext.UserInfo.Where(u => u.Age < 0);
                        foreach (UserInfoEntity u in userListFromContext)
                        {
                            u.Age = 0;
                        }
                        var count = DbContext.SaveChanges();
                        transaction.Commit();
                        return count;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
    }
}