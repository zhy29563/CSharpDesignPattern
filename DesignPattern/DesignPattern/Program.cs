using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            IDessertFactory factory = new AmericanDessertFactory();
            var coffee = factory.CreateCoffee();
            var dessert = factory.CreateDessert();
            Console.WriteLine(coffee.Name);
            dessert.Show();
	
            factory = new ItalyDessertFactory();
            coffee = factory.CreateCoffee();
            dessert = factory.CreateDessert();
            Console.WriteLine(coffee.Name);
            dessert.Show();
        }
    }
    
    // 咖啡抽象基类
    public abstract class Coffee
    {
        public abstract string Name { get; }
        public void AddSugar() => Console.WriteLine("加糖");
        public void AddMilk() => Console.WriteLine("加奶");
    }

    // 属于意大利产品族中的咖啡产品
    public class LatteCoffee : Coffee
    {
        public override string Name => "拿铁咖啡";
    }

    // 属于美式产品族中的咖啡产品
    public class AmericanCoffee : Coffee
    {
        public override string Name => "美式咖啡";
    }

    // 甜品基类
    public abstract class Dessert
    {
        public abstract void Show();
    }

    // 属于美式产品族中的甜品产品
    public class MatchaMousse : Dessert
    {
        public override void Show() => Console.WriteLine("抹茶慕斯");
    }

    // 属于意大利产品族中的甜品产品
    public class Trimisu : Dessert
    {
        public override void Show() => Console.WriteLine("提拉米苏");
    }

    // 产品族工厂抽象基类
    public interface IDessertFactory
    {
        Coffee CreateCoffee();
        Dessert CreateDessert();
    }

    // 美式产品族工厂
    public class AmericanDessertFactory : IDessertFactory
    {
        public Coffee CreateCoffee() => new AmericanCoffee();	
        public Dessert CreateDessert() => new MatchaMousse();
    }

    // 意大利产品族工厂
    public class ItalyDessertFactory : IDessertFactory
    {
        public Coffee CreateCoffee() => new LatteCoffee();
        public Dessert CreateDessert() => new Trimisu();
    }
}