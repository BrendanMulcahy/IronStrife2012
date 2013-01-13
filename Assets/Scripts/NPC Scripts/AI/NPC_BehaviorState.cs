using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPC_BehaviorState : MonoBehaviour
{
    public List<StateTransition> transitions = new List<StateTransition>();

    public abstract void Run();
    public abstract void Enable();
    public abstract void Disable();
//    public abstract NPC_BehaviorState NextState();

}
