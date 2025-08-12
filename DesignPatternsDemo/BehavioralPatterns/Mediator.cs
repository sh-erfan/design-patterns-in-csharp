using System;
using System.Collections.Generic;

namespace DesignPatternsDemo.BehavioralPatterns
{
    /// <summary>
    /// Mediator Pattern - defines how objects interact with each other
    /// </summary>
    
    // Mediator interface
    public interface IMediator
    {
        void SendMessage(string message, Colleague colleague);
    }

    // Colleague base class
    public abstract class Colleague
    {
        protected IMediator _mediator;
        protected string _name;

        public Colleague(IMediator mediator, string name)
        {
            _mediator = mediator;
            _name = name;
        }

        public abstract void Send(string message);
        public abstract void Receive(string message);
    }

    // Concrete mediator
    public class ChatMediator : IMediator
    {
        private List<Colleague> _colleagues = new List<Colleague>();

        public void AddColleague(Colleague colleague)
        {
            _colleagues.Add(colleague);
        }

        public void SendMessage(string message, Colleague sender)
        {
            foreach (var colleague in _colleagues)
            {
                // Don't send message to sender
                if (colleague != sender)
                {
                    colleague.Receive(message);
                }
            }
        }
    }

    // Concrete colleagues
    public class ConcreteColleague : Colleague
    {
        public ConcreteColleague(IMediator mediator, string name) : base(mediator, name)
        {
        }

        public override void Send(string message)
        {
            Console.WriteLine($"{_name} sends: {message}");
            _mediator.SendMessage($"{_name}: {message}", this);
        }

        public override void Receive(string message)
        {
            Console.WriteLine($"{_name} received: {message}");
        }
    }

    /// <summary>
    /// Real-world example: Air Traffic Control System
    /// </summary>
    
    public interface IAirTrafficControl
    {
        void RegisterAircraft(Aircraft aircraft);
        void SendMessage(string message, Aircraft sender);
        void RequestLanding(Aircraft aircraft);
        void RequestTakeoff(Aircraft aircraft);
    }

    public class AirTrafficControlTower : IAirTrafficControl
    {
        private List<Aircraft> _aircraft = new List<Aircraft>();
        private bool _runwayBusy = false;

        public void RegisterAircraft(Aircraft aircraft)
        {
            _aircraft.Add(aircraft);
            Console.WriteLine($"ATC: {aircraft.GetCallSign()} registered with control tower");
        }

        public void SendMessage(string message, Aircraft sender)
        {
            Console.WriteLine($"ATC Broadcasting from {sender.GetCallSign()}: {message}");
            foreach (var aircraft in _aircraft)
            {
                if (aircraft != sender)
                {
                    aircraft.ReceiveMessage($"ATC: {message}");
                }
            }
        }

        public void RequestLanding(Aircraft aircraft)
        {
            if (_runwayBusy)
            {
                aircraft.ReceiveMessage("ATC: Runway busy, please hold");
            }
            else
            {
                _runwayBusy = true;
                aircraft.ReceiveMessage("ATC: Cleared for landing on runway 27");
                Console.WriteLine($"ATC: {aircraft.GetCallSign()} cleared for landing");
                
                // Simulate landing time
                System.Threading.Thread.Sleep(100);
                _runwayBusy = false;
            }
        }

        public void RequestTakeoff(Aircraft aircraft)
        {
            if (_runwayBusy)
            {
                aircraft.ReceiveMessage("ATC: Runway busy, hold for takeoff");
            }
            else
            {
                _runwayBusy = true;
                aircraft.ReceiveMessage("ATC: Cleared for takeoff on runway 27");
                Console.WriteLine($"ATC: {aircraft.GetCallSign()} cleared for takeoff");
                
                // Simulate takeoff time
                System.Threading.Thread.Sleep(100);
                _runwayBusy = false;
            }
        }
    }

    public abstract class Aircraft
    {
        protected IAirTrafficControl _atc;
        protected string _callSign;

        public Aircraft(IAirTrafficControl atc, string callSign)
        {
            _atc = atc;
            _callSign = callSign;
        }

        public string GetCallSign() => _callSign;

        public abstract void ReceiveMessage(string message);
        public abstract void RequestLanding();
        public abstract void RequestTakeoff();
        public abstract void SendMessage(string message);
    }

    public class PassengerAircraft : Aircraft
    {
        public PassengerAircraft(IAirTrafficControl atc, string callSign) : base(atc, callSign)
        {
        }

        public override void ReceiveMessage(string message)
        {
            Console.WriteLine($"{_callSign} (Passenger): {message}");
        }

        public override void RequestLanding()
        {
            Console.WriteLine($"{_callSign}: Requesting landing clearance");
            _atc.RequestLanding(this);
        }

        public override void RequestTakeoff()
        {
            Console.WriteLine($"{_callSign}: Requesting takeoff clearance");
            _atc.RequestTakeoff(this);
        }

        public override void SendMessage(string message)
        {
            Console.WriteLine($"{_callSign}: {message}");
            _atc.SendMessage(message, this);
        }
    }

    public class CargoAircraft : Aircraft
    {
        public CargoAircraft(IAirTrafficControl atc, string callSign) : base(atc, callSign)
        {
        }

        public override void ReceiveMessage(string message)
        {
            Console.WriteLine($"{_callSign} (Cargo): {message}");
        }

        public override void RequestLanding()
        {
            Console.WriteLine($"{_callSign}: Requesting landing clearance");
            _atc.RequestLanding(this);
        }

        public override void RequestTakeoff()
        {
            Console.WriteLine($"{_callSign}: Requesting takeoff clearance");
            _atc.RequestTakeoff(this);
        }

        public override void SendMessage(string message)
        {
            Console.WriteLine($"{_callSign}: {message}");
            _atc.SendMessage(message, this);
        }
    }

    /// <summary>
    /// Another example: Smart Home System
    /// </summary>
    
    public interface ISmartHomeMediator
    {
        void Notify(object sender, string eventType);
    }

    public class SmartHomeHub : ISmartHomeMediator
    {
        private SmartLight _light;
        private SmartThermostat _thermostat;
        private SmartSecurity _security;

        public void SetComponents(SmartLight light, SmartThermostat thermostat, SmartSecurity security)
        {
            _light = light;
            _thermostat = thermostat;
            _security = security;
        }

        public void Notify(object sender, string eventType)
        {
            Console.WriteLine($"Smart Home Hub: Received {eventType} event from {sender.GetType().Name}");

            switch (eventType)
            {
                case "motion_detected":
                    _light.TurnOn();
                    _security.TriggerAlert();
                    break;
                case "temperature_low":
                    _thermostat.IncreaseTemperature();
                    break;
                case "nighttime":
                    _light.Dim();
                    _thermostat.SetNightMode();
                    _security.Activate();
                    break;
            }
        }
    }

    public abstract class SmartDevice
    {
        protected ISmartHomeMediator _mediator;

        public SmartDevice(ISmartHomeMediator mediator)
        {
            _mediator = mediator;
        }
    }

    public class SmartLight : SmartDevice
    {
        private bool _isOn = false;
        private int _brightness = 100;

        public SmartLight(ISmartHomeMediator mediator) : base(mediator)
        {
        }

        public void TurnOn()
        {
            _isOn = true;
            _brightness = 100;
            Console.WriteLine("Smart Light: Turned on at full brightness");
        }

        public void Dim()
        {
            _brightness = 30;
            Console.WriteLine("Smart Light: Dimmed to 30%");
        }
    }

    public class SmartThermostat : SmartDevice
    {
        private int _temperature = 22;

        public SmartThermostat(ISmartHomeMediator mediator) : base(mediator)
        {
        }

        public void IncreaseTemperature()
        {
            _temperature += 2;
            Console.WriteLine($"Smart Thermostat: Increased temperature to {_temperature}°C");
        }

        public void SetNightMode()
        {
            _temperature = 18;
            Console.WriteLine("Smart Thermostat: Set to night mode (18°C)");
        }

        public void CheckTemperature()
        {
            if (_temperature < 20)
            {
                _mediator.Notify(this, "temperature_low");
            }
        }
    }

    public class SmartSecurity : SmartDevice
    {
        private bool _isActive = false;

        public SmartSecurity(ISmartHomeMediator mediator) : base(mediator)
        {
        }

        public void Activate()
        {
            _isActive = true;
            Console.WriteLine("Smart Security: System activated");
        }

        public void TriggerAlert()
        {
            Console.WriteLine("Smart Security: ALERT - Motion detected!");
        }

        public void DetectMotion()
        {
            _mediator.Notify(this, "motion_detected");
        }
    }

    /// <summary>
    /// Usage example for Mediator Pattern
    /// </summary>
    public class MediatorExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Mediator Pattern Example ===");

            // Chat room example
            Console.WriteLine("1. Chat Room Example:");
            
            var chatMediator = new ChatMediator();
            var user1 = new ConcreteColleague(chatMediator, "Alice");
            var user2 = new ConcreteColleague(chatMediator, "Bob");
            var user3 = new ConcreteColleague(chatMediator, "Charlie");

            chatMediator.AddColleague(user1);
            chatMediator.AddColleague(user2);
            chatMediator.AddColleague(user3);

            user1.Send("Hello everyone!");
            user2.Send("Hi Alice!");
            user3.Send("Good morning all!");

            Console.WriteLine();

            // Air traffic control example
            Console.WriteLine("2. Air Traffic Control Example:");
            
            var atc = new AirTrafficControlTower();
            var flight1 = new PassengerAircraft(atc, "UA123");
            var flight2 = new CargoAircraft(atc, "FX456");
            var flight3 = new PassengerAircraft(atc, "AA789");

            atc.RegisterAircraft(flight1);
            atc.RegisterAircraft(flight2);
            atc.RegisterAircraft(flight3);

            flight1.RequestLanding();
            flight2.RequestLanding(); // Should be told to hold
            flight3.RequestTakeoff();

            flight1.SendMessage("Weather looks good from up here");

            Console.WriteLine();

            // Smart home example
            Console.WriteLine("3. Smart Home System Example:");
            
            var homeHub = new SmartHomeHub();
            var light = new SmartLight(homeHub);
            var thermostat = new SmartThermostat(homeHub);
            var security = new SmartSecurity(homeHub);

            homeHub.SetComponents(light, thermostat, security);

            // Simulate events
            security.DetectMotion();
            Console.WriteLine();

            thermostat.CheckTemperature();
            Console.WriteLine();

            homeHub.Notify(null, "nighttime");

            Console.WriteLine("\nMediator pattern centralizes complex communications and control logic!");
        }
    }
}