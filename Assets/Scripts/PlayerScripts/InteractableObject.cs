using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    /// <summary>
    /// The distance at which an object can be interacted with.
    /// </summary>
    public float interactionRange = 5f;

    public abstract void InteractWith(GameObject player);
}