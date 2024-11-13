using Druware.CommandLineOptions;

namespace CommandLineOptionsSample;


public class MyOptions : CommandLineOptions
{
    public MyOptions() { }

    public MyOptions(string[] args) { }

    public string? User => GetOption("User")?.Value ?? "";
    public bool Binary => GetOption("Binary")?.Selected ?? false;

}
