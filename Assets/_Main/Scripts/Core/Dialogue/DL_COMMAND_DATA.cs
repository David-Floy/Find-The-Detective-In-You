using System.Collections;
using System.Collections.Generic;
using System.Text;


public class DL_COMMAND_DATA
{
    public List<Command> commands;
    private const char COMMANDSPLITTER_ID = ',';
    private const char ARGUMENTSCONTAINER_ID = '(';
    private const string WAITCOMMAND_ID = "[wait]";
    public struct Command
    {
        public string name;
        public string[] arguments;
    }

    public DL_COMMAND_DATA(string rawCommands)
    {
        commands = RipCommands(rawCommands);
    }

    
    /// <summary>
    /// Extracts the name and und the arguments from the raw dialogue command. 
    /// </summary>
    /// <param name="rawCommands"></param>
    /// <returns></returns>
    private List<Command> RipCommands(string rawCommands)
    {
        string[] data = rawCommands.Split(COMMANDSPLITTER_ID, System.StringSplitOptions.RemoveEmptyEntries);
        List<Command> result = new List<Command>();

        foreach (string cmd in data)
        {
            Command command = new Command();
            int index = cmd.IndexOf(ARGUMENTSCONTAINER_ID);
            command.name = cmd.Substring(0, index).Trim();
            command.arguments = GetArgs(cmd.Substring(index + 1, cmd.Length - index - 2));
            result.Add(command);
        }

        return result;
    }

   /// <summary>
   /// Cuts and separates the arguments of the dialogue command.
   /// </summary>
   /// <param name="args"></param>
   /// <returns></returns>
    private string[] GetArgs(string args)
    {
        List<string> argsList = new List<string>();
        StringBuilder currentArg = new StringBuilder();

        bool inQuotes = false;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == '"')
            {
                inQuotes = !inQuotes;
                continue;
            }

            if (!inQuotes && args[i] == ' ')
            {
                argsList.Add(currentArg.ToString());
                currentArg.Clear();
                continue;
            }

            currentArg.Append(args[i]);
        }

        if (currentArg.Length > 0)
        {
            argsList.Add(currentArg.ToString());
        }

        return argsList.ToArray();
    }



}
