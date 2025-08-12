using System;

namespace DesignPatternsDemo.BehavioralPatterns
{
    // Strategy interface
    public interface IStrategy
    {
        int DoOperation(int num1, int num2);
    }

    // Concrete strategies
    public class OperationAdd : IStrategy
    {
        public int DoOperation(int num1, int num2)
        {
            return num1 + num2;
        }
    }

    public class OperationSubtract : IStrategy
    {
        public int DoOperation(int num1, int num2)
        {
            return num1 - num2;
        }
    }

    public class OperationMultiply : IStrategy
    {
        public int DoOperation(int num1, int num2)
        {
            return num1 * num2;
        }
    }

    // Context class
    public class Context
    {
        private IStrategy _strategy;

        public Context(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public int ExecuteStrategy(int num1, int num2)
        {
            return _strategy.DoOperation(num1, num2);
        }
    }

    /// <summary>
    /// Real-world example: Payment Strategy
    /// </summary>
    
    public interface IPaymentStrategy
    {
        void Pay(decimal amount);
    }

    public class CreditCardPayment : IPaymentStrategy
    {
        private string _cardNumber;
        private string _name;

        public CreditCardPayment(string cardNumber, string name)
        {
            _cardNumber = cardNumber;
            _name = name;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paid ${amount} using Credit Card ending in {_cardNumber.Substring(_cardNumber.Length - 4)}");
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        private string _email;

        public PayPalPayment(string email)
        {
            _email = email;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paid ${amount} using PayPal account: {_email}");
        }
    }

    public class BankTransferPayment : IPaymentStrategy
    {
        private string _accountNumber;

        public BankTransferPayment(string accountNumber)
        {
            _accountNumber = accountNumber;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paid ${amount} using Bank Transfer from account: ****{_accountNumber.Substring(_accountNumber.Length - 4)}");
        }
    }

    public class ShoppingCart
    {
        private decimal _totalAmount;
        private IPaymentStrategy _paymentStrategy;

        public ShoppingCart()
        {
            _totalAmount = 0;
        }

        public void AddItem(string item, decimal price)
        {
            Console.WriteLine($"Added {item}: ${price}");
            _totalAmount += price;
        }

        public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public void Checkout()
        {
            Console.WriteLine($"Total Amount: ${_totalAmount}");
            if (_paymentStrategy != null)
            {
                _paymentStrategy.Pay(_totalAmount);
                Console.WriteLine("Payment completed successfully!");
            }
            else
            {
                Console.WriteLine("No payment method selected!");
            }
        }
    }

    /// <summary>
    /// Usage example for Strategy Pattern
    /// </summary>
    public class StrategyExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Strategy Pattern Example ===");

            // Basic strategy example
            Console.WriteLine("1. Basic Strategy Example (Calculator):");
            var context = new Context(new OperationAdd());
            Console.WriteLine($"10 + 5 = {context.ExecuteStrategy(10, 5)}");

            context.SetStrategy(new OperationSubtract());
            Console.WriteLine($"10 - 5 = {context.ExecuteStrategy(10, 5)}");

            context.SetStrategy(new OperationMultiply());
            Console.WriteLine($"10 * 5 = {context.ExecuteStrategy(10, 5)}");

            // Payment strategy example
            Console.WriteLine("\n2. Payment Strategy Example:");
            var cart = new ShoppingCart();
            
            // Add some items
            cart.AddItem("Laptop", 999.99m);
            cart.AddItem("Mouse", 29.99m);
            cart.AddItem("Keyboard", 79.99m);

            Console.WriteLine("\n--- Checkout with Credit Card ---");
            cart.SetPaymentStrategy(new CreditCardPayment("1234567890123456", "John Doe"));
            cart.Checkout();

            // Create another cart
            Console.WriteLine("\n--- Another Purchase ---");
            var cart2 = new ShoppingCart();
            cart2.AddItem("Book", 15.99m);
            cart2.AddItem("Pen", 2.50m);

            Console.WriteLine("\n--- Checkout with PayPal ---");
            cart2.SetPaymentStrategy(new PayPalPayment("john.doe@email.com"));
            cart2.Checkout();

            // Third cart
            Console.WriteLine("\n--- Third Purchase ---");
            var cart3 = new ShoppingCart();
            cart3.AddItem("Monitor", 299.99m);

            Console.WriteLine("\n--- Checkout with Bank Transfer ---");
            cart3.SetPaymentStrategy(new BankTransferPayment("9876543210"));
            cart3.Checkout();

            Console.WriteLine("\nStrategy pattern allows changing payment methods at runtime!");
        }
    }
}