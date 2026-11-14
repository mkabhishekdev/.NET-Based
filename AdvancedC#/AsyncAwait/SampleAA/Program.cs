using System.Threading.Tasks;

public class Program
{
    public static async Task Main()
    {
        Program p = new Program();
        await p.MakeTeaAsync();
    }

    public async Task MakeTeaAsync()
    {
        Task receiveTheBoiledWater = BoilWaterAsync(); /* the return of this Method should be assigned to a Task since we need to 
                                                        to refer back to this AsynMethod after 10s down the line */
        
        Console.WriteLine("Take the cups");
        Console.WriteLine("Pour Tea and Sugar");

        await receiveTheBoiledWater; /* I will wait until I receive back(since next step needs this), 
                                             that receive back is through the task in line13  */

        Console.WriteLine("Add the boiling water");
        Console.WriteLine("Tea is ready");
    }

    public async Task BoilWaterAsync()
    {
        Console.WriteLine("Start the kettle");
        Console.WriteLine("Pour water");
       
        await Task.Delay(10000);    //'MakeTea' thread gets free and this will be returned in another thread after 10s
        /* In Synchronous waiting, the above is accomplished by doing Thread.Sleep(10000) wherein in this case the
        'Thread' is kept on hold but in the case of async the 'Thread' is freed and later after completion of the 
          process, another thread would return back the result to the calling method */
        Console.WriteLine("Boiling water is ready");
    }
}

/*
CODE WITHOUT ASYNC/AWAIT

 public static void Main()
    {
        Program p = new Program();
        p.MakeTea();
    }

    public void MakeTea()
    {
        BoilWater();
        
        Console.WriteLine("Take the cups");
        Console.WriteLine("Pour Tea and Sugar");
        Console.WriteLine("Add the boiling water");
        Console.WriteLine("Tea is ready");
    }

    public void BoilWater()
    {
        Console.WriteLine("Start the kettle");
        Console.WriteLine("Pour water");
        Task.Delay(2000);
        Console.WriteLine("Boiling water is ready");
    }

*/