using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    public float[] parameters;

    public abstract void ActivateEffect();

    void Start()
    {
        ActivateEffect();
    }
}