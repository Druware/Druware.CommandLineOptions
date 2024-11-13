using System.Reflection;
using Druware.CommandLineOptions;

namespace CommandLineOptionsSample;

class Program
{
    static void ShowResources()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resources = assembly.GetManifestResourceNames();
        if (resources.Length == 0) return;
        Console.WriteLine($"**DEBUG** Resources in {assembly.FullName}");
        foreach (var resource in resources)
            Console.WriteLine(resource);
    }
    
    static void Main(string[] args)
    {
        // Show the Resources in case needed
        // parse the options
        
        #if DEBUG
        ShowResources();
        #endif
        
        // Parse the options
        var assembly = Assembly.GetExecutingAssembly();
        var options = CommandLineOptions.GetInstance<MyOptions>("CommandLineOptionsSample.Resources.MyOptions.json", assembly);
        options.Parse(args);
        
        // Should we Show the Help? 
        if (options.ShouldShowHelp)
        {
            options.ShowHelp();
            return;
        }
        
        // If Verbose? 
        if (options.IsVerbose)
            Console.WriteLine("Welcome to the Comamnd Line Options Sample.");
        
        // Handle the options
        if (!string.IsNullOrEmpty(options.User))
            Console.WriteLine($"The User is {options.User}.");
        
        if (options.Binary)
            Console.WriteLine($"The Binary option is set.");
        
        if (options.IsVerbose)
            Console.WriteLine("The Command Line Options Sample has completed.");
    }
}