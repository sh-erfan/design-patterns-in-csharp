using System;

namespace DesignPatternsDemo.StructuralPatterns
{
    // Component interface
    public interface IComponent
    {
        void Operation();
    }

    // Base decorator class
    public abstract class ComponentDecorator : IComponent
    {
        protected IComponent _component;

        public ComponentDecorator(IComponent component)
        {
            _component = component;
        }

        public virtual void Operation()
        {
            _component?.Operation();
        }
    }

    // Concrete component
    public class ConcreteComponent : IComponent
    {
        public void Operation()
        {
            Console.WriteLine("ConcreteComponent: Basic operation");
        }
    }

    // Concrete decorators
    public class ConcreteDecoratorA : ComponentDecorator
    {
        public ConcreteDecoratorA(IComponent component) : base(component)
        {
        }

        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("ConcreteDecoratorA: Added behavior A");
        }
    }

    public class ConcreteDecoratorB : ComponentDecorator
    {
        public ConcreteDecoratorB(IComponent component) : base(component)
        {
        }

        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("ConcreteDecoratorB: Added behavior B");
        }
    }

    /// <summary>
    /// Real-world example: Coffee Shop Decorator
    /// </summary>
    
    public interface ICoffee
    {
        double GetCost();
        string GetDescription();
    }

    // Base coffee
    public class SimpleCoffee : ICoffee
    {
        public double GetCost()
        {
            return 2.0;
        }

        public string GetDescription()
        {
            return "Simple Coffee";
        }
    }

    // Base coffee decorator
    public abstract class CoffeeDecorator : ICoffee
    {
        protected ICoffee _coffee;

        public CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee;
        }

        public virtual double GetCost()
        {
            return _coffee.GetCost();
        }

        public virtual string GetDescription()
        {
            return _coffee.GetDescription();
        }
    }

    // Concrete coffee decorators
    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(ICoffee coffee) : base(coffee)
        {
        }

        public override double GetCost()
        {
            return base.GetCost() + 0.5;
        }

        public override string GetDescription()
        {
            return base.GetDescription() + ", Milk";
        }
    }

    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(ICoffee coffee) : base(coffee)
        {
        }

        public override double GetCost()
        {
            return base.GetCost() + 0.2;
        }

        public override string GetDescription()
        {
            return base.GetDescription() + ", Sugar";
        }
    }

    public class WhipDecorator : CoffeeDecorator
    {
        public WhipDecorator(ICoffee coffee) : base(coffee)
        {
        }

        public override double GetCost()
        {
            return base.GetCost() + 0.7;
        }

        public override string GetDescription()
        {
            return base.GetDescription() + ", Whip";
        }
    }

    /// <summary>
    /// Usage example for Decorator Pattern
    /// </summary>
    public class DecoratorExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Decorator Pattern Example ===");

            // Basic decorator example
            Console.WriteLine("1. Basic Decorator Example:");
            IComponent component = new ConcreteComponent();
            component.Operation();

            Console.WriteLine("\nWith Decorator A:");
            component = new ConcreteDecoratorA(component);
            component.Operation();

            Console.WriteLine("\nWith Decorator B added:");
            component = new ConcreteDecoratorB(component);
            component.Operation();

            // Coffee shop example
            Console.WriteLine("\n2. Coffee Shop Decorator Example:");
            
            // Start with simple coffee
            ICoffee coffee = new SimpleCoffee();
            Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost():F2}");

            // Add milk
            coffee = new MilkDecorator(coffee);
            Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost():F2}");

            // Add sugar
            coffee = new SugarDecorator(coffee);
            Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost():F2}");

            // Add whip
            coffee = new WhipDecorator(coffee);
            Console.WriteLine($"{coffee.GetDescription()} - ${coffee.GetCost():F2}");

            // Create another coffee with different decorations
            Console.WriteLine("\nAnother coffee order:");
            ICoffee anotherCoffee = new WhipDecorator(new SugarDecorator(new SimpleCoffee()));
            Console.WriteLine($"{anotherCoffee.GetDescription()} - ${anotherCoffee.GetCost():F2}");
        }
    }
}