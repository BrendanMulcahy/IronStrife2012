class ScoreCommand : ConsoleCommand
{
    public override string[] Names { get { string[] names = { "score" }; return names; } }


    public override string HelpMessage
    {
        get { return "Displays the current game score"; }
    }

    public override void Execute(params string[] parameters)
    {
        DebugGUI.Print("Good Score : " + GameState.goodScore);
        DebugGUI.Print("Evil Score : " + GameState.evilScore);
        DebugGUI.Print("Score Limit : " + GameState.scoreLimit);
    }
}

