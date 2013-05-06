using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class NPC_Controller : MonoBehaviour
{
    CharacterController characterController;
    float moveSpeed;
    NPCStats stats;

    public Vector3 targetLocation;
    public Transform targetTransform;
    private NavMeshAgent navMeshAgent;

    bool canMove = true;

    public void SetTarget(Vector3 location)
    {
        if (this.targetLocation != location)
        {
            this.targetLocation = location;
            this.targetTransform = null;
            UpdatePath();
        }
    }

    public void SetTarget(Transform transform)
    {
        if (this.targetTransform != transform)
        {
            this.targetTransform = transform;
            this.targetLocation = new Vector3();
            UpdatePath();
        }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    Vector3 moveDirection = new Vector3();
    Vector3 targetMoveDirection = new Vector3();

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

    private PlayerMotor motor;

    // Use this for initialization
    void Start()
    {
        if (Network.isClient) { this.enabled = false; return; }
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        moveDirection = Vector3.forward;
        motor = GetComponent<PlayerMotor>();
        stats = gameObject.GetCharacterStats() as NPCStats;
        stats.Damaged += stats_Damaged;
        stats.Died += stats_Died;

        navMeshAgent.speed = stats.MoveSpeed.ModifiedValue;
        navMeshAgent.stoppingDistance = 1.0f;
        navMeshAgent.angularSpeed = 220;
    }

    void stats_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        if (navMeshAgent)
            navMeshAgent.Stop();
        StopAllCoroutines();
        collider.enabled = false;
        gameObject.GetPlayerMotor().enabled = false;
        this.enabled = false;

    }

    void stats_Damaged(GameObject sender, DamageEventArgs e)
    {
        StartCoroutine(StopMovingForSeconds(.5f));
    }

    private IEnumerator StopMovingForSeconds(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePath();
        navMeshAgent.speed = canMove ? stats.MoveSpeed.ModifiedValue : 0;
    }

    private void UpdatePath()
    {
        var pos = targetTransform ? targetTransform.position : targetLocation;

        navMeshAgent.SetDestination(pos);


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
        if (motor.TotalImpactMagnitude >= .2f)
            return;
        characterController.SimpleMove(moveDirection * moveSpeed);
    }

    private void UpdateMoveDirection()
    {
        moveDirection = Vector3.RotateTowards(moveDirection, targetMoveDirection.normalized, 3.14f * Time.deltaTime, 1f);
        if (moveDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveDirection);
    }

    [RPC]
    public void BeginSwingAttack()
    {
        if (Network.isServer) networkView.RPC("BeginSwingAttack", RPCMode.Others);

        StartCoroutine(SwingAttack());
    }

    public void LookAtTarget(Vector3 target)
    {
        this.transform.LookAt(target);
    }

    private IEnumerator SwingAttack()
    {
        canMove = false;
        SendMessage("StartAttacking", SendMessageOptions.DontRequireReceiver);
        if (!stats) yield break;
        var swingLength = stats.attackDuration;
        yield return new WaitForSeconds(swingLength * .25f);
        GetComponentInChildren<WeaponCollider>().StartSwingCollisionChecking();

        yield return new WaitForSeconds(swingLength * .45f);
        GetComponentInChildren<WeaponCollider>().StopSwingCollisionChecking();
        SendMessage("StopAttacking", SendMessageOptions.DontRequireReceiver);
        canMove = true;
        yield break;
    }
}
