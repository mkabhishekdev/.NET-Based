SOLID PRINCIPLES
------------------

S: Single Responsibility Principle(SRP)
- A class should have only one reason to change, meaning that it should have only one responsibility or 
purpose.
A class should do one job and do it well.

Bad Example (Violates SRP)
public class Invoice
{
    public void CalculateTotal() { }

    public void PrintInvoice() { }

    public void SaveToDatabase() { }
}

This class has three responsibilities:

Calculate invoice
Print invoice
Save invoice

If printing changes, the class changes.
If database logic changes, the class changes.

It has multiple reasons to change, violating SRP.

Good Example (Follows SRP)
public class Invoice
{
    public void CalculateTotal() { }
}

public class InvoicePrinter
{
    public void Print() { }
}

public class InvoiceRepository
{
    public void Save() { }
}

Now each class has one responsibility.

Interview One-Liner

The Single Responsibility Principle states that a class should have only one responsibility, meaning it should have 
only one reason to change.

--------------------------------------------------------

O: Open Closed Principle(OCP)
- Software entities(classes, modules, functions etc) should be open for extension but closed for modification.


----------------------------------------------------------

L: Liskov Substitution Principle(LSP)
- Objects of a superclass should be replaceable with objects of its subclass without affecting the correctness of 
the program.
LSP states that a child class should be able to replace its parent class without changing the correctness of the program.

Good Example:

public class Animal
{
    public virtual void Eat()
    {
        Console.WriteLine("animal is eating");
    }
}

public class Dog : Animal
{
    public override void Eat()
    {
        Console.WriteLine("guddu is eating");
    }
}

Animal a = new Dog();
a.eat() // guddu is eating

Bad Example:

public class LivingBeing
{
    public virtual void Fly()
    {
        Console.WriteLine("LivingBeing is Flying");
    }
}

public class Human : LivingBeing
{
    public override void Fly()
    {
        throw new NotImplementedException();
    }
}

LivingBeing lb = new Human();
lb.Fly(); //“A Human object cannot safely substitute for a LivingBeing object because callers expect Fly() to work, but Human.Fly() throws.”
The assignment itself is valid C#. LSP is about behavioral correctness, not just whether the code compiles.

The underlying design problem is putting Fly() on LivingBeing: flying is not a capability shared by all living beings. A better design separates that capability:

public class LivingBeing
{
    public virtual void Eat()
    {
        Console.WriteLine("Living being is eating");
    }
}

public interface IFlyable
{
    void Fly();
}

public class Human : LivingBeing
{
    // Inherits Eat().
}

public class Bird : LivingBeing, IFlyable
{
    public void Fly()
    {
        Console.WriteLine("Bird is flying");
    }
}

Now code that needs eating accepts LivingBeing; code that needs flying accepts IFlyable.

One nuance: throwing an exception in an override does not automatically violate LSP. It violates LSP when it breaks the base class’s contract—as your example does by rejecting an operation the base class promises to support.

Remember: inheritance establishes the relationship; LSP requires the child to keep the parent’s promises.

The Liskov Substitution Principle states that a derived class should be substitutable for its base class without altering the correctness or expected behavior of the program.

------------------------------------------------------------

I: Interface Segregation Principle(ISP)
- Clients should not be forced to depend on interfaces they do not use.
ISP states that a class should not be forced to implement methods it doesn't need.

Create small, specific interfaces instead of one large interface.

Classes should implement interfaces which they use and not be forced to use interfaces which they do not completely implement, rather segregate the interfaces based on the need

Clients should not be forced to depend on methods they do not use. Split interfaces into focused contracts so classes depend only on what they need.

“Clients” includes code that consumes an interface—not just classes implementing it.

Bad Example:

public interface IRoutineTasks
{
    public void Eat();
    public void Work();
}

public class Human : IRoutineTasks
{
    public void Eat()
    {
        Console.WriteLine("Human is eating");
    }
 
    public void Work()
    {
        Console.WriteLine("Human is working");
    }
}

public class Robot : IRoutineTasks
{
    public void Work()
    {
        Console.WriteLine("Robot is working");
    }

    public void Eat()
    {
        throw new NotImplementedException();
    }
}

Good Example:

public interface IEater
{
    public void Eat();
}

public interface IWorker
{
   public void Work();
}

public class Human : IEater, IWorker
{
    public void Eat()
    {
        Console.WriteLine("Human is eating");
    }
 
    public void Work()
    {
        Console.WriteLine("Human is working");
    }
}

public class Robot : IWorker
{
     public void Work()
    {
        Console.WriteLine("Robot is working");
    }
}

The split also helps consuming code. For example, a method that starts work only needs IWorkable:

public static void StartWork(IWorkable worker)
{
    worker.Work();
}

StartWork(new Human()); // Human is working
StartWork(new Robot()); // Robot is working

StartWork() has no reason to depend on eating, and your design reflects that.

The Interface Segregation Principle states that clients should not be forced to depend on or implement methods they do not use. 
It encourages creating small, focused interfaces rather than one large interface.

---------------------------------------------------------------

D: Dependency Inversion Principle(DIP)
- High level modules should not depend on low level modules. Both should depend on abstractions(i.e interfaces
and not concrete implementations).
- The concept of 'Dependency Injection'
       * promotes loose coupling by removing direct dependencies
       * so we depend on abstractions and not on concrete implementations -> which helps with maintainability
       * DI simplifies unit testing by allowing components to be replaced with mock implementations

