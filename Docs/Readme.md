# Using Druware.CommandLineOptions

The Druware.CommandLineOptions project is a simple tool for quickly creating, supporting
and managing a parsing and processing object for the command line options your program 
supports. 

By default, the engine supports a base two options:

```
/? or --help
    Show the help, optionally for the {Command}
/v or --verbose
    Print the progress to the screen as the process runs
```

Your own options are added to the CommandLineOptions.json resource file ( a sample is
provided in the samples folder and the full syntax for the json file is in
[Resource Syntax](Resource-Syntax.md) )

## Getting Started

The following section will help you get started with the command line options tools
and build your application to support it.

### Install the Package

_Using the Manage Packages option:_
* Search Druware.CommandLineOptions
* Add to the project

_Powershell_
```
NuGet\Install-Package Druware.CommandLineOptions -Version 0.5.0
```

### Create you Options Resource

* In your project, create a Resources folder ( if one does not already exist )
* Create a new .json file in the Resources folder
* Add the basic CommandLineOptions to your new .json

```
{
  "name": "MyNiftyProgram",
  "description": "A program that I wrote that does something",
  "options":[
    {
      "name":"User",
      "description": "Sets the user name",
      "expectsVariable": true,
      "commands": [
        "/U",
        "--user"
      ]
    },    
    {
      "name":"Binary",
      "description": "Boolean item ( not present is equal to false )",
      "expectsVariable": false,
      "commands": [
        "/B",
        "--binary"
      ]
    },
   ]
}
```

### Include your Resource in the Project

Edit your project file, .csproj or other to include the Resource you added:

```
    <ItemGroup>
        <EmbeddedResource Include="Resources\MyOptions.json" />
    </ItemGroup>
```

### Create your CommandLineOptions subclass

Add a new class to your project, the example is in C#.

```
public class MyOptions : CommandLineOptions
{
    public Options() { }

    public Options(string[] args) { }

    public string? User => GetOption("User")?.Value ?? "";
    public bool Binary => GetOption("Binary")?.Selected ?? false;

}
```

### Parse your Options

In your programs startup, add the things required to initiate the parse.

```
        Assembly = Assembly.GetExecutingAssembly();
        Options = CommandLineOptions.GetInstance<Options>("MyProgram.Resources.MyOptions.json");
        Options.Parse(args);
```

### Using your Options

With that done, when you build the application, you should be able to reference the Options.Property
to fetch teh value of the property ( or it's default ) if there is no corresponding value. 

## Future Items to Add

### Add a Required Option

Any option with the 'Required' flag set true will trigger a failure if the parse fails to find it

### Add a full sample program

Create a simple sample that shows the features and how they work, as well as a couple of handy 
debug tools.

## Change Log / History {collapsible="true"}

### Version 0.5.0 - 2024.11.04

Initial release, absent example or documentation. Just enough for us to confirm usability 
and viability removed from our internal use package. 


