using System;

namespace DesignPatternsDemo.StructuralPatterns
{
    /// <summary>
    /// Proxy Pattern - provides a placeholder or surrogate for another object to control access to it
    /// </summary>
    
    // Subject interface
    public interface IImage
    {
        void Display();
    }

    // Real subject
    public class RealImage : IImage
    {
        private string _filename;

        public RealImage(string filename)
        {
            _filename = filename;
            LoadFromDisk();
        }

        private void LoadFromDisk()
        {
            Console.WriteLine($"Loading {_filename} from disk...");
        }

        public void Display()
        {
            Console.WriteLine($"Displaying {_filename}");
        }
    }

    // Proxy
    public class ImageProxy : IImage
    {
        private RealImage _realImage;
        private string _filename;

        public ImageProxy(string filename)
        {
            _filename = filename;
        }

        public void Display()
        {
            // Lazy initialization
            if (_realImage == null)
            {
                _realImage = new RealImage(_filename);
            }
            _realImage.Display();
        }
    }

    /// <summary>
    /// Real-world example: Internet access proxy with caching and access control
    /// </summary>
    
    public interface IInternet
    {
        void ConnectTo(string serverHost);
    }

    public class RealInternet : IInternet
    {
        public void ConnectTo(string serverHost)
        {
            Console.WriteLine($"Connecting to {serverHost}");
        }
    }

    public class ProxyInternet : IInternet
    {
        private RealInternet _internet = new RealInternet();
        private static readonly string[] BannedSites = { "abc.com", "def.com", "ijk.com", "lnm.com" };

        public void ConnectTo(string serverHost)
        {
            if (IsBlocked(serverHost))
            {
                Console.WriteLine($"Access to {serverHost} denied. Site is blocked!");
                return;
            }

            _internet.ConnectTo(serverHost);
        }

        private bool IsBlocked(string site)
        {
            foreach (var bannedSite in BannedSites)
            {
                if (site.ToLower().Contains(bannedSite))
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Another example: Bank account proxy for security
    /// </summary>
    
    public interface IBankAccount
    {
        void Withdraw(decimal amount);
        void Deposit(decimal amount);
        decimal GetBalance();
    }

    public class BankAccount : IBankAccount
    {
        private decimal _balance;
        private string _accountNumber;

        public BankAccount(string accountNumber, decimal initialBalance)
        {
            _accountNumber = accountNumber;
            _balance = initialBalance;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= _balance)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrew ${amount}. New balance: ${_balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds");
            }
        }

        public void Deposit(decimal amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposited ${amount}. New balance: ${_balance}");
        }

        public decimal GetBalance()
        {
            return _balance;
        }
    }

    public class BankAccountProxy : IBankAccount
    {
        private BankAccount _bankAccount;
        private string _password;
        private string _accountNumber;

        public BankAccountProxy(string accountNumber, string password, decimal initialBalance)
        {
            _accountNumber = accountNumber;
            _password = password;
            _bankAccount = new BankAccount(accountNumber, initialBalance);
        }

        public void Withdraw(decimal amount)
        {
            if (Authenticate())
            {
                _bankAccount.Withdraw(amount);
            }
            else
            {
                Console.WriteLine("Authentication failed. Access denied.");
            }
        }

        public void Deposit(decimal amount)
        {
            if (Authenticate())
            {
                _bankAccount.Deposit(amount);
            }
            else
            {
                Console.WriteLine("Authentication failed. Access denied.");
            }
        }

        public decimal GetBalance()
        {
            if (Authenticate())
            {
                return _bankAccount.GetBalance();
            }
            else
            {
                Console.WriteLine("Authentication failed. Access denied.");
                return 0;
            }
        }

        private bool Authenticate()
        {
            // Simulate authentication
            Console.WriteLine("Authenticating user...");
            // In real implementation, this would verify user credentials
            return true; // Assume authentication succeeds for demo
        }
    }

    /// <summary>
    /// Usage example for Proxy Pattern
    /// </summary>
    public class ProxyExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Proxy Pattern Example ===");

            // Image proxy example (Virtual Proxy)
            Console.WriteLine("1. Image Proxy Example (Virtual Proxy):");
            
            IImage image = new ImageProxy("photo.jpg");
            
            // Image will be loaded from disk
            image.Display();
            Console.WriteLine();
            
            // Image will not be loaded from disk
            image.Display();
            
            Console.WriteLine();

            // Internet proxy example (Protection Proxy)
            Console.WriteLine("2. Internet Access Proxy Example (Protection Proxy):");
            
            IInternet internet = new ProxyInternet();
            internet.ConnectTo("google.com");
            internet.ConnectTo("abc.com");
            internet.ConnectTo("facebook.com");
            internet.ConnectTo("def.com");

            Console.WriteLine();

            // Bank account proxy example (Security Proxy)
            Console.WriteLine("3. Bank Account Proxy Example (Security Proxy):");
            
            IBankAccount account = new BankAccountProxy("12345", "password", 1000);
            account.Deposit(200);
            account.Withdraw(150);
            Console.WriteLine($"Current balance: ${account.GetBalance()}");

            Console.WriteLine("\nProxy pattern controls access to objects and adds functionality!");
        }
    }
}