# Resource Syntax

At the root are three possible options:

_name_: 

This is the displayed name when the program displays any of the help options.

_description_:

This is the description displayed below the name when help is requested.

_options_:

This is an array of Options ( see below ), that the program supports. The 
options will be listed if /? or --help is requested, and the program supports
the Options.ShowHelp() method. If the Help is passed with an argument, it will
in turn show the help specific to that command.

The options are an array of possible options:

_name_:

This is the displayed name when the program displays the help list.

_description_:

This is the displayed content when the program displays the help for this option.

_expectsVariable_:

When true, the Value will be set to the value of the passed in variable passed 
after the option. When false, if the option is present the Selected option will 
be set to true.

_required_: 

(not yet implemented) Identifies if the argument is required for the 
program to operate.

_commands_:

An array of strings that are to possible command line arguments to be parsed from
the array of arguments passed in.

## Sample 

```
{
  "name": "CommandLineOptions Sample Program",
  "description": "A simple example showing the implementation and usage of the command line tools.",
  "options":[
    {
      "name":"User",
      "description": "Sets userr",
      "expectsVariable": true,
      "commands": [
        "/U",
        "--user"
      ]
    },
    {
      "name":"Binary",
      "description": "Shows a Bool options",
      "expectsVariable": false,
      "commands": [
        "/B",
        "--binary"
      ]
    }
  ]
}
```