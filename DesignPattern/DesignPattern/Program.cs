using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new PetrolCar() {Color = new WhiteColor()}.Move();
            new ElectricCar() {Color = new RedColor()}.Move();
        }
    }
    
    /// <summary>
    /// 颜色接口
    /// </summary>
    public interface IColor
    {
        string Name {get;}
    }

    /// <summary>
    /// 红色
    /// </summary>
    public class RedColor : IColor
    {
        public string Name => "Red";
    }

    /// <summary>
    /// 白色
    /// </summary>
    public class WhiteColor : IColor
    {
        /// <summary>
        /// 颜色
        /// </summary>
        public string Name => "Red";
    }

    /// <summary>
    /// 抽象汽车类
    /// </summary>
    public abstract class Car
    {
        /// <summary>
        /// 聚合颜色属性
        /// </summary>
        public IColor Color {get;set;}
        
        /// <summary>
        /// 行驶功能
        /// </summary>
        public abstract void Move();
    }

    /// <summary>
    /// 汽油车类
    /// </summary>
    public class PetrolCar : Car
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine($"{this.Color.Name} {nameof(PetrolCar)} is " + nameof(Move));
    }

    /// <summary>
    /// 电动车类
    /// </summary>
    public class ElectricCar : Car
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine($"{this.Color.Name}  {nameof(ElectricCar)} is " + nameof(Move));
    }
}