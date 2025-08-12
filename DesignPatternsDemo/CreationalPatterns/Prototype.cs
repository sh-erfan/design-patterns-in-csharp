using System;

namespace DesignPatternsDemo.CreationalPatterns
{
    // Prototype interface
    public interface IPrototype<T>
    {
        T Clone();
    }

    // Concrete prototype - Shape
    public abstract class Shape : IPrototype<Shape>
    {
        public string Id { get; set; }
        public string Type { get; protected set; }

        public abstract void Draw();
        public abstract Shape Clone();

        public override string ToString()
        {
            return $"Shape [Id={Id}, Type={Type}]";
        }
    }

    // Concrete implementations
    public class Rectangle : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle()
        {
            Type = "Rectangle";
        }

        public override void Draw()
        {
            Console.WriteLine($"Drawing Rectangle: {Width}x{Height}");
        }

        public override Shape Clone()
        {
            Console.WriteLine("Cloning Rectangle");
            return new Rectangle
            {
                Id = this.Id,
                Width = this.Width,
                Height = this.Height
            };
        }
    }

    public class Circle : Shape
    {
        public int Radius { get; set; }

        public Circle()
        {
            Type = "Circle";
        }

        public override void Draw()
        {
            Console.WriteLine($"Drawing Circle: Radius={Radius}");
        }

        public override Shape Clone()
        {
            Console.WriteLine("Cloning Circle");
            return new Circle
            {
                Id = this.Id,
                Radius = this.Radius
            };
        }
    }

    public class Square : Shape
    {
        public int Side { get; set; }

        public Square()
        {
            Type = "Square";
        }

        public override void Draw()
        {
            Console.WriteLine($"Drawing Square: Side={Side}");
        }

        public override Shape Clone()
        {
            Console.WriteLine("Cloning Square");
            return new Square
            {
                Id = this.Id,
                Side = this.Side
            };
        }
    }

    // Prototype registry/cache
    public class ShapeCache
    {
        private static System.Collections.Generic.Dictionary<string, Shape> _shapeMap = 
            new System.Collections.Generic.Dictionary<string, Shape>();

        public static Shape GetShape(string shapeId)
        {
            var cachedShape = _shapeMap[shapeId];
            return cachedShape.Clone();
        }

        public static void LoadCache()
        {
            var circle = new Circle
            {
                Id = "1",
                Radius = 10
            };
            _shapeMap.Add(circle.Id, circle);

            var square = new Square
            {
                Id = "2",
                Side = 5
            };
            _shapeMap.Add(square.Id, square);

            var rectangle = new Rectangle
            {
                Id = "3",
                Width = 15,
                Height = 20
            };
            _shapeMap.Add(rectangle.Id, rectangle);
        }
    }

    /// <summary>
    /// Real-world example: Document templates
    /// </summary>
    public class Document : IPrototype<Document>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Author { get; set; }

        public Document Clone()
        {
            Console.WriteLine($"Cloning document: {Title}");
            return new Document
            {
                Title = this.Title + " (Copy)",
                Content = this.Content,
                CreatedDate = DateTime.Now, // New creation date for the copy
                Author = this.Author
            };
        }

        public void Display()
        {
            Console.WriteLine($"Document: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Created: {CreatedDate:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Content: {Content}");
        }
    }

    /// <summary>
    /// Usage example for Prototype Pattern
    /// </summary>
    public class PrototypeExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Prototype Pattern Example ===");

            // Shape cloning example
            Console.WriteLine("1. Shape Cloning Example:");
            ShapeCache.LoadCache();

            var clonedShape1 = ShapeCache.GetShape("1");
            Console.WriteLine($"Original: Circle");
            clonedShape1.Draw();

            var clonedShape2 = ShapeCache.GetShape("2");
            Console.WriteLine($"Original: Square");
            clonedShape2.Draw();

            var clonedShape3 = ShapeCache.GetShape("3");
            Console.WriteLine($"Original: Rectangle");
            clonedShape3.Draw();

            // Document template example
            Console.WriteLine("\n2. Document Template Example:");
            var originalDocument = new Document
            {
                Title = "Project Proposal Template",
                Content = "This is a template for project proposals. Please fill in your specific details...",
                CreatedDate = DateTime.Now.AddDays(-30),
                Author = "Template Creator"
            };

            Console.WriteLine("Original Document:");
            originalDocument.Display();

            Console.WriteLine("\nCloning document for new project:");
            var projectDocument = originalDocument.Clone();
            projectDocument.Author = "John Doe";
            projectDocument.Content = "Proposal for implementing design patterns in our codebase...";

            Console.WriteLine("\nCloned Document:");
            projectDocument.Display();

            Console.WriteLine("\nPrototype pattern allows creating objects by cloning existing instances!");
        }
    }
}