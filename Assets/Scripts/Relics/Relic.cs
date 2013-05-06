using UnityEngine;
using System.Linq;

public class Relic : InteractableObject
{
    public string teamBuffName;
    public string holderBuffName;

    private RelicDropArea dropArea;

    private bool isPickedUp = false;
    private int _controllingTeam = 0;
    public int ControllingTeam { get { return _controllingTeam; } set { SetControllingTeam(value); } }

    void Start() { this.gameObject.layer = 20; }

    public override void InteractWith(GameObject player)
    {
        if (!isPickedUp)
            networkView.RPCToServer("TryPickupRelic", player.GetNetworkViewID());
    }

    [RPC]
    void TryPickupRelic(NetworkViewID viewID)
    {
        if (!isPickedUp)
            networkView.RPC("CommitPickupRelic", RPCMode.All, viewID);
        else
            Debug.LogWarning(viewID.GetGameObject() + " tried to pick up a relic that was already picked up!");
    }

    [RPC]
    void CommitPickupRelic(NetworkViewID viewID)
    {
        isPickedUp = true;
        this.collider.enabled = false;
        var player = viewID.GetGameObject();
        SetControllingTeam(player.GetTeamNumber());
        AddHolderBuffs(player);

        this.transform.SetParentAndCenter(player.transform);
        this.transform.localPosition += Vector3.up * 3.5f;
        rigidbody.isKinematic = true;

        if (Network.isServer)
        {
            player.GetCharacterStats().Died += RelicHolder_Died;
        }
    }

    private void AddHolderBuffs(GameObject player)
    {
        if (holderBuffName != "")
        {
            RelicBuff relicBuff = player.AddComponent(holderBuffName) as RelicBuff;
            relicBuff.Relic = this;
        }
    }

    private void AddTeamBuffs(int teamNumber)
    {
        if (teamBuffName == "") return;
        var teamMates = ((PlayerStats[])FindObjectsOfType(typeof(PlayerStats))).Where(stats => stats.TeamNumber == teamNumber);
        foreach (PlayerStats stats in teamMates)
        {
            var buff = stats.gameObject.AddComponent(teamBuffName);
            ((RelicBuff)buff).Relic = this;
        }
    }

    void RelicHolder_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        networkView.RPC("CommitDropRelic", RPCMode.All);
    }

    private void SetControllingTeam(int teamNumber)
    {
        if (teamNumber == _controllingTeam) return;

        RemoveTeamBuffs(_controllingTeam);
        PopupMessage.Display("Team " + _controllingTeam + " has lost control of <color=#d9d919>The " + this.gameObject.name + "</color>!", 4.0f, fontsize: 32);

        _controllingTeam = teamNumber;

        if (teamNumber != 0)
        {
            AddTeamBuffs(teamNumber);
            PopupMessage.Display("Team " + _controllingTeam + " has gained control of <color=#d9d919>The " + this.gameObject.name + "</color>!", 4.0f, fontsize: 32);
        }
    }

    [RPC]
    void TryDropRelic()
    {
        networkView.RPC("CommitDropRelic", RPCMode.All);
    }

    [RPC]
    void CommitDropRelic()
    {
        if (!this.isPickedUp) return;

        var player = this.transform.root.gameObject;
        RemoveHolderBuffs(player);

        isPickedUp = false;
        this.collider.enabled = true;
        this.transform.parent = null;
        rigidbody.isKinematic = false;

        
        // If this buff isn't in a drop area, then nobody controls this relic anymore.
        if (!this.dropArea)
        {
            SetControllingTeam(0);
        }
    }

    private void RemoveTeamBuffs(int teamNumber)
    {
        var teamMates = ((PlayerStats[])FindObjectsOfType(typeof(PlayerStats))).Where(stats => stats.TeamNumber == teamNumber);
        foreach (PlayerStats stats in teamMates)
        {
            var go = stats.gameObject;
            var teamBuff = go.GetComponent(teamBuffName);
            if (teamBuff)
                Destroy(teamBuff);
        }
    }

    private void RemoveHolderBuffs(GameObject player)
    {
        var playerBuff = player.GetComponent(holderBuffName);
        if (playerBuff)
            Destroy(playerBuff);
    }

    internal void OnEnteredDropArea(RelicDropArea relicDropArea)
    {
        this.dropArea = relicDropArea;
        if (!isPickedUp)
        {
            SetControllingTeam(relicDropArea.controlPoint.controllingTeam);
        }
    }

    internal void OnExitedDropArea()
    {
        this.dropArea = null;
        if (!this.isPickedUp)
        {
            SetControllingTeam(0);
        }
    }
}