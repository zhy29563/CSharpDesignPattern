using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var agent = new Agent()
            {
                Star = new Star() {Name = "林青霞"},
                Fans = new Fans() {Name = "李四"},
                Company = new Company() {Name = "新宇宙媒体公司"}
            };
            
            agent.Meeting();
            agent.Business();
        }
    }
    
    /// <summary>
    /// 明星类
    /// </summary>
    public class Star
    {
        public string Name {get;set;}
    }

    /// <summary>
    /// 粉丝类
    /// </summary>
    public class Fans
    {
        public string Name {get;set;}
    }

    /// <summary>
    /// 公司类
    /// </summary>
    public class Company
    {
        public string Name {get;set;}
    }

    /// <summary>
    /// 经纪人类
    /// </summary>
    public class Agent
    {
        public Star Star { get; set; }
        public Fans Fans { get; set; }
        public Company Company { get; set; }

        /// <summary>
        /// 粉丝见面会功能
        /// </summary>
        public void Meeting() => Console.WriteLine(Star.Name + "和粉丝" + Fans.Name + "见面");

        /// <summary>
        /// 业务洽谈功能
        /// </summary>
        public void Business() => Console.WriteLine(Star.Name + "和" + Company.Name + "洽谈");
    }
}