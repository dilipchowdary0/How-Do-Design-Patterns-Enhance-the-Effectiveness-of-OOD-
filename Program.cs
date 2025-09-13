using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- 1. Testing Singleton Pattern ---");
        Logger.Instance.Log("System initialized.");
        Logger.Instance.Log("User logged in.");

        Console.WriteLine("\n--- 2. Testing Observer Pattern ---");
        var service = new NotificationService();
        var alice = new User("Alice");
        var bob = new User("Bob");
        service.Subscribe(alice);
        service.Subscribe(bob);
        service.Notify("New update available!");

        Console.WriteLine("\n--- 3. Testing Factory Method Pattern ---");
        ShapeFactory factory = new CircleFactory();
        Shape shape = factory.CreateShape();
        shape.Draw();
    }
}

public sealed class Logger
{
    private static readonly Logger _instance = new Logger();
    private Logger() { }
    public static Logger Instance
    {
        get { return _instance; }
    }
    public void Log(string message)
    {
        Console.WriteLine($"[LOG]: {message}");
    }
}

public interface IObserver
{
    void Update(string message);
}

public class NotificationService
{
    private readonly List<IObserver> _observers = new List<IObserver>();
    public void Subscribe(IObserver observer)
    {
        _observers.Add(observer);
    }
    public void Unsubscribe(IObserver observer)
    {
        _observers.Remove(observer);
    }
    public void Notify(string message)
    {
        foreach (var observer in _observers)
        {
            observer.Update(message);
        }
    }
}

public class User : IObserver
{
    private readonly string _name;
    public User(string name)
    {
        _name = name;
    }
    public void Update(string message)
    {
        Console.WriteLine($"{_name} has received a notification: {message}");
    }
}

public abstract class Shape
{
    public abstract void Draw();
}

public class Circle : Shape
{
    public override void Draw() => Console.WriteLine("Drawing Circle");
}

public class Square : Shape
{
    public override void Draw() => Console.WriteLine("Drawing Square");
}

public abstract class ShapeFactory
{
    public abstract Shape CreateShape();
}

public class CircleFactory : ShapeFactory
{
    public override Shape CreateShape() => new Circle();
}

public class SquareFactory : ShapeFactory
{
    public override Shape CreateShape() => new Square();
}