using UnityEngine;
using System.Collections;

public class NPCCombatController : ClientCombatController {

    
    GameObject targetPlayer;
	// Use this for initialization
	void Start () {
        attackDuration = 0.5f;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isAttacking)
        {
            attackTimeRemaining -= Time.deltaTime;

            if (attackTimeRemaining <= 0.0f)
            {
                isAttacking = false;
            }
        }

        if (targetPlayer == null)
        {
            targetPlayer = GetComponent<NPCController>().Firstplayer;
        }
        if (targetPlayer != null)
        {
            Vector3 direction = targetPlayer.transform.localPosition - this.transform.localPosition;
            if (direction.sqrMagnitude <= 4 && !isAttacking && GetComponent<NPCController>().attackTimer <= 0 && GetComponent<NPCController>().playerseen && !GetComponent<NPCController>().dead)
            {
                SendTryAttackMessage();
            }
        }

	}

    // This RPC is only performed on the server. A client cannot call it. 
    // Therefore the RPC is run on the server's instance of the caller's GameObject (the caller's character) in the server.
    [RPC]
    private void SendTryAttackMessage()
    {
        SendMessage("Attack");	
        isAttacking = true;
        attackTimeRemaining = attackDuration;
        networkView.RPC("SendAttackStartedMessage", RPCMode.Others);	// Notifies all clients that this person has started attacking.
    }

    [RPC]
    private void SendAttackStartedMessage()
    {
        isAttacking = true;		// [Clients] For animation purposes : Sets the boolean to true and sets the animation time to its initial length.
        attackTimeRemaining = attackDuration;
    }
}
