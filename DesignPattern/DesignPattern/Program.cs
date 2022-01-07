using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var store = new CoffeeStore();
            var coffee = store.OrderCoffee("american");
            Console.WriteLine(coffee.Name);
	
            Console.WriteLine("----------------------------");
            coffee = store.OrderCoffee("latte");
            Console.WriteLine(coffee.Name);
        }
    }
    
    /// <summary>
    /// 咖啡的抽象基类
    /// </summary>
    public abstract class Coffee
    {
        /// <summary>
        /// 咖啡名称
        /// </summary>
        public abstract string Name {get; }

        /// <summary>
        /// 加糖功能
        /// </summary>
        public void AddSugar() => Console.WriteLine("加糖");

        /// <summary>
        /// 加奶功能
        /// </summary>
        public void AddMilk() => Console.WriteLine("加奶");
    }

    public class LatteCoffee : Coffee
    {
        /// <summary>
        /// <inheritdoc cref="Name"/>
        /// </summary>
        public override string Name => "拿铁咖啡";
    }

    public class AmericanCoffee : Coffee
    {
        /// <summary>
        /// <inheritdoc cref="Name"/>
        /// </summary>
        public override string Name => "美式咖啡";
    }

    /// <summary>
    /// 简单工厂类
    /// </summary>
    public class SimpleCoffeeFactory
    {
        /// <summary>
        /// 制作咖啡
        /// </summary>
        /// <param name="type">咖啡名</param>
        /// <returns>咖啡<see cref="Coffee"/></returns>
        /// <exception cref="Exception"></exception>
        public static Coffee CreateCoffee(string type)
        {
            Coffee coffee = null;
            switch (type)
            {
                case "american":
                    coffee = new AmericanCoffee();
                    break;
                case "latte":
                    coffee = new LatteCoffee();
                    break;
                default:
                    throw new Exception("对不起，您所点的咖啡没有");
            }

            return coffee;
        }
    }
    
    /// <summary>
    /// 咖啡店点餐系统，该系统可以为顾客提供美式咖啡与拿铁咖啡
    /// </summary>
    public class CoffeeStore
    {
        /// <summary>
        /// 点咖啡功能
        /// </summary>
        /// <param name="type">咖啡名</param>
        /// <returns>咖啡<see cref="Coffee"/></returns>
        public Coffee OrderCoffee(string type)
        {
            var coffee = SimpleCoffeeFactory.CreateCoffee(type);

            // 加奶
            coffee.AddMilk();
            // 加糖
            coffee.AddSugar();

            return coffee;
        }
    }
}