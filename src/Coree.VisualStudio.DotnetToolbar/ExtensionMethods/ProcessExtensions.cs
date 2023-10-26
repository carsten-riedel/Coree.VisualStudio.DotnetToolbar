﻿using System;
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
    }
}