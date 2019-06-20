using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Common.DesignPattern
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public class Singleton
    {
        //定义一个静态唯一变量来保存实例
        private static Singleton uniqueInstance;

        //定义一个标识来判断线程同步
        private static readonly object locker = new object();

        //定义私有构造函数，使外部无法创建实例
        private Singleton()
        {

        }

        /// <summary>
        /// 提供一个全局访问方法
        /// </summary>
        /// <returns></returns>
        public static Singleton GetInstance()
        {
            //当第一个线程运行到这里，会对locker对象进行加锁
            //当第二个线程运行到这里，先检测locker是否被锁定，如正被锁定则会挂起等待第一个线程解锁
            //lock语句运行完之后，会对对象进行解锁
            //双重锁定
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    //如果实例不存在，则创建，否则直接返回
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new Singleton();
                    }
                }
            }

            return uniqueInstance;
        }


    }
}
