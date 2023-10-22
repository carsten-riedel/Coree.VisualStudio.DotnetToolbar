using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coree.VisualStudio.DotnetToolbar
{
    public class SolutionSettings
    {
        public bool KillAllDotnetProcessBeforeExectue { get; set; } = false;
        public bool BlockNonSdkExecute { get; set; } = true;
    }
}
