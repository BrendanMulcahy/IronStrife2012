using System.Net;
using UnityEngine;

class MasterServerCommand : ConsoleCommand
{

    public override string[] Names
    {
        get { string[] names = { "masterserver" }; return names; }
    }

    public override string HelpMessage
    {
        get { return "Changes the IP addres of the master server to the address provided. \nIf no parameters are given, the ip address is reset to the default."; }
    }

    public override void Execute(params string[] parameters)
    {
        if (parameters.Length == 0)
            StrifeMasterServer.ResetMasterServerAddress();
        else
            StrifeMasterServer.SetMasterServerAddress(parameters[0]);
    }
}