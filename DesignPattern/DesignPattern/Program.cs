using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var c = new Computer()
            {
                Cpu = new IntelCpu(),
                Memory = new KingstonMemory(),
                HardDisk = new XiJieHardDisk()
            };

            c.Run();
        }
    }
    
    public class Computer
    {
        /// <summary>
        /// CPU属性
        /// </summary>
        public ICpu Cpu { get; set; }
        
        /// <summary>
        /// 内存属性
        /// </summary>
        public IMemory Memory { get; set; }
        
        /// <summary>
        /// 硬盘属性
        /// </summary>
        public IHardDisk HardDisk { get; set; }

        /// <summary>
        /// 运行
        /// </summary>
        public void Run()
        {
            Console.WriteLine("运行计算机");
            var data = this.HardDisk.Get();
            Console.WriteLine("从硬盘上获取的数据是：" + data);
            this.Cpu.Run();
            this.Memory.Save();
        }
    }
    
    /// <summary>
    /// 硬盘接口
    /// </summary>
    public interface IHardDisk
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="data">数据</param>
        void Save(string data);
        
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据</returns>
        string Get();
    }

    /// <summary>
    /// 内存接口
    /// </summary>
    public interface IMemory
    {
        /// <summary>
        /// 保存
        /// </summary>
        void Save();
    }

    /// <summary>
    /// CPU接口
    /// </summary>
    public interface ICpu
    {
        /// <summary>
        /// 运行
        /// </summary>
        void Run();
    }

    /// <summary>
    /// 希捷硬盘类
    /// </summary>
    public class XiJieHardDisk : IHardDisk
    {
        /// <summary>
        /// <inheritdoc cref="IHardDisk"/>
        /// </summary>
        /// <param name="data">数据</param>
        public void Save(string data) => Console.WriteLine("使用希捷硬盘存储数据为：" + data);

        /// <summary>
        /// <inheritdoc cref="IHardDisk"/>
        /// </summary>
        /// <returns>数据</returns>
        public string Get()
        {
            Console.WriteLine("使用希捷希捷硬盘取数据");
            return "数据";
        }
    }

    /// <summary>
    /// 金士顿内存
    /// </summary>
    public class KingstonMemory : IMemory
    {
        /// <summary>
        /// 保存
        /// </summary>
        public void Save() => Console.WriteLine("使用金士顿内存条");
    }

    /// <summary>
    /// CPU
    /// </summary>
    public class IntelCpu : ICpu
    {
        /// <summary>
        /// 运行
        /// </summary>
        public void Run() => Console.WriteLine("使用Intel处理器");
    }

    
}