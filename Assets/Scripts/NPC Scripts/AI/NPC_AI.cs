using UnityEngine;
using System.Collections;


/// <summary>
/// A basic AI that uses a typical finite state machine to determine AI behavior
/// </summary>
[RequireComponent(typeof(NPC_Controller))]
public class NPC_AI : MonoBehaviour
{
    private float walkSpeed = 5.0f;  //the speed this AI typically walks at

    public float WalkSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }

    private GameObject lastSeenEnemy;

    public GameObject LastSeenEnemy
    {
        get { return lastSeenEnemy; }
        set { lastSeenEnemy = value; }
    }

    public NPC_BehaviorState currentState; //the current behavior of the AI is dependent on its state

    public NPC_BehaviorState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    // Use this for initialization
    void Start()
    {
        currentState = GetComponent<NPC_BehaviorState>();
        lastSeenEnemy = GameObject.Find("Brendan");

    }

    // Update is called once per frame
    void Update()
    {
        currentState.Run();
        TransitionState();
        CheckForEnemy();
    }

    private void CheckForEnemy()
    {
        //not sure if this layer mask is correct, just copied it from elsewhere
        var layerMask = 1 << 8;
        layerMask += (1 << 9);
        layerMask += (1 << 10);
        layerMask = ~layerMask;

        //if there is obstacle in front
        //RaycastHit hit;
       // if (Physics.SphereCast(transform.position, out hit, transform.rotation, 4.0f, layerMask))
       // {
    }

    private void TransitionState()
    {
        foreach (StateTransition transition in currentState.transitions)
        {
            //should probably prioritize states with a value of some sort
            if (transition.CanTransition())
            {
                currentState.Disable();
                currentState = transition.nextState;
                currentState.Enable();
                return;
            }
        }
    }
}
