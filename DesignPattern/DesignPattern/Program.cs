using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var instance1 = Singleton.Instance;
            var instance2 = Singleton.Instance;
            Console.WriteLine(instance1 == instance2);
        }
    }
    
    /// <summary>
    /// 懒汉式：延迟初始化方式
    /// </summary>
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> Lazy = new Lazy<Singleton>(()=> new Singleton());
        public static Singleton Instance => Lazy.Value;
        private Singleton() { }
    }
}