namespace EternityGUI.Tests
{
    using UnityEngine;

    public class GridPanelTester : MonoBehaviour
    {
        GridContainer panel;
        void Start()
        {
            panel = GridContainer.Create(new Vector3(.2f, .2f), new Vector2(300, 300), 3, 3);
            for (int g = 0; g < 9; g++)
            {
                panel.AddChild(BaseElement.Create("GUI/Button", new Vector3()));
                panel.elements[g].Click += EternityGUITester_Click;
            }
        }

        void EternityGUITester_Click(BaseElement sender, MouseEventArgs e)
        {
            PopupMessage.LocalDisplay("You pressed element " + panel.elements.IndexOf(sender), .1f);
        }
    }

    [EternityGUITest]
    public class InventoryPanelTester : MonoBehaviour
    {
        InventoryPanel panel;
        bool initialized = false;

        void Update()
        {
            if (!initialized && Util.MyLocalPlayerObject)
            {
                panel = InventoryPanel.Create(Util.MyLocalPlayerObject.GetInventory(), new Vector3());
                panel.ItemClicked += panel_ItemClicked;

                initialized = true;
            }
        }

        void panel_ItemClicked(BaseElement sender, MouseEventArgs e)
        {
            PopupMessage.LocalDisplay("You pressed the icon for " + ((ItemElement)sender).item.name);
        }
    }
}