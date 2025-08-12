using System;

namespace DesignPatternsDemo.StructuralPatterns
{
    /// <summary>
    /// Bridge Pattern - separates an abstraction from its implementation
    /// </summary>
    
    // Implementor interface
    public interface IDevice
    {
        bool IsEnabled();
        void Enable();
        void Disable();
        int GetVolume();
        void SetVolume(int percent);
        int GetChannel();
        void SetChannel(int channel);
    }

    // Concrete implementations
    public class TV : IDevice
    {
        private bool _on = false;
        private int _volume = 30;
        private int _channel = 1;

        public bool IsEnabled()
        {
            return _on;
        }

        public void Enable()
        {
            _on = true;
            Console.WriteLine("TV is now ON");
        }

        public void Disable()
        {
            _on = false;
            Console.WriteLine("TV is now OFF");
        }

        public int GetVolume()
        {
            return _volume;
        }

        public void SetVolume(int percent)
        {
            _volume = percent;
            Console.WriteLine($"TV volume set to {percent}%");
        }

        public int GetChannel()
        {
            return _channel;
        }

        public void SetChannel(int channel)
        {
            _channel = channel;
            Console.WriteLine($"TV channel set to {channel}");
        }
    }

    public class Radio : IDevice
    {
        private bool _on = false;
        private int _volume = 30;
        private int _channel = 1;

        public bool IsEnabled()
        {
            return _on;
        }

        public void Enable()
        {
            _on = true;
            Console.WriteLine("Radio is now ON");
        }

        public void Disable()
        {
            _on = false;
            Console.WriteLine("Radio is now OFF");
        }

        public int GetVolume()
        {
            return _volume;
        }

        public void SetVolume(int percent)
        {
            _volume = percent;
            Console.WriteLine($"Radio volume set to {percent}%");
        }

        public int GetChannel()
        {
            return _channel;
        }

        public void SetChannel(int channel)
        {
            _channel = channel;
            Console.WriteLine($"Radio station set to {channel}");
        }
    }

    // Abstraction
    public abstract class RemoteControl
    {
        protected IDevice device;

        public RemoteControl(IDevice device)
        {
            this.device = device;
        }

        public virtual void TogglePower()
        {
            Console.WriteLine("Remote: power toggle");
            if (device.IsEnabled())
            {
                device.Disable();
            }
            else
            {
                device.Enable();
            }
        }

        public virtual void VolumeDown()
        {
            Console.WriteLine("Remote: volume down");
            device.SetVolume(device.GetVolume() - 10);
        }

        public virtual void VolumeUp()
        {
            Console.WriteLine("Remote: volume up");
            device.SetVolume(device.GetVolume() + 10);
        }

        public virtual void ChannelDown()
        {
            Console.WriteLine("Remote: channel down");
            device.SetChannel(device.GetChannel() - 1);
        }

        public virtual void ChannelUp()
        {
            Console.WriteLine("Remote: channel up");
            device.SetChannel(device.GetChannel() + 1);
        }
    }

    // Refined Abstraction
    public class AdvancedRemoteControl : RemoteControl
    {
        public AdvancedRemoteControl(IDevice device) : base(device)
        {
        }

        public void Mute()
        {
            Console.WriteLine("Remote: mute");
            device.SetVolume(0);
        }
    }

    /// <summary>
    /// Real-world example: Drawing API Bridge
    /// </summary>
    
    public interface IDrawingAPI
    {
        void DrawCircle(double x, double y, double radius);
        void DrawLine(double x1, double y1, double x2, double y2);
    }

    public class DrawingAPI1 : IDrawingAPI
    {
        public void DrawCircle(double x, double y, double radius)
        {
            Console.WriteLine($"API1.circle at {x:F1},{y:F1} radius {radius:F1}");
        }

        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            Console.WriteLine($"API1.line from {x1:F1},{y1:F1} to {x2:F1},{y2:F1}");
        }
    }

    public class DrawingAPI2 : IDrawingAPI
    {
        public void DrawCircle(double x, double y, double radius)
        {
            Console.WriteLine($"API2.circle at {x:F1},{y:F1} radius {radius:F1}");
        }

        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            Console.WriteLine($"API2.line from {x1:F1},{y1:F1} to {x2:F1},{y2:F1}");
        }
    }

    public abstract class Shape
    {
        protected IDrawingAPI drawingAPI;

        protected Shape(IDrawingAPI drawingAPI)
        {
            this.drawingAPI = drawingAPI;
        }

        public abstract void Draw();
        public abstract void ResizeByPercentage(double pct);
    }

    public class CircleShape : Shape
    {
        private double x, y, radius;

        public CircleShape(double x, double y, double radius, IDrawingAPI drawingAPI) 
            : base(drawingAPI)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        public override void Draw()
        {
            drawingAPI.DrawCircle(x, y, radius);
        }

        public override void ResizeByPercentage(double pct)
        {
            radius *= (1.0 + pct / 100.0);
        }
    }

    /// <summary>
    /// Usage example for Bridge Pattern
    /// </summary>
    public class BridgeExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Bridge Pattern Example ===");

            // Remote control example
            Console.WriteLine("1. Remote Control Example:");
            
            var tv = new TV();
            var remote = new AdvancedRemoteControl(tv);
            remote.TogglePower();
            remote.VolumeUp();
            remote.ChannelUp();

            Console.WriteLine();

            var advancedRemote = new AdvancedRemoteControl(tv);
            advancedRemote.Mute();

            Console.WriteLine();

            var radio = new Radio();
            var radioRemote = new AdvancedRemoteControl(radio);
            radioRemote.TogglePower();
            radioRemote.VolumeUp();
            radioRemote.ChannelUp();

            Console.WriteLine();

            // Drawing API example
            Console.WriteLine("2. Drawing API Example:");
            
            var shapes = new Shape[]
            {
                new CircleShape(1, 2, 3, new DrawingAPI1()),
                new CircleShape(5, 7, 11, new DrawingAPI2())
            };

            foreach (var shape in shapes)
            {
                shape.ResizeByPercentage(2.5);
                shape.Draw();
            }

            Console.WriteLine("\nBridge pattern decouples abstraction from implementation!");
        }
    }
}