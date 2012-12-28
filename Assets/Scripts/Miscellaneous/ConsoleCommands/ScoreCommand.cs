using UnityEngine;
class ScoreCommand : ConsoleCommand
{
    public override string[] Names { get { string[] names = { "score" }; return names; } }


    public override string HelpMessage
    {
        get { return "Displays the current game score"; }
    }

    public override void Execute(params string[] parameters)
    {
        Debug.Log("Good Score : " + GameState.goodScore);
        Debug.Log("Evil Score : " + GameState.evilScore);
        Debug.Log("Score Limit : " + GameState.scoreLimit);
    }
}

