using System;
using System.IO;

using HumDrum.Operations.Files;

namespace Macro2
{
	public class MainClass
	{
		public static void PrintHelp()
		{
			Console.WriteLine ("Macro Help. Try typing your command in one of the following formats:");
			Console.WriteLine ();

			Console.WriteLine ("For recording macros: ");
			Console.WriteLine ("macro start [name] - Creates a macro with the script name of \"name\"");
			Console.WriteLine ();

			Console.WriteLine ("While already recording a macro: ");
			Console.WriteLine ("macro pause - This command, as well as any other commands before \"macro start\" will not be recorded");
			Console.WriteLine ("macro start - If paused, this will cease the pausing on the next command");
			Console.WriteLine ("macro undo - Undo the last command you just did. More powerful with macro pause to fix your environment");
			Console.WriteLine ();

			Console.WriteLine ("To stop recording a macro: ");
			Console.WriteLine ("macro stop");
			Console.WriteLine ();
		}

		/// <summary>
		/// Determine the file name to write to. 
		/// If the path is a directory, then it will try to write "script.sh" in the directory
		/// </summary>
		public static string DetermineFile(string path)
		{
			if (File.Exists(path) && DirectorySearch.IsDirectory (path))
				return String.Format ("{0}/script.sh", path);
			return path;
		}

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// This will always listen to either no arguments or one. Given one argument, the program
		/// will simply print help and exit. Given one argument, a filename, it will analyze it to see if it
		/// is a directory or a file, and set the output file accordingly.
		/// </summary>
		/// <param name="args">No arguments, or 1 argument: the filename</param>
		public static void Main(String[] args)
		{
			// Did the user not give us a filename, or they asked for help? Give them help
			if (args.Length < 1 || args[0].Equals("help")) {
				PrintHelp ();
				return;
			}

			Middleman middleman = new Middleman ("/bin/bash", DetermineFile(args[0]));
			middleman.Start ();

			// Middleman exited, Macro should exit too
			Console.WriteLine("[Macro]: Exiting");
		}
	}
}

