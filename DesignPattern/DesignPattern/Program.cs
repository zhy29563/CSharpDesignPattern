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
    /// 懒汉式：嵌套类方式
    /// </summary>
    public sealed class Singleton
    {
        private Singleton() { }
    
        public static Singleton Instance => Nested.instance;

        private class Nested
        {
            static Nested() { }
            internal static readonly Singleton instance = new Singleton();
        }
    }
}