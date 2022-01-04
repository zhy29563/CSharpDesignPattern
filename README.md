# 1. 设计模式简介

设计模式（Design pattern）代表了最佳的实践，通常被`面向对象`的软件开发人员所采用。设计模式是一套被反复使用、多数人知晓的、经过分类编目的、代码设计经验的总结。它描述了在软件设计过程中的一些不断重复发生的问题，以及该问题的解决方案。也就是说，它是解决特定问题的一系列套路，是前辈们的代码设计经验的总结，具有一定的普遍性，可以反复使用。

## 1.1 设计模式分类

根据参考书 `Design Patterns - Elements of Reusable Object-Oriented Software(中文译名：设计模式 - 可复用的面向对象软件元素)`中所提到的，总共有 23 种设计模式。这些模式可以分为三大类：创建型模式（Creational Patterns）、结构型模式（Structural Patterns）、行为型模式（Behavioral Patterns）。

### 1.1.1 创建型模式

用于描述`怎样创建对象`，它的主要特点是将`对象的创建与使用分离`。

1. 单例模式（Singleton Pattern）

2. 工厂模式（Factory Pattern）

    2.1 简单工厂模式（Simple Factory Pattern）

    2.2 工厂方法模式（Factory Method Pattern）

    2.3 抽象工厂模式（Abstract Factory Pattern）

3. 建造者模式（Builder Pattern）

4. 原型模式（Prototype Pattern）

### 1.1.2 结构型模式

用于描述如何将类或对象按某种布局组成更大的结构。

1. 适配器模式（Adapter Pattern）
2. 桥接模式（Bridge Pattern）
3. 组合模式（Composite Pattern）
4. 装饰器模式（Decorator Pattern）
5. 外观模式（Facade Pattern）
6. 享元模式（Flyweight Pattern）
7. 代理模式（Proxy Pattern）

### 1.1.3 行为型模式

用于描述类或对象之间怎样相互协作共同完成单个对象无法单独完成的任务，以及怎样分配职责。

1. 责任链模式（Chain of Responsibility Pattern）
2. 命令模式（Command Pattern）
3. 解释器模式（Interpreter Pattern）
4. 迭代器模式（Iterator Pattern）
5. 中介者模式（Mediator Pattern）
6. 备忘录模式（Memento Pattern）
7. 观察者模式（Observer Pattern）
8. 状态模式（State Pattern）
9. 策略模式（Strategy Pattern）
10. 模板模式（Template Pattern）
11. 访问者模式（Visitor Pattern）



# 2 设计模式原则
## 2.1 开闭原则（Open Close Principle）
> 开闭原则的含义是：`对扩展开放，对修改关闭`。在程序需要进行拓展的时候，不能去修改原有的代码，实现一个热插拔的效果。

想要达到热插拔的效果，我们需要进行抽象（接口和抽象类）。因为抽象灵活性好，适应性广，只要抽象的合理，可以基本保持软件架构的稳定。而软件中*易变的细节*可以从抽象派生来的实现类来进行扩展，当软件需要发生变化时，只需要根据需求重新派生一个实现类来扩展就可以了。

### 2.1.1 案例分析（输入法皮肤）
搜狗输入法的<font color=Red>**皮肤**</font>是输入法背景图片、窗口颜色和声音等元素的组合。用户可以根据自己的喜爱更换输入法的皮肤。这些皮肤有共同的特点，可以为其定义一个抽象类（AbstractSkin），而每个具体的皮肤（DefaultSkin和CustomSkin）是其子类。用户窗体可以根据需要选择或者增加新的主题，而不需要修改原代码，所以它是满足开闭原则的。

```cs
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
```

## 2.2 里氏代换原则（Liskov Substitution Principle）
<font color=red>**任何基类可以出现的地方，子类一定可以出现**</font>。通俗理解：子类可以扩展父类的功能，但不能改变父类原有的功能。换句话说，子类继承父类时，除添加新的方法完成新增功能外，尽量不要重写父类的方法。

如果通过重写父类的方法来完成新的功能，这样写起来虽然简单，但是整个继承体系的可复用性会比较差，特别是运用多态比较频繁时，程序运行出错的概率会非常大。
### 2.2.1 案例分析（正方形不是长方形）
在数学领域里，正方形毫无疑问是长方形，它是一个长宽相等的长方形。我们开发的一个与几何图形相关的软件系统，就可以顺理成章的让正方形继承自长方形。
```cs
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
```

假如我们把一个`普通长方形`赋值给`GraphicSystem`的`Rectangle`属性，调用Resize方法，就会看到长方形宽度逐渐增长的效果，当宽度大于长度，代码就会停止，这种行为的结果符合我们的预期；
假如我们把一个`正方形`赋值给`GraphicSystem`的`Rectangle`属性，调用Resize方法，就会看到正方形的`宽度和长度都在不断增长`，代码会一直运行下去，直至系统产生溢出错误。所以，<font color='red'>**普通的长方形是适合这段代码的，正方形不适合**</font>。因此，**Square类和Rectangle类之间的继承关系违反了里氏代换原则，它们之间的继承关系不成立，正方形不是长方形**。

### 2.2.2 案例改进

我们需要重新设计他们之间的关系。抽象出来一个四边形接口(Quadrilateral)，让Rectangle类和Square类实现Quadrilateral接口。

```csharp
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
```

## 2.3 依赖倒转原则（Dependence Inversion Principle）

高层模块不应该依赖低层模块，两者都应该依赖其抽象；抽象不应该依赖细节，细节应该依赖抽象。简单的说就是要求对抽象进行编程，不要对实现进行编程，这样就降低了客户与实现模块间的耦合。

### 2.3.1 案例分析（组装电脑）

组装一台电脑，需要CPU，硬盘，内存条等配件。这些配件又各自具有多种品牌，例如，CPU有Intel与AMD，硬盘有择希捷与西数，内存条有金士顿与海盗船。

```csharp
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
```

上面组装的电脑能够正常运行，但配件的品牌是固定的，不可以改变。如果用户想使用自己中意的品牌，则上述设计是不满足要求的。

### 2.3.2 案例改进

根据依赖倒转原则进行改进：

代码我们只需要修改Computer类，让Computer类`依赖抽象（各个配件的接口）`，而不是依赖于各个组件具体的实现类。

```csharp
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
```

改进后，`Computer`类的成员`HardDisk`、`Cpu`、`Memory`可以使用实现`IHardDisk`、`ICpu`、`IMemory`接口的任意类型。



## 2.4 接口隔离原则（Interface Segregation Principle）

客户端不应该被迫依赖于它不使用的方法；一个类对另一个类的依赖应该建立在`最小的接口`上。

### 2.4.1 案例分析（安全门）

我们需要创建一个`A品牌`的安全门，该安全门具有`防火`、`防水`、`防盗`的功能。可以将`防火`，`防水`，`防盗`功能提取成`一个接口`，形成`一套规范`。

```csharp
using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var door = new ASafetyDoor();
            door.AntiTheft();
            door.FireProof();
            door.WaterProof();
        }
    }
    
    /// <summary>
    /// 防盗门接口
    /// </summary>
    public interface ISafetyDoor
    {
        /// <summary>
        /// 防盗功能
        /// </summary>
        void AntiTheft();
        
        /// <summary>
        /// 防火功能
        /// </summary>
        void FireProof();
        
        /// <summary>
        /// 防水功能
        /// </summary>
        void WaterProof();
    }

    /// <summary>
    /// A 品牌功能
    /// </summary>
    public class ASafetyDoor : ISafetyDoor
    {
        /// <summary>
        /// <inheritdoc cref="ISafetyDoor"/>
        /// </summary>
        public void AntiTheft() => Console.WriteLine("防盗");

        /// <summary>
        /// <inheritdoc cref="ISafetyDoor"/>
        /// </summary>
        public void FireProof() => Console.WriteLine("防火");

        /// <summary>
        /// <inheritdoc cref="ISafetyDoor"/>
        /// </summary>
        public void WaterProof() => Console.WriteLine("防水");
    }
}
```

上述的设计能够使得`A品牌`的安全门具有`防盗`，`防水`，`防火`的功能，能够满足最初的要求。随着市场需求的开发，发现并不是所有的安全门用户都需要安全门具有`防盗`，`防水`，`防火`的功能，有的仅需要其中两项，甚至一项。如果以上述的`接口`定义仅有`防盗`、`防水`功能的`B品牌`安全门，显然如果实现`ISafetyDoor`接口就违背了接口隔离原则。

### 2.4.2 案例改进

```csharp
using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var doorA = new ASafetyDoor();
            doorA.AntiTheft();
            doorA.Fireproof();
            doorA.Waterproof();

            Console.WriteLine("============");
            var doorB = new BSafetyDoor();
            doorB.AntiTheft();
            doorB.Fireproof();
        }
    }
    
    /// <summary>
    /// 防盗接口
    /// </summary>
    public interface IAntiTheft
    {
        /// <summary>
        /// 防盗功能
        /// </summary>
        void AntiTheft();
    }

    /// <summary>
    /// 防水接口
    /// </summary>
    public interface IWaterproof
    {
        /// <summary>
        /// 防水功能
        /// </summary>
        void Waterproof();
    }

    /// <summary>
    /// 防火接口
    /// </summary>
    public interface IFireproof
    {
        /// <summary>
        /// 防火功能
        /// </summary>
        void Fireproof();
    }


    /// <summary>
    /// A 品牌安全门
    /// </summary>
    public class ASafetyDoor :  IAntiTheft, IFireproof, IWaterproof
    {
        /// <summary>
        /// <inheritdoc cref="AntiTheft"/>
        /// </summary>
        public void AntiTheft() => Console.WriteLine("防盗");

        /// <summary>
        /// <inheritdoc cref="Fireproof"/>
        /// </summary>
        public void Fireproof() => Console.WriteLine("防火");

        /// <summary>
        /// <inheritdoc cref="Waterproof"/>
        /// </summary>
        public void Waterproof() => Console.WriteLine("防水");
    }

    /// <summary>
    /// B 品牌安全门
    /// </summary>
    public class BSafetyDoor : IAntiTheft, IFireproof
    {
        /// <summary>
        /// <inheritdoc cref="AntiTheft"/>
        /// </summary>
        public void AntiTheft() => Console.WriteLine("防盗");

        /// <summary>
        /// <inheritdoc cref="Fireproof"/>
        /// </summary>
        public void Fireproof() => Console.WriteLine("防火");
    }
}
```

## 2.5 迪米特法则（Demeter Principle）

又称最少知道原则。指：一个实体应当尽量少地与其他实体之间发生相互作用，使得系统功能模块相对独立。

其含义是：如果两个软件实体无须直接通信，那么就不应当发生直接的相互调用，可以通过第三方转发该调用。其目的是降低类之间的耦合度，提高模块的相对独立性。

迪米特法则中的“朋友”是指：当前对象本身、当前对象的成员对象、当前对象所创建的对象、当前对象的方法参数等，这些对象同当前对象存在关联、聚合或组合关系，可以直接访问这些对象的方法。

### 2.5.1 案例分析（明星、经纪人、粉丝、媒体公司）

明星由于全身心投入艺术，所以许多日常事务由经纪人负责处理，如和粉丝的见面会，和媒体公司的业务洽淡等。这里的经纪人是明星的朋友，而粉丝和媒体公司是陌生人，所以适合使用迪米特法则。

```csharp
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
```

## 2.6 合成复用原则（Composite Reuse Principle）

合成复用原则是指：尽量先使用`组合或者聚合等关联关系`来实现，其次才考虑使用`继承关系`来实现。通常类的复用分为继承复用和合成复用两种。

继承复用虽然有简单和易实现的优点，但它也存在以下缺点：

1. 继承复用破坏了类的封装性。因为继承会将父类的实现细节暴露给子类，父类对子类是透明的，所以这种复用又称为`白箱`复用。
2. 子类与父类的耦合度高。父类的实现的任何改变都会导致子类的实现发生变化，这不利于类的扩展与维护。
3. 它限制了复用的灵活性。从父类继承而来的实现是静态的，在编译时已经定义，所以在运行时不可能发生变化。


采用组合或聚合复用时，可以将已有对象纳入新对象中，使之成为新对象的一部分，新对象可以调用已有对象的功能，它有以下优点：

1. 它维持了类的封装性。因为成分对象的内部细节是新对象看不见的，所以这种复用又称为“黑箱”复用。
2. 对象间的耦合度低。可以在类的成员位置声明抽象。
3. 复用的灵活性高。这种复用可以在运行时动态进行，新对象可以动态地引用与成分对象类型相同的对象。

### 2.6.1 案例分析（汽车分类管理）

汽车按`动力源`划分可分为汽油汽车、电动汽车等；按`颜色`划分可分为白色汽车、黑色汽车和红色汽车等。如果同时考虑这两种分类，其组合就很多。

```csharp
using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new RedPetrolCar().Move();
            new RedElectricCar().Move();
            new WhitePetrolCar().Move();
            new WhiteElectricCar().Move();
        }
    }
    
    /// <summary>
    /// 抽象汽车类
    /// </summary>
    public abstract class Car
    {
        /// <summary>
        /// 行驶功能
        /// </summary>
        public abstract void Move();
    }

    /// <summary>
    /// 汽油车类
    /// </summary>
    public abstract class PetrolCar : Car
    {
    }

    /// <summary>
    /// 电动汽车类
    /// </summary>
    public abstract class ElectricCar : Car
    {
    }

    /// <summary>
    /// 红色汽油汽车类
    /// </summary>
    public class RedPetrolCar : PetrolCar
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine(nameof(RedPetrolCar));
    }

    /// <summary>
    /// 白色汽油汽车类
    /// </summary>
    public class WhitePetrolCar : PetrolCar
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine(nameof(WhitePetrolCar));
    }

    /// <summary>
    /// 红色电动汽车类
    /// </summary>
    public class RedElectricCar : ElectricCar
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine(nameof(RedPetrolCar));
    }

    /// <summary>
    /// 白色电动汽车类
    /// </summary>
    public class WhiteElectricCar : ElectricCar
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine(nameof(WhitePetrolCar));
    }
}
```

如果此时研发了一种新型能源（例如，氢能源）的汽车，该类型汽车又有红色和白色两个种类，这样就需要另外定义`HydrogenCar:Car`，`RedHydrogenCar:HydrogenCar`，`WhiteHydrogenCar:HydrogenCar`，易造成`类爆炸`。

### 2.5.2 案例改进

```csharp
using System;

namespace DesignPattern
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new PetrolCar() {Color = new WhiteColor()}.Move();
            new ElectricCar() {Color = new RedColor()}.Move();
        }
    }
    
    /// <summary>
    /// 颜色接口
    /// </summary>
    public interface IColor
    {
        string Name {get;}
    }

    /// <summary>
    /// 红色
    /// </summary>
    public class RedColor : IColor
    {
        public string Name => "Red";
    }

    /// <summary>
    /// 白色
    /// </summary>
    public class WhiteColor : IColor
    {
        /// <summary>
        /// 颜色
        /// </summary>
        public string Name => "Red";
    }

    /// <summary>
    /// 抽象汽车类
    /// </summary>
    public abstract class Car
    {
        /// <summary>
        /// 聚合颜色属性
        /// </summary>
        public IColor Color {get;set;}
        
        /// <summary>
        /// 行驶功能
        /// </summary>
        public abstract void Move();
    }

    /// <summary>
    /// 汽油车类
    /// </summary>
    public class PetrolCar : Car
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine($"{this.Color.Name} {nameof(PetrolCar)} is " + nameof(Move));
    }

    /// <summary>
    /// 电动车类
    /// </summary>
    public class ElectricCar : Car
    {
        /// <summary>
        /// <inheritdoc cref="Move"/>
        /// </summary>
        public override void Move() => Console.WriteLine($"{this.Color.Name}  {nameof(ElectricCar)} is " + nameof(Move));
    }
}
```

通过聚合关系，减少了类数量。如果增加新型能源的汽车，只需要增加一个类`HydrogenCar`。



# 2. 创建型模式

## 2.1 单例模式(Singleton Pattern) 

在软件系统中，经常有这样一些特殊的类，必须保证它们`在系统中只存在一个实例`，才能确保它们的逻辑正确性、以及良好的效率。这个类提供了一种访问其唯一的对象的方式，可以直接访问，不需要实例化该类的对象。

`这应该是类设计者的责任，而不是类使用者的责任。`

### 2.1.1 分类

- 饿汉式：类加载就会导致该单实例对象被创建

- 懒汉式：类加载不会导致该单实例对象被创建，而是首次使用该对象时才会创建

### 2.1.2 实现

#### 2.1.2.1 单线程

```c#
public sealed class Singleton
{
    private static Singleton instance = null;
    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }
    
    private Singleton() { }
}
```

> 该版本在多线程下可能会创建多个实例，是线程不安全的。因为如果两个线程同时运行到`if(instance == null)`判断时，就会创建两个实例，这是违背单例模式的初衷的。

#### 2.1.2.2 简单线程安全

```c#
public sealed class Singleton
{
    private static readonly object obj = new object();
    private static Singleton instance = null;
    public Singleton Instance
    {
        get
        {
            lock (obj)
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
    
    private Singleton() { }
}
```

> 该版本是线程安全的。通过对一个过线程共享的对象进行加锁操作，保证了在同一时刻只有一个线程在执行lock{}里的代码。当第一个线程在进行instance判断或创建时，后续线程必须等待直到前一线程执行完毕，因此保证了只有第一个线程能够创建instance实例。
>
> 但不幸的是，`因为每次对instance的请求都会进行lock操作，其性能是不佳的`。

#### 2.1.2.3 双检查线程安全

```c#
public sealed class Singleton
{
    private static object obj = new object();
    private static Singleton instance = null;
    
    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
    
    private Singleton() { }
}
```

> 该方式避免2.3.2中指出的性能不佳的问题

#### 2.1.2.4 不完全懒汉式，但不加锁的线程安全

```c#
public sealed class Singleton
{
    private static readonly Singleton instance = new Singleton();
    
    /// <summary>
    /// 显式的静态构造函数用来告诉C#编译器在其内容实例化之前不要标记其类型
    /// </summary>
    static Singleton() { }
    private Singleton() { }
    public static Singleton Instance { get { return instance; } }
}
```

> 这个版本是的实现非常的简单，但是却又是线程安全的。
>
> C#的`静态构造函数`只有在当其`类的实例被创建`或者有`静态成员被引用`时执行，在整个应用程序域中只会被执行一次。使用当前方式明显比前面版本中进行额外的判断要快。
>
> 当然这个版本也存在一些瑕疵：
>
> - 不是真正意义上的懒汉模式(需要的时候才创建实例)，若单例类还存在其他静态成员，当其他类第一次引用这些成员时便会创建该instance。下个版本实现会修正这个问题；
> - 只有.NET中才具有beforefieldinit特性，即懒汉式实现。且在.Net 1.1以前的编译器不支持，不过这个现在来看问题不大；
>
> 所有版本中，只有这里将instance设置成了readonly，这不仅保证了代码的高校且显得十分短小。

#### 2.1.2.5 完全懒汉实例化

```c#
public sealed class Singleton
{
    private Singleton() { }
    
    public static Singleton Instance { get { return Nested.instance; } }
    
    private class Nested
    {
        static Nested() { }
        internal static readonly Singleton instance = new Singleton();
    }
}
```

> 该版本看起来稍微复杂难懂，其实只是在写法上实现了上一版本的瑕疵，通过内嵌类的方式先实现了只有在真正应用Instance时才进行实例化。其性能表现与上一版本无异。

#### 2.1.2.6 实现延迟初始化技术

```c#
public sealed class Singleton
{
    private static readonly Lazy<Singleton> lazy = new Lazy<Singleton>(()=> new Singleton());
    public static Singleton Instance { get { return lazy.Value; } }
    private Singleton() { }
}
```

> 如果你使用的是.NET 4或其以上版本，可以使用System.Lazy type来实现完全懒汉式。其代码看起来也很简洁且性能表现也很好。

## 2.2 工厂模式（Factory Pattern）

### 2.2.1 需求描述

设计一个咖啡店点餐系统，该系统可以为顾客提供美式咖啡与拿铁咖啡。

### 2.2.2 最简单的设计

```csharp
void Main()
{
	CoffeeStore store = new CoffeeStore();
	Coffee coffee = store.orderCoffee("american");
	Console.WriteLine(coffee.Name);
	
	Console.WriteLine("----------------------------");
	coffee = store.orderCoffee("latte");
	Console.WriteLine(coffee.Name);
}

public abstract class Coffee
{
	public abstract string Name {get; }

	public void addsugar()
	{
		Console.WriteLine("加糖");
	}

    public void addMilk()
	{
        Console.WriteLine("加奶");
	}
}

public class LatteCoffee : Coffee
{
	public override string Name => "拿铁咖啡";
}

public class AmericanCoffee : Coffee
{
	public override string Name => "美式咖啡";
}

public class CoffeeStore
{

	public Coffee orderCoffee(String type)
	{
		Coffee coffee = null;
		
		if ("american".Equals(type))
		{
			coffee = new AmericanCoffee();
		}
		else if ("latte".Equals(type))
		{
			coffee = new LatteCoffee();
		}
		else
		{
			throw new Exception("对不起，您所点的咖啡没有");
		}

		coffee.addMilk();
		coffee.addsugar();

		return coffee;
	}
}
```

这种方式确实满足了需求，咖啡店也能够正常的运转。但是，随着咖啡店生意的火爆，顾客对咖啡店仅有两种咖啡产生了不满。为了应对这种情况，咖啡店老板计划增加一批新的咖啡。此时，原始程序需要改动才能满足新的需求，不满足`开闭原则`。

### 2.2.3 简单工厂模式

简单工厂模式`根据提供给它的数据`，返回`几个可能类中的一个类的实例`。**通常它返回的类都有一个共同的父类和共同的方法，但每个方法执行的任务不同**。简单工厂模式实际上不属于23个`GoF`模式，但它可以作为我们稍后要讨论的工厂方法模式的一个引导。

为了应对`2.2.2`结尾处提出的新需求，现使用简单工厂模式对其进行修改。增加一个`SimpleCoffeeFactory`类专门用于生产咖啡。

```csharp
void Main()
{
	CoffeeStore store = new CoffeeStore();
	Coffee coffee = store.orderCoffee("american");
	Console.WriteLine(coffee.Name);
	
	Console.WriteLine("----------------------------");
	coffee = store.orderCoffee("latte");
	Console.WriteLine(coffee.Name);
}

public abstract class Coffee
{
	public abstract string Name {get; }

	public void addsugar()
	{
		Console.WriteLine("加糖");
	}

    public void addMilk()
	{
        Console.WriteLine("加奶");
	}
}

public class LatteCoffee : Coffee
{
	public override string Name => "拿铁咖啡";
}

public class AmericanCoffee : Coffee
{
	public override string Name => "美式咖啡";
}

public class SimpleCoffeeFactory
{
	public Coffee createCoffee(String type)
	{
		Coffee coffee = null;
		
		if ("american".Equals(type))
		{
			coffee = new AmericanCoffee();
		}
		else if ("latte".Equals(type))
		{
			coffee = new LatteCoffee();
		}
		else
		{
			throw new Exception("对不起，您所点的咖啡没有");
		}

		return coffee;
	}
}

public class CoffeeStore
{
	public Coffee orderCoffee(String type)
	{

		SimpleCoffeeFactory factory = new SimpleCoffeeFactory();
		Coffee coffee = factory.createCoffee(type);

		coffee.addMilk();
		coffee.addsugar();

		return coffee;
	}
}
```

此时，对于新增加的需求，不需要修改咖啡点的相关程序，仅需要修改咖啡生产工厂的程序。单对一个咖啡店来说，`最简单的设计`方式与`简单工厂`方式没有多大的区别，仅仅是把`变化点`从咖啡店移动到咖啡生产工厂而已。但是，如果咖啡店老板开的是一个连锁咖啡店，`简单工厂`方式仅需要修改一处，而`最简单的设计`方式需要修改所有的连锁咖啡店。**这种方式亦不满足`开闭原则`**。

- 优缺点

    - **优点：**

        封装了创建对象的过程，可以通过参数直接获取对象。把对象的创建和业务逻辑层分开，这样以后就避免了修改客户代码，如果要实现新产品直接修改工厂类，而不需要在原代码中修改，这样就降低了客户代码修改的可能性，更加容易扩展。

    - **缺点：**

        增加新产品时还是需要修改工厂类的代码，违背了“开闭原则”。

### 2.2.4 静态工厂模式

该模式仅仅是将简单工厂模式中的工厂方法修改为静态方法。

```csharp
public class SimpleCoffeeFactory
{
	public static Coffee createCoffee(String type)
	{
		Coffee coffee = null;
		
		if ("american".Equals(type))
		{
			coffee = new AmericanCoffee();
		}
		else if ("latte".Equals(type))
		{
			coffee = new LatteCoffee();
		}
		else
		{
			throw new Exception("对不起，您所点的咖啡没有");
		}

		return coffee;
	}
}
```

### 2.2.5 工厂方法模式

简单工厂模式的`变化点`在`工厂类`，依据设计模式的一般准则`哪里变化，封装哪里`，因此需要对工厂方法做进一步封装。

- 概念

    定义一个用于创建对象的接口，让子类决定实例化哪个产品类对象。工厂方法使一个产品类的实例化延迟到其工厂的子类。

- 结构

    工厂方法模式的主要角色：

    * 抽象工厂（Abstract Factory）：提供了创建产品的接口，调用者通过它访问具体工厂的工厂方法来创建产品。
    * 具体工厂（ConcreteFactory）：主要是实现抽象工厂中的抽象方法，完成具体产品的创建。
    * 抽象产品（Product）：定义了产品的规范，描述了产品的主要特性和功能。
    * 具体产品（ConcreteProduct）：实现了抽象产品角色所定义的接口，由具体工厂来创建，它同具体工厂之间一一对应。

- 实现

    ```csharp
    void Main()
    {
    	CoffeeStore store = new CoffeeStore();
    	store.Factory = new LatteCoffeeFactory();
    	Coffee coffee = store.orderCoffee();
    	Console.WriteLine(coffee.Name);
    	
    	Console.WriteLine("----------------------------");
    	store.Factory = new AmericanCoffeeFactory();
    	store.orderCoffee();
    	Console.WriteLine(coffee.Name);
    }
    
    public abstract class Coffee
    {
    	public abstract string Name {get; }
    
    	public void addsugar()
    	{
    		Console.WriteLine("加糖");
    	}
    
        public void addMilk()
    	{
            Console.WriteLine("加奶");
    	}
    }
    
    public class LatteCoffee : Coffee
    {
    	public override string Name => "拿铁咖啡";
    }
    
    public class AmericanCoffee : Coffee
    {
    	public override string Name => "美式咖啡";
    }
    
    public interface CoffeeFactory
    {
    	Coffee createCoffee();
    }
    
    public class LatteCoffeeFactory : CoffeeFactory
    {
    	public Coffee createCoffee()
    	{
    		return new LatteCoffee();
    	}
    }
    
    public class AmericanCoffeeFactory : CoffeeFactory
    {
    	public Coffee createCoffee()
    	{
    		return new AmericanCoffee();
    	}
    }
    
    public class CoffeeStore
    {
    	public CoffeeFactory Factory {get;set;}
    	public Coffee orderCoffee()
    	{
    
    		Coffee coffee = this.Factory.createCoffee();
    		coffee.addMilk();
    		coffee.addsugar();
    
    		return coffee;
    	}
    }
    ```

    从以上的编写的代码可以看到，要增加产品类时也要相应地增加工厂类，不需要修改工厂类的代码了，这样就解决了简单工厂模式的缺点。

    工厂方法模式是简单工厂模式的进一步抽象。由于使用了多态性，工厂方法模式保持了简单工厂模式的优点，而且克服了它的缺点。

- 优缺点

    **优点：**

    - 用户只需要知道具体工厂的名称就可得到所要的产品，无须知道产品的具体创建过程；
    - 在系统增加新的产品时只需要添加具体产品类和对应的具体工厂类，无须对原工厂进行任何修改，满足开闭原则；

    **缺点：**

    * 每增加一个产品就要增加一个具体产品类和一个对应的具体工厂类，这增加了系统的复杂度。

### 2.2.6 抽象工厂模式

前面介绍的工厂方法模式中考虑的是一类产品的生产，如畜牧场只养动物、电视机厂只生产电视机、传智播客只培养计算机软件专业的学生等。

这些工厂只生产同种类产品，同种类产品称为同等级产品，也就是说：工厂方法模式只考虑生产同等级的产品，但是在现实生活中许多工厂是综合型的工厂，能生产多等级（种类） 的产品，如电器厂既生产电视机又生产洗衣机或空调，大学既有软件专业又有生物专业等。

本节要介绍的抽象工厂模式将考虑多等级产品的生产，将同一个具体工厂所生产的位于不同等级的一组产品称为一个产品族，下图所示横轴是产品等级，也就是同一类产品；纵轴是产品族，也就是同一品牌的产品，同一品牌的产品产自同一个工厂。

![image-20211206162045949](设计模式.assets/image-20211206162045949.png)

![image-20211206162053870](设计模式.assets/image-20211206162053870.png)



#### 2.2.6.1 概念

是一种为访问类提供一个创建一组相关或相互依赖对象的接口，且访问类无须指定所要产品的具体类就能得到同族的不同等级的产品的模式结构。

抽象工厂模式是工厂方法模式的升级版本，工厂方法模式只生产一个等级的产品，而抽象工厂模式可生产多个等级的产品。

#### 2.2.6.2 结构

抽象工厂模式的主要角色如下：

* 抽象工厂（Abstract Factory）：提供了创建产品的接口，它包含多个创建产品的方法，可以创建多个不同等级的产品。
* 具体工厂（Concrete Factory）：主要是实现抽象工厂中的多个抽象方法，完成具体产品的创建。
* 抽象产品（Product）：定义了产品的规范，描述了产品的主要特性和功能，抽象工厂模式有多个抽象产品。
* 具体产品（ConcreteProduct）：实现了抽象产品角色所定义的接口，由具体工厂来创建，它 同具体工厂之间是多对一的关系。

#### 2.2.6.3 实现

现咖啡店业务发生改变，不仅要生产咖啡还要生产甜点，如提拉米苏、抹茶慕斯等，要是按照工厂方法模式，需要定义提拉米苏类、抹茶慕斯类、提拉米苏工厂、抹茶慕斯工厂、甜点工厂类，很容易发生类爆炸情况。其中拿铁咖啡、美式咖啡是一个产品等级，都是咖啡；提拉米苏、抹茶慕斯也是一个产品等级；拿铁咖啡和提拉米苏是同一产品族（也就是都属于意大利风味），美式咖啡和抹茶慕斯是同一产品族（也就是都属于美式风味）。所以这个案例可以使用抽象工厂模式实现。

```csharp
void Main()
{
	IDessertFactory factory = new AmericanDessertFactory();
	Coffee coffee = factory.createCoffee();
	Dessert dessert = factory.createDessert();
	Console.WriteLine(coffee.Name);
	dessert.show();
	
	factory = new ItalyDessertFactory();
	coffee = factory.createCoffee();
	dessert = factory.createDessert();
	Console.WriteLine(coffee.Name);
	dessert.show();
}

// 咖啡抽象基类
public abstract class Coffee
{
	public abstract string Name { get; }
	public void addsugar() => Console.WriteLine("加糖");
	public void addMilk() => Console.WriteLine("加奶");
}

// 属于意大利产品族中的咖啡产品
public class LatteCoffee : Coffee
{
	public override string Name => "拿铁咖啡";
}

// 属于美式产品族中的咖啡产品
public class AmericanCoffee : Coffee
{
	public override string Name => "美式咖啡";
}

// 甜品基类
public abstract class Dessert
{
	public abstract void show();
}

// 属于美式产品族中的甜品产品
public class MatchaMousse : Dessert
{
	public override void show() => Console.WriteLine("抹茶慕斯");
}

// 属于意大利产品族中的甜品产品
public class Trimisu : Dessert
{
	public override void show() => Console.WriteLine("提拉米苏");
}

// 产品族工厂抽象基类
public interface IDessertFactory
{
	Coffee createCoffee();
	Dessert createDessert();
}

// 美式产品族工厂
public class AmericanDessertFactory : IDessertFactory
{
	public Coffee createCoffee() => new AmericanCoffee();	
	public Dessert createDessert() => new MatchaMousse();
}

// 意大利产品族工厂
public class ItalyDessertFactory : IDessertFactory
{
	public Coffee createCoffee() => new LatteCoffee();
	public Dessert createDessert() => new Trimisu();
}
```

#### 2.2.6.4 优缺点

**优点：**

当一个产品族中的多个对象被设计成一起工作时，它能保证客户端始终只使用同一个产品族中的对象。

**缺点：**

当产品族中需要增加一个新的产品时，所有的工厂类都需要进行修改。

### 2.2.7 配置工厂

**简单工厂+配置文件解除耦合**

可以通过工厂模式+配置文件的方式解除工厂对象和产品对象的耦合。在工厂类中加载配置文件中的全类名，并创建对象进行存储，客户端如果需要对象，直接进行获取即可。

- 第一步：定义配置文件

    > LatteCoffee
    > AmericanCoffee

- 第二步：改进工厂类

    ```csharp
    void Main()
    {
    	Coffee coffee = CoffeeFactory.createCoffee("LatteCoffee");
    	Console.WriteLine(coffee.Name);
    
    	Console.WriteLine("------------------------------");
    	coffee = CoffeeFactory.createCoffee("AmericanCoffee");
    	Console.WriteLine(coffee.Name);
    }
    
    public abstract class Coffee
    {
    	public abstract string Name { get; }
    	public void addsugar() => Console.WriteLine("加糖");
    	public void addMilk() => Console.WriteLine("加奶");
    }
    
    public class LatteCoffee : Coffee
    {
    	public override string Name => "拿铁咖啡";
    }
    
    public class AmericanCoffee : Coffee
    {
    	public override string Name => "美式咖啡";
    }
    
    public class CoffeeFactory
    {
        private static Dictionary<string,Coffee> map = new Dictionary<string, Coffee>();
    	
    	static CoffeeFactory()
    	{
    		// 加载配置文件，并通过反射创建具体类型对象
    		var lines = File.ReadAllLines("D:/config.txt");
    		foreach (var line in lines)
    		{
    			var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(a => a.Name.Contains(line));
    			if (type is null)
    			{
    				continue;
    			}
    			
    			var coffee = (Coffee)Activator.CreateInstance(type);
    			if (map.ContainsKey(line))
    			{
    				continue;
    			}
    			map.Add(line, coffee);
    		}
    	}
    
        public static Coffee createCoffee(string name)
    	{
    		return map.ContainsKey(name) ? map[name] : null;
    	}
    }
    ```

## 2.3 原型模式

### 2.3.1 概述

用一个已经创建的实例作为原型，通过复制该原型对象来创建一个和原型对象相同的新对象。

### 2.3.2 结构

原型模式包含如下角色：

* 抽象原型类：规定了具体原型对象必须实现的的 clone() 方法。
* 具体原型类：实现抽象原型类的 clone() 方法，它是可被复制的对象。
* 访问类：使用具体原型类中的 clone() 方法来复制新的对象。

### 2.3.3 实现

原型模式的克隆分为浅克隆和深克隆。

> 浅克隆：创建一个新对象，新对象的属性和原来对象完全相同，对于非基本类型属性，仍指向原有属性所指向的对象的内存地址。
>
> 深克隆：创建一个新对象，属性中引用的其他对象也会被克隆，不再指向原有对象地址。

C#中的object类中提供了 `clone()` 方法来实现浅克隆。 `ICloneable` 接口是上面的类图中的抽象原型类，而实现了`ICloneable`接口的子实现类就是具体的原型类。

> **用原型模式生成“三好学生”奖状**
>
> 同一学校的“三好学生”奖状除了获奖人姓名不同，其他都相同，可以使用原型模式复制多个“三好学生”奖状出来，然后在修改奖状上的名字即可。
>
> - 浅克隆实现
>
>     ```csharp
>     void Main()
>     {
>     	var c1 = new Citation() {Name="张三"};
>     	var c2 = (Citation)c1.Clone();
>     	c2.Name = "李四";
>         
>         c1.Show();
>     	c2.Show();
>     }
>     
>     public class Citation : ICloneable
>     {
>     	public String Name {get;set;}
>     
>     	public void Show()
>     	{
>             Console.WriteLine(this.Name + "同学：在2020学年第一学期中表现优秀，被评为三好学生。特发此状！");
>         }
>     	
>     	public object Clone()
>     	{
>     		return new Citation() {Name=this.Name};
>     	}
>     }
>     ```
>
>     > 张三同学：在2020学年第一学期中表现优秀，被评为三好学生。特发此状！
>     > 李四同学：在2020学年第一学期中表现优秀，被评为三好学生。特发此状！
>
> - 浅克隆问题
>
>     ```csharp
>     void Main()
>     {
>     	var c1 = new Citation() {Student = new Student(){Name="张三"}};
>     	
>     	
>     	var c2 = (Citation)c1.Clone();
>     	c2.Student.Name = "李四";
>     	                                            
>     	c1.Show();
>     	c2.Show();
>     }
>                                                 
>     public class Student
>     {
>     	public string Name {get;set;}
>     }
>                                                 
>     public class Citation : ICloneable
>     {
>     	public Student Student {get;set;}
>                                                 
>     	public void Show()
>     	{
>             Console.WriteLine(this.Student.Name + "同学：在2020学年第一学期中表现优秀，被评为三好学生。特发此状！");
>         }
>     	                                            
>     	public object Clone()
>     	{
>     		return new Citation() {Student=this.Student};
>     	}
>     }
>     ```
>                                            
>     > 李四同学：在2020学年第一学期中表现优秀，被评为三好学生。特发此状！
>     > 李四同学：在2020学年第一学期中表现优秀，被评为三好学生。特发此状！
>                                            
>     由输出可以看到，如果类中的字段或属性成员是非基本类型外的引用类型，浅拷贝是不能完成预期要求。
>
> - 深克隆
>
>     ```csharp
>     void Main()
>     {
>     	var c1 = new Citation() {Student = new Student(){Name="张三"}};
>     	var c2 = (Citation)c1.Clone();
>     	c2.Student = Newtonsoft.Json.JsonConvert.DeserializeObject<Student>(Newtonsoft.Json.JsonConvert.SerializeObject(c1.Student));
>     	c2.Student.Name = "李四";
>     	c1.Show();
>     	c2.Show();
>     }
>                                                 
>     public class Student
>     {
>     	public string Name {get;set;}
>     }
>                                                 
>     public class Citation : ICloneable
>     {
>     	public Student Student {get;set;}
>                                                 
>     	public void Show()
>     	{
>             Console.WriteLine(this.Student.Name + "同学：在2020学年第一学期中表现优秀，被评为三好学生。特发此状！");
>         }
>     	                                            
>     	public object Clone()
>     	{
>     		return new Citation() {Student=this.Student};
>     	}
>     }
>     ```
>
>     > 张三同学：在2020学年第一学期中表现优秀，被评为三好学生。特发此状！
>     > 李四同学：在2020学年第一学期中表现优秀，被评为三好学生。特发此状！

## 2.4 建造者模式(Builder)

### 2.4.1 概述

将一个复杂对象的构建与表示分离，使得同样的构建过程可以创建不同的表示。

<img src="设计模式.assets/image-20200413225341516.png" style="zoom:60%;" />

* 分离了部件的构造(由Builder来负责)和装配(由Director负责)。 从而可以构造出复杂的对象。这个模式适用于：某个对象的构建过程复杂的情况。
* 由于实现了构建和装配的解耦。不同的构建器，相同的装配，也可以做出不同的对象；相同的构建器，不同的装配顺序也可以做出不同的对象。也就是实现了构建算法、装配算法的解耦，实现了更好的复用。
* 建造者模式可以将部件和其组装过程分开，一步一步创建一个复杂的对象。用户只需要指定复杂对象的类型就可以得到该对象，而无须知道其内部的具体构造细节。



### 2.4.2 结构

建造者（Builder）模式包含如下角色：

* 抽象建造者类（Builder）：这个接口规定要实现复杂对象的那些部分的创建，并不涉及具体的部件对象的创建。 
* 具体建造者类（ConcreteBuilder）：实现 Builder 接口，完成复杂产品的各个部件的具体创建方法。在构造过程完成后，提供产品的实例。 
* 产品类（Product）：要创建的复杂对象。
* 指挥者类（Director）：调用具体建造者来创建复杂对象的各个部分，在指导者中不涉及具体产品的信息，只负责保证对象各部分完整创建或按某种顺序创建。 

### 2.4.3 实例

**创建共享单车**

生产自行车是一个复杂的过程，它包含了车架，车座等组件的生产。而车架又有碳纤维，铝合金等材质的，车座有橡胶，真皮等材质。对于自行车的生产就可以使用建造者模式。

这里Bike是产品，包含车架，车座等组件；Builder是抽象建造者，MobikeBuilder和OfoBuilder是具体的建造者；Director是指挥者。

```csharp
void Main()
{
	var director = new Director(new MobileBuilder());
	var bike = director.construct();

	Console.WriteLine(bike.Frame);
	Console.WriteLine(bike.Seat);
	
	Console.WriteLine("---------------------------------------");

	director = new Director(new OfoBuilder());
	bike = director.construct();

	Console.WriteLine(bike.Frame);
	Console.WriteLine(bike.Seat);
}

public class Bike
{
	//车架
	public String Frame { get; set;}
	//车座
	public String Seat { get;set; }
}

public abstract class Builder
{
	//声明Bike类型的变量，并进行赋值
	protected Bike bike = new Bike();

	public abstract void buildFrame();

	public abstract void buildSeat();

	//构建自行车的方法
	public abstract Bike createBike();
}

public class MobileBuilder : Builder
{
	public override void buildFrame()
	{
		bike.Frame= "碳纤维车架";
	}
	
	public override void buildSeat()
	{
		bike.Seat = "真皮车座";
	}
	
	public override Bike createBike()
	{
		return bike;
	}
}

public class OfoBuilder : Builder
{
	public override void buildFrame()
	{
		bike.Frame = "铝合金车架";
	}

	public override void buildSeat()
	{
		bike.Seat = "橡胶车座";
	}

	public override Bike createBike()
	{
		return bike;
	}
}

public class Director
{
	//声明builder类型的变量
	private Builder builder;

	public Director(Builder builder)
	{
		this.builder = builder;
	}

	//组装自行车的功能
	public Bike construct()
	{
		builder.buildFrame();
		builder.buildSeat();
		return builder.createBike();
	}
}
```

上面示例是 Builder模式的常规用法，指挥者类 Director 在建造者模式中具有很重要的作用，它用于指导具体构建者如何构建产品，控制调用先后次序，并向调用者返回完整的产品类，但是有些情况下需要简化系统结构，可以把指挥者类和抽象建造者进行结合

```csharp
void Main()
{
	Builder builder = new MobileBuilder();
	var bike = builder.construct();

	Console.WriteLine(bike.Frame);
	Console.WriteLine(bike.Seat);
	
	Console.WriteLine("---------------------------------------");

	builder = new OfoBuilder();
	bike = builder.construct();

	Console.WriteLine(bike.Frame);
	Console.WriteLine(bike.Seat);
}

public class Bike
{
	//车架
	public String Frame { get; set;}
	//车座
	public String Seat { get;set; }
}

public abstract class Builder
{
	//声明Bike类型的变量，并进行赋值
	protected Bike bike = new Bike();

	public abstract void buildFrame();

	public abstract void buildSeat();

	//构建自行车的方法
	public abstract Bike createBike();

	public Bike construct()
	{
		this.buildFrame();
		this.buildSeat();
		return this.createBike();
	}
}

public class MobileBuilder : Builder
{
	public override void buildFrame()
	{
		bike.Frame= "碳纤维车架";
	}
	
	public override void buildSeat()
	{
		bike.Seat = "真皮车座";
	}
	
	public override Bike createBike()
	{
		return bike;
	}
}

public class OfoBuilder : Builder
{
	public override void buildFrame()
	{
		bike.Frame = "铝合金车架";
	}

	public override void buildSeat()
	{
		bike.Seat = "橡胶车座";
	}

	public override Bike createBike()
	{
		return bike;
	}
}
```



**说明：**

这样做确实简化了系统结构，但同时也加重了抽象建造者类的职责，也不是太符合单一职责原则，如果construct() 过于复杂，建议还是封装到 Director 中。

### 2.4.4 优缺点

**优点：**

- 建造者模式的封装性很好。使用建造者模式可以有效的封装变化，在使用建造者模式的场景中，一般产品类和建造者类是比较稳定的，因此，将主要的业务逻辑封装在指挥者类中对整体而言可以取得比较好的稳定性。
- 在建造者模式中，客户端不必知道产品内部组成的细节，将产品本身与产品的创建过程解耦，使得相同的创建过程可以创建不同的产品对象。
- 可以更加精细地控制产品的创建过程 。将复杂产品的创建步骤分解在不同的方法中，使得创建过程更加清晰，也更方便使用程序来控制创建过程。
- 建造者模式很容易进行扩展。如果有新的需求，通过实现一个新的建造者类就可以完成，基本上不用修改之前已经测试通过的代码，因此也就不会对原有功能引入风险。符合开闭原则。

**缺点：**

造者模式所创建的产品一般具有较多的共同点，其组成部分相似，如果产品之间的差异性很大，则不适合使用建造者模式，因此其使用范围受到一定的限制。



### 2.4.5 使用场景

建造者（Builder）模式创建的是复杂对象，其产品的各个部分经常面临着剧烈的变化，但将它们组合在一起的算法却相对稳定，所以它通常在以下场合使用。

- 创建的对象较复杂，由多个部件构成，各部件面临着复杂的变化，但构件间的建造顺序是稳定的。
- 创建复杂对象的算法独立于该对象的组成部分以及它们的装配方式，即产品的构建过程和最终的表示是独立的。



### 2.4.6 模式扩展

建造者模式除了上面的用途外，在开发中还有一个常用的使用方式，就是当一个类构造器需要传入很多参数时，如果创建这个类的实例，代码可读性会非常差，而且很容易引入错误，此时就可以利用建造者模式进行重构。

- 重构前代码

    ```csharp
    void Main()
    {
    	Phone phone = new Phone("intel","三星屏幕","金士顿","华硕");
    	Console.WriteLine(phone.ToString());
    }
    
    public class Phone
    {
    	public string Cpu {get;}
    	public string Screen {get;}
    	public string Memory {get;}
    	public string MainBoard {get;}
    	
    	public Phone(string cpu, string screen, string memory, string mainBoard)
    	{
    		this.Cpu = cpu;
    		this.Screen = screen;
    		this.Memory = memory;
    		this.MainBoard = mainBoard;
    	}
    
    	public override string ToString()
    	{
    		return $"Phone: Cpu= {this.Cpu} , Screen= {this.Screen}, Memory={this.Memory}, MainBoard={this.MainBoard}";
    	}
    }
    ```

    > 上面在客户端代码中构建Phone对象，传递了四个参数，如果参数更多呢？代码的可读性及使用的成本就是比较高。

- 重构后代码

    ```csharp
    void Main()
    {
    	// 链式编程，能够自定义组装顺序
    	Phone phone = (new Phone.Builder()).AddCpu("intel").AddScreen("三星").AddMemory("金士顿").AddMainBoard("华硕").Build();
    	Console.WriteLine(phone.ToString());
    }
    
    public class Phone
    {
    	public string Cpu {get;}
    	public string Screen {get;}
    	public string Memory {get;}
    	public string MainBoard {get;}
    	
    	private Phone(Builder builder)
    	{
    		this.Cpu = builder.Cpu;
    		this.Screen = builder.Screen;
    		this.Memory = builder.Memory;
    		this.MainBoard = builder.MainBoard;
    	}
    
    	public override string ToString()
    	{
    		return $"Phone: Cpu= {this.Cpu} , Screen= {this.Screen}, Memory={this.Memory}, MainBoard={this.MainBoard}";
    	}
    	
    	public sealed class Builder
    	{
    		public string Cpu { get; private set;}
    		public string Screen { get;  private set;}
    		public string Memory { get;  private set;}
    		public string MainBoard { get;  private set;}
    		
    		public Builder()
    		{	
    		}
    
    		public Builder AddCpu(string cpu)
    		{
    			this.Cpu = cpu;
    			return this;
    		}
    
    		public Builder AddScreen(string screen)
    		{
    			this.Screen = screen;
    			return this;
    		}
    
    		public Builder AddMemory(string memory)
    		{
    			this.Memory = memory;
    			return this;
    		}
    
    		public Builder AddMainBoard(string mainBoard)
    		{
    			this.MainBoard = mainBoard;
    			return this;
    		}
    		
    		public Phone Build() => new Phone(this);
    	}
    }
    ```

# 3. 结构型模式

结构型模式描述如何将类或对象按某种布局组成更大的结构。它分为类结构型模式和对象结构型模式，前者采用继承机制来组织接口和类，后者釆用组合或聚合来组合对象。

由于组合关系或聚合关系比继承关系耦合度低，满足“合成复用原则”，所以对象结构型模式比类结构型模式具有更大的灵活性。

## 3.1 代理模式

### 3.1.1 概述

由于某些原因需要给某对象提供一个代理以控制对该对象的访问。这时，访问对象不适合或者不能直接引用目标对象，代理对象作为访问对象和目标对象之间的中介。

### 3.1.2 结构

代理（Proxy）模式分为三种角色：

* 抽象主题（Subject）类： 通过接口或抽象类声明真实主题和代理对象实现的业务方法。
* 真实主题（Real Subject）类： 实现了抽象主题中的具体业务，是代理对象所代表的真实对象，是最终要引用的对象。
* 代理（Proxy）类 ： 提供了与真实主题相同的接口，其内部含有对真实主题的引用，它可以访问、控制或扩展真实主题的功能。

### 3.1.3 静态代理

我们通过案例来感受一下静态代理。

【例】火车站卖票

如果要买火车票的话，需要去火车站买票，坐车到火车站，排队等一系列的操作，显然比较麻烦。而火车站在多个地方都有代售点，我们去代售点买票就方便很多了。这个例子其实就是典型的代理模式，火车站是目标对象，代售点是代理对象。

```csharp
void Main()
{
	ProxyPoint proxyPoint = new ProxyPoint();
	proxyPoint.sell();
}

public interface SellTickets
{
	void sell();
}

public class TrainStation : SellTickets
{
	public void sell()
	{
		Console.WriteLine("火车站卖票");
	}
}

public class ProxyPoint : SellTickets
{
	private SellTickets trainStation = new TrainStation();
	public void sell()
	{
		Console.WriteLine("代售点收取一些服务费用");
		trainStation.sell();
	}
}
```



### 3.1.4 动态代理

动态代理指的是编码期间是没有代理类的，代理类是在运行期间动态创建的。这样做的好处是：

- 减少了编码期间的耦合，程序员感受不到代理类的存在，一定程度上简化了业务逻辑
- 对于新的类型，不用手动创建相似度较高的代理类，一定程度上减少了重复编码

后期研究

### 3.1.5 优缺点

**优点：**

- 代理模式在客户端与目标对象之间起到一个中介作用和保护目标对象的作用；
- 代理对象可以扩展目标对象的功能；
- 代理模式能将客户端与目标对象分离，在一定程度上降低了系统的耦合度；

**缺点：**

* 增加了系统的复杂度；



### 3.1.6 使用场景 

* 远程（Remote）代理

    本地服务通过网络请求远程服务。为了实现本地到远程的通信，我们需要实现网络通信，处理其中可能的异常。为良好的代码设计和可维护性，我们将网络通信部分隐藏起来，只暴露给本地服务一个接口，通过该接口即可访问远程服务提供的功能，而不必过多关心通信部分的细节。

* 防火墙（Firewall）代理

    当你将浏览器配置成使用代理功能时，防火墙就将你的浏览器的请求转给互联网；当互联网返回响应时，代理服务器再把它转给你的浏览器。

* 保护（Protect or Access）代理

    控制对一个对象的访问，如果需要，可以给不同的用户提供不同级别的使用权限。

## 3.2 适配器模式（Adapter Pattern)

### 3.2.1 概述

如果去欧洲国家去旅游的话，他们的插座如下图最左边，是欧洲标准。而我们使用的插头如下图最右边的。因此我们的笔记本电脑，手机在当地不能直接充电。所以就需要一个插座转换器，转换器第1面插入当地的插座，第2面供我们充电，这样使得我们的插头在当地能使用。生活中这样的例子很多，手机充电器（将220v转换为5v的电压），读卡器等，其实就是使用到了适配器模式。

![](设计模式.assets/转接头.png)

**定义：**

​	将一个类的接口转换成客户希望的另外一个接口，使得原本由于接口不兼容而不能一起工作的那些类能一起工作。

​	适配器模式分为类适配器模式和对象适配器模式，前者类之间的耦合度比后者高，且要求程序员了解现有组件库中的相关组件的内部结构，所以应用相对较少些。

### 3.2.2 结构

适配器模式（Adapter）包含以下主要角色：

* 目标（Target）接口：当前系统业务所期待的接口，它可以是抽象类或接口。
* 适配者（Adaptee）类：它是被访问和适配的现存组件库中的组件接口。
* 适配器（Adapter）类：它是一个转换器，通过继承或引用适配者的对象，把适配者接口转换成目标接口，让客户按目标接口的格式访问适配者。

### 3.2.3 类适配器模式

实现方式：定义一个适配器类来实现当前系统的业务接口，同时又继承现有组件库中已经存在的组件。

【例】读卡器

现有一台电脑只能读取SD卡，而要读取TF卡中的内容的话就需要使用到适配器模式。创建一个读卡器，将TF卡中的内容读取出来。

```csharp
void Main()
{
	Computer computer = new Computer();
	string msg = computer.readSD(new SDCardImpl());
	Console.WriteLine(msg);

	Console.WriteLine("===============");
	string msg1 = computer.readSD(new SDAdapterTF());
	Console.WriteLine(msg1);
}

public interface SDCard
{
    string readSD();
	void writeSD(string msg);
}

public class SDCardImpl : SDCard
{
	public string readSD() => "SDCard read msg ： hello word SD";
	public void writeSD(string msg) => Console.WriteLine("SDCard write msg ：" + msg);
}

public interface TFCard
{
	string readTF();
	void writeTF(string msg);
}

public class TFCardImpl : TFCard
{
	public string readTF() => "TFCard read msg ： hello word TFcard";
	public void writeTF(string msg) => Console.WriteLine("TFCard write msg :" + msg);
}

public class Computer
{
	public string readSD(SDCard sdCard)
	{
		if (sdCard == null)
		{
			throw new NullReferenceException("sd card is not null");
		}
		return sdCard.readSD();
	}
}

public class SDAdapterTF : TFCardImpl, SDCard
{
	public string readSD()
	{
		Console.WriteLine("adapter read tf card");
		return readTF();
	}
	
	public void writeSD(string msg)
	{
		Console.WriteLine("adapter write tf card");
		writeTF(msg);
	}
}
```

### 3.2.4 对象适配器模式

实现方式：对象适配器模式可釆用将现有组件库中已经实现的组件引入适配器类中，该类同时实现当前系统的业务接口。

【例】读卡器

我们使用对象适配器模式将读卡器的案例进行改写。

```csharp
void Main()
{
	Computer computer = new Computer();
	string msg = computer.readSD(new SDCardImpl());
	Console.WriteLine(msg);

	Console.WriteLine("===============");
	string msg1 = computer.readSD(new SDAdapterTF(new TFCardImpl()));
	Console.WriteLine(msg1);
}

public interface SDCard
{
    string readSD();
	void writeSD(string msg);
}

public class SDCardImpl : SDCard
{
	public string readSD() => "SDCard read msg ： hello word SD";
	public void writeSD(string msg) => Console.WriteLine("SDCard write msg ：" + msg);
}

public interface TFCard
{
	string readTF();
	void writeTF(string msg);
}

public class TFCardImpl : TFCard
{
	public string readTF() => "TFCard read msg ： hello word TFcard";
	public void writeTF(string msg) => Console.WriteLine("TFCard write msg :" + msg);
}

public class Computer
{
	public string readSD(SDCard sdCard)
	{
		if (sdCard == null)
		{
			throw new NullReferenceException("sd card is not null");
		}
		return sdCard.readSD();
	}
}

public class SDAdapterTF : SDCard
{
	private TFCard tfCard;
	public SDAdapterTF(TFCard tfCard)
	{
		this.tfCard = tfCard;
	}
	
	public string readSD()
	{
		Console.WriteLine("adapter read tf card");
		return this.tfCard.readTF();
	}
	
	public void writeSD(string msg)
	{
		Console.WriteLine("adapter write tf card");
		this.tfCard.writeTF(msg);
	}
}
```



### 3.2.5 应用场景

* 以前开发的系统存在满足新系统功能需求的类，但其接口同新系统的接口不一致。
* 使用第三方提供的组件，但组件接口定义和自己要求的接口定义不同。



## 3.3 装饰模式(Decorator Pattern)

### 3.3.1 概述

我们先来看一个快餐店的例子。

快餐店有炒面、炒饭这些快餐，可以额外附加鸡蛋、火腿、培根这些配菜，当然加配菜需要额外加钱，每个配菜的价钱通常不太一样，那么计算总价就会显得比较麻烦。

使用继承的方式存在的问题：

* 扩展性不好

    如果要再加一种配料（火腿肠），我们就会发现需要给FriedRice和FriedNoodles分别定义一个子类。如果要新增一个快餐品类（炒河粉）的话，就需要定义更多的子类。

* 产生过多的子类

**定义：**

​	指在不改变现有对象结构的情况下，动态地给该对象增加一些职责（即增加其额外功能）的模式。

### 3.3.2 结构

装饰（Decorator）模式中的角色：

* 抽象构件（Component）角色 ：定义一个抽象接口以规范准备接收附加责任的对象。
* 具体构件（Concrete  Component）角色 ：实现抽象构件，通过装饰角色为其添加一些职责。
* 抽象装饰（Decorator）角色 ： 继承或实现抽象构件，并包含具体构件的实例，可以通过其子类扩展具体构件的功能。
* 具体装饰（ConcreteDecorator）角色 ：实现抽象装饰的相关方法，并给具体构件对象添加附加的责任。



### 3.3.3 案例

我们使用装饰者模式对快餐店案例进行改进，体会装饰者模式的精髓。

```csharp
void Main()
{
	 //点一份炒饭
    FastFood food = new FriedRice();
    Console.WriteLine(food.GetDesc() + "  " + food.Cost() + "元");
    Console.WriteLine("===============");

	//在上面的炒饭中加一个鸡蛋
	food = new Egg(food);
	Console.WriteLine(food.GetDesc() + "  " + food.Cost() + "元");
	Console.WriteLine("================");
	
	//再加一个鸡蛋
	food = new Egg(food);
	Console.WriteLine(food.GetDesc() + "  " + food.Cost() + "元");
	Console.WriteLine("================");
	
	// 再加一个培根
	food = new Bacon(food);
	Console.WriteLine(food.GetDesc() + "  " + food.Cost() + "元");
	food.Dump();
}

public abstract class FastFood
{
	public float Price { get; }
	public string Desc { get; }

	public FastFood(float price, string desc)
	{
		this.Price = price;
		this.Desc = desc;
	}

	public FastFood() {}
	
	public abstract string GetDesc();
	public abstract double Cost();
}

public class FriedRice : FastFood
{
	public FriedRice() : base(10, "炒饭") {}

	public override double Cost()
	{
		return this.Price;
	}

	public override string GetDesc()
	{
		return this.Desc;
	}
}

public class FriedNoodles : FastFood
{
	public FriedNoodles() : base(12, "炒面") {}

	public override double Cost()
	{
		return this.Price;
	}

	public override string GetDesc()
	{
		return this.Desc;
	}
}

public abstract class Garnish : FastFood
{
	public FastFood FastFood { get; }
	public Garnish(FastFood fastFood, float price, String desc) : base(price, desc)
	{
		this.FastFood = fastFood;
	}

	public override string GetDesc()
	{
		return this.Desc + this.FastFood.GetDesc();
	}
	
	public override double Cost()
	{
		return this.Price + this.FastFood.Cost();
	}
}

public class Egg : Garnish
{
	public Egg(FastFood fastFood) : base(fastFood, 1, "鸡蛋") {}
}


public class Bacon  : Garnish
{
	public Bacon (FastFood fastFood) : base(fastFood, 2,"培根") { }
}
```

![image-20211208112541890](设计模式.assets/image-20211208112541890.png)



## 3.4 桥接模式（Bridge Pattern)

### 3.4.1 概述

现在有一个需求，需要创建不同的图形，并且每个图形都有可能会有不同的颜色。我们可以利用继承的方式来设计类的关系：

![image-20211208112723514](设计模式.assets/image-20211208112723514.png)

我们可以发现有很多的类，假如我们再增加一个形状或再增加一种颜色，就需要创建更多的类。

试想，在一个有多种可能会变化的维度的系统中，用继承方式会造成类爆炸，扩展起来不灵活。每次在一个维度上新增一个具体实现都要增加多个子类。为了更加灵活的设计系统，我们此时可以考虑使用桥接模式。

**定义：**

​	将抽象与实现分离，使它们可以独立变化。它是用组合关系代替继承关系来实现，从而降低了抽象和实现这两个可变维度的耦合度。



### 3.4.2 结构

桥接（Bridge）模式包含以下主要角色：

* 抽象化（Abstraction）角色 ：定义抽象类，并包含一个对实现化对象的引用。
* 扩展抽象化（Refined  Abstraction）角色 ：是抽象化角色的子类，实现父类中的业务方法，并通过组合关系调用实现化角色中的业务方法。
* 实现化（Implementor）角色 ：定义实现化角色的接口，供扩展抽象化角色调用。
* 具体实现化（Concrete Implementor）角色 ：给出实现化角色接口的具体实现。



### 3.4.3 案例

【例】视频播放器

需要开发一个跨平台视频播放器，可以在不同操作系统平台（如Windows、Mac、Linux等）上播放多种格式的视频文件，常见的视频格式包括RMVB、AVI、WMV等。该播放器包含了两个维度，适合使用桥接模式。

![image-20211208112759209](设计模式.assets/image-20211208112759209.png)

```csharp
void Main()
{
    PlayerBase player = new WindowsPlayer(new AviFile());
    player.play("战狼3");

	Console.WriteLine("---------------------------------");
	player = new WindowsPlayer(new RmvbFile());
	player.play("战狼2");
}

// 实现化角色
public interface VideoFile
{
	//解码功能
	void decode(string fileName);
}

// 具体的实现化角色
public class RmvbFile : VideoFile
{
	public void decode(String fileName)
	{
		Console.WriteLine("rmvb视频文件 ：" + fileName);
	}
}

// 具体的实现化角色
public class AviFile : VideoFile
{
	public void decode(String fileName)
	{
		Console.WriteLine("avi视频文件 ：" + fileName);
	}
}

// 抽象化角色
public abstract class PlayerBase
{
	//声明videFile变量
	protected VideoFile videoFile;

	public PlayerBase(VideoFile videoFile)
	{
		this.videoFile = videoFile;
	}

	public abstract void play(String fileName);
}

// 扩展抽象化角色
public class WindowsPlayer : PlayerBase
{
	public WindowsPlayer(VideoFile videoFile) : base(videoFile)
	{
	}
	
	public override void play(String fileName)
	{
		videoFile.decode(fileName);
	}
}

public class MacPlayer : PlayerBase
{
	public MacPlayer(VideoFile videoFile) : base(videoFile)
	{
	}
	
	public override void play(String fileName)
	{
		videoFile.decode(fileName);
	}
}
```

## 3.5 外观模式（Facade Pattern)

### 3.5.1 概述

有些人可能炒过股票，但其实大部分人都不太懂，这种没有足够了解证券知识的情况下做股票是很容易亏钱的，刚开始炒股肯定都会想，如果有个懂行的帮帮手就好，其实基金就是个好帮手，支付宝里就有许多的基金，它将投资者分散的资金集中起来，交由专业的经理人进行管理，投资于股票、债券、外汇等领域，而基金投资的收益归持有者所有，管理机构收取一定比例的托管管理费用。

**定义：**

​	又名门面模式，是一种通过为多个复杂的子系统提供一个一致的接口，而使这些子系统更加容易被访问的模式。该模式对外有一个统一接口，外部应用程序不用关心内部子系统的具体的细节，这样会大大降低应用程序的复杂度，提高了程序的可维护性。

​	外观（Facade）模式是“迪米特法则”的典型应用

![image-20211208113946475](设计模式.assets/image-20211208113946475.png)



### 5.5.2 结构

外观（Facade）模式包含以下主要角色：

* 外观（Facade）角色：为多个子系统对外提供一个共同的接口。
* 子系统（Sub System）角色：实现系统的部分功能，客户可以通过外观角色访问它。



### 5.5.3 案例

【例】智能家电控制

小明的爷爷已经60岁了，一个人在家生活：每次都需要打开灯、打开电视、打开空调；睡觉时关闭灯、关闭电视、关闭空调；操作起来都比较麻烦。所以小明给爷爷买了智能音箱，可以通过语音直接控制这些智能家电的开启和关闭。类图如下：

![image-20211208114005017](设计模式.assets/image-20211208114005017.png)



```csharp
void Main()
{
	SmartAppliancesFacade facade = new SmartAppliancesFacade();
	facade.say("打开家电");

	Console.WriteLine("==================");

	facade.say("关闭家电");
}

public class SmartAppliancesFacade
{
	//聚合电灯对象，电视机对象，空调对象
	private Light light;
	private TV tv;
	private AirCondition airCondition;

	public SmartAppliancesFacade()
	{
		light = new Light();
		tv = new TV();
		airCondition = new AirCondition();
	}

	//通过语言控制
	public void say(String message)
	{
		if (message.Contains("打开"))
		{
			on();
		}
		else if (message.Contains("关闭"))
		{
			off();
		}
		else
		{
			Console.WriteLine("我还听不懂你说的！！！");
		}
	}

	//一键打开功能
	private void on()
	{
		light.on();
		tv.on();
		airCondition.on();
	}

	//一键关闭功能
	private void off()
	{
		light.off();
		tv.off();
		airCondition.off();
	}
}

public class TV
{
	public void on() => Console.WriteLine("打开电视机。。。。");
	public void off() => Console.WriteLine("关闭电视机。。。。");
}

public class Light
{
	//开灯
	public void on()
	{
		Console.WriteLine("打开电灯。。。。");
	}

	//关灯
	public void off()
	{
		Console.WriteLine("关闭电灯。。。。");
	}
}

public class AirCondition
{
	public void on()
	{
		Console.WriteLine("打开空调。。。。");
	}

	public void off()
	{
		Console.WriteLine("关闭空调。。。。");
	}
}
```



**好处：**

* 降低了子系统与客户端之间的耦合度，使得子系统的变化不会影响调用它的客户类。
* 对客户屏蔽了子系统组件，减少了客户处理的对象数目，并使得子系统使用起来更加容易。

**缺点：**

* 不符合开闭原则，修改很麻烦



### 3.5.4 使用场景

* 对分层结构系统构建时，使用外观模式定义子系统中每层的入口点可以简化子系统之间的依赖关系。
* 当一个复杂系统的子系统很多时，外观模式可以为系统设计一个简单的接口供外界访问。
* 当客户端与多个子系统之间存在很大的联系时，引入外观模式可将它们分离，从而提高子系统的独立性和可移植性。



## 3.6 组合模式(Composite Pattern)

3.6.1 概述

![image-20211208115134703](设计模式.assets/image-20211208115134703.png)

对于这个图片肯定会非常熟悉，上图我们可以看做是一个文件系统，对于这样的结构我们称之为树形结构。在树形结构中可以通过调用某个方法来遍历整个树，当我们找到某个叶子节点后，就可以对叶子节点进行相关的操作。可以将这颗树理解成一个大的容器，容器里面包含很多的成员对象，这些成员对象即可是容器对象也可以是叶子对象。但是由于容器对象和叶子对象在功能上面的区别，使得我们在使用的过程中必须要区分容器对象和叶子对象，但是这样就会给客户带来不必要的麻烦，作为客户而已，它始终希望能够一致的对待容器对象和叶子对象。

**定义：**

​	又名部分整体模式，是用于把一组相似的对象当作一个单一的对象。组合模式依据树形结构来组合对象，用来表示部分以及整体层次。这种类型的设计模式属于结构型模式，它创建了对象组的树形结构。



### 5.6.2 结构

组合模式主要包含三种角色：

* 抽象根节点（Component）：定义系统各层次对象的共有方法和属性，可以预先定义一些默认行为和属性。
* 树枝节点（Composite）：定义树枝节点的行为，存储子节点，组合树枝节点和叶子节点形成一个树形结构。
* 叶子节点（Leaf）：叶子节点对象，其下再无分支，是系统层次遍历的最小单位。



### 5.6.3 案例实现

【例】软件菜单

如下图，我们在访问别的一些管理系统时，经常可以看到类似的菜单。一个菜单可以包含菜单项（菜单项是指不再包含其他内容的菜单条目），也可以包含带有其他菜单项的菜单，因此使用组合模式描述菜单就很恰当，我们的需求是针对一个菜单，打印出其包含的所有菜单以及菜单项的名称。

![image-20211208115203622](设计模式.assets/image-20211208115203622.png)



要实现该案例，我们先画出类图：



![image-20211208115216975](设计模式.assets/image-20211208115216975.png)

**代码实现：**

不管是菜单还是菜单项，都应该继承自统一的接口，这里姑且将这个统一的接口称为菜单组件。

```csharp
using System;
using System.Collections.Generic;

namespace DesignPatternDemo
{
    internal static class Program
    {
        private static void Main( )
        {
            //创建菜单树
            MenuComponent menu1 = new Menu("菜单管理", 2);
            menu1.Add(new MenuItem("页面访问", 3));
            menu1.Add(new MenuItem("展开菜单", 3));
            menu1.Add(new MenuItem("编辑菜单", 3));
            menu1.Add(new MenuItem("删除菜单", 3));
            menu1.Add(new MenuItem("新增菜单", 3));

            MenuComponent menu2 = new Menu("权限管理", 2);
            menu2.Add(new MenuItem("页面访问", 3));
            menu2.Add(new MenuItem("提交保存", 3));

            MenuComponent menu3 = new Menu("角色管理", 2);
            menu3.Add(new MenuItem("页面访问", 3));
            menu3.Add(new MenuItem("新增角色", 3));
            menu3.Add(new MenuItem("修改角色", 3));

            //创建一级菜单
            MenuComponent component = new Menu("系统管理", 1);
            //将二级菜单添加到一级菜单中
            component.Add(menu1);
            component.Add(menu2);
            component.Add(menu3);


            //打印菜单名称(如果有子菜单一块打印)
            component.Print();
        }
    }
    
    // 属于抽象根节点
    public abstract class MenuComponent
    {
        //菜单组件的名称
        protected string name;
        //菜单组件的层级
        protected int level;

        //添加子菜单
        public virtual void Add(MenuComponent menuComponent)
        {
            throw new NotSupportedException();
        }

        //移除子菜单
        public virtual void Remove(MenuComponent menuComponent)
        {
            throw new NotSupportedException();
        }

        //获取指定的子菜单
        public virtual MenuComponent GetChild(int index)
        {
            throw new NotSupportedException();
        }

        //获取菜单或者菜单项的名称
        public string GetName()
        {
            return this.name;
        }

        //打印菜单名称的方法（包含子菜单和字菜单项）
        public abstract void Print();
    }

    public class Menu : MenuComponent
    {
        //菜单可以有多个子菜单或者子菜单项
        private readonly List<MenuComponent> menuComponentList = new List<MenuComponent>();
	
        //构造方法
        public Menu(string name, int level)
        {
            this.name  = name;
            this.level = level;
        }

        public override void Add(MenuComponent menuComponent)
        {
            this.menuComponentList.Add(menuComponent);
        }

        public override void Remove(MenuComponent menuComponent)
        {
            this.menuComponentList.Remove(menuComponent);
        }

        public override MenuComponent GetChild(int index)
        {
            return this.menuComponentList[index];
        }

        public override void Print()
        {
            //打印菜单名称
            for (var i = 0; i < this.level; i++)
            {
                Console.Write("--");
            }
            Console.WriteLine( this.name);

            //打印子菜单或者子菜单项名称
            foreach (var component in this.menuComponentList)
            {
                component.Print();
            }
        }
    }

    public class MenuItem : MenuComponent
    {
        public MenuItem(string name, int level)
        {
            this.name  = name;
            this.level = level;
        }

        public override void Print()
        {
            //打印菜单项的名称
            for (var i = 0; i < this.level; i++)
            {
                Console.Write("--");
            }
            Console.WriteLine( this.name);
        }
    }

}
```

### 3.6.4 组合模式的分类

在使用组合模式时，根据抽象构件类的定义形式，我们可将组合模式分为透明组合模式和安全组合模式两种形式。

* 透明组合模式

    透明组合模式中，抽象根节点角色中声明了所有用于管理成员对象的方法，比如在示例中 `MenuComponent` 声明了 `add`、`remove` 、`getChild` 方法，这样做的好处是确保所有的构件类都有相同的接口。透明组合模式也是组合模式的标准形式。

    透明组合模式的缺点是不够安全，因为叶子对象和容器对象在本质上是有区别的，叶子对象不可能有下一个层次的对象，即不可能包含成员对象，因此为其提供 add()、remove() 等方法是没有意义的，这在编译阶段不会出错，但在运行阶段如果调用这些方法可能会出错（如果没有提供相应的错误处理代码）

* 安全组合模式

    在安全组合模式中，在抽象构件角色中没有声明任何用于管理成员对象的方法，而是在树枝节点 `Menu` 类中声明并实现这些方法。安全组合模式的缺点是不够透明，因为叶子构件和容器构件具有不同的方法，且容器构件中那些用于管理成员对象的方法没有在抽象构件类中定义，因此客户端不能完全针对抽象编程，必须有区别地对待叶子构件和容器构件。

    ![image-20211208122845635](设计模式.assets/image-20211208122845635.png)

### 3.6.5 优点

* 组合模式可以清楚地定义分层次的复杂对象，表示对象的全部或部分层次，它让客户端忽略了层次的差异，方便对整个层次结构进行控制。
* 客户端可以一致地使用一个组合结构或其中单个对象，不必关心处理的是单个对象还是整个组合结构，简化了客户端代码。
* 在组合模式中增加新的树枝节点和叶子节点都很方便，无须对现有类库进行任何修改，符合“开闭原则”。
* 组合模式为树形结构的面向对象实现提供了一种灵活的解决方案，通过叶子节点和树枝节点的递归组合，可以形成复杂的树形结构，但对树形结构的控制却非常简单。

### 3.6.6 使用场景

组合模式正是应树形结构而生，所以组合模式的使用场景就是出现树形结构的地方。比如：文件目录显示，多级目录呈现等树形结构数据的操作。

## 3.7 享元模式(Flyweight Pattern)

### 3.7.1 概述

**定义：**

​	运用共享技术来有效地支持大量细粒度对象的复用。它通过共享已经存在的对象来大幅度减少需要创建的对象数量、避免大量相似对象的开销，从而提高系统资源的利用率。



### 3.7.2 结构

享元（Flyweight ）模式中存在以下两种状态：

1. 内部状态，即不会随着环境的改变而改变的可共享部分。
2. 外部状态，指随环境改变而改变的不可以共享的部分。享元模式的实现要领就是区分应用中的这两种状态，并将外部状态外部化。

享元模式的主要有以下角色：

* 抽象享元角色（Flyweight）：通常是一个接口或抽象类，在抽象享元类中声明了具体享元类公共的方法，这些方法可以向外界提供享元对象的内部数据（内部状态），同时也可以通过这些方法来设置外部数据（外部状态）。
* 具体享元（Concrete Flyweight）角色 ：它实现了抽象享元类，称为享元对象；在具体享元类中为内部状态提供了存储空间。通常我们可以结合单例模式来设计具体享元类，为每一个具体享元类提供唯一的享元对象。
* 非享元（Unsharable Flyweight)角色 ：并不是所有的抽象享元类的子类都需要被共享，不能被共享的子类可设计为非共享具体享元类；当需要一个非共享具体享元类的对象时可以直接通过实例化创建。
* 享元工厂（Flyweight Factory）角色 ：负责创建和管理享元角色。当客户对象请求一个享元对象时，享元工厂检査系统中是否存在符合要求的享元对象，如果存在则提供给客户；如果不存在的话，则创建一个新的享元对象。



### 3.7.3 案例实现

【例】俄罗斯方块

下面的图片是众所周知的俄罗斯方块中的一个个方块，如果在俄罗斯方块这个游戏中，每个不同的方块都是一个实例对象，这些对象就要占用很多的内存空间，下面利用享元模式进行实现。

![image-20211208124826702](设计模式.assets/image-20211208124826702.png)

**先来看类图：**

![image-20211208124842198](设计模式.assets/image-20211208124842198.png)

```csharp
using System;
using System.Collections.Generic;

namespace DesignPatternDemo
{
    internal static class Program
    {
        private static void Main( )
        {
            //获取I图形对象
            var box1 = BoxFactory.Instance.GetShape("Z");
            box1.Display("灰色");

            //获取L图形对象
            var box2 = BoxFactory.Instance.GetShape("L");
            box2.Display("绿色");

            //获取O图形对象
            var box3 = BoxFactory.Instance.GetShape("O");
            box3.Display("灰色");

            //获取O图形对象
            var box4 = BoxFactory.Instance.GetShape("O");
            box4.Display("红色");

            Console.WriteLine ( "两次获取到的O图形对象是否是同一个对象：" + (box3 == box4) );
        }
    }
    
    /// <summary>
    /// 抽象享元角色
    /// </summary>
    public abstract class AbstractBox
    {
        // 获取图形的方法
        protected abstract string GetShape();

        // 显示图形及颜色
        public void Display ( string color ) => Console.WriteLine ( $"方块形状：{this.GetShape ( )}, 颜色： {color}" );
    }
    
    /// <summary>
    /// 具体享元角色
    /// </summary>
    public class ZBox : AbstractBox
    {
        protected override string GetShape ( ) => "Z";
    }
    
    /// <summary>
    /// 具体享元角色
    /// </summary>
    public class LBox : AbstractBox
    {
        protected override string GetShape ( ) => "L";
    }
    
    /// <summary>
    /// 具体享元角色
    /// </summary>
    public class OBox : AbstractBox
    {
        protected override string GetShape ( ) => "O";
    }
    
    public class BoxFactory
    {
        private readonly Dictionary<string,AbstractBox> dict;

        //在构造方法中进行初始化操作
        private BoxFactory()
        {
            this.dict = new Dictionary<string, AbstractBox>
                       {
                           { "Z", new ZBox ( ) }, { "L", new LBox ( ) }, { "O", new OBox ( ) }
                       };
        }

        //提供一个方法获取该工厂类对象
        public static  BoxFactory Instance => factory;
        private static readonly BoxFactory factory = new ();

        //根据名称获取图形对象
        public AbstractBox GetShape(string name) => this.dict[name];
    }

}
```



# 4. 行为型模式

行为型模式用于描述程序在运行时复杂的流程控制，即描述多个类或对象之间怎样相互协作共同完成单个对象都无法单独完成的任务，它涉及算法与对象间职责的分配。

行为型模式分为类行为模式和对象行为模式，前者采用继承机制来在类间分派行为，后者采用组合或聚合在对象间分配行为。由于组合关系或聚合关系比继承关系耦合度低，满足“合成复用原则”，所以对象行为模式比类行为模式具有更大的灵活性。

## 4.1 模板方法模式(Template Method Pattern)

### 4.1.1 概述

在面向对象程序设计过程中，程序员常常会遇到这种情况：设计一个系统时知道了算法所需的关键步骤，而且确定了这些步骤的执行顺序，但某些步骤的具体实现还未知，或者说某些步骤的实现与具体的环境相关。

例如，去银行办理业务一般要经过以下4个流程：取号、排队、办理具体业务、对银行工作人员进行评分等，其中取号、排队和对银行工作人员进行评分的业务对每个客户是一样的，可以在父类中实现，但是办理具体业务却因人而异，它可能是存款、取款或者转账等，可以延迟到子类中实现。

**定义：**

定义一个操作中的算法骨架，而将算法的一些步骤延迟到子类中，使得子类可以不改变该算法结构的情况下重定义该算法的某些特定步骤。



###  4.1.2 结构

模板方法（Template Method）模式包含以下主要角色：

* 抽象类（Abstract Class）：负责给出一个算法的轮廓和骨架。它由一个模板方法和若干个基本方法构成。

    * 模板方法：定义了算法的骨架，按某种顺序调用其包含的基本方法。

    * 基本方法：是实现算法各个步骤的方法，是模板方法的组成部分。基本方法又可以分为三种：

        * 抽象方法(Abstract Method) ：一个抽象方法由抽象类声明、由其具体子类实现。

        * 具体方法(Concrete Method) ：一个具体方法由一个抽象类或具体类声明并实现，其子类可以进行覆盖也可以直接继承。

        * 钩子方法(Hook Method) ：在抽象类中已经实现，包括用于判断的逻辑方法和需要子类重写的空方法两种。

            一般钩子方法是用于判断的逻辑方法，这类方法名一般为isXxx，返回值类型为boolean类型。

* 具体子类（Concrete Class）：实现抽象类中所定义的抽象方法和钩子方法，它们是一个顶级逻辑的组成步骤。



### 4.1.3 案例实现

【例】炒菜

炒菜的步骤是固定的，分为倒油、热油、倒蔬菜、倒调料品、翻炒等步骤。现通过模板方法模式来用代码模拟。类图如下：

![image-20211208152932214](设计模式.assets/image-20211208152932214.png)

```csharp
using System;

namespace DesignPatternDemo
{
    internal static class Program
    {
        private static void Main( )
        {
            AbstractClass baoCai = new ConcreteClassBaoCai();
            baoCai.CookProcess();

            Console.WriteLine("--------------------------------");
            AbstractClass caiXin = new ConcreteClassCaiXin();
            caiXin.CookProcess();
        }
    }
    
    public abstract class AbstractClass {

        // 模板方法定义
        public void CookProcess()
        {
            PourOil();
            HeatOil();
            this.PourVegetable();
            this.PourSauce();
            Fry();
        }

        //第一步：热油是一样的，所以直接实现
        private static void PourOil ( ) => Console.WriteLine ( "倒油" );

        //第二步：热油是一样的，所以直接实现
        private static void HeatOil ( ) => Console.WriteLine ( "热油" );

        //第三步：倒蔬菜是不一样的（一个下包菜，一个是下菜心）
        protected abstract void PourVegetable();

        //第四步：倒调味料是不一样
        protected abstract void PourSauce();

        //第五步：翻炒是一样的，所以直接实现
        private static void Fry() => Console.WriteLine("炒啊炒啊炒到熟啊");
    }

    public class ConcreteClassBaoCai : AbstractClass
    {
        protected override void PourVegetable() => Console.WriteLine("下锅的蔬菜是包菜");

        protected override void PourSauce() => Console.WriteLine("下锅的酱料是辣椒");
    }

    public class ConcreteClassCaiXin : AbstractClass
    {
        protected override void PourVegetable() => Console.WriteLine("下锅的蔬菜是菜心");

        protected override void PourSauce() => Console.WriteLine("下锅的酱料是蒜蓉");
    }
}
```

### 4.1.3 优缺点

**优点：**

* 提高代码复用性

    将相同部分的代码放在抽象的父类中，而将不同的代码放入不同的子类中。

* 实现了反向控制

    通过一个父类调用其子类的操作，通过对子类的具体实现扩展不同的行为，实现了反向控制 ，并符合“开闭原则”。

**缺点：**

* 对每个不同的实现都需要定义一个子类，这会导致类的个数增加，系统更加庞大，设计也更加抽象。
* 父类中的抽象方法由子类实现，子类执行的结果会影响父类的结果，这导致一种反向的控制结构，它提高了代码阅读的难度。



### 4.1.4 适用场景

* 算法的整体步骤很固定，但其中个别部分易变时，这时候可以使用模板方法模式，将容易变的部分抽象出来，供子类实现。
* 需要通过子类来决定父类算法中某个步骤是否执行，实现子类对父类的反向控制。



## 4.2 策略模式(Strategy Pattern)

### 4.2.1 概述

先看下面的图片，我们去旅游选择出行模式有很多种，可以骑自行车、可以坐汽车、可以坐火车、可以坐飞机。

![image-20211208154952881](设计模式.assets/image-20211208154952881.png)

作为一个程序猿，开发需要选择一款开发工具，当然可以进行代码开发的工具有很多，可以选择Idea进行开发，也可以使用eclipse进行开发，也可以使用其他的一些开发工具。

![image-20211208155005124](设计模式.assets/image-20211208155005124.png)



**定义：**

该模式定义了一系列算法，并将每个算法封装起来，使它们可以相互替换，且算法的变化不会影响使用算法的客户。策略模式属于对象行为模式，它通过对算法进行封装，把使用算法的责任和算法的实现分割开来，并委派给不同的对象对这些算法进行管理。



### 4.2.2 结构

策略模式的主要角色如下：

* 抽象策略（Strategy）类：这是一个抽象角色，通常由一个接口或抽象类实现。此角色给出所有的具体策略类所需的接口。
* 具体策略（Concrete Strategy）类：实现了抽象策略定义的接口，提供具体的算法实现或行为。
* 环境（Context）类：持有一个策略类的引用，最终给客户端调用。



### 4.2.3 案例实现

【例】促销活动

一家百货公司在定年度的促销活动。针对不同的节日（春节、中秋节、圣诞节）推出不同的促销活动，由促销员将促销活动展示给客户。类图如下：

![image-20211208155032306](设计模式.assets/image-20211208155032306.png)

```csharp
using System;

namespace DesignPatternDemo
{
    internal static class Program
    {
        private static void Main( )
        {
            //春节来了，使用春节促销活动
            var salesMan = new SalesMan { Strategy = new StrategyA ( ) };
            salesMan.SalesManShow();

            Console.WriteLine ( "==============" );
            //中秋节到了，使用中秋节的促销活动
            salesMan.Strategy = new StrategyB ( );
            //展示促销活动
            salesMan.SalesManShow();

            Console.WriteLine ( "==============" );
            //圣诞节到了，使用圣诞节的促销活动
            salesMan.Strategy = new StrategyC ( );
            //展示促销活动
            salesMan.SalesManShow();
        }
    }
    
    /// <summary>
    /// 促销员(环境类)
    /// </summary>
    public class SalesMan
    {
        //聚合策略类对象
        public IStrategy Strategy { get; set; }

        //由促销员展示促销活动给用户
        public void SalesManShow()
        {
            this.Strategy.Show();
        }
    }
    
    /// <summary>
    /// 抽象策略类
    /// </summary>
    public interface IStrategy
    {
        void Show();
    }
    
    /// <summary>
    /// 具体策略类
    /// </summary>
    public class StrategyA : IStrategy
    {
        public void Show() => Console.WriteLine ( "买一送一" );
    }
    
    /// <summary>
    /// 具体策略类
    /// </summary>
    public class StrategyB : IStrategy
    {
        public void Show() => Console.WriteLine ( "满200元减50元" );
    }
    
    /// <summary>
    /// 具体策略类
    /// </summary>
    public class StrategyC : IStrategy
    {
        public void Show() => Console.WriteLine ( "满1000元加一元换购任意200元以下商品" );
    }
}
```

### 4.2.4 优缺点

**1，优点：**

* 策略类之间可以自由切换

    由于策略类都实现同一个接口，所以使它们之间可以自由切换。

* 易于扩展

    增加一个新的策略只需要添加一个具体的策略类即可，基本不需要改变原有的代码，符合“开闭原则“

* 避免使用多重条件选择语句（if else），充分体现面向对象设计思想。

**2，缺点：**

* 客户端必须知道所有的策略类，并自行决定使用哪一个策略类。
* 策略模式将造成产生很多策略类，可以通过使用享元模式在一定程度上减少对象的数量。



### 4.2.5 使用场景

* 一个系统需要动态地在几种算法中选择一种时，可将每个算法封装到策略类中。
* 一个类定义了多种行为，并且这些行为在这个类的操作中以多个条件语句的形式出现，可将每个条件分支移入它们各自的策略类中以代替这些条件语句。
* 系统中各算法彼此完全独立，且要求对客户隐藏具体算法的实现细节时。
* 系统要求使用算法的客户不应该知道其操作的数据时，可使用策略模式来隐藏与算法相关的数据结构。
* 多个类只区别在表现行为不同，可以使用策略模式，在运行时动态选择具体要执行的行为。

## 4.3 命令模式(Command Pattern)

### 6.3.1 概述

日常生活中，我们出去吃饭都会遇到下面的场景。

![image-20211208165645794](设计模式.assets/image-20211208165645794.png)



**定义：**

将一个请求封装为一个对象，使发出请求的责任和执行请求的责任分割开。这样两者之间通过命令对象进行沟通，这样方便将命令对象进行存储、传递、调用、增加与管理。



### 6.3.2 结构

命令模式包含以下主要角色：

* 抽象命令类（Command）角色： 定义命令的接口，声明执行的方法。
* 具体命令（Concrete  Command）角色：具体的命令，实现命令接口；通常会持有接收者，并调用接收者的功能来完成命令要执行的操作。
* 实现者/接收者（Receiver）角色： 接收者，真正执行命令的对象。任何类都可能成为一个接收者，只要它能够实现命令要求实现的相应功能。
* 调用者/请求者（Invoker）角色： 要求命令对象执行请求，通常会持有命令对象，可以持有很多的命令对象。这个是客户端真正触发命令并要求命令执行相应操作的地方，也就是说相当于使用命令对象的入口。



### 6.3.3 案例实现

将上面的案例用代码实现，那我们就需要分析命令模式的角色在该案例中由谁来充当。

服务员： 就是调用者角色，由她来发起命令。

资深大厨： 就是接收者角色，真正命令执行的对象。

订单： 命令中包含订单。

类图如下：

![image-20211208165703707](设计模式.assets/image-20211208165703707.png)

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternDemo
{
    internal static class Program
    {
        private static void Main( )
        {
            //创建第一个订单对象
            var order1 = new Order { DiningTable = 1 };
            order1.SetFood("西红柿鸡蛋面", 1);
            order1.SetFood("小杯可乐",   2);

            //创建第二个订单对象
            var order2 = new Order { DiningTable = 2 };
            order2.SetFood("尖椒肉丝盖饭", 1);
            order2.SetFood("小杯雪碧",   1);

            //创建厨师对象
            var receiver = new SeniorChef();
            //创建命令对象
            var cmd1 = new OrderCommand(receiver, order1);
            var cmd2 = new OrderCommand(receiver, order2);

            //创建调用者（服务员对象）
            var invoke = new Waiter();
            invoke.SetCommand(cmd1);
            invoke.SetCommand(cmd2);

            //让服务员发起命令
            invoke.OrderUp();
        }
    }
    
    /// <summary>
    /// 请求内容
    /// </summary>
    public class Order
    {
        //餐桌号码
        public int DiningTable { get; init; }
        //所下的餐品及份数
        public Dictionary < string, int > FoodDir { get; } = new ( );
        // 设置菜品与份数
        public void SetFood(string name, int num) => this.FoodDir.Add(name, num);
    }
    
    /// <summary>
    /// 实现者/接收者角色
    /// </summary>
    public class SeniorChef
    {
        public void MakeFood(string name, int num) => Console.WriteLine ( num + "份" + name );
    }
    
    /// <summary>
    /// 调用者/请求者角色
    /// </summary>
    public class Waiter
    {
        // 持有多个命令对象
        private List<ICommand> mCommands = new ();

        public void SetCommand(ICommand cmd) =>this.mCommands.Add(cmd);

        //发起命令功能  喊 订单来了
        public void OrderUp()
        {
            Console.WriteLine ("美女服务员：大厨，新订单来了。。。。");
            //遍历list集合
            foreach ( var command in this.mCommands.Where ( command => command != null ) )
            {
                command.Execute();
            }
        }
    }
    
    /// <summary>
    /// 抽象命令类角色
    /// </summary>
    public interface ICommand
    {
        void Execute();
    }
    
    /// <summary>
    /// 具体命令角色
    /// </summary>
    public class OrderCommand : ICommand
    {
        // 持有接收者对象
        private SeniorChef mReceiver;
        
        // 命令内容
        private Order      mOrder;

        public OrderCommand(SeniorChef receiver, Order order)
        {
            this.mReceiver = receiver;
            this.mOrder    = order;
        }

        public void Execute() 
        {
            Console.WriteLine(this.mOrder.DiningTable + "桌的订单：");
            var foodDir = this.mOrder.FoodDir;

            foreach ( var foodName in foodDir.Keys )
            {
                this.mReceiver.MakeFood(foodName, foodDir[foodName]);
            }
            Console.WriteLine( this.mOrder.DiningTable + "桌的饭准备完毕！！！");
        }
    }
}
```

### 4.3.4 优缺点

**1，优点：**

* 降低系统的耦合度。命令模式能将调用操作的对象与实现该操作的对象解耦。
* 增加或删除命令非常方便。采用命令模式增加与删除命令不会影响其他类，它满足“开闭原则”，对扩展比较灵活。
* 可以实现宏命令。命令模式可以与组合模式结合，将多个命令装配成一个组合命令，即宏命令。
* 方便实现 Undo 和 Redo 操作。命令模式可以与后面介绍的备忘录模式结合，实现命令的撤销与恢复。

**2，缺点：**

* 使用命令模式可能会导致某些系统有过多的具体命令类。
* 系统结构更加复杂。



### 4.3.5 使用场景

* 系统需要将请求调用者和请求接收者解耦，使得调用者和接收者不直接交互。
* 系统需要在不同的时间指定请求、将请求排队和执行请求。
* 系统需要支持命令的撤销(Undo)操作和恢复(Redo)操作。



## 4.4 职责链模式(Chain of Responsibility Pattern)

### 4.4.1 概述

在现实生活中，常常会出现这样的事例：一个请求有多个对象可以处理，但每个对象的处理条件或权限不同。例如，公司员工请假，可批假的领导有部门负责人、副总经理、总经理等，但每个领导能批准的天数不同，员工必须根据自己要请假的天数去找不同的领导签名，也就是说员工必须记住每个领导的姓名、电话和地址等信息，这增加了难度。这样的例子还有很多，如找领导出差报销、生活中的“击鼓传花”游戏等。

**定义：**

又名职责链模式，为了避免请求发送者与多个请求处理者耦合在一起，将所有请求的处理者通过前一对象记住其下一个对象的引用而连成一条链；当有请求发生时，可将请求沿着这条链传递，直到有对象处理它为止。



### 4.4.2 结构

职责链模式主要包含以下角色:

* 抽象处理者（Handler）角色：定义一个处理请求的接口，包含抽象处理方法和一个后继连接。
* 具体处理者（Concrete Handler）角色：实现抽象处理者的处理方法，判断能否处理本次请求，如果可以处理请求则处理，否则将该请求转给它的后继者。
* 客户类（Client）角色：创建处理链，并向链头的具体处理者对象提交请求，它不关心处理细节和请求的传递过程。



### 4.4.3 案例实现

现需要开发一个请假流程控制系统。请假一天以下的假只需要小组长同意即可；请假1天到3天的假还需要部门经理同意；请求3天到7天还需要总经理同意才行。

类图如下：

![image-20211209085446609](设计模式.assets/image-20211209085446609.png)

```csharp
using System;

namespace DesignPatternDemo
{
    internal static class Program
    {
        private static void Main ( )
        {
            //创建一个请假条对象
            var leave = new LeaveRequest ( "小明", 8, "身体不适" );

            //创建各级领导对象
            var groupLeader    = new GroupLeader ( );
            var manager        = new Manager ( );
            var generalManager = new GeneralManager ( );

            //设置处理者链
            groupLeader.NextHandler = manager ;
            manager.NextHandler =  generalManager;

            //小明提交请假申请
            groupLeader.Submit ( leave );
        }
    }

    public class LeaveRequest
    {
        public string Content { get; } // 请假内容
        public string Name    { get; } // 姓名
        public int    Num     { get; } // 请假天数

        public LeaveRequest ( string name, int num, string content )
        {
            this.Name    = name;
            this.Num     = num;
            this.Content = content;
        }
    }

    public abstract class Handler
    {
        protected const int NumOne   = 1;
        protected const int NumThree = 3;
        protected const int NumSeven = 7;

        // 声明后续者（声明上级领导）
        public           Handler NextHandler { get; set; }
        
        // 该领导处理请假天数的上限
        private int NumEnd { get; }

        protected Handler ( int numEnd )
        {
            this.NumEnd = numEnd;
        }
        

        //各级领导处理请求条的方法
        protected abstract void HandleLeave ( LeaveRequest leave );

        //提交请求条
        public void Submit ( LeaveRequest leave )
        {
            // 该领导进行审批
            this.HandleLeave ( leave );
            if ( this.NextHandler != null && leave.Num > this.NumEnd ) // 提交给上级领导进行审批
                this.NextHandler.Submit ( leave );
            else
                Console.WriteLine ( "流程结束！" );
        }
    }

    public class GroupLeader : Handler
    {
        public GroupLeader ( ) : base ( NumOne ) { }

        protected override void HandleLeave ( LeaveRequest leave )
        {
            Console.WriteLine ( leave.Name + "请假" + leave.Num+ "天，" + leave.Content + "。" );
            Console.WriteLine ( "小组长审批：同意" );
        }
    }

    public class Manager : Handler
    {
        public Manager ( ) : base ( NumThree ) { }

        protected override void HandleLeave ( LeaveRequest leave )
        {
            Console.WriteLine ( leave.Name + "请假" + leave.Num + "天，" + leave.Content + "。" );
            Console.WriteLine ( "部门经理审批：同意" );
        }
    }

    public class GeneralManager : Handler
    {
        public GeneralManager ( ) : base ( NumSeven ) { }

        protected override void HandleLeave ( LeaveRequest leave )
        {
            Console.WriteLine ( leave.Name + "请假" + leave.Num + "天，" + leave.Content + "。" );
            Console.WriteLine ( "总经理审批：同意" );
        }
    }
}
```

### 6.4.4 优缺点

**1，优点：**

* 降低了对象之间的耦合度

    该模式降低了请求发送者和接收者的耦合度。

* 增强了系统的可扩展性

    可以根据需要增加新的请求处理类，满足开闭原则。

* 增强了给对象指派职责的灵活性

    当工作流程发生变化，可以动态地改变链内的成员或者修改它们的次序，也可动态地新增或者删除责任。

* 责任链简化了对象之间的连接

    一个对象只需保持一个指向其后继者的引用，不需保持其他所有处理者的引用，这避免了使用众多的 if 或者 if···else 语句。

* 责任分担

    每个类只需要处理自己该处理的工作，不能处理的传递给下一个对象完成，明确各类的责任范围，符合类的单一职责原则。

**2，缺点：**

* 不能保证每个请求一定被处理。由于一个请求没有明确的接收者，所以不能保证它一定会被处理，该请求可能一直传到链的末端都得不到处理。
* 对比较长的职责链，请求的处理可能涉及多个处理对象，系统性能将受到一定影响。
* 职责链建立的合理性要靠客户端来保证，增加了客户端的复杂性，可能会由于职责链的错误设置而导致系统出错，如可能会造成循环调用。





# 15 迭代器模式(Iterator Pattern)

## 15.1 动机

在软件构建过程中，集合对象内部结构常常变化各异。但对于这些集合对象，我们希望在不暴露其内部结构的同时，可以让外部客户代码透明地访问其中包含的元素;同时这种“透明遍历”也为“ 同一种算法在多种集合对象上进行操作”提供了可能。

> 使用面向对象技术将这种遍历机制抽象为“迭代器对象”为“应对变化中的集合对象”提供了一种优雅的方法。

## 15.2 意图

提供一种方法顺序访问一个聚合对象中各个元素, 而又不需暴露该对象的内部表示。

## 15.3 适用性

1. 访问一个聚合对象的内容而无需暴露它的内部表示。
2. 支持对聚合对象的多种遍历。
3. 为遍历不同的聚合结构提供一个统一的接口(即, 支持多态迭代)。

## 15.4 要点

1. 迭代抽象：访问一个聚合对象的内容而无需暴露它的内部表示。
2. 迭代多态：为遍历不同的集合结构提供一个统一的接口，从而支持同样的算法在不同的集合结构上进行操作。
3. 迭代器的健壮性考虑：遍历的同时更改迭代器所在的集合结构，会导致问题。

## 15.5 场景

我们将创建一个叙述导航方法的 *Iterator* 接口和一个返回迭代器的 *Container* 接口。实现了 *Container* 接口的实体类将负责实现 *Iterator* 接口。

```c#
using System;

namespace IteratorPattern
{
    public interface Iterator
    {
        bool hasNext();
        Object next();
    }

    public interface Container
    {
        Iterator getIterator();
    }
    
    public class NameRepository : Container 
    {
        private string[] names = new string[]{"Robert" , "John" ,"Julie" , "Lora"};
        public Iterator getIterator() 
        {
            return new NameIterator(this);
        }

        private class NameIterator : Iterator
        {
            private NameRepository nameRepository;
            public NameIterator(NameRepository nameRepository)
            {
                this.nameRepository = nameRepository;
            }
            int index;
            public bool hasNext()
            {
                if(index < this.nameRepository.names.Length)
                {
                    return true;
                }
                return false;
            }

            public Object next()
            {
                if(this.hasNext())
                {
                    return this.nameRepository.names[index++];
                }
                return null;
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            NameRepository namesRepository = new NameRepository();
 
            for(Iterator iter = namesRepository.getIterator(); iter.hasNext();)
            {
                String name = (String)iter.next();
                System.Console.WriteLine("Name : " + name);
            }  
        }
    }
}
```

# 16 观察者模式(Observer Pattern)

## 16.1 动机

在软件构建 过程中，我们需要为某些对象建立一种“通知依赖关系” --------一个对象（目标对象）的状态发生改变，所有的依赖对象（观察者对象）都将得到通知。如果这样的依赖关系过于紧密，将使软件不能很好地抵御变化。使用面 向对象技术，可以将这种依赖关系弱化，并形成一种稳定的依赖关系。从而实现软件体系结构的松耦合。

## 16.2 意图

定义对象间的一种一对多的依赖关系,当一个对象的状态发生改变时, 所有依赖于它的对象都得到通知并被自动更新。

## 16.3 适用性

1. 当一个抽象模型有两个方面, 其中一个方面依赖于另一方面。将这二者封装在独立的对象中以使它们可以各自独立地改变和复用。
2. 当对一个对象的改变需要同时改变其它对象, 而不知道具体有多少对象有待改变。
3. 当一个对象必须通知其它对象，而它又不能假定其它对象是谁。换言之, 你不希望这些对象是紧密耦合的。

> **推模式与拉模式**  
>
> 对于发布-订阅模型，大家都很容易能想到推模式与拉模式，用SQL Server做过数据库复制的朋友对这一点很清楚。在Observer模式中同样区分推模式和拉模式，我先简单的解释一下两者的区别：推模式是当有消息时，把消息信息以参数的形式传递（推）给所有观察者，而拉模式是当有消息时，通知消息的方法本身并不带任何的参数，是由观察者自己到主体对象那儿取回（拉）消息。知道了这一点，大家可能很容易发现上面我所举的例子其实是一种推模式的Observer模式。我们先看看这种模式带来了什么好处：当有消息时，所有的 观察者都会直接得到全部的消息，并进行相应的处理程序，与主体对象没什么关系，两者之间的关系是一种松散耦合。但是它也有缺陷，第一是所有的观察者得到的 消息是一样的，也许有些信息对某个观察者来说根本就用不上，也就是观察者不能“按需所取”；第二，当通知消息的参数有变化时，所有的观察者对象都要变化。鉴于以上问题，拉模式就应运而生了，它是由观察者自己主动去取消息，需要什么信息，就可以取什么，不会像推模式那样得到所有的消息参数。

## 16.4 要点

1. 使用面向对象的抽象，Observer模式使得我们可以独立地改变目标与观察者，从而使二者之间的依赖关系达到松耦合。
2. 目标发送通知时，无需指定观察者，通知（可以携带通知信息作为参数）会自动传播。观察者自己决定是否需要订阅通知。目标对象对此一无所知。
3. 在C#中的Event。委托充当了抽象的Observer接口，而提供事件的对象充当了目标对象，委托是比抽象Observer接口更为松耦合的设计。

## 16.5 场景

观察者模式使用三个类 Subject、Observer 和 Client。Subject 对象带有绑定观察者到 Client 对象和从 Client 对象解绑观察者的方法。我们创建 *Subject* 类、*Observer* 抽象类和扩展了抽象类 *Observer* 的实体类。

```c#
using System;
using System.Collections.Generic;

namespace ObserverPattern
{
    public abstract class Observer
    {
        protected Subject subject;
        public abstract void update();
    }

    public class Subject
    {
        private List<Observer> observers = new List<Observer>();
        private int state;
        public int getState()
        {
            return state;
        }

        public void setState(int state)
        {
            this.state = state;
            notifyAllObservers();
        }
        public void attach(Observer observer)
        {
            observers.Add(observer);
        }
        public void notifyAllObservers()
        {
            foreach (Observer observer in observers)
            {
                observer.update();
            }
        }
    }

    public class BinaryObserver : Observer
    {
        public BinaryObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }

        public override void update()
        {
            System.Console.WriteLine( "Binary String: "  + Convert.ToString(subject.getState(), 2)); 
        }
    }

    public class OctalObserver : Observer
    {
        public OctalObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }

        public override void update()
        {
            System.Console.WriteLine( "Octal String: " + Convert.ToString(subject.getState(), 8)); 
        }
    }

    public class HexaObserver : Observer
    {
        public HexaObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }

        public override void update()
        {
            System.Console.WriteLine( "Hex String: " + Convert.ToString(subject.getState(), 16).ToUpper()); 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Subject subject = new Subject();
            new HexaObserver(subject);
            new OctalObserver(subject);
            new BinaryObserver(subject);
        
            System.Console.WriteLine("First state change: 15");   
            subject.setState(15);

            System.Console.WriteLine("Second state change: 10");  
            subject.setState(10);
        }
    }
}
```



# 17 解释器模式(Interpreter Pattern)

## 17.1 动机

 在软件构建过程中，如果某一特定领域的问题比较复杂，类似的模式不断重复出现，如果使用普通的编程方式来实现将面临非常频繁的变化。
 在这种情况下，将特定领域的问题表达为某种文法规则下的句子，然后构建一个解释器来解释这样的句子，从而达到解决问题的目的。

## 17.2 意图

给定一个语言，定义它的文法的一种表示，并定义一个解释器，这个解释器使用该表示来解释语言中的句子。

## 17.3 适用性

1. 当有一个语言需要解释执行，并且你可将该语言中的句子表示为一个抽象语法树时，可使用解释器模式。而当存在以下情况时该模式效果最好：
2. 该文法简单对于复杂的文法，文法的类层次变得庞大而无法管理。此时语法分析程序生成器这样的工具是更好的选择。它们无需构建抽象语法树即可解释表达工，这样可以节省空间而且还可能节省时间。
3. 效率不是一个关键问题最高效的解释器通常不是通过直接解释语法分析树实现的，而是首先将它们转换成另一种形式。例如：正则表达式通常被转换成状态机。但即使在这种情况下，转换器仍可用解释器模式实现，该模式仍是有用的。

## 17.4 要点

1. Interpreter模式的应用场合是interpreter模式应用中的难点，只有满足"业务规则频繁变化，且类似的模式不断重复出现，并且容易抽象为语法规则的问题"才适合使用Interpreter模式。
2. 使用Interpreter模式来表示文法规则，从而可以使用面向对象技巧来方便地“扩展”文法。
3. Interpreter模式比较适合简单的文法表示，对于复杂的文法表示，Interpreter模式会产生比较大的类层次结构，需要求助于语法分析生成器这样的标准工具。

## 17.5 场景

我们将创建一个接口 *Expression* 和实现了 *Expression* 接口的实体类。定义作为上下文中主要解释器的 *TerminalExpression* 类。其他的类 *OrExpression*、*AndExpression* 用于创建组合式表达式。

```c#
using System;

namespace InterpreterPattern
{
    public interface Expression
    {
        bool interpret(string context);
    }

    public class TerminalExpression : Expression
    {
        private string data;
        public TerminalExpression(string data)
        {
            this.data = data; 
        }

        public bool interpret(string context)
        {
            if(context.Contains(data))
            {
                return true;
            }
            return false;
        }
    }
    public class OrExpression : Expression
    {
        private Expression expr1 = null;
        private Expression expr2 = null;
        public OrExpression(Expression expr1, Expression expr2)
        {
            this.expr1 = expr1;
            this.expr2 = expr2;
        }
        public bool interpret(string context)
        {
            return expr1.interpret(context) || expr2.interpret(context);
        }
    }

    public class AndExpression : Expression
    {
        private Expression expr1 = null;
        private Expression expr2 = null;
        public AndExpression(Expression expr1, Expression expr2)
        {
            this.expr1 = expr1;
            this.expr2 = expr2;
        }
        public bool interpret(string context)
        {
            return expr1.interpret(context) && expr2.interpret(context);
        }
    }

    class Program
    {
        //规则：Robert 和 John 是男性
        public static Expression getMaleExpression()
        {
            Expression robert = new TerminalExpression("Robert");
            Expression john = new TerminalExpression("John");
            return new OrExpression(robert, john);
        }

        //规则：Julie 是一个已婚的女性
        public static Expression getMarriedWomanExpression()
        {
            Expression julie = new TerminalExpression("Julie");
            Expression married = new TerminalExpression("Married");
            return new AndExpression(julie, married);
        }

        static void Main(string[] args)
        {
            Expression isMale = getMaleExpression();
            Expression isMarriedWoman = getMarriedWomanExpression();
 
            System.Console.WriteLine("John is male? " + isMale.interpret("John"));
            System.Console.WriteLine("Julie is a married women? " + isMarriedWoman.interpret("Married Julie"));
        }
    }
}
```





# 18 中介者模式(Mediator Pattern)

## 18.1 动机

在软件构建过程中，经常会出现多个对象互相关联交互的情况，对象之间常常会维持一种复杂的引用关系，如果遇到一些需求的更改，这种直接的引用关系将面临不断的变化。
  在这种情况下，我们可使用一个“中介对象”来管理对象间的关联关系，避免相互交互的对象之间的紧耦合引用关系，从而更好地抵御变化。

## 18.2 意图

用一个中介对象来封装一系列对象交互。中介者使各对象不需要相互引用，从而使其耦合松散，而且可以独立地改变它们之间的交互。     

## 18.3 适用性

1. 一组对象以定义良好但是复杂的方式进行通信。产生的相互依赖关系结构混乱且难以理解。
2. 一个对象引用其他很多对象并且直接与这些对象通信，导致难以复用该对象。
3. 想定制一个分布在多个类中的行为，而又不想生成太多的子类。

## 18.4 要点

1. 将多个对象间复杂的关联关系解耦，Mediator模式将多个对象间的控制逻辑进行集中管理，变“多个对象互相关系”为多“个对象和一个中介者关联”，简化了系统的维护，抵御了可能的变化。
2. 随着控制逻辑的复杂化，Mediator具体对象的实现可能相当复杂。 这时候可以对Mediator对象进行分解处理。
3. Facade模式是解耦系统外到系统内(单向)的对象关系关系;Mediator模式是解耦系统内各个对象之间(双向)的关联关系。

## 18.5 场景

我们通过聊天室实例来演示中介者模式。实例中，多个用户可以向聊天室发送消息，聊天室向所有的用户显示消息。我们将创建两个类 *ChatRoom* 和 *User*。*User* 对象使用 *ChatRoom* 方法来分享他们的消息。

```c#
using System;

namespace MediatorPattern
{
    public class ChatRoom
    {
        public static void showMessage(User user, String message)
        {
            System.Console.WriteLine(DateTime.Now.ToString() + " [" + user.getName() +"] : " + message);
        }
    }

    public class User
    {
        private String name;
        public String getName()
        {
            return name;
        }
        public void setName(String name)
        {
            this.name = name;
        }
        public User(String name)
        {
            this.name  = name;
        }
        public void sendMessage(String message)
        {
            ChatRoom.showMessage(this,message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            User robert = new User("Robert");
            User john = new User("John");
        
            robert.sendMessage("Hi! John!");
            john.sendMessage("Hello! Robert!");
        }
    }
}
```



# 19 职责链模式(Chain of Responsibility Pattern)

## 19.1 动机

在软件构建过程中，一个请求可能被多个对象处理，但是每个请求在运行时只能有一个接受者，如果显示指定，将必不可少地带来请求发送者与接受者的紧耦合。

> 如何使请求的发送者不需要指定具体的接受者？让请求的接受者自己在运行时决定来处理请求，从而使两者解耦。

## 19.2 意图

使多个对象都有机会处理请求，从而避免请求的发送者和接收者之间的耦合关系。将这些`接收者`对象连成一条链，并沿着这条链传递该请求，直到有一个对象处理它为止。

## 19.3 适用性

1. 有多个对象可以处理一个请求，哪个对象处理该请求运行时刻自动确定。
2. 你想在不明确接收者的情况下，向多个对象中的一个提交一个请求。
3. 可处理一个请求的对象集合应被动态指定。

## 19.4 要点

1. Chain of Responsibility模式的应用场合在于`一个请求可能有多个接受者，但是最后真正的接受者只有一个`，只有这时候请求发送者与接受者的耦合才有可能出现`变化脆弱`的症状，职责链的目的就是将二者解耦，从而更好地应对变化。
2. 应用了Chain of Responsibility模式后，对象的职责分派将更具灵活性。我们可以在运行时动态添加/修改请求的处理职责。
3. 如果请求传递到职责链的未尾仍得不到处理，应该有一个合理的缺省机制。这也是每一个接受对象的责任，而不是发出请求的对象的责任。

## 19.5 场景

假设银行对于`存款`和`取款`有不同的工作人员负责，储户在银行说明自己的需求(`取款`或`取款`)，工作人员根据自己的职能为客户进行服务(执行`存款`和`取款`操作）。这时，客户是请求的发送者，工作人员是请求的接收者。

```c#
using System;

namespace ChainOfResponsibility {
    enum RequestType {
        /// <summary>
        /// 存钱
        /// <summary>
        Deposit,

        /// <summary>
        /// 取钱
        /// <summary>
        Withdraw
    }

    class Request {
        public RequestType RequestType { get;}
        public Request(RequestType requestType) {
            this.RequestType = requestType;
        }
    }

    class Customer {
        public Request Request { get; private set; }
        public virtual void SendRequest(Request request) {
            this.Request = request;
            Console.WriteLine($"Customer Request: {request.RequestType}.");
        }
    }

    abstract class WaiterBase
    {
        protected RequestType RequestType;
        protected WaiterBase NextWaiter { get; set; }

        protected WaiterBase(RequestType requestType, WaiterBase waiter = null) {
            this.RequestType = requestType;
            this.NextWaiter = waiter;
        }

        public virtual void ProcessRequest(Request request) {
            if(this.NextWaiter != null) {
                this.NextWaiter.ProcessRequest(request);
            }else {
                Console.WriteLine($"工作人员正忙，请稍等...");
            }
        }

        protected virtual bool CanProcess(Request request) {
            return this.RequestType == request.RequestType;
        }
    }

    class DepositWaiter : WaiterBase {
        public DepositWaiter(WaiterBase waiter = null) : base(RequestType.Deposit, waiter) {

        }

        public override void ProcessRequest(Request request) {
            if(this.CanProcess(request)) {
                Console.WriteLine($"Waiter Type: {this.RequestType}.");
            }else {
                base.ProcessRequest(request);
            }
        }
    }

    class WithdrawWaiter : WaiterBase {
        public WithdrawWaiter(WaiterBase waiter = null) : base(RequestType.Withdraw, waiter) {
        }

        public override void ProcessRequest(Request request) {
            if(this.CanProcess(request)) {
                Console.WriteLine($"Waiter Type: {request.RequestType}.");
            }else {
                base.ProcessRequest(request);
            }
        }
    }

    class Program {
        static void Main(string[] args) {
            var withdrawWaiter = new WithdrawWaiter();
            var depositeWaiter = new DepositWaiter(withdrawWaiter);

            var customer = new Customer();
            customer.SendRequest(new Request(RequestType.Deposit));
            depositeWaiter.ProcessRequest(customer.Request);

            customer.SendRequest(new Request(RequestType.Withdraw));
            depositeWaiter.ProcessRequest(customer.Request);
        }
    }
}
```

# 20 备忘录模式(Memento Pattern)

## 20.1 动机

在软件构建过程中，某些对象的状态在转换过程中，可能由于某种需要，要求程序能够回溯到对象之前处于某个点时的状态。如果使用一些公有接口来让其他对象得到对象的状态，便会暴露对象的细节实现。

> 如何实现对象状态的良好保存与恢复？但同时又不会因此而破坏对象本身的封装性。

## 20.2 意图

在不破坏封装性的前提下，捕获一个对象的内部状态，并在该对象之外保存这个状态。这样以后可以将该对象恢复到原先保存的状态。

## 20.3 适用性

1. `必须`保存一个对象某一个时刻的(部分)状态，这样以后需要时它才能恢复到先前的状态。
2. 如果一个用接口来让其它对象直接得到这些状态，将会暴露对象的实现细节并破坏对象的封装性。

## 20.4 要点

1. `备忘录(Memento)`存储`原发器(Originator)`对象的内部状态，在需要时`恢复原发器状态`。Memento模式适用于`由原发器管理，却又必须存储在原发器之外的信息`。
2. 在实现Memento模式中，要防止原发器以外的对象访问备忘录对象。备忘录对象有两个接口，一个为原发器的宽接口;一个为其他对象使用的窄接口。
3. 在实现Memento模式时，要考虑拷贝对象状态的效率问题，如果对象开销比较大，可以采用某种增量式改变来改进Memento模式。

## 20.5 场景

假设一个工作人员一直从事某项工作，在工作过程中记录工作的进度。很不幸，在某个节点时，上级领高给该工作人员安排了一个新的紧急任务，该工作人员必须保存当前任务的状态，转手去处理紧急任务。在紧急任务处理完成之后，接着去晚上原任务。

```c#
using System;

namespace MementoPattern {
    class Cluck
    {
        public string Name { get; }
        public int Age { get; }

        public string Task { get; set; }
        public int Percent { get; set; }

        public Cluck(string name, int age) {
            this.Name = name;
            this.Age = age;
            this.Task = "";
            this.Percent = 0;
        }

        public Memento SaveMemento() {
            Console.WriteLine($"Save memento: {this.Task}, {this.Percent}");
            return new Memento(){Task = this.Task, Percent = this.Percent};
        }

        public void RestoreMemento(Memento memento) {
            this.Task = memento.Task;
            this.Percent = memento.Percent;
        }

        public void ShowState() {
            Console.WriteLine($"{this.Name}, {this.Age}, {this.Task}, {this.Percent}");
        }
    }

    class Memento {
        public string Task { get; set; }
        public int Percent { get; set; }
    }

    class Program {
        static void Main(string[] args) {
            var cluck = new Cluck("Jay", 28);
            cluck.ShowState();

            Console.WriteLine("========================");
            cluck.Task = "Task 1";
            cluck.Percent = 10;
            cluck.ShowState();

            Console.WriteLine("========================");
            cluck.Percent = 50;
            cluck.ShowState();

            Console.WriteLine("========================");
            Console.WriteLine("New Task");
            cluck.ShowState();
            var memento = cluck.SaveMemento();

            Console.WriteLine("Switch Task 2");
            cluck.Task = "Task 2";
            cluck.Percent = 0;
            cluck.ShowState();

            Console.WriteLine("========================");
            cluck.Task = "Task 2";
            cluck.Percent = 100;
            cluck.ShowState();
            Console.WriteLine("Task 2 is completed");

            Console.WriteLine("========================");
            Console.WriteLine("Switch Task 1");
            cluck.RestoreMemento(memento);
            cluck.ShowState();

            Console.WriteLine("========================");
            cluck.Percent = 100;
            cluck.ShowState();
            Console.WriteLine("Task 1 is completed");
        }
    }
}
```

# 21 策略模式(Strategy Pattern)

## 21.1 动机

在软件构建过程中，某些对象使用的算法可能多种多样，经常改变，如果将这些算法都编码对象中，将会使对象变得异常复杂;而且有时候支持不使用的算法也是一个性能负担。

> 如何在运行时根据需要透明地更改对象的算法？将算法与对象本身解耦，从而避免上述问题？

## 21.2 意图

定义一系统的算法，把它们一个个封装起来，并且使它们可相互替换。本模式使得算法可独立于使用它的客户而变化。

## 21.3 适用性

1. 许多相关的类仅仅是行为有异。`策略`提供了一种用多个行为中的一个行为来配置一个类的方法。
2. 需要使用一个算法的不同变体。例如，你可能会定义一些反映不同的空间/时间权衡的算法。当这些变体实现为一个算法的类层次时[H087]，可以使用策略模式。  
3. 算法使用客户不应该知道数据。可使用策略模式以避免暴露复杂的，与算法相关的数据结构。
4. 一个类定义了多种行为，并且这些行为在这个类的操作中以多个条件语句的形式出现。将相关的条件分支移入它们各自的Strategy类中以代替这些条件语句。

## 21.4 要点

1. Strategy及其子类为组件提供了一系列可重用的算法，从而可以使得类型在运行时方便地根据需要在各个算法之间进行切换。所谓封装算法，支持算法的变化。
2. Strategy模式提供了用条件判断语句以外的另一种选择，消除条件判断语句，就是在解耦合。含有许多条件判断语句的代码通常都需要Strategy模式。
3. 与State类似，如果Strategy对象没有实例变量，那么各个上下文可以共享同一个Strategy对象，从而节省对象开销。

## 21.5 场景

一个班级有多个学生，在上课之前需要对学生进行点名。假设，在上语文课时按照`姓名的顺序`进行点名，在上体育课时按照`身高的顺序`进行点名，其他课程课按照上述两种方式，也可自定义其他方式进行点名。

```c#
using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPattern {
    class Student {
        public string Name {get;}
        public int Age { get; }
        public float Height { get; }

        public Student (string name, int age, float height) {
            this.Name = name;
            this.Age = age;
            this.Height = height;
        }
    }

    interface ISortStrategy {
        void Sort(List<Student> students);
    }

    class NameSortStrategy : ISortStrategy {
        public void Sort (List<Student> students) {
            Console.WriteLine ("Sort by Name");
            Console.WriteLine("###############################");
            var sortedStudents = students.OrderBy(student=>student.Name).Select(student=>student).ToList();
            students.Clear();
            foreach (var student in sortedStudents) {
                students.Add(student);
            }
        }
    }

    class HeightSortStrategy : ISortStrategy {
        public void Sort (List<Student> students) {
            Console.WriteLine ("Sort by Height");
            Console.WriteLine("###############################");
            var sortedStudents = students.OrderBy(student=>student.Height).Select(student=>student).ToList();
            students.Clear();
            foreach (var student in sortedStudents) {
                students.Add(student);
            }
        }
    }

    class StudentList : List<Student> {
        public void AddStudent(Student student) {
            this.Add(student);
        }

        public void Sort (ISortStrategy sortStrategy) {
            sortStrategy.Sort(this);
            foreach (var student in this) {
                Console.WriteLine($"{student.Name}, {student.Age}, {student.Height}");
            }
        }
    }
    class Program {
        static void Main(string[] args) {
            StudentList list = new StudentList();
            list.AddStudent(new Student("zhang san", 18, 175));
            list.AddStudent(new Student("li si", 28, 155));
            list.AddStudent(new Student("wang wu", 22, 185));
            
            list.Sort(new NameSortStrategy());
            list.Sort(new HeightSortStrategy());
        }
    }
}
```

> Sort by Name
> ###############################
> li si, 28, 155
> wang wu, 22, 185
> zhang san, 18, 175
> Sort by Height
> ###############################
> li si, 28, 155
> zhang san, 18, 175
> wang wu, 22, 185

如果数学课时，希望使用`年龄的顺序`进行点名，那么只需要添加一个新的排序类，然后使用该排序类的实例即可。

```c#
class AgeSortStrategy : ISortStrategy {
    public void Sort (List<Student> students) {
        Console.WriteLine ("Sort by Age");
        Console.WriteLine("###############################");
        var sortedStudents = students.OrderBy(student=>student.Age).Select(student=>student).ToList();
        students.Clear();
        foreach (var student in sortedStudents) {
            students.Add(student);
        }
    }
}

class Program {
    static void Main(string[] args) {
        StudentList list = new StudentList();
        list.AddStudent(new Student("zhang san", 18, 175));
        list.AddStudent(new Student("li si", 28, 155));
        list.AddStudent(new Student("wang wu", 22, 185));
        
        list.Sort(new NameSortStrategy());
        list.Sort(new HeightSortStrategy());
        list.Sort(new NameSortStrategy());
    }
}
```

> Sort by Name
> ###############################
> li si, 28, 155
> wang wu, 22, 185
> zhang san, 18, 175
> Sort by Height
> ###############################
> li si, 28, 155
> zhang san, 18, 175
> wang wu, 22, 185
> Sort by Age
> ###############################
> zhang san, 18, 175
> wang wu, 22, 185
> li si, 28, 155

# 22 访问者模式(Visitor Pattern)

## 22.1 动机

在软件构建过程中，由于需求的改变，某些类层次结构中常常需要增加新的行为(方法),如果直接在基类中做这样的更改，将会给子类带来很繁重的变更负担，甚至破坏原有设计。
  如何在不更改类层次结构的前提下，在运行时根据需要透明地为类层次结构上的各个类动态添加新的操作，从而避免上述问题？

## 22.2 意图

表示一个作用于某对象结构中的各元素的操作。它使你可以在不改变各元素的类的前提下定义作用于这引起元素的新操作。

## 22.3 适用性

1. 一个对象结构包含很多类对象，它们有不同的接口，而你想对这些对象实施一些依赖于其具体类的操作。
2. 需要对一个对象结构中的对象进行很多不同的并且不相关的操作，而你想避免让这些操作"污染"这些对象的类。Visitor使得你可以将相关的操作集中起来定义在一个类中。当该对象结构被很多应用共享时，用Visitor模式让每个应用仅包含需要用到的操作。
3. 定义对象结构的类很少改变，但经常需要在结构上定义新的操作。改变对象结构类需要重定义对所有访问者的接口，这可能需要很大的代价。如果对象结构类经常改变，那么可能还是在这些类中定义这些操作较好。

## 22.4 要点

1. Visitor模式通过所谓双重分发(double dispatch)来实现在不更改Element类层次结构的前提下，在运行时透明地为类层次结构上的各个类动态添加新的操作。
2. 所谓双重分发却Visotor模式中间包括了两个多态分发（注意其中的多态机制);第一个为accept方法的多态辨析;第二个为visitor方法的多态辨析。
3. Visotor模式的最大缺点在于扩展类层次结构(增添新的Element子类），会导致Visitor类的改变。因此Visiotr模式适用"Element"类层次结构稳定，而其中的操作却经常面临频繁改动".

## 22.5 场景

我们将创建一个定义接受操作的 *ComputerPart* 接口。*Keyboard*、*Mouse*、*Monitor* 和 *Computer* 是实现了 *ComputerPart* 接口的实体类。我们将定义另一个接口 *ComputerPartVisitor*，它定义了访问者类的操作。*Computer* 使用实体访问者来执行相应的动作。

```c#
using System;

namespace VisitorPattern
{
    public interface ComputerPart
    {
        void accept(ComputerPartVisitor computerPartVisitor);
    }

    public class Keyboard : ComputerPart
    {
        public void accept(ComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.visit(this);
        }
    }

    public class Monitor : ComputerPart
    {
        public void accept(ComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.visit(this);
        }
    }

    public class Mouse : ComputerPart
    {
        public void accept(ComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.visit(this);
        }
    }

    public class Computer : ComputerPart
    {
        ComputerPart[] parts;
        public Computer()
        {
            parts = new ComputerPart[]{new Mouse(), new Keyboard(), new Monitor()};
        }
        public void accept(ComputerPartVisitor computerPartVisitor)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].accept(computerPartVisitor);
            }
            computerPartVisitor.visit(this);
        }
    }

    public interface ComputerPartVisitor
    {
        void visit(Computer computer);
        void visit(Mouse mouse);
        void visit(Keyboard keyboard);
        void visit(Monitor monitor);
    }

    public class ComputerPartDisplayVisitor : ComputerPartVisitor
    {
        public void visit(Computer computer)
        {
            System.Console.WriteLine("Displaying Computer.");
        }

        public void visit(Mouse mouse)
        {
            System.Console.WriteLine("Displaying Mouse.");
        }
        public void visit(Keyboard keyboard)
        {
            System.Console.WriteLine("Displaying Keyboard.");
        }
        public void visit(Monitor monitor)
        {
            System.Console.WriteLine("Displaying Monitor.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ComputerPart computer = new Computer();
            computer.accept(new ComputerPartDisplayVisitor());
        }
    }
}
```



# 23 状态模式(State Pattern)

## 23.1 动机

在软件构建过程中，某些对象的状态如果改变以及其行为也会随之而发生变化，比如文档处于只读状态，其支持的行为和读写状态支持的行为就可能完全不同。
如何在运行时根据对象的状态来透明更改对象的行为？而不会为对象操作和状态转化之间引入紧耦合？

## 23.2 意图

允许一个对象在其内部状态改变时改变它的行为。从而使对象看起来似乎修改了其行为。

## 23.3 适用性

1. 一个对象的行为取决于它的状态，并且它必须在运行时刻根据状态改变它的行为。
2. 一个操作中含有庞大的多分支的等条件语句，且这些分支依赖于该对象的状态。这个状态通常用一个或多个枚举常量表示。通常，有多个操作包含这一相同的条件结构。State模式将每一个分支放入一个独立的类中。这使得你可根据对象自身的情况将对象的状态作为一个对象，这一对象可以不依赖于其他对象而独立变化。

## 23.4 要点

1. State模式将所有一个特定状态相关的行为都放入一个State的子类对象中，在对象状态切换时，切换相应的对象;但同时维持State的接口，这样实现了具体操作与状态转换之间的解耦。
2. 为不同的状态引入不同的对象使得状态转换变得更加明确，而且可以保证不会出现状态不一致的情况，因为转换是原子性的----即要么彻底转换过来，要么不转换。
3. 如果State对象没有实例变量，那么各个上下文可以共享 同一个State对象，从而节省对象开销。

## 23.5 场景

我们将创建一个 *State* 接口和实现了 *State* 接口的实体状态类。*Context* 是一个带有某个状态的类。

```c#
using System;

namespace StatePattern
{
    public interface State
    {
        public void doAction(Context context);
    }

    public class StartState : State
    {
        public void doAction(Context context)
        {
            Console.WriteLine("Player is in start state");
            context.setState(this); 
        }
 
        public String toString()
        {
            return "Start State";
        }
    }

    public class StopState : State
    {
        public void doAction(Context context)
        {
            Console.WriteLine("Player is in stop state");
            context.setState(this); 
        }
        
        public String toString(){
            return "Stop State";
        }
    }

    public class Context
    {
        private State state;
        
        public Context()
        {
            state = null;
        }
        
        public void setState(State state)
        {
            this.state = state;     
        }
        
        public State getState()
        {
            return state;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
 
            StartState startState = new StartState();
            startState.doAction(context);
            Console.WriteLine(context.getState().ToString());
        
            StopState stopState = new StopState();
            stopState.doAction(context);
            Console.WriteLine(context.getState().ToString());
        }
    }
}
```

