using System;

using System.Diagnostics;
using System.Threading;

using System.IO;
using System.Collections.Generic;

namespace Macro2
{
	/// <summary>
	/// Represents a shell running on the user's system.
	/// </summary>
	public class Shell
	{
		/// <summary>
		/// The process of the shell itself
		/// </summary>
		/// <value>The shell process</value>
		Process ShellProcess { get; set; }

		/// <summary>
		/// Get or set the name of the shell executable
		/// </summary>
		/// <value>The name of the shell</value>
		public string ShellName {get; private set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="Macro2.Shell"/> class.
		/// This will supply no arguments to the shell.
		/// </summary>
		/// <param name="executablePath">The path to the shell's binary on the system.</param>
		public Shell (string executablePath, string arguments)
		{

			ShellName = executablePath;
			ProcessStartInfo info = new ProcessStartInfo (executablePath.ToString (), arguments);

			// These 2 lines allow us to control standard input from within this class
			info.RedirectStandardInput = true;
			info.UseShellExecute = false; 

			ShellProcess = new Process ();
			ShellProcess.StartInfo = info;
			ShellProcess.Start ();
		}

		/// <summary>
		/// Run the specified command on this shell, returning the output of the function, if any
		/// </summary>
		/// <param name="command">The text command to run on the shell</param>
		public void Run(string command)
		{
			ShellProcess.StandardInput.WriteLine (command);
		}

		/// <summary>
		/// Causes the Shell to close its process
		/// </summary>
		/// <param name="command">Optionally, close with a command to the shell as well</param>
		public void Close(string command)
		{
			if (command.Length > 1)
				Run (command);

			ShellProcess.Close ();
		}
	}
}

