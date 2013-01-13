using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents a transition from one state to another. Every item in the list of requirements must be met for this transition to occur.
/// Note that a BehaviorState may have multiple transitions to the same state each with different requirements.
/// </summary>
public class StateTransition
{
    //public NPC_BehaviorState startingState; //I dont think we need this
    public NPC_BehaviorState nextState;
    public LinkedList<TransitionRequirement> requirements;

    public StateTransition(NPC_BehaviorState nextState, LinkedList<TransitionRequirement> requirements)
    {
        //this.startingState = startingState;
        this.nextState = nextState;
        this.requirements = requirements;
    }

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
    protected NPCStats npcStats;

    /// <summary>
    /// Returns whether this transition requirement is fulfilled
    /// </summary>
    /// <returns>True: The requirement is satisfied. False: The requirement is not satisfied</returns>
    public abstract bool IsSatisfied();
}