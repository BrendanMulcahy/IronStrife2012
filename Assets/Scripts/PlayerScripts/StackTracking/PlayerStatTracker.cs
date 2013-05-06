using UnityEngine;
using System.Collections.Generic;

[PlayerComponent(PlayerScriptType.ServerEnabled)]
public class PlayerStatTracker : MonoBehaviour
{
    public Dictionary<string, int> itemsPurchased = new Dictionary<string, int>();
    public Dictionary<string, int> spellsCast = new Dictionary<string, int>();
    
    public int kills = 0;
    public int deaths = 0;

    public int playerId = 0;

    void Start()
    {
        this.gameObject.GetInventory().ItemAdded += PlayerStatTracker_ItemAdded;
        this.gameObject.GetCharacterStats().Died += PlayerStatTracker_Died;
        this.gameObject.GetCharacterStats().KilledEnemy += PlayerStatTracker_KilledEnemy;
    }

    void PlayerStatTracker_KilledEnemy(GameObject killer, UnitKilledEventArgs e)
    {
        kills++;
    }

    void PlayerStatTracker_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        deaths++;
    }

    void PlayerStatTracker_ItemAdded(Inventory sender, Item newItem)
    {
        this.IncrementItem(newItem.name);
    }

    private void IncrementItem(string name)
    {
        if (!itemsPurchased.ContainsKey(name))
        {
            itemsPurchased[name] = 1;
        }
        else
        {
            itemsPurchased[name]++;
        }
    }

    private void IncrementSpell(string name)
    {
        if (!spellsCast.ContainsKey(name))
        {
            spellsCast[name] = 1;
        }
        else
        {
            spellsCast[name]++;
        }
    }

    void OnDisconnectedFromServer(NetworkDisconnection mode)
    {
        UploadStatistics();    
    }

    private void UploadStatistics()
    {
        var recordToUpload = new PlayerStatRecord(playerId, kills, deaths, itemsPurchased, spellsCast);

    }
}

public class PlayerStatRecord
{
    public int playerId;
    public int numKills;
    public int numDeaths;
    //public Dictionary<string, int> items;
    //public Dictionary<string, int> spells;

    public PlayerStatRecord(int playerId, int numKills, int numDeaths, Dictionary<string, int> items, Dictionary<string, int> spells)
    {
        this.playerId = playerId;
        this.numKills = numKills;
        this.numDeaths = numDeaths;
        //this.items = items;
        //this.spells = spells;
    }

    public PlayerStatRecord()
    {
        this.playerId = -1;
        this.numKills = 0;
        this.numDeaths = 0;
        //this.items = new Dictionary<string, int>();
        //this.spells = new Dictionary<string, int>();
    }
}