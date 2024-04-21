namespace BetterHelpCommand.Patches
{
    using CommandSystem.Commands.Shared;
    using CommandSystem;
    using System.Text;
    using System.Text.RegularExpressions;
    using HarmonyLib;

    /// <summary>
    /// Patches <see cref="HelpCommand.GetCommandList"/>.
    /// </summary>
    [HarmonyPatch(typeof(HelpCommand), nameof(HelpCommand.GetCommandList))]
    internal class HelpCommandPatche
    {
        private static readonly Regex ConsoleTagReplacer = new(@"<\/?[ib]>");

        private static bool Prefix(HelpCommand __instance, ICommandHandler handler, string header, ref string __result)
        {
            StringBuilder _helpBuilder = __instance._helpBuilder;
            _helpBuilder.Clear();
            _helpBuilder.Append(header);

            string previousName = string.Empty;
            foreach (ICommand command in handler.AllCommands)
            {
                string pluginName = string.Empty;
                command.GetType()?.Namespace.Split('.').TryGet(0, out pluginName);
                if (previousName != pluginName)
                {
                    _helpBuilder.AppendLine();
                    _helpBuilder.Append("<size=40><b>");
                    _helpBuilder.Append(pluginName);
                    _helpBuilder.Append("</b></size>");
                    previousName = pluginName;
                }

                GetCommand(command);
            }

            return false;

            void GetCommand(ICommand command, int space = 0)
            {
                if (command is IHiddenCommand)
                    return;

                _helpBuilder.AppendLine();
                _helpBuilder.Append(new string(' ', space * 2));
                _helpBuilder.Append(" - <b>");
                _helpBuilder.Append(command.Command);
                _helpBuilder.Append("</b> ");
                if (command.Aliases != null && command.Aliases.Length != 0)
                {
                    _helpBuilder.Append('(');
                    _helpBuilder.Append(string.Join(", ", command.Aliases));
                    _helpBuilder.Append(')');
                }

                _helpBuilder.Append(" : <i><color=yellow>");
                _helpBuilder.Append(command.Description);
                _helpBuilder.Append("</color></i>");

                if (command is not ParentCommand parent)
                    return;

                foreach (ICommand subCommand in parent.AllCommands)
                {
                    GetCommand(subCommand, space + 1);
                }
            }
        }
    }
}
