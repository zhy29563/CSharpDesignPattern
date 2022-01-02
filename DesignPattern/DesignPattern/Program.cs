using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // 定义输入法的一个实例，使用默认皮肤进行初始化
            var souGouInput = new SouGouInput { Skin = new DefaultSkin() };
            // 显示当前使用的皮肤
            souGouInput.Display();
            
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("开始切换皮肤");
            // 切换为自定义皮肤
            souGouInput.Skin = new CustomSkin();
            // 显示当前使用的皮肤
            souGouInput.Display();
        }
    }
    
    /// <summary>
    /// 输入法皮肤的抽象基类
    /// </summary>
    public abstract class AbstractSkin
    {
        /// <summary>
        /// 用于显示当前所使用的皮肤
        /// </summary>
        public abstract void Display();
    }

    /// <summary>
    /// 默认皮肤
    /// </summary>
    public class DefaultSkin : AbstractSkin
    {
        /// <summary>
        /// <inheritdoc cref="AbstractSkin"/>
        /// </summary>
        public override void Display()
        {
            Console.WriteLine(nameof(DefaultSkin));
        }
    }

    /// <summary>
    /// 自定义皮肤
    /// </summary>
    public class CustomSkin : AbstractSkin
    {
        /// <summary>
        /// <inheritdoc cref="AbstractSkin"/>
        /// </summary>
        public override void Display()
        {
            Console.WriteLine(nameof(CustomSkin));
        }
    }

    /// <summary>
    /// 搜狗输入法类
    /// </summary>
    public class SouGouInput
    {
        /// <summary>
        /// 皮肤抽象积累的成员变量
        /// </summary>
        public AbstractSkin Skin {get;set;}

        /// <summary>
        /// 用于显示当前所使用的皮肤
        /// </summary>
        public void Display()
        {
            Skin?.Display();
        }
    }
}