using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Coree.VisualStudio.DotnetToolbar
{


    internal static class ProcessStartInfoExtensions
    {
        internal static string GetProcessStartInfoCommandline(this ProcessStartInfo processStartInfo)
        {
            return $@"{processStartInfo.FileName} {processStartInfo.Arguments}";
        }
    }
}