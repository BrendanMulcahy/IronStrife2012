using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents a transition from one state to another. Every item in the list of requirements must be met for this transition to occur.
/// Note that a BehaviorState may have multiple transitions to the same state each with different requirements.
/// </summary>
public class StateTransition
{
    public NPC_BehaviorState startingState;
    public NPC_BehaviorState endingState;
    public LinkedList<TransitionRequirement> requirements = new LinkedList<TransitionRequirement>();

    public bool CanTransition()
    {
        foreach (TransitionRequirement req in requirements)
        {
            if (!req.IsSatisfied())
            {
                return false;
            }
        }
        return true;
    }
}

/// <summary>
/// Represents a single requirement for a state transition to occur
/// </summary>
public abstract class TransitionRequirement
{
    /// <summary>
    /// Returns whether this transition requirement is fulfilled
    /// </summary>
    /// <returns>True: The requirement is satisfied. False: The requirement is not satisfied</returns>
    public virtual bool IsSatisfied();
}