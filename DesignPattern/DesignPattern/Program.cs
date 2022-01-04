using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var doorA = new ASafetyDoor();
            doorA.AntiTheft();
            doorA.Fireproof();
            doorA.Waterproof();

            Console.WriteLine("============");
            var doorB = new BSafetyDoor();
            doorB.AntiTheft();
            doorB.Fireproof();
        }
    }
    
    /// <summary>
    /// 防盗接口
    /// </summary>
    public interface IAntiTheft
    {
        /// <summary>
        /// 防盗功能
        /// </summary>
        void AntiTheft();
    }

    /// <summary>
    /// 防水接口
    /// </summary>
    public interface IWaterproof
    {
        /// <summary>
        /// 防水功能
        /// </summary>
        void Waterproof();
    }

    /// <summary>
    /// 防火接口
    /// </summary>
    public interface IFireproof
    {
        /// <summary>
        /// 防火功能
        /// </summary>
        void Fireproof();
    }


    /// <summary>
    /// A 品牌安全门
    /// </summary>
    public class ASafetyDoor :  IAntiTheft, IFireproof, IWaterproof
    {
        /// <summary>
        /// <inheritdoc cref="AntiTheft"/>
        /// </summary>
        public void AntiTheft() => Console.WriteLine("防盗");

        /// <summary>
        /// <inheritdoc cref="Fireproof"/>
        /// </summary>
        public void Fireproof() => Console.WriteLine("防火");

        /// <summary>
        /// <inheritdoc cref="Waterproof"/>
        /// </summary>
        public void Waterproof() => Console.WriteLine("防水");
    }

    /// <summary>
    /// B 品牌安全门
    /// </summary>
    public class BSafetyDoor : IAntiTheft, IFireproof
    {
        /// <summary>
        /// <inheritdoc cref="AntiTheft"/>
        /// </summary>
        public void AntiTheft() => Console.WriteLine("防盗");

        /// <summary>
        /// <inheritdoc cref="Fireproof"/>
        /// </summary>
        public void Fireproof() => Console.WriteLine("防火");
    }
}