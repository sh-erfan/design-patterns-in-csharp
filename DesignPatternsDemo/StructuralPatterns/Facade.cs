using System;

namespace DesignPatternsDemo.StructuralPatterns
{
    /// <summary>
    /// Facade Pattern - provides a unified interface to a set of interfaces in a subsystem
    /// </summary>
    
    // Complex subsystem classes
    public class CPU
    {
        public void Freeze()
        {
            Console.WriteLine("CPU: Freezing processor");
        }

        public void Jump(long position)
        {
            Console.WriteLine($"CPU: Jumping to position {position}");
        }

        public void Execute()
        {
            Console.WriteLine("CPU: Executing instructions");
        }
    }

    public class Memory
    {
        public void Load(long position, byte[] data)
        {
            Console.WriteLine($"Memory: Loading {data.Length} bytes to position {position}");
        }
    }

    public class HardDrive
    {
        public byte[] Read(long lba, int size)
        {
            Console.WriteLine($"HardDrive: Reading {size} bytes from sector {lba}");
            return new byte[size];
        }
    }

    // Facade class
    public class ComputerFacade
    {
        private CPU _cpu;
        private Memory _memory;
        private HardDrive _hardDrive;

        private const long BootAddress = 0x00;
        private const long BootSector = 0x00;
        private const int SectorSize = 512;

        public ComputerFacade()
        {
            _cpu = new CPU();
            _memory = new Memory();
            _hardDrive = new HardDrive();
        }

        public void StartComputer()
        {
            Console.WriteLine("Starting computer using Facade...");
            _cpu.Freeze();
            
            var bootData = _hardDrive.Read(BootSector, SectorSize);
            _memory.Load(BootAddress, bootData);
            
            _cpu.Jump(BootAddress);
            _cpu.Execute();
            
            Console.WriteLine("Computer started successfully!");
        }
    }

    /// <summary>
    /// Real-world example: Home Theater System
    /// </summary>
    
    public class Amplifier
    {
        public void On() => Console.WriteLine("Amplifier on");
        public void Off() => Console.WriteLine("Amplifier off");
        public void SetVolume(int level) => Console.WriteLine($"Amplifier volume set to {level}");
    }

    public class DvdPlayer
    {
        public void On() => Console.WriteLine("DVD Player on");
        public void Off() => Console.WriteLine("DVD Player off");
        public void Play(string movie) => Console.WriteLine($"DVD Player playing '{movie}'");
        public void Stop() => Console.WriteLine("DVD Player stopped");
    }

    public class Projector
    {
        public void On() => Console.WriteLine("Projector on");
        public void Off() => Console.WriteLine("Projector off");
        public void SetInput(string input) => Console.WriteLine($"Projector input set to {input}");
        public void WideScreenMode() => Console.WriteLine("Projector in widescreen mode");
    }

    public class Screen
    {
        public void Up() => Console.WriteLine("Screen going up");
        public void Down() => Console.WriteLine("Screen going down");
    }

    public class TheaterLights
    {
        public void On() => Console.WriteLine("Theater lights on");
        public void Off() => Console.WriteLine("Theater lights off");
        public void Dim(int level) => Console.WriteLine($"Theater lights dimmed to {level}%");
    }

    public class PopcornPopper
    {
        public void On() => Console.WriteLine("Popcorn popper on");
        public void Off() => Console.WriteLine("Popcorn popper off");
        public void Pop() => Console.WriteLine("Popcorn popping!");
    }

    // Home Theater Facade
    public class HomeTheaterFacade
    {
        private Amplifier _amplifier;
        private DvdPlayer _dvdPlayer;
        private Projector _projector;
        private Screen _screen;
        private TheaterLights _lights;
        private PopcornPopper _popper;

        public HomeTheaterFacade(
            Amplifier amplifier,
            DvdPlayer dvdPlayer,
            Projector projector,
            Screen screen,
            TheaterLights lights,
            PopcornPopper popper)
        {
            _amplifier = amplifier;
            _dvdPlayer = dvdPlayer;
            _projector = projector;
            _screen = screen;
            _lights = lights;
            _popper = popper;
        }

        public void WatchMovie(string movie)
        {
            Console.WriteLine("Get ready to watch a movie...");
            
            _popper.On();
            _popper.Pop();
            _lights.Dim(10);
            _screen.Down();
            _projector.On();
            _projector.WideScreenMode();
            _projector.SetInput("DVD");
            _amplifier.On();
            _amplifier.SetVolume(5);
            _dvdPlayer.On();
            _dvdPlayer.Play(movie);
            
            Console.WriteLine("Movie experience started!");
        }

        public void EndMovie()
        {
            Console.WriteLine("Shutting down movie theater...");
            
            _popper.Off();
            _lights.On();
            _screen.Up();
            _projector.Off();
            _amplifier.Off();
            _dvdPlayer.Stop();
            _dvdPlayer.Off();
            
            Console.WriteLine("Movie theater shut down");
        }
    }

    /// <summary>
    /// Usage example for Facade Pattern
    /// </summary>
    public class FacadeExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Facade Pattern Example ===");

            // Computer startup facade
            Console.WriteLine("1. Computer Startup Facade:");
            var computer = new ComputerFacade();
            computer.StartComputer();

            Console.WriteLine();

            // Home theater facade
            Console.WriteLine("2. Home Theater Facade:");
            var amplifier = new Amplifier();
            var dvdPlayer = new DvdPlayer();
            var projector = new Projector();
            var screen = new Screen();
            var lights = new TheaterLights();
            var popper = new PopcornPopper();

            var homeTheater = new HomeTheaterFacade(
                amplifier, dvdPlayer, projector, screen, lights, popper);

            homeTheater.WatchMovie("The Matrix");

            Console.WriteLine();

            homeTheater.EndMovie();

            Console.WriteLine("\nFacade pattern simplifies complex subsystem interactions!");
        }
    }
}