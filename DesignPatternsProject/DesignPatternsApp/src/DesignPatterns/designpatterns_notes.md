GANG OF FOUR
-------------------------

The Gang of Four (GoF) refers to the four authors who wrote the influential book that introduced many of the classic 
software design patterns.

Design Patterns: Elements of Reusable Object-Oriented Software

The four authors are:

Erich Gamma
Richard Helm
Ralph Johnson
John Vlissides

Because there were four of them, they became known as the Gang of Four (GoF).

Why are they important?

Before their book, developers often solved common design problems repeatedly in different ways.

The GoF documented 23 proven design patterns that could be reused across object-oriented software.

The 3 main categories of design patterns are:
----------------------------------------------

Creating objects → Connecting objects → Communicating between objects

Category	       Fundamental Question	                            Simple Meaning
Creational	      How do I create objects?	                        Object creation
Structural	    How do I connect/organize objects?	                Object structure
Behavioral	   How do objects communicate and work together?	    Object interaction

🏗️ 1. Creational Patterns = CREATE

Problem:

How should I create this object?

Instead of blindly doing:

var obj = new SomeClass();

Creational patterns provide better ways of controlling object creation.

Examples: Factory, Abstract Factory, Builder, Singleton, Prototype.

Memory word: CREATE

🧱 2. Structural Patterns = CONNECT

Now the objects already exist.

The question becomes:

How should I connect and organize these classes and objects?

Example:

Object A
   |
   ↓
Object B
   |
   ↓
Object C

Structural patterns help arrange them into a larger system.

Examples: Adapter, Decorator, Facade, Composite, Proxy, Bridge, Flyweight.

Memory word: CONNECT

🤝 3. Behavioral Patterns = COMMUNICATE (i.e TALK)

Now:

Objects are created.
Objects are connected.

The final question is:

How should these objects communicate and divide responsibilities?

Example:

Object A
   |
 sends request
   ↓
Object B
   |
 notifies
   ↓
Object C

Examples: Observer, Strategy, Command, State, Mediator, Chain of Responsibility, Visitor, Iterator, Template Method, Memento, Interpreter.

Memory word: COMMUNICATE

So remember the entire Gang of Four classification as the natural life cycle of objects:

CREATE
   ↓
CONNECT
   ↓
COMMUNICATE

Creational
   ↓
Structural
   ↓
Behavioral

My simplest one-line definition would be:

Creational patterns deal with how objects are created, Structural patterns deal with how objects are connected, 
and Behavioral patterns deal with how objects communicate and share responsibilities.

For your design-pattern notes, I would remember just CREATE → CONNECT → COMMUNICATE.


Examples include:

Creational Patterns
------------------------
Factory Method
Abstract Factory
Builder
Prototype
Singleton


Structural Patterns
-------------------------
Adapter
Bridge
Composite
Decorator
Facade
Flyweight
Proxy


Behavioral Patterns
-----------------------------
Observer
Strategy
State
Command
Mediator
Memento
Iterator
Template Method
Chain of Responsibility
Visitor
Interpreter
Simple Analogy

Think of the GoF book like a cookbook.

Before the cookbook:

Every chef invented recipes from scratch.

After the cookbook:

Chefs recognized common recipes and reused them.

Similarly, the GoF didn't invent object-oriented programming—they identified and documented common solutions to 
recurring software design problems.

Key Idea (One-line Memory Aid)

"The Gang of Four are the four authors who documented the 23 classic object-oriented design patterns that are 
still widely used today."


BEHAVIORAL PATTERNS:(COMMUNICATION)
-------------------------------------

1. Memento pattern

The Memento Pattern is used to save an object's state so it can be restored later, 
without exposing its internal details.

It's like taking a snapshot before making changes.

Analogy: Video Game Save Point
You're playing a game and reach a save point.
The game saves your current progress.
If you lose later, you can load the saved game and continue from that point.

Mapping:

Originator: The game (current progress)
Memento: Saved game file (snapshot)
Caretaker: Save/Load system that stores the snapshot
Key Idea (One-line Memory Aid)

"Take a snapshot now, so you can restore it later."

Real-world Examples
Undo/Redo in a text editor
Game save/load
Version history in a document editor

These all save a previous state so it can be restored later.

---------------------------------------------------------------------------------
2. State pattern

The State Pattern is used when an object's behavior changes based on its current state. 
Instead of using lots of if or switch statements, each state has its own behavior.

Analogy: Traffic Light

A traffic light behaves differently depending on its current state:

🟢 Green → Cars can go.
🟡 Yellow → Slow down.
🔴 Red → Stop.

The traffic light is the same object, but its behavior changes based on its state.

Mapping:

Context: Traffic Light
States: Green, Yellow, Red
Behavior: Go, Slow, Stop
Key Idea (One-line Memory Aid)

"Same object, different behavior depending on its current state."

Real-world Examples
Media Player: Playing, Paused, Stopped
ATM: No Card, Card Inserted, PIN Verified
Document: Draft, Review, Published

The object's behavior changes as its state changes.


-------------------------------------------------------------------------------------
3. Strategy pattern ("INTERFACE" related -> widely used behavioral design pattern)

The Strategy Pattern is used when there are multiple ways to perform the same task, and you want to choose the 
appropriate algorithm or behavior at runtime.

Instead of hardcoding the logic with if/switch statements, each strategy is implemented separately.

Code Example:
A very easy example is payment methods — the task is the same: Pay, but the strategy can change.

// Strategy
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

// Strategy 1
public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using Credit Card");
    }
}

// Strategy 2
public class UpiPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using UPI");
    }
}

// Context
public class PaymentService
{
    public void MakePayment(
        decimal amount,
        IPaymentStrategy paymentStrategy)
    {
        paymentStrategy.Pay(amount);
    }
}

Usage:

var service = new PaymentService();

service.MakePayment(1000, new CreditCardPayment());

service.MakePayment(500, new UpiPayment());
How to remember it
              IPaymentStrategy
                     |
             --------+--------
             |               |
       CreditCard           UPI
             |               |
             --------+--------
                     |
               PaymentService

The task is always:

Pay(amount)

But we can plug in a different strategy at runtime:

new CreditCardPayment()
new UpiPayment()

Without Strategy Pattern, you might have:

if (paymentType == "card")
{
    // card logic
}
else if (paymentType == "upi")
{
    // UPI logic
}

Memory line:

Strategy Pattern = same task, multiple interchangeable ways of doing it.

---------------------------------------------------------------------------
4. Iterator pattern

The Iterator Pattern is used to access each element in a collection one by one without knowing how the collection 
is stored internally.

"Go through a collection one item at a time without knowing its internal structure."

Real-world Examples
foreach loop in C#
Iterating through a List<T>, Array, or Dictionary<TKey, TValue>
Database result sets
Easy C# Connection

When you write:

foreach (var item in list)
{
    Console.WriteLine(item);
}

You're using the Iterator Pattern. The foreach loop uses an iterator behind the scenes to move from one element to 
the next without you worrying about how the collection stores its data.

--------------------------------------------------------------------------
5. Command pattern

The Command Pattern is used to encapsulate a request or action as an object, so it can be passed around, queued, 
logged, or undone.

Instead of the caller performing the action directly, it creates a command object and asks it to execute.

Analogy: Restaurant
A customer places an order.
The waiter writes the order on a ticket (the command).
The chef prepares the food.

The customer doesn't talk directly to the chef. The waiter carries the command.

Mapping:

Client: Customer
Command: Order ticket
Invoker: Waiter
Receiver: Chef
Key Idea (One-line Memory Aid)

"Turn a request into an object so it can be executed later or by someone else."

Real-world Examples
Undo/Redo in editors
Menu or toolbar button clicks
Task queues and job schedulers
Remote controls (each button is a command)
Easy C# Connection

When you click a "Save" button in an application:

The button doesn't save the file itself.
It invokes a SaveCommand, which knows how to perform the save operation.

This keeps the UI separate from the business logic.

---------------------------------------------------------------------------------------------
6. Template pattern

The Template Method Pattern is used when multiple processes follow the same overall steps, but some of the individual
steps are different.

The base class defines the overall workflow (the template), and derived classes customize specific steps.

Analogy: Making Tea vs Coffee

The overall process is the same:

Boil water
Add the main ingredient
Pour into a cup
Serve

The only difference is step 2:

🍵 Tea → Add tea leaves
☕ Coffee → Add coffee powder

The recipe (template) stays the same, but one step changes.

Mapping:

Template: Recipe
Concrete Implementations: Tea recipe, Coffee recipe
Variable Step: Add tea leaves or coffee powder
Key Idea (One-line Memory Aid)

"Same process, but some steps are customized by subclasses."

Real-world Examples
Generating different report types (PDF, Excel)
Data import process for different file formats (CSV, XML)
Game characters following the same turn sequence but performing different actions
--------------------------------------------------------------------------------------------------------------

7. Observer pattern (Example: Asynchronous event driven communication among microservices using a message queue)

The Observer Pattern is used when one object needs to notify multiple other objects whenever something changes.
Instead of the other objects constantly checking for updates, they subscribe once and are automatically notified when
a change happens.
"One object changes → everyone interested is automatically notified."

Code Example:

A simple example to remember is YouTube channel → subscribers.

When a channel uploads a video, all subscribers are notified.

// Observer
public interface ISubscriber
{
    void Update(string message);
}

// Concrete Observer
public class Subscriber : ISubscriber
{
    private string name;

    public Subscriber(string name)
    {
        this.name = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"{name}: {message}");
    }
}

// Subject
public class YouTubeChannel
{
    private List<ISubscriber> subscribers = new();

    public void Subscribe(ISubscriber subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void UploadVideo(string videoName)
    {
        Console.WriteLine($"Uploaded: {videoName}");

        // Notify all observers
        foreach (var subscriber in subscribers)
        {
            subscriber.Update($"New video: {videoName}");
        }
    }
}

Usage:

var channel = new YouTubeChannel();

var abhi = new Subscriber("Abhi");
var john = new Subscriber("John");

channel.Subscribe(abhi);
channel.Subscribe(john);

channel.UploadVideo("C# Design Patterns");

Output:

Uploaded: C# Design Patterns

Abhi: New video: C# Design Patterns
John: New video: C# Design Patterns

The important flow is:

Abhi ──subscribe──┐
                  ↓
John ──subscribe──→ YouTubeChannel
                         |
                    Video uploaded
                         |
                      Notify()
                      /      \
                     ↓        ↓
                   Abhi      John

Memory line:

Observer Pattern = Subscribe once → when something changes → notify all subscribers.

In .NET, events and delegates are a very common way of implementing this same Observer idea.


------------------------------------------------------------------------------------------------------
8. Mediator pattern

The Mediator Pattern is used when multiple objects need to communicate with each other, 
but instead of talking directly, they communicate through a 
central mediator.

This reduces dependencies between objects and keeps them loosely coupled.

Analogy: Air Traffic Control
Multiple airplanes need to take off and land.
Instead of pilots talking directly to every other pilot, all communication goes through the Air Traffic Control (ATC).
The ATC coordinates everything and tells each plane what to do.

Mapping:

Mediator: Air Traffic Control
Colleagues: Airplanes
Communication: Through the ATC, not directly with each other
Key Idea (One-line Memory Aid)

"Many objects communicate through one central coordinator instead of talking directly to each other."

------------------------------------------------------------------------------------------
9. Chain of Responsibility pattern (Example: ASP.NET Core Middleware (each middleware handles or passes the request to the next))

The Chain of Responsibility Pattern is used when multiple objects can handle a request, 
and the request is passed along a chain until one of them handles it.

Since you're connecting this to ASP.NET Core Middleware, a simple request-handler example is easiest to remember.

public abstract class Handler
{
    protected Handler? next;

    public void SetNext(Handler nextHandler)
    {
        next = nextHandler;
    }

    public abstract void Handle(string request);
}

Different handlers:

public class AuthenticationHandler : Handler
{
    public override void Handle(string request)
    {
        Console.WriteLine("Checking authentication...");

        // Pass to next handler
        next?.Handle(request);
    }
}

public class LoggingHandler : Handler
{
    public override void Handle(string request)
    {
        Console.WriteLine("Logging request...");

        // Pass to next handler
        next?.Handle(request);
    }
}

public class RequestHandler : Handler
{
    public override void Handle(string request)
    {
        Console.WriteLine($"Handling: {request}");
    }
}

Build the chain:

var auth = new AuthenticationHandler();
var logging = new LoggingHandler();
var requestHandler = new RequestHandler();

auth.SetNext(logging);
logging.SetNext(requestHandler);

auth.Handle("GET /api/students");

Output:

Checking authentication...
        ↓
Logging request...
        ↓
Handling: GET /api/students
Connection to ASP.NET Core

This is conceptually similar to:

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();

A request flows through a chain/pipeline:

HTTP Request
     ↓
Authentication
     ↓
Authorization
     ↓
Logging
     ↓
Controller

Each middleware can either:

Handle request
     OR
Pass it to the next middleware

For example, authentication/authorization middleware may short-circuit the pipeline when access isn't allowed.

Memory line:

Chain of Responsibility = Request → Handler 1 → Handler 2 → Handler 3, with each handler deciding whether to handle/stop or pass it forward.

------------------------------------------------------------------------------------------------------------
10. Visitor Pattern

The Visitor Pattern lets you add a new operation to a group of existing classes without modifying those classes.

One-line memory trick:
"Keep the objects the same, bring in different visitors to perform different tasks."

🏛️ Analogy: Museum Visitors

Imagine a museum.

The museum has exhibits:
Dinosaur
Painting
Sculpture

These exhibits don't change.

Now different visitors come:

👨‍🏫 Guide → Explains history
🧹 Cleaner → Cleans the exhibit
📸 Photographer → Takes pictures
📝 Inspector → Checks for damage

The same exhibit receives different visitors, each performing a different operation.

The exhibit itself doesn't need to know how to clean or inspect—it simply accepts the visitor.

In Code

Instead of writing:

dinosaur.Clean();
dinosaur.Inspect();
dinosaur.TakePhoto();

You write:

dinosaur.Accept(cleanerVisitor);
dinosaur.Accept(inspectorVisitor);
dinosaur.Accept(photoVisitor);

Each visitor performs its own logic.

Why use Visitor?

Suppose you already have many classes:

Circle
Rectangle
Triangle

Now your manager says:

"Add exporting to PDF."

Instead of modifying all three classes, create:

PdfExportVisitor

Tomorrow:

ExcelExportVisitor

Next month:

AreaCalculationVisitor

The existing shape classes remain unchanged.

Visitor Pattern allows adding new operations to existing object structures without modifying the object classes 
by placing the operation inside separate Visitor classes. 

------------------------------------------------------------------------
11. Interpreter Pattern

The Interpreter Pattern is used to understand and evaluate a language or expression based on a set of grammar rules.

One-line memory trick:
"Given a language, build an interpreter that understands it."

🗣️ Analogy: Language Translator

Imagine you travel to Japan.

You say:

"Hello"

A translator interprets it as:

"こんにちは"

Similarly, if you type:

2 + 3 * 4

An interpreter understands the symbols (2, +, 3, *, 4) according to mathematical grammar and evaluates the result:

14

The interpreter reads, understands, and executes the expression.

Another Programming Analogy

Suppose your application accepts commands like:

MOVE FORWARD
TURN LEFT
MOVE FORWARD

Instead of writing lots of if-else statements, you create an interpreter that understands these commands and executes 
them one by one.

In Code

Input:

ADD 10 20

Interpreter:

Reads ADD
Understands it means addition
Computes:
30
Where is it used?
SQL query parsers
Regular expression engines
Mathematical expression evaluators
Programming language compilers/interpreters
Rule engines
Command/script processors


STRUCTURAL PATTERNS:(CONNECTION)
-------------------------------------   

1. COMPOSITE PATTERN
------------------------
Composite Pattern allows you to treat a single object and a group of objects in the same way.

Analogy: Company Organization

Think of a company:

Company
│
├── Manager
│   ├── Employee
│   └── Employee
│
└── Manager
    ├── Employee
    └── Employee

Both an individual Employee and a Manager with multiple employees can be treated similarly.

For example:

GetSalary()
For an Employee → returns their salary.
For a Manager/Department → calculates the salaries of everyone under it.

The client doesn't need to worry whether it's dealing with one object or a group of objects.

Simple C# idea
public interface IEmployee
{
    void ShowDetails();
}

Individual object (Leaf):
------------------------------
public class Developer : IEmployee
{
    public void ShowDetails()
    {
        Console.WriteLine("Developer");
    }
}

Group of objects (Composite):
-------------------------------
public class Manager : IEmployee
{
    private List<IEmployee> employees = new();

    public void Add(IEmployee employee)
    {
        employees.Add(employee);
    }

    public void ShowDetails()
    {
        foreach(var employee in employees)
        {
            employee.ShowDetails();
        }
    }
}

The key idea is:

Developer ───────┐
                 ├── Both implement IEmployee
Manager ─────────┘
   │
   ├── Developer
   └── Developer

So the easiest way to remember it is:

Composite Pattern = Treat one object and a collection/tree of objects uniformly.


2. ADAPTER PATTERN
--------------------------------------

The Adapter Pattern allows two incompatible classes or interfaces to work together by acting as a bridge between them.

A very simple example is: your application expects Pay(), but a third-party payment service provides MakePayment().

The Adapter translates between them.

1. What your application expects
public interface IPayment
{
    void Pay(decimal amount);
}

Your application works with IPayment.

2. Third-party class with an incompatible interface
public class ThirdPartyPaymentService
{
    public void MakePayment(decimal amount)
    {
        Console.WriteLine($"Third-party payment: {amount}");
    }
}

Problem:

Our App expects        Third-party provides

Pay()        ❌        MakePayment()

3. Create an Adapter
public class PaymentAdapter : IPayment
{
    private readonly ThirdPartyPaymentService service;

    public PaymentAdapter(ThirdPartyPaymentService service)
    {
        this.service = service;
    }

    public void Pay(decimal amount)
    {
        // Translate our call into third-party call
        service.MakePayment(amount);
    }
}

Now:

var thirdPartyService = new ThirdPartyPaymentService();

IPayment payment = new PaymentAdapter(thirdPartyService);

payment.Pay(1000);

The flow is:

Our Application
     |
   Pay(1000)
     ↓
PaymentAdapter
     |
     | translates
     ↓
MakePayment(1000)
     ↓
ThirdPartyPaymentService

So you don't modify your application and you don't modify the third-party library. You put an adapter between them.

Memory line:

Adapter Pattern = "My code speaks A, external code speaks B → Adapter translates A to B."



3. BRIDGE PATTERN
---------------------------------

The Bridge Pattern separates an abstraction from its implementation so that both can evolve independently.

A simple example is Notification + how it is sent.

You may have different notifications:

Alert notification
Reminder notification

And different ways of sending them:

Email
SMS

Instead of creating classes such as EmailAlert, SmsAlert, EmailReminder, SmsReminder, Bridge separates the two dimensions.

1. Implementation side
public interface IMessageSender
{
    void Send(string message);
}

public class EmailSender : IMessageSender
{
    public void Send(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}

public class SmsSender : IMessageSender
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}

2. Abstraction side
public abstract class Notification
{
    protected IMessageSender sender;

    protected Notification(IMessageSender sender)
    {
        this.sender = sender;
    }

    public abstract void Notify(string message);
}

Different notification:

public class AlertNotification : Notification
{
    public AlertNotification(IMessageSender sender)
        : base(sender)
    {
    }

    public override void Notify(string message)
    {
        sender.Send($"ALERT: {message}");
    }
}

3. Use any combination
Notification alert1 =
    new AlertNotification(new EmailSender());

alert1.Notify("Server is down");


Notification alert2 =
    new AlertNotification(new SmsSender());

alert2.Notify("Server is down");

The important structure is:

              Notification
              (Abstraction)
                   |
                   | has-a
                   ↓
             IMessageSender
            (Implementation)
              /         \
           Email         SMS

So tomorrow you can independently add:

Notification types:        Sender types:

Alert                      Email
Reminder                   SMS
Warning                    WhatsApp
Marketing                  Push Notification

without creating a huge inheritance hierarchy for every combination.

Easy way to remember

Bridge = separate WHAT you want to do from HOW you do it.

Here:

WHAT: Notification
HOW: IMessageSender

And the IMessageSender sender field is literally the bridge connecting the abstraction to its implementation.


4. PROXY PATTERN
-------------------

The Proxy Pattern provides a substitute or middleman object that controls access to the real object.

One-line memory trick:
“Instead of accessing the real object directly, go through a proxy.”

🚪 Analogy: Security Guard

Imagine a company CEO’s office.

Visitor → Security Guard → CEO

You cannot directly enter the CEO’s office. The security guard (proxy) first checks:

Who are you?
Do you have permission?
Should you be allowed inside?

If everything is valid, the guard allows you to meet the CEO.

Similarly, in programming:

Client → Proxy → Real Object

The Proxy can perform authentication, logging, caching, lazy loading, etc., before accessing the real object.

Programming Example

Suppose you have:

Client → BankAccountProxy → RealBankAccount

When you call:

account.Withdraw(5000);

The Proxy may:

1. Check user permission
2. Log the transaction
3. Call the real BankAccount

An API Gateway can act as a Proxy in many scenarios.

Simple Example
Client → API Gateway → Microservice

Instead of the client directly accessing the microservice:

Client → Order Service

the client accesses it through the API Gateway:

Client → API Gateway (Proxy) → Order Service

The API Gateway can perform:

Authentication
Authorization
Rate limiting
Logging
Caching
Routing

and then forward the request to the actual microservice.

Analogy

Think of a hotel receptionist:

Guest → Receptionist → Hotel Service

You don't directly go into the hotel's internal departments. The receptionist receives your request and directs it 
to the appropriate service.

Similarly:

Client
   ↓
API Gateway  ← Proxy
   ↓
Microservice
Important distinction

An API Gateway can behave like a Proxy, but an API Gateway is usually more than just a Proxy.

Proxy
   ↓
Main purpose: Control/intermediate access

API Gateway
   ↓
Proxy + Routing + Authentication + Rate Limiting + Aggregation + etc.
Easy way to remember

An API Gateway is like an advanced Proxy specifically designed to manage access to backend APIs and microservices.


5. FLYWEIGHT PATTERN
------------------------

Flyweight Pattern saves memory by sharing common data between many similar objects instead of storing the same 
data repeatedly.

Analogy: Video Game Trees 🌳

Imagine a video game with 10,000 trees.

Each tree has:

Tree
├── Texture
├── Model
├── Color
├── Position X
└── Position Y

The Texture, Model, and Color may be identical for thousands of trees.

Without Flyweight:

Tree 1 → Texture + Model + Color + Position
Tree 2 → Texture + Model + Color + Position
Tree 3 → Texture + Model + Color + Position
...
Tree 10,000 → Texture + Model + Color + Position

You're unnecessarily storing the same heavy data thousands of times.

With Flyweight:

                 Shared Tree Data
                Texture + Model + Color
                         ↑
              ┌──────────┼──────────┐
              │          │          │
            Tree 1     Tree 2     Tree 3
             X,Y        X,Y        X,Y

Now all trees share the common heavy data, while each tree stores only its unique position.

The key idea
COMMON DATA → SHARE IT

UNIQUE DATA → STORE SEPARATELY

In design-pattern terminology:

Intrinsic state = shared/common data, such as tree texture and model.
Extrinsic state = unique data, such as each tree's X and Y position.
Easy way to remember

Flyweight = When you have a huge number of similar objects, save memory by sharing their common data.

For your design-pattern notes:

Flyweight Pattern: 
Shares common data among many similar objects to reduce memory usage. Analogy: Thousands of trees in a video game 
share the same texture and model, 
while each stores only its unique position.


6. FACADE PATTERN
------------------------

Facade Pattern provides one simple interface to interact with a complex system containing many classes.

Facade - Structural design pattern

Say we have a scenario like below:
inventoryservice - checkItem()
orderservice - orderItem()
paymentservice - paymentItem()

Instead of calling above individually, we will have a a common layer above them which does all this work.

Like Below:
public class BuyItemFacade
{
    private InventoryService inventoryService = new InventoryService();
    private OrderService orderService = new OrderService();
    private PaymentService paymentService = new PaymentService();

    public void BuyItem()
    {
        inventoryService.CheckItem();
        orderService.OrderItem();
        paymentService.PaymentItem();
    }

}

Usage:
------------

BuyItemFacade bif = new BuyItemFacade();
bif.BuyItem();


So:

Client
   ↓
OrderFacade
   ↓
────────────────────────
↓           ↓          ↓
Inventory  Payment   Shipping
Easy way to remember

Facade = One simple front door to a complex system.

For your notes:

Facade Pattern: Provides a simplified interface to a complex subsystem. Analogy: A hotel reception desk acts as 
one point of contact instead of making guests interact with every hotel department individually.


7. DECORATOR PATTERN
-----------------------------

Decorator Pattern means adding new features or responsibilities to an object dynamically without 
changing its original class.

☕ Analogy: Coffee Shop

You start with a basic coffee.

Then you can decorate it with:

Coffee + Milk
Coffee + Sugar
Coffee + Whipped Cream

Each addition adds extra behavior/cost, but the original Coffee class remains unchanged.

In Programming
Basic Coffee
     ↓
Milk Decorator
     ↓
Sugar Decorator
     ↓
Whipped Cream Decorator

Each decorator wraps the previous object and adds something extra.

Simple Definition to Remember

Decorator Pattern = Wrap an object to add extra functionality without modifying its original class.

Key idea: Wrapping instead of inheritance.


CREATIONAL PATTERNS:(CREATION)
-------------------------------------   
 
1. PROTOTYPE PATTERN
----------------------- 

The Prototype Pattern creates new objects by copying (cloning) an existing object instead of creating them 
from scratch.

Analogy:
Think of a photocopy machine 📄.

You already have one completed document. Instead of writing the same document again from scratch, 
you simply make a copy and modify it if needed.

In programming:

Existing Object → Clone → New Object

Key idea:

Prototype Pattern = Create new objects by cloning existing objects.


2. SINGLETON PATTERN
-----------------------

The Singleton Pattern ensures that a class has only one object (instance) throughout 
the application and provides a common way to access it.

Analogy:
Think of the President of a country.

At a given time, there is only one President, and everyone who needs to communicate with the President 
refers to that same person.

In programming:

Application → One Shared Instance

Key idea:

Singleton Pattern = Create only one instance of a class and share it everywhere.

Singleton Pattern – Database Configuration Example

Suppose your application reads the database connection settings from appsettings.json.

You don't need to create and load a new configuration object every time a service needs the database settings. 
Instead, the application can create one configuration instance and share it throughout the application.

appsettings.json → One Configuration Instance → Used by Multiple Services

Analogy: Think of an office notice board. There is only one central notice board, and all employees refer to the 
same board for information.

Singleton = One shared instance used throughout the application.



3. FACTORY PATTERN
-----------------------

"Don't create the object yourself; ask the factory to create the appropriate object."

Factory - Creational Design pattern

public class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal Speaks");
    }
}

public class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Boww");
    }
}

public class Cat : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Meoww");
    }
}

public class AnimalFactory
{
    public virtual Animal CreateAnimal() // Factory Method
    {
        return new Animal();
    }
}

public class DogFactory : AnimalFactory
{
    public override Animal CreateAnimal(). // Factory Method
    {
        return new Dog();
    }
}

Usage
----------

AnimalFactory af = new DogFactory();
af.CreateAnimal().Speak(); // Boww

AnimalFactory af = new DogFactory();
af.CreateAnimal().Speak(); // Boww

Here is what happens:

af refers to a DogFactory object.
CreateAnimal() executes the override in DogFactory, returning a Dog.
Speak() executes the override in Dog, printing "Boww".

Your usage is equivalent to:

AnimalFactory af = new DogFactory();

Animal animal = af.CreateAnimal();
animal.Speak(); // Boww

You also defined Cat. To support creating it through its own factory, add:

public class CatFactory : AnimalFactory
{
    public override Animal CreateAnimal()
    {
        return new Cat();
    }
}

Then:

AnimalFactory af = new CatFactory();
af.CreateAnimal().Speak(); // Meoww

The key: CreateAnimal() is the Factory Method. Each factory subclass overrides it to decide which concrete animal to create.

4. ABSTRACT FACTORY PATTERN
-----------------------------

The Abstract Factory Pattern creates families of related objects without the client directly creating the specific 
objects.

Analogy:
Think of a furniture factory 🪑

You can choose:

Modern Furniture Factory style → Creates Modern Chair + Modern Table
Old Furniture Factory style → Creates Old Chair + Old Table

The objects created belong to the same family/style.

Modern Factory
   → Modern Chair
   → Modern Table

Victorian Factory
   → OldStyle Chair
   → OldStyle Table

Key idea:

Abstract Factory = Create styles/families of related objects without directly using new.

Difference from Factory Method:

Factory Method → Creates one type of object
Abstract Factory → Creates a family of related objects


5. BUILDER PATTERN
-----------------------
The Builder Pattern creates a complex object step by step instead of creating everything at once.

Analogy:
Think of building a custom burger 🍔.

You create it step by step:

Add Bun → Add Patty → Add Cheese → Add Sauce → Final Burger

You can use the same building process to create different burgers by choosing different ingredients.

In programming:

Builder → Step 1 → Step 2 → Step 3 → Final Object

Key idea:

Builder Pattern = Construct a complex object step by step.

