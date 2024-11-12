# Druware.CommandLineOptions

A C# implementation of a command line option parser and processor. The summary 
view of how it is implemented is pretty straight forward. The options themselves
are defined in a single json resource file that is embedded into the 
project.  During load, it reads that assembly, and a very thin veneer subclass 
of the CommandLineOptions class becomes the program interface for it.

At this time, this very initial release, we do not yet have samples, or full 
documentation, but the source is available here on Github, and the package is 
available on Nuget.  Over the next weeks we will add the samples, and the 
documentation.

## Getting Started 

Installation via nuget: 

```
NuGet\Install-Package Druware.CommandLineOptions -Version 0.5.0
```

## Documentation 

We are using the Github Wiki for documentation, though most of the content is 
also being mastered in the /Docs/ folder as markdown files.

https://github.com/Druware/Druware.CommandLineOptions/wiki

## History

### 0.5.0 - 2024/11/04

Initial publish, without much information, but fully functional. Sample code, 
documentation and additional resources to be completed.