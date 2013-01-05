using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    public Inventory inventory;
    public bool visible = false;
    public UIPanel inventoryPanel;

    void Awake()
    {
        inventory = Util.MyLocalPlayerObject.GetInventory();
        inventoryPanel = new UIPanel();
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