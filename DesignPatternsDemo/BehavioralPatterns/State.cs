using System;

namespace DesignPatternsDemo.BehavioralPatterns
{
    /// <summary>
    /// State Pattern - allows an object to alter its behavior when its internal state changes
    /// </summary>
    
    // State interface
    public interface IState
    {
        void InsertQuarter(GumballMachine machine);
        void EjectQuarter(GumballMachine machine);
        void TurnCrank(GumballMachine machine);
        void Dispense(GumballMachine machine);
        string ToString();
    }

    // Context
    public class GumballMachine
    {
        private IState _soldOutState;
        private IState _noQuarterState;
        private IState _hasQuarterState;
        private IState _soldState;

        private IState _state;
        private int _count = 0;

        public GumballMachine(int numberGumballs)
        {
            _soldOutState = new SoldOutState();
            _noQuarterState = new NoQuarterState();
            _hasQuarterState = new HasQuarterState();
            _soldState = new SoldState();

            _count = numberGumballs;
            if (numberGumballs > 0)
            {
                _state = _noQuarterState;
            }
            else
            {
                _state = _soldOutState;
            }
        }

        public void InsertQuarter()
        {
            _state.InsertQuarter(this);
        }

        public void EjectQuarter()
        {
            _state.EjectQuarter(this);
        }

        public void TurnCrank()
        {
            _state.TurnCrank(this);
            _state.Dispense(this);
        }

        public void ReleaseBall()
        {
            Console.WriteLine("A gumball comes rolling out the slot...");
            if (_count != 0)
            {
                _count--;
            }
        }

        public void SetState(IState state)
        {
            _state = state;
        }

        public IState GetSoldOutState() => _soldOutState;
        public IState GetNoQuarterState() => _noQuarterState;
        public IState GetHasQuarterState() => _hasQuarterState;
        public IState GetSoldState() => _soldState;
        public int GetCount() => _count;

        public override string ToString()
        {
            return $"Mighty Gumball, Inc.\nInventory: {_count} gumball{(_count != 1 ? "s" : "")}\n{_state}";
        }
    }

    // Concrete states
    public class NoQuarterState : IState
    {
        public void InsertQuarter(GumballMachine machine)
        {
            Console.WriteLine("You inserted a quarter");
            machine.SetState(machine.GetHasQuarterState());
        }

        public void EjectQuarter(GumballMachine machine)
        {
            Console.WriteLine("You haven't inserted a quarter");
        }

        public void TurnCrank(GumballMachine machine)
        {
            Console.WriteLine("You turned, but there's no quarter");
        }

        public void Dispense(GumballMachine machine)
        {
            Console.WriteLine("You need to pay first");
        }

        public override string ToString()
        {
            return "waiting for quarter";
        }
    }

    public class HasQuarterState : IState
    {
        public void InsertQuarter(GumballMachine machine)
        {
            Console.WriteLine("You can't insert another quarter");
        }

        public void EjectQuarter(GumballMachine machine)
        {
            Console.WriteLine("Quarter returned");
            machine.SetState(machine.GetNoQuarterState());
        }

        public void TurnCrank(GumballMachine machine)
        {
            Console.WriteLine("You turned...");
            machine.SetState(machine.GetSoldState());
        }

        public void Dispense(GumballMachine machine)
        {
            Console.WriteLine("No gumball dispensed");
        }

        public override string ToString()
        {
            return "waiting for turn of crank";
        }
    }

    public class SoldState : IState
    {
        public void InsertQuarter(GumballMachine machine)
        {
            Console.WriteLine("Please wait, we're already giving you a gumball");
        }

        public void EjectQuarter(GumballMachine machine)
        {
            Console.WriteLine("Sorry, you already turned the crank");
        }

        public void TurnCrank(GumballMachine machine)
        {
            Console.WriteLine("Turning twice doesn't get you another gumball!");
        }

        public void Dispense(GumballMachine machine)
        {
            machine.ReleaseBall();
            if (machine.GetCount() > 0)
            {
                machine.SetState(machine.GetNoQuarterState());
            }
            else
            {
                Console.WriteLine("Oops, out of gumballs!");
                machine.SetState(machine.GetSoldOutState());
            }
        }

        public override string ToString()
        {
            return "dispensing a gumball";
        }
    }

    public class SoldOutState : IState
    {
        public void InsertQuarter(GumballMachine machine)
        {
            Console.WriteLine("You can't insert a quarter, the machine is sold out");
        }

        public void EjectQuarter(GumballMachine machine)
        {
            Console.WriteLine("You can't eject, you haven't inserted a quarter yet");
        }

        public void TurnCrank(GumballMachine machine)
        {
            Console.WriteLine("You turned, but there are no gumballs");
        }

        public void Dispense(GumballMachine machine)
        {
            Console.WriteLine("No gumball dispensed");
        }

        public override string ToString()
        {
            return "sold out";
        }
    }

    /// <summary>
    /// Real-world example: Media Player State
    /// </summary>
    
    public interface IPlayerState
    {
        void Play(MediaPlayer player);
        void Pause(MediaPlayer player);
        void Stop(MediaPlayer player);
        void Next(MediaPlayer player);
        void Previous(MediaPlayer player);
        string GetStatus();
    }

    public class MediaPlayer
    {
        private IPlayerState _playingState;
        private IPlayerState _pausedState;
        private IPlayerState _stoppedState;

        private IPlayerState _currentState;
        private string _currentSong;

        public MediaPlayer()
        {
            _playingState = new PlayingState();
            _pausedState = new PausedState();
            _stoppedState = new StoppedState();

            _currentState = _stoppedState;
            _currentSong = "No song loaded";
        }

        public void Play() => _currentState.Play(this);
        public void Pause() => _currentState.Pause(this);
        public void Stop() => _currentState.Stop(this);
        public void Next() => _currentState.Next(this);
        public void Previous() => _currentState.Previous(this);

        public void SetState(IPlayerState state) => _currentState = state;
        public IPlayerState GetPlayingState() => _playingState;
        public IPlayerState GetPausedState() => _pausedState;
        public IPlayerState GetStoppedState() => _stoppedState;

        public void LoadSong(string song)
        {
            _currentSong = song;
            Console.WriteLine($"Loaded: {song}");
        }

        public string GetCurrentSong() => _currentSong;
        public string GetStatus() => _currentState.GetStatus();
    }

    public class PlayingState : IPlayerState
    {
        public void Play(MediaPlayer player)
        {
            Console.WriteLine("Already playing");
        }

        public void Pause(MediaPlayer player)
        {
            Console.WriteLine("Pausing playback");
            player.SetState(player.GetPausedState());
        }

        public void Stop(MediaPlayer player)
        {
            Console.WriteLine("Stopping playback");
            player.SetState(player.GetStoppedState());
        }

        public void Next(MediaPlayer player)
        {
            Console.WriteLine("Playing next song");
            player.LoadSong("Next Song");
        }

        public void Previous(MediaPlayer player)
        {
            Console.WriteLine("Playing previous song");
            player.LoadSong("Previous Song");
        }

        public string GetStatus() => "Playing";
    }

    public class PausedState : IPlayerState
    {
        public void Play(MediaPlayer player)
        {
            Console.WriteLine("Resuming playback");
            player.SetState(player.GetPlayingState());
        }

        public void Pause(MediaPlayer player)
        {
            Console.WriteLine("Already paused");
        }

        public void Stop(MediaPlayer player)
        {
            Console.WriteLine("Stopping from pause");
            player.SetState(player.GetStoppedState());
        }

        public void Next(MediaPlayer player)
        {
            Console.WriteLine("Loading next song");
            player.LoadSong("Next Song");
        }

        public void Previous(MediaPlayer player)
        {
            Console.WriteLine("Loading previous song");
            player.LoadSong("Previous Song");
        }

        public string GetStatus() => "Paused";
    }

    public class StoppedState : IPlayerState
    {
        public void Play(MediaPlayer player)
        {
            Console.WriteLine("Starting playback");
            player.SetState(player.GetPlayingState());
        }

        public void Pause(MediaPlayer player)
        {
            Console.WriteLine("Can't pause when stopped");
        }

        public void Stop(MediaPlayer player)
        {
            Console.WriteLine("Already stopped");
        }

        public void Next(MediaPlayer player)
        {
            Console.WriteLine("Loading next song");
            player.LoadSong("Next Song");
        }

        public void Previous(MediaPlayer player)
        {
            Console.WriteLine("Loading previous song");
            player.LoadSong("Previous Song");
        }

        public string GetStatus() => "Stopped";
    }

    /// <summary>
    /// Usage example for State Pattern
    /// </summary>
    public class StateExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== State Pattern Example ===");

            // Gumball machine example
            Console.WriteLine("1. Gumball Machine Example:");
            
            var gumballMachine = new GumballMachine(5);
            
            Console.WriteLine(gumballMachine);
            Console.WriteLine();

            gumballMachine.InsertQuarter();
            gumballMachine.TurnCrank();
            Console.WriteLine(gumballMachine);
            Console.WriteLine();

            gumballMachine.InsertQuarter();
            gumballMachine.EjectQuarter();
            gumballMachine.TurnCrank();
            Console.WriteLine(gumballMachine);
            Console.WriteLine();

            // Media player example
            Console.WriteLine("2. Media Player Example:");
            
            var player = new MediaPlayer();
            player.LoadSong("Song 1");
            
            Console.WriteLine($"Status: {player.GetStatus()}");
            player.Play();
            Console.WriteLine($"Status: {player.GetStatus()}");
            
            player.Pause();
            Console.WriteLine($"Status: {player.GetStatus()}");
            
            player.Play();
            player.Next();
            player.Stop();
            Console.WriteLine($"Status: {player.GetStatus()}");

            Console.WriteLine("\nState pattern encapsulates state-dependent behavior!");
        }
    }
}