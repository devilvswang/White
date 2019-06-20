using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Common.DesignPattern.SimpleFactory
{
    /// <summary>
    /// 简单工厂模式
    /// 优点：1.简单工厂模式解决了客户端直接依赖于具体对象的问题，客户端可以消除直接创建对象的责任，而仅仅是消费产品。简单工厂模式实现了对责任的分割。
    ///       2.简单工厂模式也起到了代码复用的作用，因为之前的实现（自己做饭的情况）中，换了一个人同样要去在自己的类中实现做菜的方法，然后有了简单工厂之后，
    ///         去餐馆吃饭的所有人都不用那么麻烦了，只需要负责消费就可以了。此时简单工厂的烧菜方法就让所有客户共用了。
    /// 缺点：1.工厂类集中了所有产品创建逻辑，一旦不能正常工作，整个系统都会受到影响（通俗地意思就是：一旦餐馆没饭或者关门了，很多不愿意做饭的人就没饭吃了）
    ///       2.系统扩展困难，一旦添加新产品就不得不修改工厂逻辑，这样就会造成工厂逻辑过于复杂。
    /// 场景：1.当工厂类负责创建的对象比较少时可以考虑使用简单工厂模式（）
    ///       2.客户如果只知道传入工厂类的参数，对于如何创建对象的逻辑不关心时可以考虑使用简单工厂模式
    /// </summary>
    public class SimpleFactory
    {
        //想要什么菜直接点，让餐馆去做
        public string GetFood(string wantFoodName)
        {
            var food = "";

            Food shop = FoodSimpleFactory.CreateFood(wantFoodName);

            food = shop.Print();

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

    /// <summary>
    /// 简单工厂，直接炒出菜
    /// </summary>
    public class FoodSimpleFactory
    {
        public static Food CreateFood(string foodName)
        {
            Food food = null;

            if (foodName == "土豆肉丝")
            {
                food = new PotatoAndPork();
            }
            else if (foodName == "西红柿炒蛋")
            {
                food = new TomatoAndEgg();
            }

            return food;
        }
    }

}
