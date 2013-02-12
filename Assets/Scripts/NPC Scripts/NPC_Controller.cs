using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class NPC_Controller : MonoBehaviour
{
    CharacterController characterController;
    float moveSpeed;
    NPCStats stats;

    Vector3 targetLocation;
    Transform targetTransform;
    private NavMeshAgent navMeshAgent;

    public void SetTarget(Vector3 location)
    {
        this.targetLocation = location; 
        this.targetTransform = null; 
        UpdatePath();
    }
    public void SetTarget(Transform transform)
    {
        this.targetTransform = transform; 
        this.targetLocation = new Vector3(); 
        UpdatePath();
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    Vector3 moveDirection = new Vector3();
    Vector3 targetMoveDirection = new Vector3();

    private Vector3 TargetMoveDirection
    {
        get { return targetMoveDirection; }
        set { targetMoveDirection = value; }
    }

    private Vector3 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value; }
    }

    private PlayerMotor motor;

    // Use this for initialization
    void Start()
    {
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
            navMeshAgent.enabled = false;
        StopAllCoroutines();
        collider.enabled = false;
        gameObject.GetPlayerMotor().enabled = false;
    }

    void stats_Damaged(GameObject sender, DamageEventArgs e)
    {
        StartCoroutine(StopMovingForSeconds(.5f));
    }

    private IEnumerator StopMovingForSeconds(float time)
    {
        navMeshAgent.Stop();
        yield return new WaitForSeconds(time);
        navMeshAgent.Resume();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdatePath();
        //UpdateMoveDirection();
        //AvoidObstacles();
        navMeshAgent.speed = stats.MoveSpeed.ModifiedValue;
    }

    private void UpdatePath()
    {
        navMeshAgent.ResetPath();

        if (targetTransform != null)
            navMeshAgent.SetDestination(targetTransform.position);
        else
            navMeshAgent.SetDestination(targetLocation);

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
    private void Move()
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

    private IEnumerator SwingAttack()
    {
        navMeshAgent.Stop();
        SendMessage("StartAttacking", SendMessageOptions.DontRequireReceiver);
        var swingLength = stats.attackLength;
        yield return new WaitForSeconds(swingLength * .25f);
        GetComponentInChildren<WeaponCollider>().StartSwingCollisionChecking();

        yield return new WaitForSeconds(swingLength * .45f);
        GetComponentInChildren<WeaponCollider>().StopSwingCollisionChecking();
        SendMessage("StopAttacking", SendMessageOptions.DontRequireReceiver);
        navMeshAgent.Resume();
        yield break;
    }
}
