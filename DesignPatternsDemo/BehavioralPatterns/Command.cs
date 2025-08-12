using System;

namespace DesignPatternsDemo.BehavioralPatterns
{
    /// <summary>
    /// Command Pattern - encapsulates a request as an object
    /// </summary>
    
    // Command interface
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // Receiver classes
    public class Light
    {
        private string _location;

        public Light(string location)
        {
            _location = location;
        }

        public void TurnOn()
        {
            Console.WriteLine($"{_location} light is ON");
        }

        public void TurnOff()
        {
            Console.WriteLine($"{_location} light is OFF");
        }
    }

    public class Stereo
    {
        private string _location;

        public Stereo(string location)
        {
            _location = location;
        }

        public void On()
        {
            Console.WriteLine($"{_location} stereo is ON");
        }

        public void Off()
        {
            Console.WriteLine($"{_location} stereo is OFF");
        }

        public void SetVolume(int volume)
        {
            Console.WriteLine($"{_location} stereo volume set to {volume}");
        }
    }

    // Concrete commands
    public class LightOnCommand : ICommand
    {
        private Light _light;

        public LightOnCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.TurnOn();
        }

        public void Undo()
        {
            _light.TurnOff();
        }
    }

    public class LightOffCommand : ICommand
    {
        private Light _light;

        public LightOffCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.TurnOff();
        }

        public void Undo()
        {
            _light.TurnOn();
        }
    }

    public class StereoOnWithVolumeCommand : ICommand
    {
        private Stereo _stereo;
        private int _volume;

        public StereoOnWithVolumeCommand(Stereo stereo, int volume)
        {
            _stereo = stereo;
            _volume = volume;
        }

        public void Execute()
        {
            _stereo.On();
            _stereo.SetVolume(_volume);
        }

        public void Undo()
        {
            _stereo.Off();
        }
    }

    public class StereoOffCommand : ICommand
    {
        private Stereo _stereo;

        public StereoOffCommand(Stereo stereo)
        {
            _stereo = stereo;
        }

        public void Execute()
        {
            _stereo.Off();
        }

        public void Undo()
        {
            _stereo.On();
        }
    }

    // Null Object pattern for commands
    public class NoCommand : ICommand
    {
        public void Execute() { }
        public void Undo() { }
    }

    // Invoker
    public class RemoteControl
    {
        private ICommand[] _onCommands;
        private ICommand[] _offCommands;
        private ICommand _undoCommand;

        public RemoteControl()
        {
            _onCommands = new ICommand[7];
            _offCommands = new ICommand[7];
            
            var noCommand = new NoCommand();
            for (int i = 0; i < 7; i++)
            {
                _onCommands[i] = noCommand;
                _offCommands[i] = noCommand;
            }
            _undoCommand = noCommand;
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            _onCommands[slot] = onCommand;
            _offCommands[slot] = offCommand;
        }

        public void OnButtonPressed(int slot)
        {
            _onCommands[slot].Execute();
            _undoCommand = _onCommands[slot];
        }

        public void OffButtonPressed(int slot)
        {
            _offCommands[slot].Execute();
            _undoCommand = _offCommands[slot];
        }

        public void UndoButtonPressed()
        {
            _undoCommand.Undo();
        }

        public override string ToString()
        {
            var output = "\n------ Remote Control -------\n";
            for (int i = 0; i < _onCommands.Length; i++)
            {
                output += $"[slot {i}] {_onCommands[i].GetType().Name}    {_offCommands[i].GetType().Name}\n";
            }
            return output;
        }
    }

    /// <summary>
    /// Usage example for Command Pattern
    /// </summary>
    public class CommandExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Command Pattern Example ===");

            // Create receiver objects
            var livingRoomLight = new Light("Living Room");
            var kitchenLight = new Light("Kitchen");
            var stereo = new Stereo("Living Room");

            // Create command objects
            var livingRoomLightOn = new LightOnCommand(livingRoomLight);
            var livingRoomLightOff = new LightOffCommand(livingRoomLight);
            var kitchenLightOn = new LightOnCommand(kitchenLight);
            var kitchenLightOff = new LightOffCommand(kitchenLight);
            var stereoOnWithVolume = new StereoOnWithVolumeCommand(stereo, 11);
            var stereoOff = new StereoOffCommand(stereo);

            // Create invoker
            var remote = new RemoteControl();

            // Set up remote control
            remote.SetCommand(0, livingRoomLightOn, livingRoomLightOff);
            remote.SetCommand(1, kitchenLightOn, kitchenLightOff);
            remote.SetCommand(2, stereoOnWithVolume, stereoOff);

            Console.WriteLine(remote);

            // Test remote control
            Console.WriteLine("Testing remote control:");
            remote.OnButtonPressed(0);
            remote.OffButtonPressed(0);
            remote.OnButtonPressed(1);
            remote.OffButtonPressed(1);
            remote.OnButtonPressed(2);
            remote.OffButtonPressed(2);

            Console.WriteLine("\nTesting undo functionality:");
            remote.OnButtonPressed(0);
            remote.UndoButtonPressed();
            remote.OffButtonPressed(0);
            remote.UndoButtonPressed();

            Console.WriteLine("\nCommand pattern encapsulates requests as objects!");
        }
    }
}