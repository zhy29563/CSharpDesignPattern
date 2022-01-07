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
    /// 懒汉式：简单线程安全
    /// </summary>
    public sealed class Singleton
    {
        private static readonly object Locker = new object();
        private static Singleton _instance = null;
        public static Singleton Instance
        {
            get
            {
                lock (Locker)
                {
                    return _instance ?? (_instance = new Singleton());
                }
            }
        }
    
        private Singleton() { }
    }
}