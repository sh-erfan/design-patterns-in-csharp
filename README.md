# Design Patterns in C#

This repository contains implementations and usage examples of the 23 classic Gang of Four (GoF) design patterns in C#.

## Overview

Design patterns are reusable solutions to common problems in software design. This repository provides clear implementations of all 23 GoF patterns organized into three categories:

## Pattern Categories

### 🏗️ Creational Patterns (5)
Patterns that deal with object creation mechanisms.

- **Abstract Factory** - Provides an interface for creating families of related objects
- **Builder** - Constructs complex objects step by step
- **Factory Method** - Creates objects without specifying their exact class
- **Prototype** - Creates objects by cloning existing instances
- **Singleton** - Ensures a class has only one instance

### 🔧 Structural Patterns (7)
Patterns that deal with object composition and relationships.

- **Adapter** - Allows incompatible interfaces to work together
- **Bridge** - Separates abstraction from implementation
- **Composite** - Composes objects into tree structures
- **Decorator** - Adds behavior to objects dynamically
- **Facade** - Provides a unified interface to a subsystem
- **Flyweight** - Minimizes memory usage by sharing data efficiently
- **Proxy** - Provides a placeholder or surrogate for another object

### 🎭 Behavioral Patterns (11)
Patterns that deal with communication between objects and the assignment of responsibilities.

- **Chain of Responsibility** - Passes requests along a chain of handlers
- **Command** - Encapsulates requests as objects
- **Interpreter** - Defines a grammar and interpreter for a language
- **Iterator** - Provides a way to access elements sequentially
- **Mediator** - Defines how objects interact with each other
- **Memento** - Captures and restores object state
- **Observer** - Notifies multiple objects about state changes
- **State** - Changes object behavior based on internal state
- **Strategy** - Defines a family of algorithms
- **Template Method** - Defines the skeleton of an algorithm
- **Visitor** - Separates algorithms from object structure

## Project Structure

```
DesignPatternsInCsharp/
├── DesignPatternsDemo/          # Console application with all examples
│   ├── CreationalPatterns/      # Creational pattern implementations
│   ├── StructuralPatterns/      # Structural pattern implementations
│   ├── BehavioralPatterns/      # Behavioral pattern implementations
│   └── Program.cs               # Main entry point with pattern demos
└── README.md                    # This file
```

## How to Run

1. Clone the repository
2. Navigate to the project directory
3. Run the demo application:
   ```bash
   dotnet run --project DesignPatternsDemo
   ```

## Usage Examples

Each pattern includes:
- **Implementation** - The core pattern structure
- **Usage Example** - Practical demonstration of the pattern
- **Comments** - Explanations of key concepts

## Building and Testing

```bash
# Build the solution
dotnet build

# Run the demo application
dotnet run --project DesignPatternsDemo
```

## Contributing

Feel free to contribute by:
- Adding more real-world examples
- Improving documentation
- Fixing bugs or improving implementations

## License

This project is for educational purposes and demonstrates common design patterns in C#.