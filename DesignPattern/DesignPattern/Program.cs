using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new RedPetrolCar().Move();
            new RedElectricCar().Move();
            new WhitePetrolCar().Move();
            new WhiteElectricCar().Move();
        }
    }
    
    /// <summary>
    /// 抽象汽车类
    /// </summary>
    public abstract class Car
    {
        /// <summary>
        /// 行驶功能
        /// </summary>
        public abstract void Move();
    }

    /// <summary>
    /// 汽油车类
    /// </summary>
    public abstract class PetrolCar : Car
    {
    }

    /// <summary>
    /// 电动汽车类
    /// </summary>
    public abstract class ElectricCar : Car
    {
    }

    /// <summary>
    /// 红色汽油汽车类
    /// </summary>
    public class RedPetrolCar : PetrolCar
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine(nameof(RedPetrolCar));
    }

    /// <summary>
    /// 白色汽油汽车类
    /// </summary>
    public class WhitePetrolCar : PetrolCar
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine(nameof(WhitePetrolCar));
    }

    /// <summary>
    /// 红色电动汽车类
    /// </summary>
    public class RedElectricCar : ElectricCar
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine(nameof(RedPetrolCar));
    }

    /// <summary>
    /// 白色电动汽车类
    /// </summary>
    public class WhiteElectricCar : ElectricCar
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine(nameof(WhitePetrolCar));
    }
}