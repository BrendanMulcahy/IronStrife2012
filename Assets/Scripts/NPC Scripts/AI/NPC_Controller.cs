using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class NPC_Controller : MonoBehaviour 
{
    CharacterController characterController;
    float moveSpeed;
    float walkSpeed;
    Vector3 moveDirection;

    public Vector3 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value; }
    }

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
        walkSpeed = GetComponent<NPC_AI>().WalkSpeed;
        moveDirection = Vector3.forward;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<CharacterController>().SimpleMove(moveDirection * moveSpeed);
	}

    /// <summary>
    /// Makes the NPC walk forward at its walk speed
    /// </summary>
    public void Walk()
    {
        moveSpeed = walkSpeed;
    }

    /// <summary>
    /// Causes the NPC to stop moving and stand still
    /// </summary>
    public void StopMoving()
    {
        moveSpeed = 0.0f;
    }

    /// <summary>
    /// Changes the NPCs move direction
    /// </summary>
    /// <param name="direction">vector pointing in the direction to move towards</param>
    public void ChangeDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
}
