
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Druware.CommandLineOptions;
public class CommandLineOptions
{
    public static T GetInstance<T>(string resourceName, Assembly? forAssembly = null) where T : CommandLineOptions
    {
        var assembly = forAssembly ?? Assembly.GetExecutingAssembly();

        // read the options
        var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            Console.WriteLine("Resource not found.");
            throw new Exception("Resource not found.");
        }
        var task = Task.Run(async() => await JsonSerializer.DeserializeAsync<T>(stream));
        var options = task.Result;
        stream.Close();

        if (options == null) throw new Exception("No Options Found");
        
        return options;
    }

    public void Parse(string[] args)
    {
        var i = 0;
        foreach (var arg in args)
        {
            #if DEBUG 
            Console.WriteLine("Argument: {0}", arg);
            #endif

            switch (arg)
            {
                case "/?" or "--help":
                    ShouldShowHelp = true;
                    // if there is another parameter use it for the help detail
                    // otherwise, use showAllHelp.
                    if (i < args.Length - 1)
                        HelpForCommand = args[i + 1];
                    break;
                case "/v" or "--verbose":
                    IsVerbose = true;
                    break;
            }

            // parse the 'options' key
            if (Options == null) continue;

            foreach (var opt in Options)
            {
                if (!opt.Commands!.Contains(arg)) continue;
                opt.Selected = true;
                if (!opt.ExpectsVariable) continue;
                if (i < args.Length - 1)
                    opt.Value = args[i + 1];
            }
            i++;
        }
    }

    protected CommandLineOption? GetOption(string named) =>
        Options == null ? null : Options!.FirstOrDefault(option => option.Name == named);

    private string? HelpForCommand { get; set; }

    private string SplitDescription(string description)
    {
        if (description.Length < 80) return description;
        var result = "";
        var words = description.Split(' ');
        var lines = new List<string>();
        
        foreach (var word in words)
        {
            if (result.Length + word.Length > 80)
            {
                lines.Add(result);
                result = "";
            }
            result += word + " ";
        }
        lines.Add(result);
        result = string.Join("\n", lines);
        result = result.Substring(0, result.Length - 1);
        return result.TrimEnd();
    }
    private string SplitOptDescription(string description)
    {
        if (description.Length < 45) return description;
        var result = "";
        var words = description.Split(' ');
        var lines = new List<string>();
        
        foreach (var word in words)
        {
            if (result.Length + word.Length > 45)
            {
                lines.Add(result);
                result = "";
            }
            result += word + " ";
        }
        lines.Add(result);
        result = string.Join("\n".PadRight(36), lines);
        result = result.Substring(0, result.Length - 37);
        return result.TrimEnd();
    }
    
    public void ShowHelp()
    {
        Console.WriteLine(Name);
        Console.WriteLine(SplitDescription(Description ?? ""));
        Console.WriteLine("");
        if (HelpForCommand == null)
        {
            Console.WriteLine(
                "Help                /?, --help     Show the help, optionally for the {Command}");
            Console.WriteLine(
                "Verbose             /v, --verbose  Print the progress to the screen.");
        }

        if (Options == null) return;
        foreach (var opt in Options)
        {
            if (opt.Commands == null) continue;
            if (HelpForCommand != null &&
                !opt.Commands.Contains(HelpForCommand)) continue;
            var cmd = string.Join(", ", opt.Commands);
                
                
            Console.WriteLine($"{opt.Name?.PadRight(20)}{cmd?.PadRight(15)}{SplitOptDescription(opt.Description ?? "")}");
        }
    }
    
    public bool ShouldShowHelp { get; set; }
    public bool IsVerbose { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; } = "";

    [JsonPropertyName("description")]
    public string? Description { get; set; } = "";
    
    [JsonPropertyName("options")]
    public CommandLineOption[]? Options { get; set; }
}


public class CommandLineOption
{
    [JsonPropertyName("name")]
    public string? Name { get; set; } = "";

    [JsonPropertyName("description")]
    public string? Description { get; set; } = "";
    
    [JsonPropertyName("expectsVariable")]
    public bool ExpectsVariable { get; set; } = false;

    [JsonPropertyName("commands")]
    public string[]? Commands { get; set; }

    public bool Selected { get; set; } = false;
    
    public string? Value { get; set; } 
}



