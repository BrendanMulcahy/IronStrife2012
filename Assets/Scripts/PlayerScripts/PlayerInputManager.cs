using UnityEngine;
using System.Collections;

[PlayerComponent(PlayerScriptType.AllEnabled)]
/// <summary>
/// This component handles storing user input, be it from a local player, 
/// or from a remote client (if the component is on the server).
/// </summary>
public class PlayerInputManager : MonoBehaviour {

    public float verticalInput = 0f;
    public float horizontalInput = 0f;
    public bool jumpButton = false;
    public bool sprintButton = false;
    public bool attackButton = false;
    public bool defendButton = false;
    public bool aimButton = false;
    public Vector3 forwardCameraDirection = new Vector3();
    public bool lockButton = false;
    public CameraMode cameraMode = CameraMode.Regular;

    public GameObject homingTarget;

    public Vector3 targetClickLocation;

    public bool spellButton = false;
    /// <summary>
    /// The current spell being cast
    /// </summary>
    public Spell spellBeingCast;

    /// <summary>
    /// The time so far spent casting the current spell. When this reaches the spell's cast time, the spell is cast.
    /// </summary>
    public float spellCastProgress = 0f;

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            int spell = (int)spellBeingCast;
            stream.Serialize(ref spell);
            stream.Serialize(ref spellCastProgress);
        }
        else
        {
            int spell = 0;
            stream.Serialize(ref spell);
            spellBeingCast = (Spell)spell;

            stream.Serialize(ref spellCastProgress);
        }
    }
}
