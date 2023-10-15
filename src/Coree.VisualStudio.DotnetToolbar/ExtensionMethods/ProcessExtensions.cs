using System.Diagnostics;

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