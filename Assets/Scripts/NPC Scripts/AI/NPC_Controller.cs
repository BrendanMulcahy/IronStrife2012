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
        moveDirection = Vector3.forward;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMoveDirection();
        AvoidObstacles();
	}

    private void AvoidObstacles()
    {
        //not sure if this layer mask is correct, just copied it from elsewhere
        var layerMask = 1 << 8;
        layerMask += (1 << 9);
        layerMask += (1 << 10);
        layerMask = ~layerMask;

        //if there is obstacle in front
        if (Physics.Raycast(transform.position, moveDirection, 4.0f, layerMask))
        {
            //if right is free
            if (!Physics.Raycast(transform.position, Vector3.Cross(moveDirection, Vector3.up), 4.0f, layerMask))
            {
                targetMoveDirection = Vector3.Cross(moveDirection, Vector3.up);
            }
            //else if left is free
            else if (!Physics.Raycast(transform.position, Vector3.Cross(moveDirection, Vector3.down), 4.0f, layerMask))
            {
                targetMoveDirection = Vector3.Cross(moveDirection, Vector3.down);
            }
            else
            {
                //turn around
                targetMoveDirection = Vector3.Reflect(moveDirection, Vector3.up);
            }
        }

        if (Physics.Raycast(transform.position, moveDirection, 2.0f, layerMask))
        {
            moveSpeed = 0.0f;
        }
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
