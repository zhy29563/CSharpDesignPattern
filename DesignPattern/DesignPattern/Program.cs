using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var store = new CoffeeStore();
            store.Factory = new LatteCoffeeFactory();
            var coffee = store.OrderCoffee();
            Console.WriteLine(coffee.Name);
	
            Console.WriteLine("----------------------------");
            store.Factory = new AmericanCoffeeFactory();
            store.OrderCoffee();
            Console.WriteLine(coffee.Name);
        }
    }
    
    /// <summary>
    /// 抽象产品
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

    /// <summary>
    /// 具体产品
    /// </summary>
    public class LatteCoffee : Coffee
    {
        /// <summary>
        /// <inheritdoc cref="Name"/>
        /// </summary>
        public override string Name => "拿铁咖啡";
    }

    /// <summary>
    /// 具体产品
    /// </summary>
    public class AmericanCoffee : Coffee
    {
        /// <summary>
        /// <inheritdoc cref="Name"/>
        /// </summary>
        public override string Name => "美式咖啡";
    }

    /// <summary>
    /// 抽象工厂
    /// </summary>
    public interface ICoffeeFactory
    {
        /// <summary>
        /// 制作咖啡功能
        /// </summary>
        /// <returns>咖啡</returns>
        Coffee CreateCoffee();
    }

    /// <summary>
    /// 具体工厂
    /// </summary>
    public class LatteCoffeeFactory : ICoffeeFactory
    {
        /// <inheritdoc cref="Coffee"/>
        public Coffee CreateCoffee()=>new LatteCoffee();
    }

    /// <summary>
    /// 具体工厂
    /// </summary>
    public class AmericanCoffeeFactory : ICoffeeFactory
    {
        /// <inheritdoc cref="Coffee"/>
        public Coffee CreateCoffee() => new AmericanCoffee();
    }
    
    /// <summary>
    /// 咖啡店点餐系统，该系统可以为顾客提供美式咖啡与拿铁咖啡
    /// </summary>
    public class CoffeeStore
    {
        /// <summary>
        /// 咖啡工厂抽象基类属性
        /// </summary>
        public ICoffeeFactory Factory {get;set;}
        
        /// <summary>
        /// 点咖啡功能
        /// </summary>
        /// <returns>咖啡<see cref="Coffee"/></returns>
        public Coffee OrderCoffee()
        {
            var coffee = this.Factory.CreateCoffee();

            // 加奶
            coffee.AddMilk();
            // 加糖
            coffee.AddSugar();

            return coffee;
        }
    }
}