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
        
        // Behavioral Patterns
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("BEHAVIORAL PATTERNS");
        Console.WriteLine(new string('=', 50));
        
        ObserverExample.RunExample();
        StrategyExample.RunExample();
        CommandExample.RunExample();
        TemplateMethodExample.RunExample();
        
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("Demo completed! Check the code to see how each pattern is implemented.");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
