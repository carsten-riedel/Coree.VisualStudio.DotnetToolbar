using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coree.VisualStudio.DotnetToolbar.ExtensionMethods
{
    public static class EnvDTEExtensions
    {
        public static void WindowActivateEx(this EnvDTE80.DTE2 dte2)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            Window window = dte2.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            var ss = window.Visible;
            window.Activate();
        }

        public static void AddPane(this EnvDTE80.DTE2 dte2,string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            bool found = false;
            foreach (EnvDTE.OutputWindowPane item in dte2.ToolWindows.OutputWindow.OutputWindowPanes)
            {
                if (item.Name.Equals(name, StringComparison.CurrentCulture))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                dte2.ToolWindows.OutputWindow.OutputWindowPanes.Add(name);
            }
        }

        public static void AddText(this EnvDTE80.DTE2 dte2, string name,string text)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            dte2.AddPane(name);
            foreach (EnvDTE.OutputWindowPane item in dte2.ToolWindows.OutputWindow.OutputWindowPanes)
            {
                if (item.Name.Equals(name, StringComparison.CurrentCulture))
                {
                    item.OutputString(text + Environment.NewLine);
                    item.Activate();
                    break;
                }
            }
        }
    }
}
