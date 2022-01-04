using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var door = new ASafetyDoor();
            door.AntiTheft();
            door.FireProof();
            door.WaterProof();
        }
    }
    
    /// <summary>
    /// 防盗门接口
    /// </summary>
    public interface ISafetyDoor
    {
        /// <summary>
        /// 防盗功能
        /// </summary>
        void AntiTheft();
        
        /// <summary>
        /// 防火功能
        /// </summary>
        void FireProof();
        
        /// <summary>
        /// 防水功能
        /// </summary>
        void WaterProof();
    }

    /// <summary>
    /// A 品牌功能
    /// </summary>
    public class ASafetyDoor : ISafetyDoor
    {
        /// <summary>
        /// <inheritdoc cref="ISafetyDoor"/>
        /// </summary>
        public void AntiTheft() => Console.WriteLine("防盗");

        /// <summary>
        /// <inheritdoc cref="ISafetyDoor"/>
        /// </summary>
        public void FireProof() => Console.WriteLine("防火");

        /// <summary>
        /// <inheritdoc cref="ISafetyDoor"/>
        /// </summary>
        public void WaterProof() => Console.WriteLine("防水");
    }
}