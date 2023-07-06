# Macro2
C# application used to create Shell Scripts from your console history.

It's a lot easier to do _some action_ in a terminal directly than to write a script for it, for a few reasons. This application is supposed to compliment that workflow, allowing you to perform a task once, and save it as a **Macro**.

# How it works
Macro2 is a simple man-in-the-middle between you and your shell. Each command is executed on the shell, and saved into the final script.

# How to use Macro2
The first step is to start recording a macro:

    Macro2.exe filename.sh

This command will start recording a macro into filename.sh. Then, you can just enter commands as normal into the program. When you're ready to save your program, type:

    macro stop

and it will write your file and exit the internal shell.

# Functionality
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

# License
MIT license
