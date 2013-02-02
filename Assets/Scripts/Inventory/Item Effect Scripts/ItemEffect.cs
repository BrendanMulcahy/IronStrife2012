using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    public string[] parameters;
    public string[] stringParameters;

    public abstract void ActivateEffect();

    void Start()
    {
        ActivateEffect();
    }
}