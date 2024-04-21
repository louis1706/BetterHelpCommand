using BetterHelpCommand.Patches;
using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterHelpCommand
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }
        public ConsoleColorPatched.AnsiUsage AnsiUsage { get; set; } = ConsoleColorPatched.AnsiUsage.Enable;
    }
}
