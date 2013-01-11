namespace EternityGUI
{
    using UnityEngine;

    public class GridPanel : UIElementContainer
    {
        public float width;
        public float height;

        public int numColumns;
        public int numRows;

        public float columnWidth;
        public float rowHeight;

        Vector2 padding;

        void Update()
        {
            var counter = 0;
            for (int row = 0; row < numRows; row++)
            {
                for (int column = 0; column < numColumns; column++)
                {
                    elements[counter].transform.position = transform.position + (new Vector3(column * columnWidth, row * rowHeight)).ScreenToViewport();
                    counter++;
                }
            }
        }

        public static GridPanel Create(Vector3 position, Vector2 dimensions, int numColumns, int numRows)
        {
            var go = new GameObject("GridPanel");
            var panel = go.AddComponent<GridPanel>();
            panel.width = dimensions.x; panel.height = dimensions.y;
            panel.numColumns = numColumns;
            panel.numRows = numRows;
            panel.transform.position = position;

            panel.columnWidth = dimensions.x / numColumns;
            panel.rowHeight = dimensions.y / numRows; 

            return panel;
        }
    }
}