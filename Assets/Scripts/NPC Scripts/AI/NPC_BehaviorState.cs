using UnityEngine;
using System.Collections;

public abstract class NPC_BehaviorState : MonoBehaviour
{

    public abstract void Run();
    public abstract void Enable();
    public abstract void Disable();
//    public abstract NPC_BehaviorState NextState();

}
