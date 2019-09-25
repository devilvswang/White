using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Common.DesignPattern.BuilderPattern
{
    /// <summary>
    /// 建造者模式
    /// 建造者模式使得建造代码与表示代码的分离，可以使客户端不必知道产品内部组成的细节，从而降低了客户端与具体产品之间的耦合度
    /// 抽象工厂模式解决了“系列产品”的需求变化，而建造者模式解决的是 “产品部分” 的需要变化
    /// 建造者隐藏了具体产品的组装过程，所以要改变一个产品的内部表示，只需要再实现一个具体的建造者就可以了，从而能很好地应对产品组成组件的需求变化
    /// </summary>
    class BuilderPattern
    {
        private Builder builder = new Builder();

        //客户端获取产品
        public void Client()
        {
            builder.Construct();
            var product = builder.GetProduct();
        }
    }

    /// <summary>
    /// 产品
    /// </summary>
    public class Product
    {
        //产品组件集合
        private List<string> parts = new List<string>();

        //把单个组件添加到产品组件集合
        public void Add(string part)
        {
            parts.Add(part);
        }

        public void Show()
        {

        }
    }

    /// <summary>
    /// 建造者模式的演变,省略了指挥者角色和抽象建造者角色
    /// 此时具体建造者角色扮演了指挥者和建造者两个角色
    /// </summary>
    public class Builder
    {
        private Product product = new Product();

        public void BuildPartA()
        {
            product.Add("PartA");
        }

        public void BuildPartB()
        {
            product.Add("PartB");
        }

        public Product GetProduct()
        {
            return product;
        }

        //指挥者角色
        public void Construct()
        {
            BuildPartA();
            BuildPartB();
        }
    }
}
