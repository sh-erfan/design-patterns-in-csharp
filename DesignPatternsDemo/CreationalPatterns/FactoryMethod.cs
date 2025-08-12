using System;

namespace DesignPatternsDemo.CreationalPatterns
{
    // Product interface
    public interface IProduct
    {
        void Use();
    }

    // Concrete products
    public class ConcreteProductA : IProduct
    {
        public void Use()
        {
            Console.WriteLine("Using Concrete Product A");
        }
    }

    public class ConcreteProductB : IProduct
    {
        public void Use()
        {
            Console.WriteLine("Using Concrete Product B");
        }
    }

    // Creator abstract class
    public abstract class Creator
    {
        // Factory method - subclasses will implement this
        public abstract IProduct FactoryMethod();

        // Template method that uses the factory method
        public void SomeOperation()
        {
            var product = FactoryMethod();
            product.Use();
        }
    }

    // Concrete creators
    public class ConcreteCreatorA : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProductA();
        }
    }

    public class ConcreteCreatorB : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProductB();
        }
    }

    /// <summary>
    /// Usage example for Factory Method Pattern
    /// </summary>
    public class FactoryMethodExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Factory Method Pattern Example ===");

            // Create different types of creators
            Creator creatorA = new ConcreteCreatorA();
            Creator creatorB = new ConcreteCreatorB();

            // Use the creators - they will create appropriate products
            Console.WriteLine("Creator A:");
            creatorA.SomeOperation();

            Console.WriteLine("\nCreator B:");
            creatorB.SomeOperation();

            // The client code doesn't need to know the concrete product classes
            Console.WriteLine("\nClient code works with any creator through the base Creator class");
        }
    }
}