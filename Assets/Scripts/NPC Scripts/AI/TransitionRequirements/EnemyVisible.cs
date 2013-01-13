using UnityEngine;
using System.Collections.Generic;

public class EnemyVisible : TransitionRequirement
{
    /// <summary>
    /// 
    /// </summary>
    public List<GameObject> charactersNearby;

    public override bool IsSatisfied()
    {
        return false;
    }
}
