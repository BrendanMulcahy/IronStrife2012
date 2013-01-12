namespace EternityGUI
{
    using UnityEngine;

    public class GridContainer : UIElementContainer
    {
        public float width;
        public float height;

        public int numColumns;
        public int numRows;

        public int columnWidth;
        public int rowHeight;

        Vector2 padding;

        public bool resize = false;
        public bool resizeElementsToFit = false;
        public bool revertToDefaultElementSize = false;

        public Alignment alignment = Alignment.BottomLeft;

        void Update()
        {
            if (resize)
                ResizeElements();
            if (resizeElementsToFit)
                ResizeElementsToFit();
            if (revertToDefaultElementSize)
                RevertToDefaultElementSize();

            var counter = 0;

            for (int row = 0; row < numRows; row++)
            {
                for (int column = 0; column < numColumns; column++)
                {
                    elements[counter].transform.localPosition = new Vector3(column * columnWidth, row * rowHeight).ScreenToViewport();
                    counter++;
                    if (counter >= elements.Count)
                        return;
                }
            }
            CorrectLayer();
        }

        private void CorrectLayer()
        {
            var pos = transform.position;
            pos.z = EternityUtil.GetElementLayer(this.gameObject);
            transform.position = pos;
        }

        public static GridContainer Create(Vector3 position, Vector2 dimensions, int numColumns, int numRows)
        {
            var go = new GameObject("GridContainer");
            var panel = go.AddComponent<GridContainer>();
            panel.width = dimensions.x; panel.height = dimensions.y;
            panel.numColumns = numColumns;
            panel.numRows = numRows;
            panel.transform.position = position;

            panel.columnWidth = (int)(dimensions.x / numColumns);
            panel.rowHeight = (int)(dimensions.y / numRows);

            return panel;
        }

        public void ResizeElements()
        {
            this.columnWidth = (int)(width / numColumns);
            this.rowHeight = (int)(height / numRows);

            resize = false;

        }

        public void ResizeElementsToFit()
        {
            foreach (BaseElement element in elements)
            {
                element.Resize(columnWidth, rowHeight);
            }
            resizeElementsToFit = false;
        }

        public void RevertToDefaultElementSize()
        {
            foreach (BaseElement element in elements)
            {
                element.ResetSize();
            }
            revertToDefaultElementSize = false;
        }

    }
}