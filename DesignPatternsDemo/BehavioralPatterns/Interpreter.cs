using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternsDemo.BehavioralPatterns
{
    /// <summary>
    /// Interpreter Pattern - defines a grammar and interpreter for a language
    /// </summary>
    
    // Context class
    public class InterpreterContext
    {
        private Dictionary<string, int> _variables = new Dictionary<string, int>();

        public void SetVariable(string name, int value)
        {
            _variables[name] = value;
        }

        public int GetVariable(string name)
        {
            return _variables.ContainsKey(name) ? _variables[name] : 0;
        }
    }

    // Abstract expression
    public abstract class Expression
    {
        public abstract int Interpret(InterpreterContext context);
    }

    // Terminal expressions
    public class NumberExpression : Expression
    {
        private int _number;

        public NumberExpression(int number)
        {
            _number = number;
        }

        public override int Interpret(InterpreterContext context)
        {
            return _number;
        }

        public override string ToString()
        {
            return _number.ToString();
        }
    }

    public class VariableExpression : Expression
    {
        private string _variableName;

        public VariableExpression(string variableName)
        {
            _variableName = variableName;
        }

        public override int Interpret(InterpreterContext context)
        {
            return context.GetVariable(_variableName);
        }

        public override string ToString()
        {
            return _variableName;
        }
    }

    // Non-terminal expressions
    public class AddExpression : Expression
    {
        private Expression _leftExpression;
        private Expression _rightExpression;

        public AddExpression(Expression left, Expression right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public override int Interpret(InterpreterContext context)
        {
            return _leftExpression.Interpret(context) + _rightExpression.Interpret(context);
        }

        public override string ToString()
        {
            return $"({_leftExpression} + {_rightExpression})";
        }
    }

    public class SubtractExpression : Expression
    {
        private Expression _leftExpression;
        private Expression _rightExpression;

        public SubtractExpression(Expression left, Expression right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public override int Interpret(InterpreterContext context)
        {
            return _leftExpression.Interpret(context) - _rightExpression.Interpret(context);
        }

        public override string ToString()
        {
            return $"({_leftExpression} - {_rightExpression})";
        }
    }

    public class MultiplyExpression : Expression
    {
        private Expression _leftExpression;
        private Expression _rightExpression;

        public MultiplyExpression(Expression left, Expression right)
        {
            _leftExpression = left;
            _rightExpression = right;
        }

        public override int Interpret(InterpreterContext context)
        {
            return _leftExpression.Interpret(context) * _rightExpression.Interpret(context);
        }

        public override string ToString()
        {
            return $"({_leftExpression} * {_rightExpression})";
        }
    }

    /// <summary>
    /// Simple expression parser
    /// </summary>
    public class ExpressionParser
    {
        public static Expression Parse(string expression)
        {
            // This is a very simple parser for demonstration
            // In real world, you'd use a proper parsing library
            var tokens = expression.Replace("(", "").Replace(")", "").Split(' ');
            
            if (tokens.Length == 1)
            {
                // Single token - number or variable
                if (int.TryParse(tokens[0], out int number))
                {
                    return new NumberExpression(number);
                }
                else
                {
                    return new VariableExpression(tokens[0]);
                }
            }
            else if (tokens.Length == 3)
            {
                // Simple binary expression: left operator right
                var left = int.TryParse(tokens[0], out int leftNum) 
                    ? (Expression)new NumberExpression(leftNum) 
                    : new VariableExpression(tokens[0]);

                var right = int.TryParse(tokens[2], out int rightNum) 
                    ? (Expression)new NumberExpression(rightNum) 
                    : new VariableExpression(tokens[2]);

                return tokens[1] switch
                {
                    "+" => new AddExpression(left, right),
                    "-" => new SubtractExpression(left, right),
                    "*" => new MultiplyExpression(left, right),
                    _ => throw new ArgumentException("Unknown operator")
                };
            }
            
            throw new ArgumentException("Invalid expression format");
        }
    }

    /// <summary>
    /// Real-world example: SQL-like query interpreter
    /// </summary>
    
    public class QueryContext
    {
        private List<Dictionary<string, object>> _data;

        public QueryContext(List<Dictionary<string, object>> data)
        {
            _data = data;
        }

        public List<Dictionary<string, object>> GetData() => _data;
    }

    public abstract class QueryExpression
    {
        public abstract List<Dictionary<string, object>> Interpret(QueryContext context);
    }

    public class SelectExpression : QueryExpression
    {
        private string[] _columns;
        private QueryExpression _fromExpression;

        public SelectExpression(string[] columns, QueryExpression fromExpression)
        {
            _columns = columns;
            _fromExpression = fromExpression;
        }

        public override List<Dictionary<string, object>> Interpret(QueryContext context)
        {
            var data = _fromExpression.Interpret(context);
            var result = new List<Dictionary<string, object>>();

            foreach (var row in data)
            {
                var newRow = new Dictionary<string, object>();
                foreach (var column in _columns)
                {
                    if (column == "*")
                    {
                        // Select all columns
                        foreach (var kvp in row)
                        {
                            newRow[kvp.Key] = kvp.Value;
                        }
                    }
                    else if (row.ContainsKey(column))
                    {
                        newRow[column] = row[column];
                    }
                }
                result.Add(newRow);
            }

            return result;
        }
    }

    public class FromExpression : QueryExpression
    {
        public override List<Dictionary<string, object>> Interpret(QueryContext context)
        {
            return context.GetData();
        }
    }

    public class WhereExpression : QueryExpression
    {
        private QueryExpression _sourceExpression;
        private string _column;
        private object _value;

        public WhereExpression(QueryExpression source, string column, object value)
        {
            _sourceExpression = source;
            _column = column;
            _value = value;
        }

        public override List<Dictionary<string, object>> Interpret(QueryContext context)
        {
            var data = _sourceExpression.Interpret(context);
            var result = new List<Dictionary<string, object>>();

            foreach (var row in data)
            {
                if (row.ContainsKey(_column) && row[_column].Equals(_value))
                {
                    result.Add(row);
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Roman numeral interpreter example
    /// </summary>
    
    public class RomanContext
    {
        public int Number { get; set; }

        public RomanContext(int number)
        {
            Number = number;
        }
    }

    public abstract class RomanExpression
    {
        public abstract string Interpret(RomanContext context);
        
        protected string Repeat(string symbol, int count)
        {
            var result = "";
            for (int i = 0; i < count; i++)
            {
                result += symbol;
            }
            return result;
        }
    }

    public class ThousandExpression : RomanExpression
    {
        public override string Interpret(RomanContext context)
        {
            int thousands = context.Number / 1000;
            context.Number %= 1000;
            return Repeat("M", thousands);
        }
    }

    public class HundredExpression : RomanExpression
    {
        public override string Interpret(RomanContext context)
        {
            int hundreds = context.Number / 100;
            context.Number %= 100;

            return hundreds switch
            {
                9 => "CM",
                >= 5 => "D" + Repeat("C", hundreds - 5),
                4 => "CD",
                _ => Repeat("C", hundreds)
            };
        }
    }

    public class TenExpression : RomanExpression
    {
        public override string Interpret(RomanContext context)
        {
            int tens = context.Number / 10;
            context.Number %= 10;

            return tens switch
            {
                9 => "XC",
                >= 5 => "L" + Repeat("X", tens - 5),
                4 => "XL",
                _ => Repeat("X", tens)
            };
        }
    }

    public class OneExpression : RomanExpression
    {
        public override string Interpret(RomanContext context)
        {
            int ones = context.Number;
            context.Number = 0;

            return ones switch
            {
                9 => "IX",
                >= 5 => "V" + Repeat("I", ones - 5),
                4 => "IV",
                _ => Repeat("I", ones)
            };
        }
    }

    /// <summary>
    /// Usage example for Interpreter Pattern
    /// </summary>
    public class InterpreterExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Interpreter Pattern Example ===");

            // Mathematical expression interpreter
            Console.WriteLine("1. Mathematical Expression Interpreter:");
            
            var context = new InterpreterContext();
            context.SetVariable("x", 10);
            context.SetVariable("y", 5);

            var expressions = new[]
            {
                "5 + 3",
                "x - y", 
                "x * 2",
                "15 - 7"
            };

            foreach (var expr in expressions)
            {
                try
                {
                    var parsedExpression = ExpressionParser.Parse(expr);
                    var result = parsedExpression.Interpret(context);
                    Console.WriteLine($"{expr} = {result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error parsing '{expr}': {e.Message}");
                }
            }

            Console.WriteLine();

            // Query interpreter example
            Console.WriteLine("2. Simple Query Interpreter:");
            
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> { {"id", 1}, {"name", "Alice"}, {"age", 25} },
                new Dictionary<string, object> { {"id", 2}, {"name", "Bob"}, {"age", 30} },
                new Dictionary<string, object> { {"id", 3}, {"name", "Charlie"}, {"age", 25} }
            };

            var queryContext = new QueryContext(data);

            // SELECT * FROM data
            var query1 = new SelectExpression(new[] { "*" }, new FromExpression());
            var result1 = query1.Interpret(queryContext);
            
            Console.WriteLine("SELECT * FROM data:");
            foreach (var row in result1)
            {
                Console.WriteLine($"  {string.Join(", ", row.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}");
            }

            // SELECT name FROM data WHERE age = 25
            var query2 = new SelectExpression(
                new[] { "name" }, 
                new WhereExpression(new FromExpression(), "age", 25)
            );
            var result2 = query2.Interpret(queryContext);
            
            Console.WriteLine("\nSELECT name FROM data WHERE age = 25:");
            foreach (var row in result2)
            {
                Console.WriteLine($"  {row["name"]}");
            }

            Console.WriteLine();

            // Roman numeral interpreter
            Console.WriteLine("3. Roman Numeral Interpreter:");
            
            var romanInterpreters = new List<RomanExpression>
            {
                new ThousandExpression(),
                new HundredExpression(),
                new TenExpression(),
                new OneExpression()
            };

            var numbers = new[] { 1, 5, 9, 27, 48, 59, 93, 141, 163, 402, 575, 911, 1024, 3000 };

            foreach (var number in numbers)
            {
                var romanContext = new RomanContext(number);
                var roman = "";
                
                foreach (var interpreter in romanInterpreters)
                {
                    roman += interpreter.Interpret(romanContext);
                }
                
                Console.WriteLine($"{number} = {roman}");
            }

            Console.WriteLine("\nInterpreter pattern defines grammar for a language and interprets sentences!");
        }
    }
}