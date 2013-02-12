using UnityEngine;

/// <summary>
/// Base Behaviour for handling NPCs that spawn from a neutral wave, which move towards a control point
/// </summary>
public class Assault : NPC_BehaviorState
{
    private GameObject target;

    public GameObject Target
    {
        get { return target; }
        set
        {
            target = value;
            if (GetComponent<NPC_AI>().CurrentState == this)
            {
                GetComponent<NPC_Controller>().SetTarget(value.transform);
            }
        }
    }

    public override void Run()
    {

    }

    public override void Enable()
    {
        GetComponent<NPC_Controller>().SetTarget(target.transform);
    }

    public override void Disable()
    {

    }
}