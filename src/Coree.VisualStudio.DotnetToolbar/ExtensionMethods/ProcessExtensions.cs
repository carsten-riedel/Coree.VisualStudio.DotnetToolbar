
using System;
using System.Diagnostics;

namespace Coree.VisualStudio.DotnetToolbar
{
    internal static class ProcessStartInfoExtensions
    {
        internal static string GetProcessStartInfoCommandline(this ProcessStartInfo processStartInfo)
        {
            return $@"{processStartInfo.FileName} {processStartInfo.Arguments}";
        }

        internal static void AllDontNetKill(this System.Diagnostics.Process processx, string name)
        {
            Process[] dotnetProcesses = Process.GetProcessesByName(name);

            // Iterate through all the 'dotnet' processes
            foreach (var process in dotnetProcesses)
            {
                try
                {
                    // Kill the process
                    process.Kill();
                    Debug.WriteLine($"Killed process {process.Id}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to kill process {process.Id}: {ex.Message}");
                }
            }
        }

        internal static void quickexec(string fi, string args, string wdir)
        {
            
            var process = new System.Diagnostics.Process();

            var x = new ProcessStartInfo()
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = fi,
                Arguments = args,
                WorkingDirectory = wdir,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
            };
            process.StartInfo = x;
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();
        }

        // Get all processes with the name 'dotnet'
    }
}