using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public abstract void InteractWith(GameObject player);
}