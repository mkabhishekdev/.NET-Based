using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        
    }

    public async Task<string> InputOutputN()
    {
        var client = new HttpClient();
        var content = await client.GetStringAsync("my site");

        return content;
    }

    public Task<string> InputOutputNA()
    {
        var client = new HttpClient();
        return client.GetStringAsync("my site");
    }
}