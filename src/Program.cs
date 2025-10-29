// Purpose:
// This is a time-based C# program to turn on/off Network connection on Windows OS.
// For my personal use, I want to disable the network connection for the following two time ranges:
// Time Range 1: 6pm to 11.59pm
// Time Range 2: 12am to 6am
//
// Feel free to modify according to your needs.

namespace nwcontrol
{
    using System;
    using System.Diagnostics;

    internal class Program
    {
        public static void RunCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe"; // Or the path to the executable you want to run directly
            startInfo.Arguments = "/C " + command; // "/C" executes the command and then terminates
                                                   // Use "/K" to execute and keep the window open
            startInfo.RedirectStandardOutput = true; // Redirects the standard output stream
            startInfo.RedirectStandardError = true;  // Redirects the standard error stream
            startInfo.UseShellExecute = false;       // Required for redirecting streams
            startInfo.CreateNoWindow = true;         // Do not create a new window for the process

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();

                // Read the output
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit(); // Waits for the process to exit

                /*
                Console.WriteLine("Output:");
                Console.WriteLine(output);
                Console.WriteLine("Error:");
                Console.WriteLine(error);
                Console.WriteLine($"Exit Code: {process.ExitCode}");
                */
            }
        }

        public static void Main(string[] args)
        {
            TimeSpan startTime1 = new TimeSpan(18, 0, 0);    //  6.00pm
            TimeSpan endTime1 = new TimeSpan(23, 59, 59);   // 11.59pm
            TimeSpan startTime2 = new TimeSpan(12, 0, 0);   // 12.00 am
            TimeSpan endTime2 = new TimeSpan(6, 0, 0);      //  6.00 am
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (
                (now > startTime1) && (now < endTime1)
                || (now > startTime2) && (now < endTime2)
                )
            {
                // In time range, so diable Network
                Console.WriteLine("Disable network");
                RunCommand("ipconfig /release *");
            } else {
                Console.WriteLine("Enable network");
                RunCommand("ipconfig /renew");
            }
        }

    }
}
