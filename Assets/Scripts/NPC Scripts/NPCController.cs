using UnityEngine;
using System.Collections;
using System;
using System.Linq;

/// <summary>
/// NPC controller
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class NPCController : MonoBehaviour {
    public GameObject Firstplayer;

    //if an enemy as further than maxDistance from you, it cannot see you
    public float maxDistance = 20;
    public float maxDistanceSquared;
    public bool playerseen = false;


    public float wanderspeed = 3;
    public float followspeed = 5;
    public float currentspeed;
    public float directionChangeInterval = 3;

    protected bool checkingForPlayer = true;

    public int currenthealth = 100;

    float heading;
    Vector3 targetRotation;

    public float attackTimer = 0;
    public bool isAttacking = false;
    public bool dead = false;


    void Awake()
    {
        maxDistanceSquared = maxDistance * maxDistance;
        
        // Set random initial rotation
        heading = UnityEngine.Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        currentspeed = 0;

        //StartCoroutine(CheckforPlayer());
    }

    void Start()
    {
        gameObject.GetCharacterStats().Died += NPCDied;
        StartCoroutine(CheckforPlayer());
    }


    void Update()
    {
        if (!isAttacking && !dead)
        {
            if (!playerseen)
            {
                Wandering();
            }
            else if (playerseen)
            {
                FollowPlayer();
            }


            ////Check to see if a player is within sight distance
            //if (!playerseen && !dead)
            //{
            //    CheckforPlayer();
            //}

            if (Firstplayer != null)
            {
                if ((Firstplayer.transform.position - this.transform.position).sqrMagnitude > (maxDistanceSquared))
                {
                    playerseen = false;
                    Firstplayer = null;
                }
            }
        }

        directionChangeInterval -= Time.deltaTime;

        attackTimer -= Time.deltaTime;
        //Stop attack animation after 0.5 seconds
        if (attackTimer <= .8 && isAttacking)
        {
            GetComponentInChildren<WeaponCollider>().isActive = false;
            GetComponent<NPCAnimation>().ToggleAttackAnimation();
            isAttacking = false;
        }

    }


    //start attacking
    public void Attack()
    {
        GetComponentInChildren<WeaponCollider>().StartSwingCollisionChecking();
        currentspeed = 0;
        isAttacking = true;
        GetComponent<NPCAnimation>().ToggleAttackAnimation();
        attackTimer = 1.5f;
    }


    /// Calculates a new direction to move towards.
    protected virtual void Wandering()
    {
        currentspeed = wanderspeed;
        if (directionChangeInterval <= 0)
        {
            heading = UnityEngine.Random.Range(-90, 90);
            transform.Rotate(Vector3.up * heading);
            directionChangeInterval = UnityEngine.Random.Range(2, 5);
        }
        //checks to see if been hit from behind then turns around
        if (GetComponent<CharacterStats>().Health < currenthealth)
        {
            transform.Rotate(Vector3.up * 180);
            currenthealth = GetComponent<CharacterStats>().Health;
        }
        GetComponent<CharacterController>().SimpleMove(transform.TransformDirection(Vector3.forward) * currentspeed);
    }

    //Function for NPC to follow player by rotating towards and moving forward
    protected void FollowPlayer()
    {
        //find direction vector from npc to player and rotates the npc to face that direction and move forward
        Vector3 direction = Firstplayer.transform.localPosition - this.transform.localPosition;
        if (direction.sqrMagnitude > 4)
        {
            currentspeed = followspeed;
        }
        else
            currentspeed = 0;
        //Turn towards the players supposed position on ground if jumping.
        var targetDirection = Quaternion.LookRotation((direction.x * Vector3.right) + (direction.z * Vector3.forward));
        transform.rotation = targetDirection;
        GetComponent<CharacterController>().SimpleMove(transform.TransformDirection(Vector3.forward) * currentspeed);
    }

    //Check for a player in the vicinity and in eyesight
    protected virtual IEnumerator CheckforPlayer()
    {
        while (checkingForPlayer)
        {
            yield return new WaitForSeconds(1f);
            //First looks through all gameobjects and finds the player by finding the object with characterstats
            foreach (GameObject gameObj in GameObject.FindObjectsOfType(typeof(GameObject)))
            {

                if (gameObj.GetComponent<ThirdPersonController>() != null)
                {

                    Firstplayer = gameObj;

                    //check to see if player is within a certain distance of player and within 180 degrees of sight
                    Vector3 rayDirection = gameObj.transform.localPosition - this.transform.localPosition;
                    Vector3 enemyDirection = transform.TransformDirection(Vector3.forward);
                    var angleDot = Vector3.Dot(rayDirection, enemyDirection);
                    var playerInFrontOfEnemy = angleDot > 0.0;
                    var playerCloseToEnemy = rayDirection.sqrMagnitude < maxDistanceSquared;

                    if (playerInFrontOfEnemy && playerCloseToEnemy)
                    {
                        playerseen = true;
                        currentspeed = followspeed;
                        FollowPlayer();
                        break;
                    }

                }
            }
        }

    }



    //Death Function
    IEnumerator Die()
    {
        dead = true;
        GetComponent<NPCAnimation>().Die();
        Destroy(gameObject.GetDamageReceiver());
        currentspeed = 0;
        playerseen = false;
        yield return new WaitForSeconds(10);
        networkView.RPC("SelfDestruct", RPCMode.All);
    }

    void NPCDied(GameObject sender, UnitDiedEventArgs e)
    {
        CharacterStats.RewardPlayersInArea(e.deathPosition, e.killer, e.reward);
        StartCoroutine(Die());
    }


    [RPC]
    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public enum NPCState
    {
        Attacking,
        Wandering,
        Following,
        Dead
    }



    
}