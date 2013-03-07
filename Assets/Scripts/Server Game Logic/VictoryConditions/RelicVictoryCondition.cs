using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RelicVictoryCondition : VictoryCondition
{
    List<Relic> allRelics = new List<Relic>();
    RelicVictoryDropArea dropArea;
    const int relicsNeededForVictory = 3;
    int winningTeam = -1;
    float elapsedTime = 0f;
    public static float relicVictoryHoldTime = 6f;
    float previousTime = 9999f;

    private static RelicVictoryCondition _main;
    public static RelicVictoryCondition Main
    {
        get
        {
            if (!_main)
                _main = Object.FindObjectOfType(typeof(RelicVictoryCondition)) as RelicVictoryCondition;
            return _main;
        }
    }

    void Awake() // Ensure there is only one RelicVictoryCondition in the scene.
    {
        if (Main != this)
            Destroy(this);

    }

    protected override void OnMasterGameLogicAdded()
    {
        base.OnMasterGameLogicAdded();
        allRelics = Object.FindObjectsOfType(typeof(Relic)).Cast<Relic>().ToList();
        dropArea = Object.FindObjectOfType(typeof(RelicVictoryDropArea)) as RelicVictoryDropArea;
        dropArea.RelicAdded += RelicVictory_RelicAdded;
    }


    private void RelicVictory_RelicAdded(RelicDropArea dropArea, Relic relic)
    {
        if (dropArea.relicsInArea.Count >= relicsNeededForVictory)
        {
            InitiateRelicVictory(dropArea.controlPoint.controllingTeam);
        }
    }

    private void InitiateRelicVictory(int winningTeam)
    {
        PopupMessage.NetworkDisplay("Team " + winningTeam + " has collected " + relicsNeededForVictory + " Relics in the Temple!\nThey will achieve a Relic Victory unless stopped within " + relicVictoryHoldTime + " hours!");
        this.winningTeam = winningTeam;
        dropArea.RelicRemoved += RelicVictory_RelicRemoved;
        elapsedTime = 0f;
        previousTime = GameTime.CurrentTime;
    }

    void RelicVictory_RelicRemoved(RelicDropArea dropArea, Relic relic)
    {
        if (dropArea.relicsInArea.Count < relicsNeededForVictory)
            StopRelicVictory();
    }

    private void StopRelicVictory()
    {
        this.winningTeam = -1;
        PopupMessage.NetworkDisplay("Team " + winningTeam + "'s Relic Victory has been interrupted!");

    }

    public void RegisterRelic(Relic relic)
    {
        if (!allRelics.Contains(relic))
            allRelics.Add(relic);
    }

    public void RemoveRelic(Relic relic)
    {
        if (allRelics.Contains(relic))
            allRelics.Remove(relic);
    }

    protected override void ClientUpdate()
    {

    }

    protected override void ServerUpdate()
    {
        if (winningTeam != -1)
        {
            if (GameTime.CurrentTime < previousTime)
                elapsedTime += GameTime.CurrentTime - (previousTime - 24);
            else
                elapsedTime += GameTime.CurrentTime - previousTime;

            previousTime = GameTime.CurrentTime;

            if (elapsedTime >= relicVictoryHoldTime)
            {
                AchieveRelicVictory(winningTeam);
            }
        }
    }

    private void AchieveRelicVictory(int winningTeam)
    {
        PopupMessage.NetworkDisplay("Team " + winningTeam + " has achieved a Relic Victory!");
        Time.timeScale = .1f;
    }

    protected override void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo msg)
    {

    }
}