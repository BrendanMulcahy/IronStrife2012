using UnityEngine;
using System.Collections;

/// <summary>
/// [Deprecated] Don't use this class
/// </summary>
public class ClientCombatController : MonoBehaviour
{
    public bool isAttacking = false;
    public bool IsAttacking
    {
      get { return isAttacking; }
      set { isAttacking = value; }
    }

    bool isDefending = false;
    public bool IsDefending
    {
      get { return isDefending; }
      set { isDefending = value; }
    }

    public float attackDuration = .6f;
    public float attackTimeRemaining = 0.0f;
    public bool isServer = false;

    GameObject player;
    //private WeaponType weaponType = WeaponType.Swing;

    private bool isAiming;
    public bool IsAiming
    {
        get { return isAiming; }
    }

    public WeaponType WeaponType
    {
        get { return 0; }
    }

    private void Start () 
    {	
	    if (Network.player.ToString() == "0")
	    {
		    isServer = true;
	    }
    }

    private void Update () 
    {
	    if (isAttacking)
	    {
		    attackTimeRemaining -= Time.deltaTime;
		
		    if (attackTimeRemaining <= 0.0f)
		    {
			    isAttacking=false;
		    }
	    }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && !isAttacking && !isDefending)
        {
            isDefending = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            isDefending = false;
        }

	    if (Input.GetButtonDown("Fire1") && !isAttacking && !isDefending && !isAiming)
	    {
            //if (weaponType == WeaponType.Swing)
		        TryStartSwingAttack();
           // else if (weaponType == global::WeaponType.Aim)
           // {
          //      TryBeginAiming();
          //  }

	    }
    }

    private void TryBeginAiming()
    {
        isAiming = true;
    }

    // private void is called when a client tries to punch (by pressing Fire1 and is not currently punching
    // Sends an RPC request to the server to start punching.
    private void TryStartSwingAttack()
    {	
	    //Server
	    if (isServer)
	    {
		    SendTryAttackMessage();
	    }
        //Client
        else
        {
            networkView.RPC("SendTryAttackMessage", RPCMode.Server);
        }
    }

    // This RPC is only performed on the server. A client cannot call it. 
    // Therefore the RPC is run on the server's instance of the caller's GameObject (the caller's character) in the server.
    [RPC]
    private void SendTryAttackMessage()
    {
	    SendMessage("StartAttacking");		// [Server] Calls the private void StartAttacking() in CombatLogic
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