using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Common.DesignPattern.AbstractFactory
{

    public class Client
    {
        public string GetFood()
        {
            //南昌鸭脖
            var nanchangFactory = new NanChangFactory();
            var nanchangYabo = nanchangFactory.CreateYaBo().Print();

            //上海鸭架
            var shanghaiFactoru = new ShangHaiFactory();
            var shanghaiYajia = shanghaiFactoru.CreateYaJia().Print();

            return "";
        }
    }


    /// <summary>
    /// 抽象工厂模式
    /// 抽象工厂模式：提供一个创建产品的接口来负责创建相关或依赖的对象，而不具体明确指定具体类
    /// 抽象工厂模式很难支持新种类产品的变化。这是因为抽象工厂接口中已经确定了可以被创建的产品集合，
    /// 如果需要添加新产品，此时就必须去修改抽象工厂的接口，这样就涉及到抽象工厂类的以及所有子类的改变，这样也就违背了“开发——封闭”原则。
    /// 场景：一个系统不要求依赖产品类实例如何被创建、组合和表达的表达，这点也是所有工厂模式应用的前提。
    ///       这个系统有多个系列产品，而系统中只消费其中某一系列产品
    ///       系统要求提供一个产品类的库，所有产品以同样的接口出现，客户端不需要依赖具体实现。
    /// </summary>
    public abstract class AbstractFactory
    {
        public abstract YaBo CreateYaBo();
        public abstract YaJia CreateYaJia();
    }

    /// <summary>
    /// 南昌工厂
    /// </summary>
    public class NanChangFactory : AbstractFactory
    {
        public override YaBo CreateYaBo()
        {
            return new NanChangYaBo();
        }

        public override YaJia CreateYaJia()
        {
            return new NanChangYaJia();
        }
    }

    /// <summary>
    /// 上海工厂
    /// </summary>
    public class ShangHaiFactory : AbstractFactory
    {
        public override YaBo CreateYaBo()
        {
            return new ShangHaiYaBo();
        }

        public override YaJia CreateYaJia()
        {
            return new ShangHaiYaJia();
        }
    }

    /// <summary>
    /// 鸭脖基础抽象类
    /// </summary>
    public abstract class YaBo
    {
        public abstract string Print();
    }

    /// <summary>
    /// 鸭架基础抽象类
    /// </summary>
    public abstract class YaJia
    {
        public abstract string Print();
    }


    #region 具体产品对象

    public class NanChangYaBo : YaBo
    {
        public override string Print()
        {
            return "南昌的鸭脖";
        }
    }

    public class NanChangYaJia : YaJia
    {
        public override string Print()
        {
            return "南昌的鸭架";
        }
    }

    public class ShangHaiYaBo : YaBo
    {
        public override string Print()
        {
            return "上海的鸭脖";
        }
    }

    public class ShangHaiYaJia : YaJia
    {
        public override string Print()
        {
            return "上海的鸭架";
        }
    }

    #endregion
}
