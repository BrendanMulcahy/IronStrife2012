using UnityEngine;

public abstract class AIBehaviourTreeBuilder : MonoBehaviour
{
    private void Start()
    {
        if (Network.isClient) return;

        GenerateBehaviourTree();
    }

    protected abstract void GenerateBehaviourTree();
}