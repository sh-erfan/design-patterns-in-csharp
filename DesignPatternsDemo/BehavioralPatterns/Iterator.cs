using System;
using System.Collections;
using System.Collections.Generic;

namespace DesignPatternsDemo.BehavioralPatterns
{
    /// <summary>
    /// Iterator Pattern - provides a way to access elements of a collection sequentially
    /// without exposing its underlying structure
    /// </summary>
    
    // Iterator interface
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
        void Reset();
    }

    // Aggregate interface
    public interface IIterable<T>
    {
        IIterator<T> CreateIterator();
    }

    // Concrete collection
    public class BookCollection : IIterable<string>
    {
        private List<string> _books = new List<string>();

        public void AddBook(string book)
        {
            _books.Add(book);
        }

        public void RemoveBook(string book)
        {
            _books.Remove(book);
        }

        public int Count => _books.Count;

        public IIterator<string> CreateIterator()
        {
            return new BookIterator(_books);
        }

        // Also implement forward and reverse iterators
        public IIterator<string> CreateReverseIterator()
        {
            return new ReverseBookIterator(_books);
        }
    }

    // Concrete iterator
    public class BookIterator : IIterator<string>
    {
        private List<string> _books;
        private int _position = 0;

        public BookIterator(List<string> books)
        {
            _books = books;
        }

        public bool HasNext()
        {
            return _position < _books.Count;
        }

        public string Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No more elements");

            return _books[_position++];
        }

        public void Reset()
        {
            _position = 0;
        }
    }

    // Reverse iterator
    public class ReverseBookIterator : IIterator<string>
    {
        private List<string> _books;
        private int _position;

        public ReverseBookIterator(List<string> books)
        {
            _books = books;
            _position = books.Count - 1;
        }

        public bool HasNext()
        {
            return _position >= 0;
        }

        public string Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No more elements");

            return _books[_position--];
        }

        public void Reset()
        {
            _position = _books.Count - 1;
        }
    }

    /// <summary>
    /// Real-world example: Social Media Profile Iterator
    /// </summary>
    
    public class Profile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Friends { get; set; } = new List<string>();

        public Profile(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }

    // Different iterator types for social media
    public interface ISocialIterator
    {
        bool HasNext();
        Profile Next();
    }

    public class FacebookIterator : ISocialIterator
    {
        private List<Profile> _profiles;
        private int _position = 0;

        public FacebookIterator(List<Profile> profiles)
        {
            _profiles = profiles;
        }

        public bool HasNext()
        {
            return _position < _profiles.Count;
        }

        public Profile Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No more profiles");

            var profile = _profiles[_position++];
            Console.WriteLine($"Facebook: Fetching profile {profile.Name}");
            return profile;
        }
    }

    public class LinkedInIterator : ISocialIterator
    {
        private List<Profile> _profiles;
        private int _position = 0;

        public LinkedInIterator(List<Profile> profiles)
        {
            _profiles = profiles;
        }

        public bool HasNext()
        {
            return _position < _profiles.Count;
        }

        public Profile Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No more profiles");

            var profile = _profiles[_position++];
            Console.WriteLine($"LinkedIn: Fetching professional profile {profile.Name}");
            return profile;
        }
    }

    public class SocialSpammer
    {
        public void SendSpamToFriends(ISocialIterator iterator, string message)
        {
            while (iterator.HasNext())
            {
                var profile = iterator.Next();
                Console.WriteLine($"Sending '{message}' to {profile.Name} at {profile.Email}");
            }
        }
    }

    /// <summary>
    /// C# specific example using IEnumerable and yield
    /// </summary>
    
    public class NumberSequence : IEnumerable<int>
    {
        private int _start;
        private int _end;

        public NumberSequence(int start, int end)
        {
            _start = start;
            _end = end;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = _start; i <= _end; i++)
            {
                Console.WriteLine($"Generating number: {i}");
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Custom iterator methods
        public IEnumerable<int> GetEvenNumbers()
        {
            for (int i = _start; i <= _end; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine($"Generating even number: {i}");
                    yield return i;
                }
            }
        }

        public IEnumerable<int> GetSquares()
        {
            for (int i = _start; i <= _end; i++)
            {
                int square = i * i;
                Console.WriteLine($"Generating square of {i}: {square}");
                yield return square;
            }
        }
    }

    /// <summary>
    /// Usage example for Iterator Pattern
    /// </summary>
    public class IteratorExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Iterator Pattern Example ===");

            // Book collection example
            Console.WriteLine("1. Book Collection Iterator Example:");
            
            var bookCollection = new BookCollection();
            bookCollection.AddBook("Design Patterns");
            bookCollection.AddBook("Clean Code");
            bookCollection.AddBook("Refactoring");
            bookCollection.AddBook("The Pragmatic Programmer");

            Console.WriteLine("Forward iteration:");
            var iterator = bookCollection.CreateIterator();
            while (iterator.HasNext())
            {
                Console.WriteLine($"  - {iterator.Next()}");
            }

            Console.WriteLine("\nReverse iteration:");
            var reverseIterator = bookCollection.CreateReverseIterator();
            while (reverseIterator.HasNext())
            {
                Console.WriteLine($"  - {reverseIterator.Next()}");
            }

            Console.WriteLine();

            // Social media example
            Console.WriteLine("2. Social Media Iterator Example:");
            
            var profiles = new List<Profile>
            {
                new Profile("Alice", "alice@example.com"),
                new Profile("Bob", "bob@example.com"),
                new Profile("Charlie", "charlie@example.com")
            };

            var spammer = new SocialSpammer();

            Console.WriteLine("Spamming via Facebook:");
            spammer.SendSpamToFriends(new FacebookIterator(profiles), "Check out this amazing offer!");

            Console.WriteLine("\nSpamming via LinkedIn:");
            spammer.SendSpamToFriends(new LinkedInIterator(profiles), "Professional opportunity!");

            Console.WriteLine();

            // C# yield example
            Console.WriteLine("3. C# Yield Iterator Example:");
            
            var numbers = new NumberSequence(1, 5);

            Console.WriteLine("All numbers:");
            foreach (var number in numbers)
            {
                Console.WriteLine($"  Processing: {number}");
            }

            Console.WriteLine("\nEven numbers only:");
            foreach (var even in numbers.GetEvenNumbers())
            {
                Console.WriteLine($"  Processing even: {even}");
            }

            Console.WriteLine("\nSquares:");
            foreach (var square in numbers.GetSquares())
            {
                Console.WriteLine($"  Processing square: {square}");
            }

            Console.WriteLine("\nIterator pattern provides uniform access to collection elements!");
        }
    }
}