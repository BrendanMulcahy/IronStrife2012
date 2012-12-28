using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used only by the server to control his player. 
/// It is essentially the same as the NetworkController but does not need to relay the information over the network.
/// </summary>
[RequireComponent(typeof(PlayerInputManager))]
public class ServerController : MonoBehaviour
{
    public PlayerInputManager targetController;
    public RegularCamera regularCamera;
    public AbilityManager abilityManager;

    void Start()
    {
        targetController = GetComponent<PlayerInputManager>();
        regularCamera = Camera.main.GetComponent<RegularCamera>();
        abilityManager = GetComponent<AbilityManager>();
    }

    void Update()
    {
        // Sample user input	
        targetController.verticalInput = Input.GetAxisRaw("Vertical");
        targetController.horizontalInput = Input.GetAxisRaw("Horizontal");
        targetController.jumpButton = Input.GetButton("Jump");
        targetController.sprintButton = Input.GetKey(KeyCode.LeftShift);
        targetController.attackButton = Input.GetButton("Fire1");
        targetController.aimButton = Input.GetButton("Fire2");
        targetController.defendButton = Input.GetKey(KeyCode.LeftAlt);
        targetController.forwardCameraDirection = Camera.main.transform.TransformDirection(Vector3.forward);
        targetController.lockButton = Input.GetKeyDown(KeyCode.Tab);
        targetController.cameraMode = regularCamera.CameraMode;

        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(abilityManager.spellButtons[i]) && abilityManager.equippedSpells[i] != -1)
            {
                DebugGUI.Print("You are pressing a bound spell button.");
                targetController.spellButton = true;
                targetController.spellBeingCast = (Spell)abilityManager.equippedSpells[i];
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
            }
        }
    }

    /// <summary>
    /// Resets all input. Should be done after the controller is disabled so that 
    /// your input doesn't "stick" to the last frame's input.
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
