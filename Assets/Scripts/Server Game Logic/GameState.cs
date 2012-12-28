using UnityEngine;

/// <summary>
/// Static class containing all variables relating to the game's state
/// </summary>
public static class GameState
{
    /// <summary>
    /// The score of the good team.
    /// </summary>
    public static int goodScore = 0;
    /// <summary>
    /// The score of the evil team.
    /// </summary>
    public static int evilScore = 0;

    /// <summary>
    /// The score limit. Game ends when one team reaches this score.
    /// </summary>
    public static int scoreLimit = 100;

    /// <summary>
    /// The number of control points currently controlled by the good team.
    /// </summary>
    public static int numGoodControlPoints = 0;

    /// <summary>
    /// The number of control points currently controlled by the evil team.
    /// </summary>
    public static int numEvilControlPoints = 0;



    internal static void Reset(int maxScore)
    {
        scoreLimit = maxScore;
        evilScore = 0;
        goodScore = 0;
        numGoodControlPoints = 0;
        numEvilControlPoints = 0;
    }
}