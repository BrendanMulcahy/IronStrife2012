using UnityEngine;
using System.Collections;

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

    public Vector3 targetClickLocation;

    public bool spellButton = false;
    public Spell spellBeingCast;
}
