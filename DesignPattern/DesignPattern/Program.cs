using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var graphicSystem = new GraphicSystem();
            var rectangle = new Rectangle();
            rectangle.SetLength(20);
            rectangle.SetWidth(10);
            graphicSystem.Rectangle = rectangle;
            
            graphicSystem.Resize();
            graphicSystem.PrintLengthAndWidth();

            Console.WriteLine("============");

            var square = new Square();
            square.SetLength(10);
            graphicSystem.Rectangle = square;
            graphicSystem.Resize();
            graphicSystem.PrintLengthAndWidth();
        }
    }

    /// <summary>
    /// 图形系统
    /// </summary>
    public class GraphicSystem
    {
        /// <summary>
        /// 基类对象属性
        /// </summary>
        public Rectangle Rectangle { get; set; }
        
        /// <summary>
        /// 当宽度小于等于长度时，直到宽度大于长度
        /// </summary>
        public void Resize()
        {
            var index = 0;
            while (this.Rectangle.GetWidth() <= this.Rectangle.GetLength())
            {
                Console.WriteLine($"正在执行第 {index++} 次循环");
                this.Rectangle.SetWidth(this.Rectangle.GetWidth() + 1);
            }
        }

        /// <summary>
        /// 打印当前的长度和宽度
        /// </summary>
        public void PrintLengthAndWidth()
        {
            Console.WriteLine(this.Rectangle.GetLength());
            Console.WriteLine(this.Rectangle.GetWidth());
        }
    }
    
    /// <summary>
    /// 矩形类
    /// </summary>
    public class Rectangle
    {
        /// <summary>
        /// 矩形长度
        /// </summary>
        private double length;
        /// <summary>
        /// 矩形宽度
        /// </summary>
        private double width;

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns>长度</returns>
        public double GetLength() => this.length;

        /// <summary>
        /// 设置长度
        /// </summary>
        /// <param name="length">长度</param>
        public virtual void SetLength(double length) => this.length = length;

        /// <summary>
        /// 获取宽度
        /// </summary>
        /// <returns>宽度</returns>
        public double GetWidth() => this.width;

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        public virtual void SetWidth(double width)=>this.width = width;
    }

    /// <summary>
    /// 正方形类
    /// </summary>
    public class Square : Rectangle
    {
        /// <summary>
        /// <inheritdoc cref="Rectangle"/>
        /// </summary>
        /// <param name="width">宽度</param>
        public override void SetWidth(double width)
        {
            base.SetLength(width);
            base.SetWidth(width);
        }

        /// <summary>
        /// <inheritdoc cref="Rectangle"/>
        /// </summary>
        /// <param name="length">长度</param>
        public override void SetLength(double length)
        {
            base.SetLength(length);
            base.SetWidth(length);
        }
    }
}