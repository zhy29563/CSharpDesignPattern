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
    /// 不完全懒汉式，不加锁的线程安全
    /// </summary>
    public sealed class Singleton
    {
        /// <summary>
        /// 显式的静态构造函数用来告诉C#编译器在其内容实例化之前不要标记其类型
        /// </summary>
        static Singleton() { }
        private Singleton() { }
        public static Singleton Instance { get; } = new Singleton();
    }
}