using System;
using System.Text;

namespace DesignPatternsDemo.CreationalPatterns
{
    /// <summary>
    /// Product class that we want to build
    /// </summary>
    public class Computer
    {
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string GPU { get; set; }
        public bool HasWifi { get; set; }
        public bool HasBluetooth { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Computer Specifications:");
            sb.AppendLine($"  CPU: {CPU}");
            sb.AppendLine($"  RAM: {RAM}");
            sb.AppendLine($"  Storage: {Storage}");
            sb.AppendLine($"  GPU: {GPU}");
            sb.AppendLine($"  WiFi: {(HasWifi ? "Yes" : "No")}");
            sb.AppendLine($"  Bluetooth: {(HasBluetooth ? "Yes" : "No")}");
            return sb.ToString();
        }
    }

    /// <summary>
    /// Builder interface
    /// </summary>
    public interface IComputerBuilder
    {
        IComputerBuilder SetCPU(string cpu);
        IComputerBuilder SetRAM(string ram);
        IComputerBuilder SetStorage(string storage);
        IComputerBuilder SetGPU(string gpu);
        IComputerBuilder AddWifi();
        IComputerBuilder AddBluetooth();
        Computer Build();
    }

    /// <summary>
    /// Concrete builder implementation
    /// </summary>
    public class ComputerBuilder : IComputerBuilder
    {
        private readonly Computer _computer = new Computer();

        public IComputerBuilder SetCPU(string cpu)
        {
            _computer.CPU = cpu;
            return this;
        }

        public IComputerBuilder SetRAM(string ram)
        {
            _computer.RAM = ram;
            return this;
        }

        public IComputerBuilder SetStorage(string storage)
        {
            _computer.Storage = storage;
            return this;
        }

        public IComputerBuilder SetGPU(string gpu)
        {
            _computer.GPU = gpu;
            return this;
        }

        public IComputerBuilder AddWifi()
        {
            _computer.HasWifi = true;
            return this;
        }

        public IComputerBuilder AddBluetooth()
        {
            _computer.HasBluetooth = true;
            return this;
        }

        public Computer Build()
        {
            return _computer;
        }
    }

    /// <summary>
    /// Director class (optional) for building common configurations
    /// </summary>
    public class ComputerDirector
    {
        public Computer BuildGamingComputer(IComputerBuilder builder)
        {
            return builder
                .SetCPU("Intel i9-11900K")
                .SetRAM("32GB DDR4")
                .SetStorage("1TB NVMe SSD")
                .SetGPU("NVIDIA RTX 3080")
                .AddWifi()
                .AddBluetooth()
                .Build();
        }

        public Computer BuildOfficeComputer(IComputerBuilder builder)
        {
            return builder
                .SetCPU("Intel i5-11400")
                .SetRAM("16GB DDR4")
                .SetStorage("512GB SSD")
                .SetGPU("Integrated Graphics")
                .AddWifi()
                .Build();
        }
    }

    /// <summary>
    /// Usage example for Builder Pattern
    /// </summary>
    public class BuilderExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Builder Pattern Example ===");

            // Direct building using fluent interface
            var customComputer = new ComputerBuilder()
                .SetCPU("AMD Ryzen 7 5800X")
                .SetRAM("16GB DDR4")
                .SetStorage("500GB NVMe SSD")
                .SetGPU("NVIDIA RTX 3070")
                .AddWifi()
                .Build();

            Console.WriteLine("Custom Computer:");
            Console.WriteLine(customComputer);

            // Using Director for common configurations
            var director = new ComputerDirector();
            var builder = new ComputerBuilder();

            var gamingComputer = director.BuildGamingComputer(builder);
            Console.WriteLine("Gaming Computer (built using Director):");
            Console.WriteLine(gamingComputer);

            // Build office computer with a new builder instance
            var officeComputer = director.BuildOfficeComputer(new ComputerBuilder());
            Console.WriteLine("Office Computer (built using Director):");
            Console.WriteLine(officeComputer);
        }
    }
}