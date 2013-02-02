using UnityEngine;
using System.Collections;


/// <summary>
/// A basic AI that uses a typical finite state machine to determine AI behavior
/// </summary>
public class NPC_AI : MonoBehaviour
{
    private float walkSpeed = 5.0f;  //the speed this AI typically walks at

    public float WalkSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }

    private EnemySearcher enemySearcher;

    public EnemySearcher Searcher
    {
        get { return enemySearcher; }
        set { enemySearcher = value; }
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
        if (Network.isClient)
        {
            Util.Destroy(GetComponents<NPC_BehaviorState>());
            Destroy(GetComponent<NPCGenericAnimation>());
            Destroy(this);
        }

        AddEnemySearcher();
        gameObject.GetCharacterStats().Died += NPC_AI_Died;

    }

    void NPC_AI_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        if (deadUnit)
        {
            Util.Destroy(deadUnit.GetComponents<NPC_BehaviorState>());
            currentState = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
		if (currentState == null) return;
        currentState.Run();
        TransitionState();
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
	
	    private void AddEnemySearcher()
    {
        var sphereGO = new GameObject("EnemySearcher");
        sphereGO.layer = 16;
        sphereGO.transform.SetParentAndCenter(gameObject.transform);
        var searcher = sphereGO.AddComponent<EnemySearcher>();
		enemySearcher = searcher;
    }
	
	public void SetInitialState(NPC_BehaviorState behavior)
	{
        this.currentState = behavior;
        currentState.Enable();
	}
}
