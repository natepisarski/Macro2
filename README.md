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

# Functionality Known to Work
Macro2 has been tested (lightly) for its intended purpose: making scripts from commands. 

# Functionality which needs testing
Pause and start functionality are in the code, but have yet to be tested.

# License
MIT license