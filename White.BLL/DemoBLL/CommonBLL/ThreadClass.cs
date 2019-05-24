using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace White.BLL
{
    public class ThreadClass
    {
        ///<summary>    
        ///线程开始时执行此方法    
        ///</summary>    
        public void InstanceMethod()
        {
            Console.WriteLine("ThreadClass.InstanceMethod is running on another thread.");

            // 线程延迟3000秒，使线程的执行更加明显         
            Thread.Sleep(3000);

            Console.WriteLine("The instance method called by the worker thread has ended.");
        }

        ///<summary>    
        ///线程开始时执行此方法    
        ///</summary>    
        public static void StaticMethod()
        {
            Console.WriteLine("ThreadClass.StaticMethod is running on another thread.");

            //线程延迟5000秒         
            Thread.Sleep(5000);

            Console.WriteLine("The static method called by the worker thread has ended.");
        }

    }

    ///<summary>
    ///线程创建类，封装了线程创建及执行方法
    ///</summary>
    public class Simple
    {
        public static void Main()
        {
            Console.WriteLine("Thread Simple Sample");
            ThreadClass threadObject = new ThreadClass();

            //创建线程对象，并传入serverObject.InstanceMethod方法
            Thread InstanceCaller = new Thread(new ThreadStart(threadObject.InstanceMethod));
            //开始执行线程
            InstanceCaller.Start();

            Console.WriteLine("The Main() thread calls this after " + "starting the new InstanceCaller thread.");

            //创建线程对象，并传入serverObject.InstanceMethod方法
            Thread StaticCaller = new Thread(new ThreadStart(ThreadClass.StaticMethod));
            //开始线程
            StaticCaller.Start();

            Console.WriteLine("The Main() thread calls this after " + "starting the new StaticCaller thread.");
        }
    }
}



