namespace EternityGUI
{
    using UnityEngine;

    public class InventoryPanel : Panel
    {
        public Inventory inventory;
        public PlayerStats stats;

        public event MouseEventHandler ItemClicked;

        public GridContainer itemGrid;

        public static InventoryPanel Create(Inventory inventory, Vector3 position)
        {
            var inventoryPanel = new GameObject(inventory.gameObject.name + "InventoryPanel").AddComponent<InventoryPanel>();
            inventoryPanel.inventory = inventory;
            inventoryPanel.inventory.ItemAdded += inventoryPanel.inventory_ItemAdded;
            inventoryPanel.width = 800;
            inventoryPanel.height = 450;
            inventoryPanel.transform.position = position.ScreenToViewport();
            inventoryPanel.gameObject.AddComponent<GUITexture>().texture = Resources.Load("GUI/MainMenuLargeBG") as Texture2D;

            var itemGrid = GridContainer.Create(new Vector3(), new Vector2(400, 400), 3, 3);
            inventoryPanel.itemGrid = itemGrid;
            itemGrid.resizeElementsToFit = true;
            inventoryPanel.AddContainer(itemGrid);
            itemGrid.transform.position = new Vector3(375, 25, EternityUtil.GetElementLayer(itemGrid.gameObject)).ScreenToViewport();

            foreach (Item i in inventoryPanel.inventory.Items)
            {
                inventoryPanel.AddItemToGrid(i);
            }

            return inventoryPanel;
        }

        void inventory_ItemAdded(Inventory sender, Item newItem)
        {
            AddItemToGrid(newItem);
        }

        private void AddItemToGrid(Item i)
        {
            var itemElement = ItemElement.Create(i);
            itemGrid.AddChild(itemElement);
            itemElement.Click += ItemButton_Click;
        }

        void ItemButton_Click(BaseElement sender, MouseEventArgs e)
        {
            if (ItemClicked != null)
                ItemClicked(sender, e);

        }

    }
}