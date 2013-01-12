using UnityEngine;
using System.Collections;

/// <summary>
/// A basic AI that uses a typical finite state machine to determine AI behavior
/// </summary>
public class NPC_AI : MonoBehaviour {
    private float walkSpeed = 5.0f;  //the speed this AI typically walks at

    public float WalkSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }

    private NPC_BehaviorState currentState; //the current behavior of the AI is dependent on its state

    public NPC_BehaviorState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }
    private bool stateHasChanged = true; //true if the state has changed recently

	// Use this for initialization
	void Start () {
        currentState = GetComponent<NPC_BehaviorState>();
	}
	
	// Update is called once per frame
	void Update () {
        if (stateHasChanged)
        {
            stateHasChanged = false;
            currentState.Run();
        }
	}

    /// <summary>
    /// Changes the AI's current state to the newState
    /// </summary>
    /// <param name="newState">The new behavior state the AI should enter</param>
    public void ChangeState(NPC_BehaviorState newState)
    {
        currentState = newState;
        stateHasChanged = true;
    }
}
