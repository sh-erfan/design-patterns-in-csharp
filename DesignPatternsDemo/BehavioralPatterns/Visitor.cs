using System;
using System.Collections.Generic;

namespace DesignPatternsDemo.BehavioralPatterns
{
    /// <summary>
    /// Visitor Pattern - separates algorithms from object structure
    /// </summary>
    
    // Visitor interface
    public interface IShapeVisitor
    {
        void Visit(Circle circle);
        void Visit(Rectangle rectangle);
        void Visit(Triangle triangle);
    }

    // Element interface
    public interface IShape
    {
        void Accept(IShapeVisitor visitor);
    }

    // Concrete elements
    public class Circle : IShape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public void Accept(IShapeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Rectangle : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void Accept(IShapeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Triangle : IShape
    {
        public double Base { get; set; }
        public double Height { get; set; }

        public Triangle(double baseLength, double height)
        {
            Base = baseLength;
            Height = height;
        }

        public void Accept(IShapeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // Concrete visitors
    public class AreaCalculatorVisitor : IShapeVisitor
    {
        public void Visit(Circle circle)
        {
            double area = Math.PI * circle.Radius * circle.Radius;
            Console.WriteLine($"Circle area: {area:F2}");
        }

        public void Visit(Rectangle rectangle)
        {
            double area = rectangle.Width * rectangle.Height;
            Console.WriteLine($"Rectangle area: {area:F2}");
        }

        public void Visit(Triangle triangle)
        {
            double area = 0.5 * triangle.Base * triangle.Height;
            Console.WriteLine($"Triangle area: {area:F2}");
        }
    }

    public class PerimeterCalculatorVisitor : IShapeVisitor
    {
        public void Visit(Circle circle)
        {
            double perimeter = 2 * Math.PI * circle.Radius;
            Console.WriteLine($"Circle perimeter: {perimeter:F2}");
        }

        public void Visit(Rectangle rectangle)
        {
            double perimeter = 2 * (rectangle.Width + rectangle.Height);
            Console.WriteLine($"Rectangle perimeter: {perimeter:F2}");
        }

        public void Visit(Triangle triangle)
        {
            // Assuming it's an isosceles triangle for simplicity
            double side = Math.Sqrt((triangle.Base / 2) * (triangle.Base / 2) + triangle.Height * triangle.Height);
            double perimeter = triangle.Base + 2 * side;
            Console.WriteLine($"Triangle perimeter: {perimeter:F2}");
        }
    }

    public class DrawingVisitor : IShapeVisitor
    {
        public void Visit(Circle circle)
        {
            Console.WriteLine($"Drawing a circle with radius {circle.Radius}");
        }

        public void Visit(Rectangle rectangle)
        {
            Console.WriteLine($"Drawing a rectangle {rectangle.Width} x {rectangle.Height}");
        }

        public void Visit(Triangle triangle)
        {
            Console.WriteLine($"Drawing a triangle with base {triangle.Base} and height {triangle.Height}");
        }
    }

    /// <summary>
    /// Real-world example: File system operations
    /// </summary>
    
    public interface IFileSystemVisitor
    {
        void Visit(File file);
        void Visit(Directory directory);
    }

    public interface IFileSystemElement
    {
        string Name { get; }
        void Accept(IFileSystemVisitor visitor);
    }

    public class File : IFileSystemElement
    {
        public string Name { get; private set; }
        public long Size { get; private set; }
        public string Extension { get; private set; }

        public File(string name, long size, string extension)
        {
            Name = name;
            Size = size;
            Extension = extension;
        }

        public void Accept(IFileSystemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Directory : IFileSystemElement
    {
        public string Name { get; private set; }
        public List<IFileSystemElement> Children { get; private set; } = new List<IFileSystemElement>();

        public Directory(string name)
        {
            Name = name;
        }

        public void Add(IFileSystemElement element)
        {
            Children.Add(element);
        }

        public void Accept(IFileSystemVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var child in Children)
            {
                child.Accept(visitor);
            }
        }
    }

    // Concrete file system visitors
    public class SizeCalculatorVisitor : IFileSystemVisitor
    {
        private long _totalSize = 0;

        public void Visit(File file)
        {
            _totalSize += file.Size;
            Console.WriteLine($"File: {file.Name} ({file.Size} bytes)");
        }

        public void Visit(Directory directory)
        {
            Console.WriteLine($"Directory: {directory.Name}/");
        }

        public long GetTotalSize() => _totalSize;
    }

    public class FileSearchVisitor : IFileSystemVisitor
    {
        private string _searchExtension;
        private List<File> _foundFiles = new List<File>();

        public FileSearchVisitor(string extension)
        {
            _searchExtension = extension;
        }

        public void Visit(File file)
        {
            if (file.Extension.Equals(_searchExtension, StringComparison.OrdinalIgnoreCase))
            {
                _foundFiles.Add(file);
                Console.WriteLine($"Found {_searchExtension} file: {file.Name}");
            }
        }

        public void Visit(Directory directory)
        {
            // Just traverse, no output for directories in search
        }

        public List<File> GetFoundFiles() => _foundFiles;
    }

    public class BackupVisitor : IFileSystemVisitor
    {
        public void Visit(File file)
        {
            Console.WriteLine($"Backing up file: {file.Name} ({file.Size} bytes)");
        }

        public void Visit(Directory directory)
        {
            Console.WriteLine($"Creating backup directory: {directory.Name}/");
        }
    }

    /// <summary>
    /// Usage example for Visitor Pattern
    /// </summary>
    public class VisitorExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Visitor Pattern Example ===");

            // Shape operations example
            Console.WriteLine("1. Shape Operations Example:");
            
            var shapes = new List<IShape>
            {
                new Circle(5),
                new Rectangle(4, 6),
                new Triangle(3, 4)
            };

            var areaCalculator = new AreaCalculatorVisitor();
            var perimeterCalculator = new PerimeterCalculatorVisitor();
            var drawer = new DrawingVisitor();

            Console.WriteLine("Calculating areas:");
            foreach (var shape in shapes)
            {
                shape.Accept(areaCalculator);
            }

            Console.WriteLine("\nCalculating perimeters:");
            foreach (var shape in shapes)
            {
                shape.Accept(perimeterCalculator);
            }

            Console.WriteLine("\nDrawing shapes:");
            foreach (var shape in shapes)
            {
                shape.Accept(drawer);
            }

            Console.WriteLine();

            // File system example
            Console.WriteLine("2. File System Operations Example:");
            
            // Build a file system structure
            var root = new Directory("root");
            var documents = new Directory("documents");
            var pictures = new Directory("pictures");

            root.Add(documents);
            root.Add(pictures);
            root.Add(new File("readme.txt", 1024, ".txt"));

            documents.Add(new File("report.doc", 10240, ".doc"));
            documents.Add(new File("presentation.ppt", 5120, ".ppt"));
            documents.Add(new File("data.csv", 2048, ".csv"));

            pictures.Add(new File("vacation.jpg", 3072, ".jpg"));
            pictures.Add(new File("profile.png", 1536, ".png"));

            // Calculate total size
            Console.WriteLine("Size calculation:");
            var sizeCalculator = new SizeCalculatorVisitor();
            root.Accept(sizeCalculator);
            Console.WriteLine($"Total size: {sizeCalculator.GetTotalSize()} bytes");

            Console.WriteLine();

            // Search for specific file types
            Console.WriteLine("Searching for .jpg files:");
            var jpgSearcher = new FileSearchVisitor(".jpg");
            root.Accept(jpgSearcher);
            Console.WriteLine($"Found {jpgSearcher.GetFoundFiles().Count} .jpg files");

            Console.WriteLine();

            // Backup operation
            Console.WriteLine("Backup operation:");
            var backupVisitor = new BackupVisitor();
            root.Accept(backupVisitor);

            Console.WriteLine("\nVisitor pattern allows adding new operations without changing object structure!");
        }
    }
}