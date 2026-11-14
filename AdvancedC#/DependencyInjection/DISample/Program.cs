public class Program
{
    public static void Main()
    {
       /* var service = new HelloService();
        var consumer = new ServiceConsumer(service); */

        var container = new DependencyContainer();
        container.AddDependency(typeof(HelloService));
        container.AddDependency<ServiceConsumer>();

        var a = Console.WriteLine(typeof(HelloService));
        var service = Activator.CreateInstance(typeof(HelloService));
        var consumer = Activator.CreateInstance(typeof(ServiceConsumer), service);

        service.Print();
        consumer.Print();
    }
    
    public class DependencyResolver
    {
        DependencyContainer _container;
        public DependencyResolver(DependencyContainer container)
        {
            _container = container;
        }    
    }

    public class DependencyContainer
    {
        List<type> _dependencies;

        public void AddDependency(Type type)
        {
            _dependencies = new List<type>();
            _dependencies.Add(type);
        }    
        
        public void AddDependency<T>()
        {
            _dependencies.Add(typeof(T));    
        }

        public Type GetDependency(Type type)
        {
            return _dependencies.First(x => x.Name == type.Name);
        }
    }

    public class ServiceConsumer
    {
        HelloService _hello;
        
        public ServiceConsumer(HelloService hello)
        {
            _hello = hello;
        }

        public void Print()
        {
            _hello.Print();
        }
    }

    public class HelloService()
    {
        public void Print()
        {
            Console.WriteLine("Hello User");
        }
    }
}