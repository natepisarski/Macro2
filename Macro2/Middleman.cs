using System;
using System.Collections.Generic;
using System.IO;

using System.Threading;

using HumDrum.Collections;

namespace Macro2
{
	/// <summary>
	/// Acts as a command buffer for a given shell. The 
	/// shell runs in its own thread, and all user input is passed through this class, 
	/// which is later fed to the shell. This allows us to capture the input to make the script file.
	/// </summary>
	public class Middleman
	{
		/// <summary>
		/// The actual text of the command script
		/// </summary>
		/// <value>The command script</value>
		List<string> CommandScript {get; set;} // TODO: Find extranneous commands and prune them from the command script

		/// <summary>
		/// The shell which will be executing the commands
		/// </summary>
		/// <value>The executing shell</value>
		Shell ExecutingShell {get; set;}

		/// <summary>
		/// Whether or not this Middleman is paused
		/// </summary>
		/// <value><c>true</c> if paused; otherwise, <c>false</c>.</value>
		bool Paused {get; set;}

		/// <summary>
		/// The output file for this Middleman. This is where the script will actually get written to.
		/// </summary>
		/// <value>The output file</value>
		string Output {get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="Macro2.Middleman"/> class.
		/// This will start the shell in the user's workingDirectory.
		/// </summary>
		/// <param name="shellPath">The path</param>
		public Middleman (string shellPath, string output)
		{ 
			Output = output;
			ExecutingShell = new Shell (shellPath, "");
			CommandScript = new List<string> ();
			Paused = false;
		}

		/// <summary>
		/// Starts the MiddleMan REPL, almost as if it were a shell
		/// </summary>
		public void Start()
		{
			string command = "";
			Console.WriteLine ("Starting " + ExecutingShell.ShellName );

			for (; ;) {
				Console.WriteLine ("[Macro]: Line " + CommandScript.Length ());

				Console.Write ("[Macro] {" + ExecutingShell.ShellName + "} > ");
				command = Console.ReadLine ();
				Console.WriteLine ();

				// No command? Don't even try to parse it
				if (command == null || command.Equals (" "))
					continue; 

				// Dispatch the parsing methods over the command
				ParseCommand (command);

				// Does it want us to stop recording? Break out of the loop and tidy up
				if (command != null && (command.Equals ("macro stop") || command.Equals("macro end") || command.Equals("macro quit") || command.Equals("macro exit")))
					break;
			}

			Console.WriteLine ("The script has been written to " + Output);
		}

		/// <summary>
		/// The entry point for parsing commands from the command line.
		/// </summary>
		/// <param name="command">The command to parse</param>
		public void ParseCommand(string command)
		{
			IEnumerable<string> commandWords = command.Split(' ');

			// Test to see if the command is a macro command
			if (commandWords.Get (0).Equals ("macro")) // Tail will drop the word "macro" from the MacroParse
				ParseMacroCommand (Sections.RepairStringWith(commandWords.Tail (), " "));
			else
				ParseShellComamnd(Sections.RepairStringWith(commandWords, " "));
		}

		/// <summary>
		/// Parses a command designated to control the Macro program itself
		/// </summary>
		public void ParseMacroCommand(string command)
		{
			// Gets the first word of the macro command. This can be any of these: {pause, start, undo, stop}
			switch (command.Split (' ').Get (0)) {
			case "pause":
				Paused = true;
				break;
			case "start":
				Paused = false;
				break;
			case "undo":
				CommandScript = new List<string>(CommandScript.DropLast ());
				break;
			case "stop": case "quit": case "end":  // Synonyms for "stop" that I've tried in testing...
				WriteScript (); // The loop actually handles the exiting
				break;
			default:
				break;
			}

		}

		/// <summary>
		/// Parses a command designated for the shell
		/// </summary>
		public void ParseShellComamnd(string command)
		{
			try {
				CommandScript.Add (command);
				ExecutingShell.Run (command);
			} catch(Exception e) {
				Console.WriteLine ("[Macro Middleman] Error: " + e);
			}
		}

		/// <summary>
		/// Writes the script to the file
		/// </summary>
		public void WriteScript()
		{
			File.WriteAllLines (Output, (Transformations.Concatenate(Transformations.Wrap("#!" + ExecutingShell.ShellName), CommandScript)));
		}
	}
}

