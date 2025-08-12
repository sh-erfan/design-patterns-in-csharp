using DesignPatternsDemo.CreationalPatterns;
using DesignPatternsDemo.StructuralPatterns;
using DesignPatternsDemo.BehavioralPatterns;

namespace DesignPatternsDemo;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Design Patterns in C# - Usage Examples ===");
        Console.WriteLine("This demo showcases various design patterns with practical examples.");
        
        // Creational Patterns
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("CREATIONAL PATTERNS");
        Console.WriteLine(new string('=', 50));
        
        SingletonExample.RunExample();
        FactoryMethodExample.RunExample();
        BuilderExample.RunExample();
        AbstractFactoryExample.RunExample();
        PrototypeExample.RunExample();
        
        // Structural Patterns
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("STRUCTURAL PATTERNS");
        Console.WriteLine(new string('=', 50));
        
        AdapterExample.RunExample();
        DecoratorExample.RunExample();
        FacadeExample.RunExample();
        CompositeExample.RunExample();
        BridgeExample.RunExample();
        ProxyExample.RunExample();
        FlyweightExample.RunExample();
        
        // Behavioral Patterns
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("BEHAVIORAL PATTERNS");
        Console.WriteLine(new string('=', 50));
        
        ObserverExample.RunExample();
        StrategyExample.RunExample();
        CommandExample.RunExample();
        TemplateMethodExample.RunExample();
        StateExample.RunExample();
        ChainOfResponsibilityExample.RunExample();
        MementoExample.RunExample();
        IteratorExample.RunExample();
        MediatorExample.RunExample();
        VisitorExample.RunExample();
        InterpreterExample.RunExample();
        
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("🎉 ALL 23 DESIGN PATTERNS COMPLETED! 🎉");
        Console.WriteLine("This demo showcased all Gang of Four design patterns:");
        Console.WriteLine("✅ 5 Creational Patterns");
        Console.WriteLine("✅ 7 Structural Patterns"); 
        Console.WriteLine("✅ 11 Behavioral Patterns");
        Console.WriteLine("Check the code to see how each pattern is implemented.");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
