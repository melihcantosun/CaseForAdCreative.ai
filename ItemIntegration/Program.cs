using ItemIntegration.Provider.Lock;
using ItemIntegration.Service;
using ItemIntegration.Service.Common;
using ItemIntegration.Service.Provider.Items;
using Microsoft.Extensions.Configuration;

namespace ItemIntegration;

public abstract class Program
{
    public static AppSettings AppSettings = new AppSettings();
    public static async Task Main(string[] args)
    { 
        //Get app settings from 'appsettings.json'
        GetConfiguration();
        if (AppSettings == null || AppSettings.CacheSettings == null) throw new Exception("The service cannot be run. Make sure the content of 'appsettings.json' is correct.");
        List<string> contents = new();
        GenerateData(ref contents);
        var lockProvider = new LockProvider(AppSettings);
        var itemProvider = new ItemProvider(lockProvider);
        var service = new ItemIntegrationService(itemProvider);

        var tasks = new List<Task<Result>>();

        //Sync processes
        Console.WriteLine(service.SaveItem("a").Message);
        Console.WriteLine(service.SaveItem("b").Message);
        Console.WriteLine(service.SaveItem("c").Message);

        //Create async processes
        contents.ForEach(content => tasks.Add(Task.Run(() => service.SaveItem(content))));
        contents.ForEach(content => tasks.Add(Task.Run(() => service.SaveItem(content))));
        contents.ForEach(content => tasks.Add(Task.Run(() => service.SaveItem(content))));

        //Run processes and get results
        var results = await Task.WhenAll(tasks);

        //Print the results one by one
        foreach (var result in results) Console.WriteLine(result.Message);

        //Get all items from service
        service.GetAllItems().ForEach(Console.WriteLine);
        Console.ReadKey();
    }

    static void GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfiguration configuration = builder.Build();
        configuration.GetSection("AppSettings").Bind(AppSettings);
    }

    static void GenerateData(ref List<string> list)
    {
        for (char c = 'a'; c <= 'z'; c++) list.Add(c.ToString());
    }
}