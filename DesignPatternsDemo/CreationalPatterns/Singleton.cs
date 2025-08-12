using System;

namespace DesignPatternsDemo.CreationalPatterns
{
    /// <summary>
    /// Singleton Pattern - Ensures a class has only one instance and provides global access to it.
    /// Thread-safe implementation using lazy initialization.
    /// </summary>
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> _instance = new Lazy<Singleton>(() => new Singleton());

        private Singleton()
        {
            Console.WriteLine("Singleton instance created.");
        }

        public static Singleton Instance => _instance.Value;

        public void DoSomething()
        {
            Console.WriteLine("Singleton is doing something...");
        }
    }

    /// <summary>
    /// Usage example for Singleton Pattern
    /// </summary>
    public class SingletonExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Singleton Pattern Example ===");
            
            // Get the singleton instance
            var singleton1 = Singleton.Instance;
            singleton1.DoSomething();
            
            // Get another reference - should be the same instance
            var singleton2 = Singleton.Instance;
            singleton2.DoSomething();
            
            // Verify they are the same instance
            Console.WriteLine($"Are both references the same instance? {ReferenceEquals(singleton1, singleton2)}");
        }
    }
}