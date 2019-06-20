using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Common.DesignPattern.FactoryMethod
{
    /// <summary>
    /// 工厂模式
    /// 在工厂方法模式中，工厂类与具体产品类具有平行的等级结构，它们之间是一一对应的
    /// 工厂方法模式通过面向对象编程中的多态性来将对象的创建延迟到具体工厂中，从而解决了简单工厂模式中存在的问题，也很好地符合了开放封闭原则（即对扩展开发，对修改封闭）。
    /// </summary>
    public class FactoryMethod
    {
        public string GetFood()
        {
            var food = "";

            var tomatoFactory = new TomatoAndEggFactory().CreateFoodFactory();

            food = tomatoFactory.Print();

            return food;
        }
    }

    /// <summary>
    /// 菜的抽象类
    /// </summary>
    public abstract class Food
    {
        //输出点了什么菜
        public abstract string Print();
    }

    public class TomatoAndEgg : Food
    {
        public override string Print()
        {
            return "西红柿炒蛋";
        }
    }

    public class PotatoAndPork : Food
    {
        public override string Print()
        {
            return "土豆肉丝";
        }
    }

    //抽象工厂
    public abstract class Creator
    {
        //工厂方法
        public abstract Food CreateFoodFactory();
    }

    //西红柿炒蛋工厂
    public class TomatoAndEggFactory : Creator
    {
        public override Food CreateFoodFactory()
        {
            return new TomatoAndEgg();
        }
    }


    //土豆肉丝工厂
    public class PotatoAndPorkFactory : Creator
    {
        public override Food CreateFoodFactory()
        {
            return new PotatoAndPork();
        }
    }


}
