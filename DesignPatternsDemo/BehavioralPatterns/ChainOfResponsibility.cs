using System;

namespace DesignPatternsDemo.BehavioralPatterns
{
    /// <summary>
    /// Chain of Responsibility Pattern - passes requests along a chain of potential handlers
    /// </summary>
    
    // Handler interface
    public abstract class Handler
    {
        protected Handler _nextHandler;

        public void SetNext(Handler handler)
        {
            _nextHandler = handler;
        }

        public abstract void HandleRequest(int request);
    }

    // Concrete handlers
    public class ConcreteHandler1 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 0 && request < 10)
            {
                Console.WriteLine($"ConcreteHandler1 handled request {request}");
            }
            else if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(request);
            }
        }
    }

    public class ConcreteHandler2 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 10 && request < 20)
            {
                Console.WriteLine($"ConcreteHandler2 handled request {request}");
            }
            else if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(request);
            }
        }
    }

    public class ConcreteHandler3 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 20 && request < 30)
            {
                Console.WriteLine($"ConcreteHandler3 handled request {request}");
            }
            else if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(request);
            }
            else
            {
                Console.WriteLine($"No handler found for request {request}");
            }
        }
    }

    /// <summary>
    /// Real-world example: Support ticket system
    /// </summary>
    
    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

    public class SupportTicket
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }

        public SupportTicket(int id, string description, Priority priority)
        {
            Id = id;
            Description = description;
            Priority = priority;
        }
    }

    public abstract class SupportHandler
    {
        protected SupportHandler _nextHandler;

        public void SetNext(SupportHandler handler)
        {
            _nextHandler = handler;
        }

        public abstract void HandleTicket(SupportTicket ticket);
    }

    public class Level1Support : SupportHandler
    {
        public override void HandleTicket(SupportTicket ticket)
        {
            if (ticket.Priority == Priority.Low)
            {
                Console.WriteLine($"Level 1 Support resolved ticket #{ticket.Id}: {ticket.Description}");
            }
            else if (_nextHandler != null)
            {
                Console.WriteLine($"Level 1 Support escalating ticket #{ticket.Id}");
                _nextHandler.HandleTicket(ticket);
            }
        }
    }

    public class Level2Support : SupportHandler
    {
        public override void HandleTicket(SupportTicket ticket)
        {
            if (ticket.Priority == Priority.Medium)
            {
                Console.WriteLine($"Level 2 Support resolved ticket #{ticket.Id}: {ticket.Description}");
            }
            else if (_nextHandler != null)
            {
                Console.WriteLine($"Level 2 Support escalating ticket #{ticket.Id}");
                _nextHandler.HandleTicket(ticket);
            }
        }
    }

    public class Level3Support : SupportHandler
    {
        public override void HandleTicket(SupportTicket ticket)
        {
            if (ticket.Priority == Priority.High)
            {
                Console.WriteLine($"Level 3 Support resolved ticket #{ticket.Id}: {ticket.Description}");
            }
            else if (_nextHandler != null)
            {
                Console.WriteLine($"Level 3 Support escalating ticket #{ticket.Id}");
                _nextHandler.HandleTicket(ticket);
            }
        }
    }

    public class ManagerSupport : SupportHandler
    {
        public override void HandleTicket(SupportTicket ticket)
        {
            if (ticket.Priority == Priority.Critical)
            {
                Console.WriteLine($"Manager resolved critical ticket #{ticket.Id}: {ticket.Description}");
            }
            else
            {
                Console.WriteLine($"Manager could not resolve ticket #{ticket.Id}");
            }
        }
    }

    /// <summary>
    /// Another example: Expense approval system
    /// </summary>
    
    public class ExpenseRequest
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }

        public ExpenseRequest(int id, decimal amount, string purpose)
        {
            Id = id;
            Amount = amount;
            Purpose = purpose;
        }
    }

    public abstract class ExpenseHandler
    {
        protected ExpenseHandler _successor;
        protected decimal _approvalLimit;

        public ExpenseHandler(decimal approvalLimit)
        {
            _approvalLimit = approvalLimit;
        }

        public void SetSuccessor(ExpenseHandler successor)
        {
            _successor = successor;
        }

        public abstract void ProcessRequest(ExpenseRequest request);
    }

    public class TeamLead : ExpenseHandler
    {
        public TeamLead() : base(1000) { }

        public override void ProcessRequest(ExpenseRequest request)
        {
            if (request.Amount <= _approvalLimit)
            {
                Console.WriteLine($"Team Lead approved expense #{request.Id} of ${request.Amount} for {request.Purpose}");
            }
            else if (_successor != null)
            {
                Console.WriteLine($"Team Lead forwarding expense #{request.Id} to higher authority");
                _successor.ProcessRequest(request);
            }
        }
    }

    public class Manager : ExpenseHandler
    {
        public Manager() : base(5000) { }

        public override void ProcessRequest(ExpenseRequest request)
        {
            if (request.Amount <= _approvalLimit)
            {
                Console.WriteLine($"Manager approved expense #{request.Id} of ${request.Amount} for {request.Purpose}");
            }
            else if (_successor != null)
            {
                Console.WriteLine($"Manager forwarding expense #{request.Id} to higher authority");
                _successor.ProcessRequest(request);
            }
        }
    }

    public class Director : ExpenseHandler
    {
        public Director() : base(10000) { }

        public override void ProcessRequest(ExpenseRequest request)
        {
            if (request.Amount <= _approvalLimit)
            {
                Console.WriteLine($"Director approved expense #{request.Id} of ${request.Amount} for {request.Purpose}");
            }
            else
            {
                Console.WriteLine($"Expense #{request.Id} of ${request.Amount} exceeds all approval limits. Requires board approval.");
            }
        }
    }

    /// <summary>
    /// Usage example for Chain of Responsibility Pattern
    /// </summary>
    public class ChainOfResponsibilityExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Chain of Responsibility Pattern Example ===");

            // Basic chain example
            Console.WriteLine("1. Basic Chain Example:");
            
            var handler1 = new ConcreteHandler1();
            var handler2 = new ConcreteHandler2();
            var handler3 = new ConcreteHandler3();

            handler1.SetNext(handler2);
            handler2.SetNext(handler3);

            // Client makes requests
            int[] requests = { 2, 5, 14, 22, 18, 3, 27, 20 };

            foreach (int request in requests)
            {
                handler1.HandleRequest(request);
            }

            Console.WriteLine();

            // Support ticket system
            Console.WriteLine("2. Support Ticket System Example:");
            
            var level1 = new Level1Support();
            var level2 = new Level2Support();
            var level3 = new Level3Support();
            var manager = new ManagerSupport();

            level1.SetNext(level2);
            level2.SetNext(level3);
            level3.SetNext(manager);

            var tickets = new[]
            {
                new SupportTicket(1, "Password reset", Priority.Low),
                new SupportTicket(2, "Software installation", Priority.Medium),
                new SupportTicket(3, "Network connectivity issue", Priority.High),
                new SupportTicket(4, "System down", Priority.Critical)
            };

            foreach (var ticket in tickets)
            {
                Console.WriteLine($"\nProcessing ticket #{ticket.Id}:");
                level1.HandleTicket(ticket);
            }

            Console.WriteLine();

            // Expense approval system
            Console.WriteLine("3. Expense Approval System Example:");
            
            var teamLead = new TeamLead();
            var manager2 = new Manager();
            var director = new Director();

            teamLead.SetSuccessor(manager2);
            manager2.SetSuccessor(director);

            var expenses = new[]
            {
                new ExpenseRequest(1, 500, "Office supplies"),
                new ExpenseRequest(2, 3000, "New laptop"),
                new ExpenseRequest(3, 8000, "Team training"),
                new ExpenseRequest(4, 15000, "Conference sponsorship")
            };

            foreach (var expense in expenses)
            {
                Console.WriteLine();
                teamLead.ProcessRequest(expense);
            }

            Console.WriteLine("\nChain of Responsibility decouples request senders from receivers!");
        }
    }
}