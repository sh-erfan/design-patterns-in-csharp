using System;
using System.Collections.Generic;

namespace DesignPatternsDemo.StructuralPatterns
{
    /// <summary>
    /// Flyweight Pattern - minimizes memory usage by sharing efficiently among similar objects
    /// </summary>
    
    // Flyweight interface
    public interface ITreeType
    {
        void Render(int x, int y, string context);
    }

    // Concrete flyweight - stores intrinsic state
    public class TreeType : ITreeType
    {
        private string _name;
        private string _color;
        private string _sprite;

        public TreeType(string name, string color, string sprite)
        {
            _name = name;
            _color = color;
            _sprite = sprite;
        }

        public void Render(int x, int y, string context)
        {
            Console.WriteLine($"Rendering {_name} tree at ({x},{y}) with {_color} color in context: {context}");
        }
    }

    // Flyweight factory
    public class TreeTypeFactory
    {
        private static Dictionary<string, TreeType> _treeTypes = new Dictionary<string, TreeType>();

        public static TreeType GetTreeType(string name, string color, string sprite)
        {
            string key = $"{name}-{color}-{sprite}";
            
            if (!_treeTypes.ContainsKey(key))
            {
                _treeTypes[key] = new TreeType(name, color, sprite);
                Console.WriteLine($"Created new TreeType flyweight: {key}");
            }

            return _treeTypes[key];
        }

        public static int GetCreatedFlyweights()
        {
            return _treeTypes.Count;
        }
    }

    // Context class - stores extrinsic state
    public class Tree
    {
        private int _x;
        private int _y;
        private ITreeType _type;

        public Tree(int x, int y, ITreeType type)
        {
            _x = x;
            _y = y;
            _type = type;
        }

        public void Render(string context)
        {
            _type.Render(_x, _y, context);
        }
    }

    // Client
    public class Forest
    {
        private List<Tree> _trees = new List<Tree>();

        public void PlantTree(int x, int y, string name, string color, string sprite)
        {
            var type = TreeTypeFactory.GetTreeType(name, color, sprite);
            var tree = new Tree(x, y, type);
            _trees.Add(tree);
        }

        public void RenderForest(string context)
        {
            Console.WriteLine($"Rendering forest with {_trees.Count} trees:");
            foreach (var tree in _trees)
            {
                tree.Render(context);
            }
        }

        public int GetTreeCount()
        {
            return _trees.Count;
        }
    }

    /// <summary>
    /// Real-world example: Text editor with character formatting
    /// </summary>
    
    public interface ICharacter
    {
        void Display(int position, int fontSize);
    }

    public class ConcreteCharacter : ICharacter
    {
        private char _symbol;
        private string _color;
        private string _fontFamily;

        public ConcreteCharacter(char symbol, string color, string fontFamily)
        {
            _symbol = symbol;
            _color = color;
            _fontFamily = fontFamily;
        }

        public void Display(int position, int fontSize)
        {
            Console.WriteLine($"Character '{_symbol}' at position {position}, font: {_fontFamily}, color: {_color}, size: {fontSize}");
        }
    }

    public class CharacterFactory
    {
        private static Dictionary<string, ICharacter> _characters = new Dictionary<string, ICharacter>();

        public static ICharacter GetCharacter(char symbol, string color, string fontFamily)
        {
            string key = $"{symbol}-{color}-{fontFamily}";
            
            if (!_characters.ContainsKey(key))
            {
                _characters[key] = new ConcreteCharacter(symbol, color, fontFamily);
                Console.WriteLine($"Created new Character flyweight: '{symbol}' ({color}, {fontFamily})");
            }

            return _characters[key];
        }

        public static int GetCreatedFlyweights()
        {
            return _characters.Count;
        }
    }

    public class Document
    {
        private List<(ICharacter character, int position, int fontSize)> _characters = 
            new List<(ICharacter character, int position, int fontSize)>();

        public void AddCharacter(char symbol, string color, string fontFamily, int position, int fontSize)
        {
            var character = CharacterFactory.GetCharacter(symbol, color, fontFamily);
            _characters.Add((character, position, fontSize));
        }

        public void Display()
        {
            Console.WriteLine("Document contents:");
            foreach (var (character, position, fontSize) in _characters)
            {
                character.Display(position, fontSize);
            }
        }

        public int GetCharacterCount()
        {
            return _characters.Count;
        }
    }

    /// <summary>
    /// Usage example for Flyweight Pattern
    /// </summary>
    public class FlyweightExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Flyweight Pattern Example ===");

            // Forest example
            Console.WriteLine("1. Forest Rendering Example:");
            
            var forest = new Forest();
            
            // Plant many trees (but only a few types)
            for (int i = 0; i < 10; i++)
            {
                forest.PlantTree(
                    random.Next(0, 100), 
                    random.Next(0, 100),
                    GetRandomTreeType(),
                    GetRandomColor(),
                    "tree_sprite.png"
                );
            }

            forest.RenderForest("summer day");
            Console.WriteLine($"Total trees: {forest.GetTreeCount()}");
            Console.WriteLine($"Flyweight objects created: {TreeTypeFactory.GetCreatedFlyweights()}");

            Console.WriteLine();

            // Document example
            Console.WriteLine("2. Document Character Example:");
            
            var document = new Document();
            
            // Add some text with formatting
            string text = "Hello World!";
            for (int i = 0; i < text.Length; i++)
            {
                document.AddCharacter(text[i], "Black", "Arial", i, 12);
            }

            // Add more text with different formatting
            string moreText = "Design Patterns";
            for (int i = 0; i < moreText.Length; i++)
            {
                document.AddCharacter(moreText[i], "Blue", "Times", i + text.Length, 14);
            }

            document.Display();
            Console.WriteLine($"Total characters in document: {document.GetCharacterCount()}");
            Console.WriteLine($"Character flyweight objects created: {CharacterFactory.GetCreatedFlyweights()}");

            Console.WriteLine("\nFlyweight pattern saves memory by sharing common data!");
        }

        private static Random random = new Random();
        
        private static string GetRandomTreeType()
        {
            string[] types = { "Oak", "Pine", "Birch", "Maple" };
            return types[random.Next(types.Length)];
        }

        private static string GetRandomColor()
        {
            string[] colors = { "Green", "Brown", "Yellow" };
            return colors[random.Next(colors.Length)];
        }
    }
}