Start with the thing you already know

Imagine:

public class PageService
{
    private readonly IPageRepository _repository;

    public PageService(IPageRepository repository)
    {
        _repository = repository;
    }
}

PageService is saying:

"I need an IPageRepository to work. Someone else please give me one."

That "someone else" is the DI container.

In ASP.NET Core you typically register:

builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IPageService, PageService>();

Then when ASP.NET needs a PageService, conceptually it does this:

I need PageService
       ↓
Look at PageService constructor
       ↓
It needs IPageRepository
       ↓
Look in registrations:
IPageRepository → PageRepository
       ↓
Create PageRepository
       ↓
Create PageService(PageRepository)
       ↓
Return PageService

What the presenter does is build his own crude version of that mechanism.

1. First file: activator.linq

The first example is showing a basic .NET feature called Activator.CreateInstance.

His classes are essentially:

public class ServiceConsumer
{
    private HelloService _hello;

    public ServiceConsumer(HelloService hello)
    {
        _hello = hello;
    }
}

and:

public class HelloService
{
    public void Print()
    {
        Console.WriteLine("Hello World");
    }
}

Normally you write:

var service = new HelloService();

var consumer = new ServiceConsumer(service);

You, the developer, explicitly create everything.

But he replaces new with:

var service =
    (HelloService)Activator.CreateInstance(typeof(HelloService));

var consumer =
    (ServiceConsumer)Activator.CreateInstance(
        typeof(ServiceConsumer),
        service);

The repository contains exactly this progression.

Activator.CreateInstance() basically means:

"Here is a Type. Create an object of that type for me at runtime."

So:

Activator.CreateInstance(typeof(HelloService))

is roughly equivalent to:

new HelloService()

Why bother?

Because a DI container cannot have hardcoded code like:

new PageService(...)
new GalleryService(...)
new StudentService(...)
new EmailService(...)

It needs a generic mechanism capable of constructing arbitrary classes.

That's where reflection + Activator come in.

2. Now imagine the dependency chain gets deeper

The presenter then changes the classes into something like:

ServiceConsumer
      ↓ needs
HelloService
      ↓ needs
MessageService

In code:

public class ServiceConsumer
{
    public ServiceConsumer(HelloService hello)
    {
    }
}
public class HelloService
{
    public HelloService(MessageService message)
    {
    }
}
public class MessageService
{
}

Now suppose I ask:

resolver.GetService<ServiceConsumer>();

What needs to happen?

Create ServiceConsumer
       │
       └── Wait — constructor needs HelloService
                           │
                           └── Create HelloService
                                      │
                                      └── Wait — constructor needs MessageService
                                                        │
                                                        └── Create MessageService

Then construction happens from the bottom back upward:

new MessageService()
        ↓
new HelloService(messageService)
        ↓
new ServiceConsumer(helloService)

That recursive dependency resolution is the most important "under the hood" concept in the whole example.

3. DependencyContainer is basically the registration list

In resolver_container.linq, he creates:

var container = new DependencyContainer();

container.AddDependency(typeof(HelloService));
container.AddDependency<ServiceConsumer>();
container.AddDependency<MessageService>();

The container stores the registered types.

So conceptually:

DEPENDENCY CONTAINER

Registered services
-------------------------
ServiceConsumer
HelloService
MessageService

Think of this as an extremely primitive version of:

builder.Services.AddScoped<ServiceConsumer>();
builder.Services.AddScoped<HelloService>();
builder.Services.AddScoped<MessageService>();

ASP.NET Core's real implementation is obviously much more sophisticated, but the concept is identical.

Container vs Resolver

This distinction is useful.

The container remembers:

"What services are available?"

The resolver figures out:

"How do I actually build the service being requested?"

So:

DependencyContainer
        ↓
registration information

DependencyResolver
        ↓
object construction

4. This part of his resolver is the real magic

Look at this:

var constructor = dependency.GetConstructors().Single();

var parameters = constructor.GetParameters().ToArray();

He is using reflection.

Suppose dependency is:

typeof(ServiceConsumer)

.NET inspects:

public ServiceConsumer(HelloService hello)

and discovers:

Constructor parameter:
HelloService

So the DI resolver effectively asks:

"What does this class need in order for me to create it?"

For ServiceConsumer, answer:

HelloService

For HelloService, answer:

MessageService

For MessageService, answer:

nothing

The repository's resolver performs this inspection using GetConstructors() and GetParameters().

5. Then this line performs recursion

This is probably the most important line in his custom DI implementation:

parameterImplementations[i] =
    GetService(parameters[i].ParameterType);

Notice that GetService() calls itself.

Say we started with:

GetService(typeof(ServiceConsumer))

The resolver discovers:

ServiceConsumer needs HelloService

Therefore it calls:

GetService(typeof(HelloService))

But now that call discovers:

HelloService needs MessageService

So it calls:

GetService(typeof(MessageService))

MessageService doesn't need anything.

Therefore:

Activator.CreateInstance(typeof(MessageService))

creates it.

Now recursion unwinds.

GetService(ServiceConsumer)

    GetService(HelloService)

        GetService(MessageService)

            new MessageService()
                 ↑

        new HelloService(messageService)
                 ↑

    new ServiceConsumer(helloService)

That's dependency injection under the hood in its simplest form.

6. Why is this called Dependency Injection?

Because ServiceConsumer does not do this:

public class ServiceConsumer
{
    private HelloService _hello = new HelloService();
}

That class would be constructing its own dependency.

Instead:

public ServiceConsumer(HelloService hello)
{
    _hello = hello;
}

The dependency comes from outside the class.

It's injected.

Without DI

ServiceConsumer
      │
      └── creates HelloService itself


With DI

DI Container
      │
      ├── creates HelloService
      │
      ▼
ServiceConsumer

That's why the phrase dependency injection is actually very literal.

A dependency that the class requires is injected into it from outside.

7. Then the presenter introduces lifetimes

The third file expands the homemade container to support:

container.AddTransient<ServiceConsumer>();
container.AddTransient<HelloService>();

container.AddSingleton<MessageService>();

Now he's mimicking ASP.NET Core's:

AddTransient()
AddSingleton()

The classes generate random numbers so you can tell whether an object was recreated or reused.

Suppose:

ServiceConsumer = Transient
HelloService    = Transient
MessageService  = Singleton

Every call:

resolver.GetService<ServiceConsumer>();

means:

Request #1

new ServiceConsumer
      ↓
new HelloService
      ↓
new MessageService     ← first time created


Request #2

new ServiceConsumer
      ↓
new HelloService
      ↓
same MessageService    ← reused


Request #3

new ServiceConsumer
      ↓
new HelloService
      ↓
same MessageService

That's exactly what he's demonstrating with the random values.

8. How his homemade Singleton works

His Dependency class contains something like:

public object Implementation { get; set; }

public bool Implemented { get; set; }

Then:

if (dependency.Implemented)
{
    return dependency.Implementation;
}

If it hasn't been created yet:

var implementation = factory(dependency.Type);

And for a singleton:

if (dependency.Lifetime == DependencyLifetime.Singleton)
{
    dependency.AddImplementation(implementation);
}

So the homemade container is basically saying:

Someone asks for MessageService

Do I already have one?
       │
       ├── YES → return existing object
       │
       └── NO  → create one
                  save it
                  return it

That's the essence of Singleton DI lifetime.

9. Transient is even simpler

For transient:

Someone asks for HelloService
          ↓
Always create a new HelloService
          ↓
Return it

No previously created instance is stored.

That's why:

AddTransient<HelloService>()

means:

Give me a new HelloService whenever it needs to be resolved.

10. One major thing his sample doesn't really cover: Scoped

ASP.NET Core commonly gives you three:

AddTransient
AddScoped
AddSingleton

His example mainly builds Transient and Singleton.

For a Web API, understand them this way:

TRANSIENT
New object every time requested.

SCOPED
One object per HTTP request.

SINGLETON
One object for the application lifetime.

Suppose one API call comes in:

GET /api/pages/about

and several services require the same scoped FirstGenDbContext.

ASP.NET might conceptually do:

HTTP Request #1
│
├── PageController
│      ↓
├── PageService
│      ↓
├── PageRepository
│      ↓
└── FirstGenDbContext #ABC

Everyone within that HTTP request gets the same scoped DbContext.

Next API request:

HTTP Request #2
│
└── FirstGenDbContext #XYZ

A new scope, therefore a new instance.

11. Now connect the video directly to ASP.NET Core

When you write:

builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IPageRepository, PageRepository>();

you can mentally picture:

DI CONTAINER REGISTRATION

IPageService
    → PageService
    → Scoped

IPageRepository
    → PageRepository
    → Scoped

Later ASP.NET encounters:

public PageController(IPageService pageService)
{
    _pageService = pageService;
}

Then internally the conceptual flow is:

Need PageController
       ↓
Inspect constructor
       ↓
Needs IPageService
       ↓
Search DI registrations
       ↓
IPageService → PageService
       ↓
Inspect PageService constructor
       ↓
Needs IPageRepository
       ↓
Search DI registrations
       ↓
IPageRepository → PageRepository
       ↓
Inspect PageRepository constructor
       ↓
Needs FirstGenDbContext
       ↓
Resolve FirstGenDbContext
       ↓
Create PageRepository(dbContext)
       ↓
Create PageService(repository)
       ↓
Create PageController(pageService)

And now you can see why your controllers/services seem to magically receive objects in their constructors.

There isn't actually magic.

The framework has a sophisticated version of the resolver that this presenter is building.

The video in one picture

I would compress the entire tutorial into this:

                    REGISTRATION
                        ↓
          ┌──────────────────────────┐
          │       DI CONTAINER       │
          │                          │
          │ HelloService  Transient  │
          │ MessageService Singleton │
          │ ServiceConsumer Transient│
          └────────────┬─────────────┘
                       │
                       │ GetService<ServiceConsumer>()
                       ↓
                  DI RESOLVER
                       │
              inspect constructor
                       ↓
               ServiceConsumer
                       │
                  needs ↓
                 HelloService
                       │
                  needs ↓
                MessageService
                       │
                       ↓
             create dependencies
                       │
                       ↑
       new MessageService()
                       │
       new HelloService(message)
                       │
       new ServiceConsumer(hello)
                       │
                       ▼
               return object

Understand these four ideas:

Registration — tell the container which implementations/services exist.
Reflection — the container can inspect constructors to discover what a class needs.
Recursive resolution — if A needs B and B needs C, the container resolves C → B → A.
Lifetime management — the container decides whether to create a new instance or reuse one.

So when you see your normal ASP.NET Core code:

builder.Services.AddScoped<IPageService, PageService>();

you can now mentally translate it into:

"DI container, remember this mapping. If somebody asks for IPageService, figure out how to construct PageService, recursively construct everything its constructor requires, respect the scoped lifetime, and inject the resulting object."

Dependency injection is the design technique; "the DI container is the machinery that automates it". You can do DI manually without a container; ASP.NET Core gives you a container so you don't have to manually wire hundreds of objects together.