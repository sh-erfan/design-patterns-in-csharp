using System;

namespace DesignPatternsDemo.BehavioralPatterns
{
    /// <summary>
    /// Template Method Pattern - defines the skeleton of an algorithm in a base class, 
    /// letting subclasses override specific steps without changing the algorithm's structure
    /// </summary>
    
    // Abstract class defining the template method
    public abstract class DataMiner
    {
        // Template method - defines the skeleton of the algorithm
        public void MineData(string path)
        {
            var file = OpenFile(path);
            var rawData = ExtractData(file);
            var data = ParseData(rawData);
            var analysis = AnalyzeData(data);
            SendReport(analysis);
            CloseFile(file);
        }

        // These methods are the same for all data miners
        protected virtual object OpenFile(string path)
        {
            Console.WriteLine($"Opening file: {path}");
            return new object(); // Simulate file object
        }

        protected virtual void CloseFile(object file)
        {
            Console.WriteLine("Closing file");
        }

        // These abstract methods must be implemented by concrete classes
        protected abstract string ExtractData(object file);
        protected abstract object ParseData(string rawData);
        protected abstract string AnalyzeData(object data);

        // Hook method - can be overridden but has default implementation
        protected virtual void SendReport(string analysis)
        {
            Console.WriteLine($"Sending report: {analysis}");
        }
    }

    // Concrete implementation for PDF files
    public class PDFDataMiner : DataMiner
    {
        protected override string ExtractData(object file)
        {
            Console.WriteLine("Extracting data from PDF file");
            return "PDF raw data";
        }

        protected override object ParseData(string rawData)
        {
            Console.WriteLine("Parsing PDF data");
            return $"Parsed {rawData}";
        }

        protected override string AnalyzeData(object data)
        {
            Console.WriteLine("Analyzing PDF data");
            return $"PDF Analysis: {data}";
        }
    }

    // Concrete implementation for CSV files
    public class CSVDataMiner : DataMiner
    {
        protected override string ExtractData(object file)
        {
            Console.WriteLine("Extracting data from CSV file");
            return "CSV raw data";
        }

        protected override object ParseData(string rawData)
        {
            Console.WriteLine("Parsing CSV data");
            return $"Parsed {rawData}";
        }

        protected override string AnalyzeData(object data)
        {
            Console.WriteLine("Analyzing CSV data");
            return $"CSV Analysis: {data}";
        }

        // Override hook method for different report format
        protected override void SendReport(string analysis)
        {
            Console.WriteLine($"Sending CSV report via email: {analysis}");
        }
    }

    // Concrete implementation for DOC files
    public class DOCDataMiner : DataMiner
    {
        protected override string ExtractData(object file)
        {
            Console.WriteLine("Extracting data from DOC file");
            return "DOC raw data";
        }

        protected override object ParseData(string rawData)
        {
            Console.WriteLine("Parsing DOC data");
            return $"Parsed {rawData}";
        }

        protected override string AnalyzeData(object data)
        {
            Console.WriteLine("Analyzing DOC data");
            return $"DOC Analysis: {data}";
        }
    }

    /// <summary>
    /// Real-world example: Beverage preparation
    /// </summary>
    
    public abstract class Beverage
    {
        // Template method
        public void PrepareBeverage()
        {
            BoilWater();
            Brew();
            PourInCup();
            if (CustomerWantsCondiments())
            {
                AddCondiments();
            }
        }

        // Common methods
        private void BoilWater()
        {
            Console.WriteLine("Boiling water");
        }

        private void PourInCup()
        {
            Console.WriteLine("Pouring into cup");
        }

        // Abstract methods - must be implemented by subclasses
        protected abstract void Brew();
        protected abstract void AddCondiments();

        // Hook method - subclasses can override this to change behavior
        protected virtual bool CustomerWantsCondiments()
        {
            return true;
        }
    }

    public class Tea : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Steeping the tea");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Adding lemon");
        }

        protected override bool CustomerWantsCondiments()
        {
            Console.Write("Would you like lemon with your tea (y/n)? ");
            // For demo, we'll just return true
            Console.WriteLine("y");
            return true;
        }
    }

    public class Coffee : Beverage
    {
        protected override void Brew()
        {
            Console.WriteLine("Dripping coffee through filter");
        }

        protected override void AddCondiments()
        {
            Console.WriteLine("Adding sugar and milk");
        }

        protected override bool CustomerWantsCondiments()
        {
            Console.Write("Would you like milk and sugar with your coffee (y/n)? ");
            // For demo, we'll just return true
            Console.WriteLine("y");
            return true;
        }
    }

    /// <summary>
    /// Usage example for Template Method Pattern
    /// </summary>
    public class TemplateMethodExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Template Method Pattern Example ===");

            // Data mining example
            Console.WriteLine("1. Data Mining Example:");
            
            Console.WriteLine("\nMining PDF data:");
            var pdfMiner = new PDFDataMiner();
            pdfMiner.MineData("data.pdf");

            Console.WriteLine("\nMining CSV data:");
            var csvMiner = new CSVDataMiner();
            csvMiner.MineData("data.csv");

            Console.WriteLine("\nMining DOC data:");
            var docMiner = new DOCDataMiner();
            docMiner.MineData("data.doc");

            // Beverage preparation example
            Console.WriteLine("\n2. Beverage Preparation Example:");
            
            Console.WriteLine("\nPreparing tea:");
            var tea = new Tea();
            tea.PrepareBeverage();

            Console.WriteLine("\nPreparing coffee:");
            var coffee = new Coffee();
            coffee.PrepareBeverage();

            Console.WriteLine("\nTemplate Method defines algorithm skeleton while allowing customization!");
        }
    }
}