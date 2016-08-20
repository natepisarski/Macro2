# Macro2
Have you ever wanted to write a shell script? Do you have a terminal window open, and you just write down what wins up working in the shell script? That's wasted effort! That's like going to class and taking notes on what the teacher is saying. If you're running the commands, no need to duplicate the effort - save the commands to a script, directly, with Macro2.

# How it works
Macro2 works as a layer between you and your shell. When you enter commands into the Macro2 command prompt, it saves them in Macro2's internal memory, as well as executing them on the shell.

# How to use Macro2
The first step is to start recording a macro:

    mono Macro2.exe filename.sh

This command will start recording a macro into filename.sh. Then, you can just enter commands as normal into the program. When you're ready to save your program, type:

    macro stop

and it will write your file and exit the internal shell.

# Functionality (current)
Macro2 has the following functionality:

## Configurable Shell
When running Macro2, you can specify the shell as such:

	mono Macro2.exe filename.sh /usr/bin/zsh

## View Contents
To view the contents of the script that you have so far, do this:

	macro contents

within the Macro shell.

## Undo the last operation
If you messed up a command and don't want it in the shell, you can use

	macro undo

to simply forget that it ever happened.

## Pause command saving
If you want to still run commands on the shell but not put them in the script, you can use

	macro pause

to resume, you simply type

	macro start

# Functionality (Future)
This is a work in progress. If you would like to implement some functionality, or just want a heads up on what might show up in Macro2, here are some of my currents ideas:

* Delete specific lines in the script
* Start writing based on an existing script
* Configure the Macro2 prompt shown to the user

# Known Bugs
* Command output may move the insertion point below the prompt
* Obviously, tty-based programs don't execute in Macro2.

# License
MIT license
