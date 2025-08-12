using System;

namespace DesignPatternsDemo.CreationalPatterns
{
    // Abstract factory interface
    public interface IUIFactory
    {
        IButton CreateButton();
        ITextBox CreateTextBox();
    }

    // Abstract product interfaces
    public interface IButton
    {
        void Click();
        void Render();
    }

    public interface ITextBox
    {
        void SetText(string text);
        void Render();
    }

    // Windows implementations
    public class WindowsButton : IButton
    {
        public void Click()
        {
            Console.WriteLine("Windows Button clicked");
        }

        public void Render()
        {
            Console.WriteLine("Rendering Windows-style button");
        }
    }

    public class WindowsTextBox : ITextBox
    {
        private string _text = "";

        public void SetText(string text)
        {
            _text = text;
        }

        public void Render()
        {
            Console.WriteLine($"Rendering Windows-style textbox with text: '{_text}'");
        }
    }

    // Mac implementations
    public class MacButton : IButton
    {
        public void Click()
        {
            Console.WriteLine("Mac Button clicked");
        }

        public void Render()
        {
            Console.WriteLine("Rendering Mac-style button");
        }
    }

    public class MacTextBox : ITextBox
    {
        private string _text = "";

        public void SetText(string text)
        {
            _text = text;
        }

        public void Render()
        {
            Console.WriteLine($"Rendering Mac-style textbox with text: '{_text}'");
        }
    }

    // Concrete factories
    public class WindowsUIFactory : IUIFactory
    {
        public IButton CreateButton()
        {
            return new WindowsButton();
        }

        public ITextBox CreateTextBox()
        {
            return new WindowsTextBox();
        }
    }

    public class MacUIFactory : IUIFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }

        public ITextBox CreateTextBox()
        {
            return new MacTextBox();
        }
    }

    // Client class that uses the abstract factory
    public class Application
    {
        private IButton _button;
        private ITextBox _textBox;

        public Application(IUIFactory factory)
        {
            _button = factory.CreateButton();
            _textBox = factory.CreateTextBox();
        }

        public void CreateUI()
        {
            _button.Render();
            _textBox.SetText("Hello World");
            _textBox.Render();
        }

        public void InteractWithUI()
        {
            _button.Click();
        }
    }

    /// <summary>
    /// Usage example for Abstract Factory Pattern
    /// </summary>
    public class AbstractFactoryExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Abstract Factory Pattern Example ===");

            // Create Windows application
            Console.WriteLine("Creating Windows Application:");
            var windowsFactory = new WindowsUIFactory();
            var windowsApp = new Application(windowsFactory);
            windowsApp.CreateUI();
            windowsApp.InteractWithUI();

            Console.WriteLine();

            // Create Mac application
            Console.WriteLine("Creating Mac Application:");
            var macFactory = new MacUIFactory();
            var macApp = new Application(macFactory);
            macApp.CreateUI();
            macApp.InteractWithUI();

            Console.WriteLine("\nAbstract Factory allows creating families of related objects!");
        }
    }
}