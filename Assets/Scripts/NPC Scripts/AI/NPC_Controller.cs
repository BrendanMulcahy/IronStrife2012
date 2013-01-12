using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class NPC_Controller : MonoBehaviour 
{
    CharacterController characterController;
    float moveSpeed;

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    float walkSpeed;
    Vector3 moveDirection;
    Vector3 targetMoveDirection;

    public Vector3 TargetMoveDirection
    {
        get { return targetMoveDirection; }
        set { targetMoveDirection = value; }
    }

    public Vector3 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value; }
    }

	// Use this for initialization
	void Start ()
    {
        characterController = GetComponent<CharacterController>();
        walkSpeed = GetComponent<NPC_AI>().WalkSpeed;
        moveDirection = Vector3.forward;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMoveDirection();
	}

    /// <summary>
    /// Makes the NPC walk forward at its walk speed
    /// </summary>
    public void Move()
    {
        characterController.SimpleMove(moveDirection * moveSpeed);
    }

    private void UpdateMoveDirection()
    {
        moveDirection = Vector3.RotateTowards(moveDirection, targetMoveDirection, 3.14f * Time.deltaTime, 1f);
        transform.rotation = Quaternion.LookRotation(moveDirection);
    }
}
