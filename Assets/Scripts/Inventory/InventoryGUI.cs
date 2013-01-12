using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    public Inventory inventory;
    public bool visible = false;

    void Awake()
    {
        inventory = Util.MyLocalPlayerObject.GetInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            visible = !visible;
        }
        if (visible)
        {

        }
    }
}