using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ConquestVictory : VictoryCondition
{
    List<ControlPoint> controlPoints;
    Dictionary<int, int> teamControlPoints = new Dictionary<int, int>();
    const int numTeams = 3;
    int winningTeam = -1;

    private static ConquestVictory _main;
    public static ConquestVictory Main
    {
        get
        {
            if (!_main)
                _main = Object.FindObjectOfType(typeof(ConquestVictory)) as ConquestVictory;
            return _main;
        }
    }

    void Awake() // Ensure there is only one ConquestVictory in the scene.
    {
        if (Main != this)
            Destroy(this);

    }

    protected virtual int ControlPointsNeeded
    {
        get
        {
            return controlPoints.Count - 1;
        }
    }

    protected override void OnMasterGameLogicAdded()
    {
        base.OnMasterGameLogicAdded();
        Initialize();
    }

    void Initialize()
    {
        for (int g = 0; g < numTeams; g++) { teamControlPoints[g] = 0; }
        controlPoints = Object.FindObjectsOfType(typeof(ControlPoint)).Cast<ControlPoint>().ToList();
        foreach (ControlPoint cp in controlPoints)
        {
            teamControlPoints[cp.controllingTeam]++;
            cp.AllegianceChanged += ConquestVictory_CpAllegianceChanged;
        }

        GameTime.Main.Dawn += ConquestVictory_Dawn;
    }

    private void ConquestVictory_Dawn()
    {
        for (int g = 1; g < numTeams; g++)
        {
            if (teamControlPoints[g] >= ControlPointsNeeded)
            {
                InitiateConquestVictory(g);
            }
        }
    }

    /// <summary>
    /// Initiates a conquest victory for the given team. At dusk, this team will win unless stopped.
    /// </summary>
    /// <param name="teamNumber"></param>
    private void InitiateConquestVictory(int teamNumber)
    {
        PopupMessage.NetworkDisplay("Team " + teamNumber + " will achieve a Conquest Victory unless stopped before Dusk!");
        winningTeam = teamNumber;
        GameTime.Main.Dusk += ConquestVictory_Dusk;
        foreach (ControlPoint cp in controlPoints)
        {
            cp.AllegianceChanged += ConquestVictoryInterrupt_CpAllegianceChanged;
        }
    }

    private void ConquestVictoryInterrupt_CpAllegianceChanged(ControlPoint sender, int oldTeam, int newTeam)
    {
        if (newTeam != winningTeam)
        {
            winningTeam = -1;
            GameTime.Main.Dusk -= ConquestVictory_Dusk;
            foreach (ControlPoint cp in controlPoints)
            {
                cp.AllegianceChanged -= ConquestVictoryInterrupt_CpAllegianceChanged;
            }
        }
    }

    private void ConquestVictory_Dusk()
    {
        if (winningTeam != -1)
        {
            PopupMessage.NetworkDisplay("Team " + winningTeam + " has achieved a Conquest Victory!");
            Time.timeScale = .1f;
        }
    }

    void ConquestVictory_CpAllegianceChanged(ControlPoint sender, int oldTeam, int newteam)
    {
        teamControlPoints[oldTeam]--;
        teamControlPoints[newteam]++;
        Debug.Log("Team " + oldTeam + " now controls " + teamControlPoints[oldTeam] + " bases and Team " + newteam + " now controls " + teamControlPoints[newteam]);
    }

    protected override void ServerUpdate()
    {

    }

    protected override void ClientUpdate()
    {

    }

    protected override void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo msg)
    {

    }
}