using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var c = new Computer
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
        /// Inter Cpu 属性
        /// </summary>
        public IntelCpu Cpu { get; set; }
        
        /// <summary>
        /// 金士顿 内存 属性
        /// </summary>
        public KingstonMemory Memory { get; set; }
        
        /// <summary>
        /// 希捷 硬盘 属性
        /// </summary>
        public XiJieHardDisk HardDisk { get; set; }

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
    /// 希捷硬盘类
    /// </summary>
    public class XiJieHardDisk
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="data">数据</param>
        public void Save(string data)
        {
            Console.WriteLine("使用希捷硬盘存储数据为：" + data);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据</returns>
        public string Get()
        {
            Console.WriteLine("使用希捷希捷硬盘取数据");
            return "数据";
        }
    }

    /// <summary>
    /// 金士顿内存类
    /// </summary>
    public class KingstonMemory
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save()
        {
            Console.WriteLine("使用金士顿内存条");
        }
    }

    /// <summary>
    /// 因特尔CPU类
    /// </summary>
    public class IntelCpu
    {
        /// <summary>
        /// 运行
        /// </summary>
        public void Run()
        {
            Console.WriteLine("使用Intel处理器");
        }
    }
}