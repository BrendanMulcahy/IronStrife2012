using UnityEngine;
using System.Collections;

public class Wander : NPC_BehaviorState
{
    NPC_Controller npcController;

    void Start ()
    {
        npcController = GetComponent<NPC_Controller>();
    }

    public override void Run()
    {
        DebugGUI.print("NPC is wandering.");
        if (ShouldWaitBriefly())
        {
            WaitABit();
        }
        else
        {
            RandomWalk();
        }
        StartCoroutine("CheckIfContinueWandering");
    }

    /// <summary>
    /// Causes the NPC to walk in a random direction
    /// </summary>
    private void RandomWalk()
    {
        DebugGUI.print("NPC is walking.");
        npcController.MoveDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
        npcController.Walk();
    }

    /// <summary>
    /// Waits for 5 seconds before continuing to wander
    /// </summary>
    private void WaitABit()
    {
        DebugGUI.print("NPC is waiting.");
        npcController.StopMoving();
    }

    /// <summary>
    /// Randomly decides if the NPC should wait briefly before walking more
    /// </summary>
    /// <returns>true if the AI should wait, false otherwise</returns>
    private bool ShouldWaitBriefly()
    {
        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator CheckIfContinueWandering()
    {
        yield return new WaitForSeconds(5.0f);
        if (GetComponent<NPC_AI>().CurrentState is Wander)
        {
            Run();
        }
    }
}