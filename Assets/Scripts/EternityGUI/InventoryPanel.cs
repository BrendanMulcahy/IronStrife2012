namespace EternityGUI
{
    using UnityEngine;

    public class InventoryPanel : Panel
    {
        public Inventory inventory;
        public PlayerStats stats;

        public event MouseEventHandler ItemClicked;

        public GridContainer itemGrid;
        public RenderTextureElement modelPreviewElement;

        protected override float TextureRatio
        {
            get
            {
                if (_textureRatio == -1)
                {
                    _textureRatio = (float)background.guiTexture.texture.width / (float)background.guiTexture.texture.height;

                }
                return _textureRatio;
            }
        }

        public static InventoryPanel Create(Inventory inventory, Vector3 position)
        {
            var inventoryPanel = new GameObject(inventory.gameObject.name + "InventoryPanel").AddComponent<InventoryPanel>();
            inventoryPanel.gameObject.layer = 12;
            inventoryPanel.inventory = inventory;
            inventoryPanel.inventory.ItemAdded += inventoryPanel.inventory_ItemAdded;
            inventoryPanel.width = 750;

            inventoryPanel.transform.position = position.ScreenToViewport();

            inventoryPanel.GenerateBackgroundImage();

            inventoryPanel.Resize(inventoryPanel.width, inventoryPanel.width);

            var itemGrid = GridContainer.Create(new Vector3(), new Vector2(inventoryPanel.width /2 - 50, inventoryPanel.height - 30), 3, 3);
            inventoryPanel.itemGrid = itemGrid;
            itemGrid.resizeElementsToFit = true;
            inventoryPanel.AddContainer(itemGrid);
            itemGrid.transform.position = new Vector3(inventoryPanel.width / 2 + 25, 15, EternityUtil.GetElementLayer(itemGrid.gameObject)).ScreenToViewport();
            itemGrid.transform.localScale = new Vector3(1, 1, 1);
            foreach (Item i in inventoryPanel.inventory.Items)
            {
                inventoryPanel.AddItemToGrid(i);
            }

            var rte = RenderTextureElement.Create(EternityUtil.InventoryCamera.targetTexture, inventoryPanel.width / 2 -80, inventoryPanel.height -100);
            inventoryPanel.modelPreviewElement = rte;
            rte.transform.parent = inventoryPanel.transform;
            rte.transform.position = new Vector3(40, 50).ScreenToViewport();

            inventoryPanel.gameObject.SetActive(false);
            return inventoryPanel;
        }

        private void GenerateBackgroundImage()
        {
            var baseElement = BaseElement.Create("GUI/MainMenuLargeBGCropped", new Vector3());
            baseElement.gameObject.layer = 12;
            baseElement.layerOffset = -1;
            background = baseElement.gameObject;
            background.transform.parent = transform;
            background.transform.position = new Vector3();

            baseElement.MouseDown += baseElement_MouseDown;
            baseElement.MouseUp += baseElement_MouseUp;

        }

        void baseElement_MouseUp(BaseElement sender, MouseEventArgs e)
        {
            this.OnMouseUp();
        }

        void baseElement_MouseDown(BaseElement sender, MouseEventArgs e)
        {
            this.OnMouseDown();
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
            itemElement.Dropped += itemElement_Dropped;
        }

        void itemElement_Dropped(BaseElement sender, MouseDropEventArgs e)
        {
            if (e.handled) return;

            if (e.dropTarget is ItemElement)
            {
                Debug.Log("You dropped onto another item.");
                var indexOfDragged = itemGrid.elements.IndexOf(e.draggedObject);
                var indexOfDropped = itemGrid.elements.IndexOf(e.dropTarget);

                itemGrid.elements[indexOfDragged] = e.dropTarget;
                itemGrid.elements[indexOfDropped] = e.draggedObject;
            }
        }

        void ItemButton_Click(BaseElement sender, MouseEventArgs e)
        {
            if (ItemClicked != null)
                ItemClicked(sender, e);

        }

        internal override void ResetSize()
        {
            var tex = background.guiTexture.texture;
            background.guiTexture.pixelInset = new Rect(0, 0, tex.width, tex.height);
        }

        internal override void Resize(int newWidth, int newHeight)
        {
            this.transform.localScale = new Vector3(1,1,1);
            var newInset = new Rect(0, 0, newWidth, newHeight);
            if (preserveAspectRatio)
            {
                if (background.guiTexture.texture.width > background.guiTexture.texture.height)
                {
                    newInset.height = newInset.width * (1 / TextureRatio);
                    
                }
                else
                {
                    newInset.width = newInset.height * (TextureRatio);
                }
            }
            background.guiTexture.pixelInset = newInset;
            this.height = (int)newInset.height;
            this.width = (int)newInset.width;
        }

    }
}