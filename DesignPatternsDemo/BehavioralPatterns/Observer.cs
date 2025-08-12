using System;
using System.Collections.Generic;

namespace DesignPatternsDemo.BehavioralPatterns
{
    // Observer interface
    public interface IObserver
    {
        void Update(string message);
    }

    // Subject interface
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    // Concrete subject
    public class ConcreteSubject : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private string _state;

        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                Notify();
            }
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
            Console.WriteLine("Observer attached");
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
            Console.WriteLine("Observer detached");
        }

        public void Notify()
        {
            Console.WriteLine("Notifying observers...");
            foreach (var observer in _observers)
            {
                observer.Update(_state);
            }
        }
    }

    // Concrete observer
    public class ConcreteObserver : IObserver
    {
        private string _name;

        public ConcreteObserver(string name)
        {
            _name = name;
        }

        public void Update(string message)
        {
            Console.WriteLine($"{_name} received update: {message}");
        }
    }

    /// <summary>
    /// Real-world example: News Agency and News Channels
    /// </summary>
    
    public interface INewsChannel
    {
        void Update(string news);
    }

    public class NewsAgency
    {
        private List<INewsChannel> _channels = new List<INewsChannel>();
        private string _news;

        public void Subscribe(INewsChannel channel)
        {
            _channels.Add(channel);
        }

        public void Unsubscribe(INewsChannel channel)
        {
            _channels.Remove(channel);
        }

        public void SetNews(string news)
        {
            _news = news;
            NotifyAll();
        }

        private void NotifyAll()
        {
            foreach (var channel in _channels)
            {
                channel.Update(_news);
            }
        }
    }

    public class NewsChannel : INewsChannel
    {
        private string _channelName;

        public NewsChannel(string channelName)
        {
            _channelName = channelName;
        }

        public void Update(string news)
        {
            Console.WriteLine($"{_channelName} broadcasting: {news}");
        }
    }

    /// <summary>
    /// Stock market example with events (C# specific)
    /// </summary>
    
    public class Stock
    {
        private string _symbol;
        private decimal _price;

        public event EventHandler<StockPriceEventArgs> PriceChanged;

        public string Symbol => _symbol;

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    var oldPrice = _price;
                    _price = value;
                    OnPriceChanged(new StockPriceEventArgs(_symbol, oldPrice, _price));
                }
            }
        }

        public Stock(string symbol, decimal price)
        {
            _symbol = symbol;
            _price = price;
        }

        protected virtual void OnPriceChanged(StockPriceEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }
    }

    public class StockPriceEventArgs : EventArgs
    {
        public string Symbol { get; }
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }

        public StockPriceEventArgs(string symbol, decimal oldPrice, decimal newPrice)
        {
            Symbol = symbol;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
    }

    public class StockPortfolio
    {
        private string _name;

        public StockPortfolio(string name)
        {
            _name = name;
        }

        public void OnStockPriceChanged(object sender, StockPriceEventArgs e)
        {
            Console.WriteLine($"Portfolio {_name}: {e.Symbol} changed from ${e.OldPrice} to ${e.NewPrice}");
        }
    }

    /// <summary>
    /// Usage example for Observer Pattern
    /// </summary>
    public class ObserverExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Observer Pattern Example ===");

            // Basic observer example
            Console.WriteLine("1. Basic Observer Example:");
            var subject = new ConcreteSubject();
            var observer1 = new ConcreteObserver("Observer 1");
            var observer2 = new ConcreteObserver("Observer 2");

            subject.Attach(observer1);
            subject.Attach(observer2);

            subject.State = "New State 1";
            subject.State = "New State 2";

            subject.Detach(observer1);
            subject.State = "New State 3";

            // News agency example
            Console.WriteLine("\n2. News Agency Example:");
            var newsAgency = new NewsAgency();
            var cnnChannel = new NewsChannel("CNN");
            var bbcChannel = new NewsChannel("BBC");

            newsAgency.Subscribe(cnnChannel);
            newsAgency.Subscribe(bbcChannel);

            newsAgency.SetNews("Breaking News: Observer Pattern Explained!");
            
            newsAgency.Unsubscribe(cnnChannel);
            newsAgency.SetNews("Update: Only BBC is listening now");

            // Stock market example with events
            Console.WriteLine("\n3. Stock Market Example (using C# Events):");
            var appleStock = new Stock("AAPL", 150.00m);
            var portfolio1 = new StockPortfolio("Portfolio A");
            var portfolio2 = new StockPortfolio("Portfolio B");

            // Subscribe to events
            appleStock.PriceChanged += portfolio1.OnStockPriceChanged;
            appleStock.PriceChanged += portfolio2.OnStockPriceChanged;

            // Change stock price - observers will be notified automatically
            appleStock.Price = 155.50m;
            appleStock.Price = 149.25m;

            // Unsubscribe one portfolio
            appleStock.PriceChanged -= portfolio1.OnStockPriceChanged;
            appleStock.Price = 160.00m;
        }
    }
}