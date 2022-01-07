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
    /// 懒汉式：双检查线程安全
    /// </summary>
    public sealed class Singleton
    {
        private static readonly object Locker = new object();
        private static volatile Singleton _instance;
        public static Singleton Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (Locker)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                }
                return _instance;
            }
        }
    
        private Singleton() { }
    }
}