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
            graphicSystem.Quadrilateral = rectangle;
            
            graphicSystem.Resize();
            graphicSystem.PrintLengthAndWidth();

            Console.WriteLine("============");

            var square = new Square();
            square.SetSide(10);
            graphicSystem.Quadrilateral = square;
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
        public Quadrilateral Quadrilateral { get; set; }
        
        /// <summary>
        /// 当宽度小于等于长度时，直到宽度大于长度
        /// </summary>
        public void Resize()
        {
            if (!(this.Quadrilateral is Rectangle rectangle))
                return;
            
            var index = 0;
            while (this.Quadrilateral.GetWidth() <= this.Quadrilateral.GetLength())
            {
                Console.WriteLine($"正在执行第 {index++} 次循环");
                rectangle.SetWidth(this.Quadrilateral.GetWidth() + 1);
            }
        }

        /// <summary>
        /// 打印当前的长度和宽度
        /// </summary>
        public void PrintLengthAndWidth()
        {
            Console.WriteLine(this.Quadrilateral.GetLength());
            Console.WriteLine(this.Quadrilateral.GetWidth());
        }
    }
    
    /// <summary>
    /// 四边形基类
    /// </summary>
    public interface Quadrilateral 
    {
        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns>长度</returns>
        double GetLength();
        
        /// <summary>
        /// 获取宽度
        /// </summary>
        /// <returns>宽度</returns>
        double GetWidth();
    }
    
    /// <summary>
    /// 矩形类
    /// </summary>
    public class Rectangle : Quadrilateral
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
        /// <inheritdoc cref="Quadrilateral"/>
        /// </summary>
        /// <returns>长度</returns>
        public double GetLength() => this.length;

        /// <summary>
        /// 设置长度
        /// </summary>
        /// <param name="length">长度</param>
        public void SetLength(double length) => this.length = length;

        /// <summary>
        /// <inheritdoc cref="Quadrilateral"/>
        /// </summary>
        /// <returns>宽度</returns>
        public double GetWidth() => this.width;

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        public void SetWidth(double width)=>this.width = width;
    }

    /// <summary>
    /// 正方形类
    /// </summary>
    public class Square : Quadrilateral
    {
        /// <summary>
        /// 正方向边长
        /// </summary>
        private double side; 
        
        /// <summary>
        /// 设置正方向边长
        /// </summary>
        /// <param name="side">边长</param>
        public void SetSide(double side)=>this.side = side; 
        
        /// <summary>
        /// <inheritdoc cref="Quadrilateral"/>
        /// </summary>
        /// <returns>长度</returns>
        public double GetLength() => this.side; 
        
        /// <summary>
        /// <inheritdoc cref="Quadrilateral"/>
        /// </summary>
        /// <returns>宽度</returns>
        public double GetWidth() => this.side;
    }
}