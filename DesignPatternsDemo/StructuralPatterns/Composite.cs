using System;
using System.Collections.Generic;

namespace DesignPatternsDemo.StructuralPatterns
{
    /// <summary>
    /// Composite Pattern - composes objects into tree structures to represent part-whole hierarchies
    /// </summary>
    
    // Component interface
    public abstract class FileSystemComponent
    {
        protected string _name;

        public FileSystemComponent(string name)
        {
            _name = name;
        }

        public abstract void Display(int depth = 0);
        public abstract long GetSize();

        // Default implementations for leaf nodes
        public virtual void Add(FileSystemComponent component)
        {
            throw new NotSupportedException("Cannot add to a leaf component");
        }

        public virtual void Remove(FileSystemComponent component)
        {
            throw new NotSupportedException("Cannot remove from a leaf component");
        }

        protected string GetIndentation(int depth)
        {
            return new string('-', depth * 2);
        }
    }

    // Leaf - File
    public class File : FileSystemComponent
    {
        private long _size;

        public File(string name, long size) : base(name)
        {
            _size = size;
        }

        public override void Display(int depth = 0)
        {
            Console.WriteLine($"{GetIndentation(depth)}{_name} ({_size} bytes)");
        }

        public override long GetSize()
        {
            return _size;
        }
    }

    // Composite - Directory
    public class Directory : FileSystemComponent
    {
        private List<FileSystemComponent> _children = new List<FileSystemComponent>();

        public Directory(string name) : base(name)
        {
        }

        public override void Add(FileSystemComponent component)
        {
            _children.Add(component);
        }

        public override void Remove(FileSystemComponent component)
        {
            _children.Remove(component);
        }

        public override void Display(int depth = 0)
        {
            Console.WriteLine($"{GetIndentation(depth)}{_name}/");
            
            foreach (var child in _children)
            {
                child.Display(depth + 1);
            }
        }

        public override long GetSize()
        {
            long totalSize = 0;
            foreach (var child in _children)
            {
                totalSize += child.GetSize();
            }
            return totalSize;
        }
    }

    /// <summary>
    /// Real-world example: Organization structure
    /// </summary>
    
    public abstract class Employee
    {
        protected string _name;
        protected string _position;
        protected decimal _salary;

        public Employee(string name, string position, decimal salary)
        {
            _name = name;
            _position = position;
            _salary = salary;
        }

        public abstract void ShowDetails(int depth = 0);
        public abstract decimal GetTotalSalary();

        public virtual void Add(Employee employee)
        {
            throw new NotSupportedException("Cannot add subordinate to this employee type");
        }

        public virtual void Remove(Employee employee)
        {
            throw new NotSupportedException("Cannot remove subordinate from this employee type");
        }

        protected string GetIndentation(int depth)
        {
            return new string(' ', depth * 4);
        }
    }

    // Leaf - Individual Contributor
    public class IndividualContributor : Employee
    {
        public IndividualContributor(string name, string position, decimal salary) 
            : base(name, position, salary)
        {
        }

        public override void ShowDetails(int depth = 0)
        {
            Console.WriteLine($"{GetIndentation(depth)}{_name} - {_position} (${_salary:N0})");
        }

        public override decimal GetTotalSalary()
        {
            return _salary;
        }
    }

    // Composite - Manager
    public class Manager : Employee
    {
        private List<Employee> _subordinates = new List<Employee>();

        public Manager(string name, string position, decimal salary) 
            : base(name, position, salary)
        {
        }

        public override void Add(Employee employee)
        {
            _subordinates.Add(employee);
        }

        public override void Remove(Employee employee)
        {
            _subordinates.Remove(employee);
        }

        public override void ShowDetails(int depth = 0)
        {
            Console.WriteLine($"{GetIndentation(depth)}{_name} - {_position} (${_salary:N0}) [Manager]");
            
            foreach (var subordinate in _subordinates)
            {
                subordinate.ShowDetails(depth + 1);
            }
        }

        public override decimal GetTotalSalary()
        {
            decimal totalSalary = _salary;
            foreach (var subordinate in _subordinates)
            {
                totalSalary += subordinate.GetTotalSalary();
            }
            return totalSalary;
        }
    }

    /// <summary>
    /// Usage example for Composite Pattern
    /// </summary>
    public class CompositeExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Composite Pattern Example ===");

            // File system example
            Console.WriteLine("1. File System Example:");
            
            var root = new Directory("root");
            var home = new Directory("home");
            var user = new Directory("user");
            
            root.Add(home);
            home.Add(user);
            
            user.Add(new File("document.txt", 1024));
            user.Add(new File("image.jpg", 2048));
            
            var projects = new Directory("projects");
            user.Add(projects);
            
            projects.Add(new File("project1.cs", 4096));
            projects.Add(new File("project2.cs", 3072));
            
            root.Display();
            Console.WriteLine($"Total size: {root.GetSize()} bytes");

            Console.WriteLine();

            // Organization structure example
            Console.WriteLine("2. Organization Structure Example:");
            
            var ceo = new Manager("John Smith", "CEO", 200000);
            
            var cto = new Manager("Alice Johnson", "CTO", 150000);
            var cfo = new Manager("Bob Wilson", "CFO", 150000);
            
            ceo.Add(cto);
            ceo.Add(cfo);
            
            var devManager = new Manager("Carol Brown", "Dev Manager", 120000);
            cto.Add(devManager);
            
            devManager.Add(new IndividualContributor("David Lee", "Senior Developer", 100000));
            devManager.Add(new IndividualContributor("Eva Davis", "Developer", 80000));
            devManager.Add(new IndividualContributor("Frank Miller", "Junior Developer", 60000));
            
            cto.Add(new IndividualContributor("Grace Taylor", "System Admin", 85000));
            
            cfo.Add(new IndividualContributor("Henry Wilson", "Accountant", 70000));
            cfo.Add(new IndividualContributor("Ivy Chen", "Financial Analyst", 75000));
            
            Console.WriteLine("Organization Chart:");
            ceo.ShowDetails();
            Console.WriteLine($"\nTotal company salary budget: ${ceo.GetTotalSalary():N0}");

            Console.WriteLine("\nComposite pattern allows treating individual and composite objects uniformly!");
        }
    }
}