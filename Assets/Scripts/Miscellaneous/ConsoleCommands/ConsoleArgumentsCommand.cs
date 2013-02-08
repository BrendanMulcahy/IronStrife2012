using UnityEngine;

class ConsoleArgumentsCommand : ConsoleCommand
{

    public override string[] Names
    {
        get { string[] names = { "consolearguments" }; return names; }
    }

    public override string HelpMessage
    {
        get { return "Prints the command line arguments that this process was run with."; }
    }

    public override void Execute(params string[] parameters)
    {
        var args = System.Environment.GetCommandLineArgs();
        foreach (string s in args)
        {
            Debug.Log(s);
        }
    }
}