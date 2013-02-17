using UnityEngine;
using System.Linq;

public class Relic : InteractableObject
{
    public string teamBuffName;
    public string holderBuffName;

    private bool isPickedUp = false;

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
        var player = viewID.GetGameObject();

        this.transform.SetParentAndCenter(player.transform);
        this.transform.localPosition += Vector3.up * 3.5f;
        rigidbody.isKinematic = true;

        PopupMessage.Display(player.name + " has picked up <color=#d9d919>The " + this.gameObject.name + "</color> for Team " + player.GetTeamNumber() + "!", 4.0f, fontsize: 32);

        if (holderBuffName != "")
        {
            RelicBuff relicBuff = player.AddComponent(holderBuffName) as RelicBuff;
            relicBuff.Relic = this;
        }
        var teamMates = ((PlayerStats[])FindObjectsOfType(typeof(PlayerStats))).Where(stats => stats.TeamNumber == player.GetTeamNumber());
        foreach (PlayerStats stats in teamMates)
        {
            var buff = stats.gameObject.AddComponent(teamBuffName);
            ((RelicBuff)buff).Relic = this;
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
        var player = this.transform.root.gameObject;

        isPickedUp = false;
        this.transform.parent = null;
        rigidbody.isKinematic = false;

        PopupMessage.Display(player.name + " has dropped up <color=#d9d919>The " + this.gameObject.name + "</color>!", 4.0f, fontsize: 32);

        var playerBuff = player.GetComponent(holderBuffName);
        if (playerBuff)
            Destroy(playerBuff);

        var teamMates = ((PlayerStats[])FindObjectsOfType(typeof(PlayerStats))).Where(stats => stats.TeamNumber == player.GetTeamNumber());
        foreach (PlayerStats stats in teamMates)
        {
            var go = stats.gameObject;
            var teamBuff = go.GetComponent(teamBuffName);
            if (teamBuff)
                Destroy(teamBuff);
        }
    }
}