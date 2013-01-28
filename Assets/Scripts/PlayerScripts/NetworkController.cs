using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[PlayerComponent(PlayerScriptType.ClientOwnerEnabled, PlayerScriptType.ServerDisabled, PlayerScriptType.ServerOwnerDeleted)]
public class NetworkController : MonoBehaviour
{
    public PlayerInputManager targetController;
    public RegularCamera regularCamera;
    public ThirdPersonController tpController;
    private float MaxLockRange { get { return tpController.maxLockRange; } }
    private AbilityManager abilityManager;

    private bool jumpButton;
    private float verticalInput;
    private float horizontalInput;
    private Vector3 forwardCameraDirection;
    private bool sprintButton;
    private bool defendButton;
    private bool attackButton;
    private bool aimButton;
    private CameraMode cameraMode;
    private bool spellButton;
    private int spellBeingCast;

    private GameObject spellTargetReticle;


    //NOT SYNCED
    private bool lockButton;

    void Awake()
    {
        regularCamera = Camera.main.GetComponent<RegularCamera>();
        tpController = GetComponent<ThirdPersonController>();
        abilityManager = GetComponent<AbilityManager>();
        targetController = GetComponent<PlayerInputManager>();
    }

    void OnSetOwnership()
    {
        if (Network.isServer) Destroy(this);
        else
        {
            GenerateNetworkView();
            spellTargetReticle = Instantiate(Resources.Load("SpellEffects/SpellTarget") as GameObject) as GameObject;
            spellTargetReticle.SetActive(false);
        }
    }

    private void GenerateNetworkView()
    {
        var previousNetworkView = networkView;
        var viewID = Network.AllocateViewID();
        var nv = gameObject.AddComponent<NetworkView>();
        nv.observed = this;
        nv.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
        nv.viewID = viewID;
        previousNetworkView.RPC("SetControllerNetworkView", RPCMode.Server, viewID);
    }

    [RPC]
    void SetControllerNetworkView(NetworkViewID viewID)
    {
        Debug.Log("Receiving " + gameObject.name + "'s viewID for his network controller. The viewID is " + viewID.ToString());
        var nv = gameObject.AddComponent<NetworkView>();
        nv.observed = this;
        nv.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
        nv.viewID = viewID;
    }

    void Update()
    {
        // Sample user input	
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpButton = Input.GetButton("Jump");
        sprintButton = Input.GetKey(KeyCode.LeftShift);
        attackButton = Input.GetButton("Fire1");
        aimButton = Input.GetButton("Fire2");
        defendButton = Input.GetKey(KeyCode.LeftAlt);
        lockButton = Input.GetKeyDown(KeyCode.Tab);
        cameraMode = regularCamera.CameraMode;
        forwardCameraDirection = Camera.main.transform.TransformDirection(Vector3.forward);

        //targetController.spellButton = false;
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(abilityManager.spellButtons[i]) && abilityManager.equippedSpells[i] != -1)
            {
                Debug.Log("You are pressing a bound spell button.");
                targetController.spellBeingCast = (Spell)abilityManager.equippedSpells[i];
                targetController.spellButton = true;
                networkView.RPC("SendSpellCastInfo", RPCMode.Server, abilityManager.equippedSpells[i]);
            }
        }

        if (lockButton)
        {
            if (tpController.LockedOn)
            {
                tpController.LockedOn = false; tpController.LockedTarget = null;
                networkView.RPC("ClientStopLocking", RPCMode.Server);
                regularCamera.CameraMode = CameraMode.Regular;

            }
            else
            {
                GameObject closestTarget = FindLockableTarget();
                if (closestTarget != null)
                {
                    networkView.RPC("ClientLockTarget", RPCMode.Server, closestTarget.networkView.viewID);
                    tpController.LockedTarget = closestTarget;
                    tpController.LockedOn = true;
                    regularCamera.CameraMode = CameraMode.Locked;
                }
            }
        }

        if (Input.GetButton("Fire1"))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int mask = 1 << 11;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 50f, mask))
            {
                targetController.targetClickLocation = hit.point;
                networkView.RPC("SendTargetClickLocation", RPCMode.Server, hit.point);
            }
        }

        targetController.forwardCameraDirection = forwardCameraDirection;
        targetController.jumpButton = jumpButton;
        targetController.verticalInput = verticalInput;
        targetController.horizontalInput = horizontalInput;
        targetController.sprintButton = sprintButton;
        targetController.attackButton = attackButton;
        targetController.defendButton = defendButton;
        targetController.aimButton = aimButton;
        targetController.cameraMode = cameraMode;

    }

    [DontAutoSerialize]
    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.Serialize(ref verticalInput);
            stream.Serialize(ref horizontalInput);
            stream.Serialize(ref jumpButton);
            stream.Serialize(ref sprintButton);
            stream.Serialize(ref attackButton);
            stream.Serialize(ref aimButton);
            stream.Serialize(ref defendButton);
            int camMode = (int)cameraMode;
            stream.Serialize(ref camMode);
            stream.Serialize(ref forwardCameraDirection);
        }
        else if (stream.isReading)
        {

            stream.Serialize(ref verticalInput);
            stream.Serialize(ref horizontalInput);
            stream.Serialize(ref jumpButton);
            stream.Serialize(ref sprintButton);
            stream.Serialize(ref attackButton);
            stream.Serialize(ref aimButton);
            stream.Serialize(ref defendButton);
            int camMode = (int)cameraMode;
            stream.Serialize(ref camMode);
            cameraMode = (CameraMode)camMode;
            stream.Serialize(ref forwardCameraDirection);

            SetTargetControllerValues();
        }
    }

    private void SetTargetControllerValues()
    {
        targetController.forwardCameraDirection = forwardCameraDirection;
        targetController.jumpButton = jumpButton;
        targetController.verticalInput = verticalInput;
        targetController.horizontalInput = horizontalInput;
        targetController.sprintButton = sprintButton;
        targetController.attackButton = attackButton;
        targetController.defendButton = defendButton;
        targetController.aimButton = aimButton;
        targetController.cameraMode = cameraMode;
    }

    private GameObject FindLockableTarget()
    {
        GameObject closestTarget = null;
        float targetDistance = 10000;

        var playerTargets = GameObject.FindGameObjectsWithTag("Player");
        var lockableTargets = GameObject.FindGameObjectsWithTag("Lockable");
        GameObject[] allTargets = playerTargets.Concat(lockableTargets).ToArray();
        foreach (GameObject gameObject in allTargets)
        {
            if (gameObject.transform.root == this.transform.root)
                continue;
            Vector3 gameObjectPosition = gameObject.transform.position;
            Camera yourCamera = Camera.main;

            Vector3 viewpoint = yourCamera.WorldToViewportPoint(gameObjectPosition);
            if (viewpoint.x >= 0 && viewpoint.x <= 1 && viewpoint.y >= 0 && viewpoint.y <= 1 && viewpoint.z > 0 && viewpoint.z < MaxLockRange)
            {
                if (Vector3.Distance(transform.position, gameObjectPosition) < targetDistance)
                {
                    closestTarget = gameObject;
                    targetDistance = Vector3.Distance(transform.position, gameObjectPosition);
                }
            }
        }
        return closestTarget;
    }

    [RPC]
    void SendSpellCastInfo(int spellID)
    {
        targetController.spellButton = true;
        targetController.spellBeingCast = (Spell)spellID;
    }

    [RPC]
    void SendTargetClickLocation(Vector3 clickLocation)
    {
        targetController.targetClickLocation = clickLocation;
    }

    /// <summary>
    /// Resets all input. Should be done after the controller is disabled so that your input doesn't "stick" to the last frame's input.
    /// </summary>
    internal void Reset()
    {
        targetController.verticalInput = 0;
        targetController.horizontalInput = 0;
        targetController.jumpButton = false;
        targetController.sprintButton = false;
        targetController.attackButton = false;
        targetController.aimButton = false;
        targetController.defendButton = false;
        targetController.lockButton = false;
    }
}

public class DontAutoSerializeAttribute : Attribute { }