using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public virtual void InteractWith(GameObject player)
    {
        PopupMessage.LocalDisplay("You have interacted with " + gameObject.name);
    }
}