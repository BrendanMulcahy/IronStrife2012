using UnityEngine;
public class DreamVessel : Spell,  ISelfSpellWithViewID
{
    public void Execute(GameObject caster, NetworkViewID viewID)
    {
        Debug.Log("Instantiating the vessel");
        var vesselPrefab = Resources.Load("SpellEffects/DreamVessel") as GameObject;
        var go = GameObject.Instantiate(vesselPrefab) as GameObject;
        if (!go.networkView)
            go.AddComponent<NetworkView>();
        go.networkView.viewID = viewID;
        go.networkView.observed = go.transform;
        go.networkView.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
        go.AddComponent<DreamVesselObject>().team = caster.GetCharacterStats().TeamNumber;
        go.transform.position = caster.transform.position + caster.transform.forward * 2f;

    }

    public override string name
    {
        get { return "Dream Vessel"; }
    }

    public override SpellAffectType AffectType
    {
        get { return SpellAffectType.Allies; }
    }

    protected override void InitializeSpellValues()
    {
        manaCost = 50;
        castTime = 1.0f;
    }
}

public class DreamVesselObject : InteractableObject
{
    public int team;
    public float moveSpeed = 15.0f;
    public GameObject rider;

    void Start()
    {
        interactionRange = 1.0f;
    }

    public override void InteractWith(GameObject player)
    {
        if (player.GetCharacterStats().TeamNumber != this.team) return;
        if (rider) return;

        if (Network.isServer)
        {
            TryEnterVessel(player.networkView.viewID);
        }
        else
        {
            networkView.RPC("TryEnterVessel", RPCMode.Server, player.networkView.viewID);
        }
    }

    [RPC]
    void TryEnterVessel(NetworkViewID playerViewID)
    {
        Debug.Log("Trying to enter vessel");
        if (!rider)
        {
            networkView.RPC("CommitEnterVessel", RPCMode.All, playerViewID);
        }
    }

    [RPC]
    void CommitEnterVessel(NetworkViewID enteringPlayer)
    {
        Debug.Log("Commit vessel entry");
        GameObject entered = enteringPlayer.GetGameObject();
        entered.transform.position = this.transform.position;
        this.transform.SetParentAndCenter(entered.transform);
        this.transform.position += Vector3.up * 1.5f;
        this.rider = entered;

        if (entered.IsMyLocalPlayer() || Network.isServer)
        {
            entered.GetComponent<ThirdPersonController>().enabled = false;
            entered.AddComponent<VesselController>().vessel = this;
        }
    }

    [RPC]
    internal void CommitExitVessel(NetworkViewID networkViewID)
    {
        this.transform.parent = null;
        this.rider = null;
    }
}

public class VesselController : MonoBehaviour
{
    private PlayerInputManager input;
    public DreamVesselObject vessel;

    void Awake()
    {
        this.input = GetComponent<PlayerInputManager>();
    }

    void Update()
    {
        // Forward vector relative to the camera along the x-z plane	
        Vector3 forward = input.forwardCameraDirection;
        forward = forward.normalized;

        // Right vector relative to the camera
        // Always orthogonal to the forward vector
        var right = Vector3.Cross(Vector3.up, forward);
        var up = Vector3.Cross(forward,right);
        right = right.normalized;
        up = up.normalized;
        Vector3 targetDirection = input.horizontalInput * right + input.verticalInput * forward;
        if (input.jumpButton)
            targetDirection += up * 1f;

        this.transform.position += targetDirection * vessel.moveSpeed * Time.deltaTime;

        if (this.gameObject.IsMyLocalPlayer())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Network.isServer)
                {
                    this.TryExitVessel(networkView.viewID);
                }
                else
                {
                    this.networkView.RPC("TryExitVessel", RPCMode.Server, this.networkView.viewID);
                }
            }
        }
    }

    [RPC]
    void TryExitVessel(NetworkViewID viewID)
    {
        var player = viewID.GetGameObject();

        vessel.networkView.RPC("CommitExitVessel", RPCMode.All, viewID);
        if (gameObject.IsMyLocalPlayer())
        {
            ExitVessel();
        }
        else
        {
            if (Network.isServer)
                ExitVessel();
            networkView.RPC("ExitVessel", PlayerManager.Main.FindRecord(player).networkPlayer);
        }
    }

    [RPC]
    public void ExitVessel()
    {
        this.GetComponent<ThirdPersonController>().enabled = true;
        Destroy(this);
    }
}