using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Differencing;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Coree.VisualStudio.DotnetToolbar.ExtensionMethods
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

        internal static string[] GetOutput(this System.Diagnostics.Process processx, string name)
        {
            return null;
        }

        internal static async Task<string> StartAsync(this System.Diagnostics.Process process, string fileName, string arguments, string workingDirectory)
        {
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.Start();

            await process.WaitForExitAsync();
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            return String.Concat(error, Environment.NewLine, output);
        }

        internal static List<string> StartSync(this System.Diagnostics.Process process, string fileName, string arguments, string workingDirectory)
        {
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.WorkingDirectory = workingDirectory;

            StringBuilder outputBuilder = new StringBuilder();
            StringBuilder errorBuilder = new StringBuilder();

            // Capture standard output and error
            process.OutputDataReceived += (sender, e) => { if (e.Data != null) outputBuilder.AppendLine(e.Data); };
            process.ErrorDataReceived += (sender, e) => { if (e.Data != null) errorBuilder.AppendLine(e.Data); };

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            string all = String.Concat(errorBuilder.ToString(), Environment.NewLine, outputBuilder.ToString());
            List<string> retval = all.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            return retval;
        }

        internal static List<string> StartDotNetVersionSync(this System.Diagnostics.Process process, string fileName, string arguments, string workingDirectory)
        {
            var output = String.Join(Environment.NewLine,process.StartSync(fileName,arguments, workingDirectory));
            Regex regex = new Regex(@"(\d+\.\d+)\.\d+");
            var matches = regex.Matches(output);

            List<string> retval = new List<string>();
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                retval.Add(match.Groups[1].Value);
                
            }

            // Use Distinct to remove duplicates
            retval = retval.Distinct().ToList();

            return retval;  
        }

            

        }
}