using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public float interactionRange = 5f;

    public abstract void InteractWith(GameObject player);
}