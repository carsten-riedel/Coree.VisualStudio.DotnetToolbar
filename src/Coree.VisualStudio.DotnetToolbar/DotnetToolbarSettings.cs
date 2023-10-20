using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Coree.VisualStudio.DotnetToolbar
{
    public class Settings
    {
        public string CommandLineAppend { get; set; }
    }

    public class SettingsManager
    {
        public static Settings settings { get; set; } = new Settings();

        internal static async Task<bool> LoadOrInitAsync(AsyncPackage package,string UserLocalDataPath, string solutionFullName, string solutionName, string solutionGuid)
        {
            await Task.Delay(10);
            settings.CommandLineAppend = "foo";
            return true;
        }

        public static async Task<string> ReadTextAsync(string fi)
        {
            using (StreamReader sr = new StreamReader(fi))
            {
                return await sr.ReadToEndAsync();
            }
        }

        public static async Task WriteTextAsync(string fi,string ma)
        {
            using (StreamWriter sw = new StreamWriter(fi))
            {
                await sw.WriteAsync(ma);
            }
        }
    }


}
