namespace EternityGUI
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class EternityUtil
    {
        public static Vector3 ScreenToViewport(this Vector3 v)
        {
            return new Vector3(v.x / Screen.width, v.y / Screen.height, v.z);
        }

        public static int GetElementLayer(GameObject go)
        {
            var t = go.transform;
            var root = go.transform.root;
            int toReturn = -1;
            if (go.GetComponent<RenderTextureElement>())
                toReturn++;

            while (t != root)
            {
                t = t.parent;
                toReturn--;
            }
            return toReturn;
        }

        private static Camera _inventoryCamera;
        public static Camera InventoryCamera
        {
            get
            {
                if (!_inventoryCamera)
                {
                    _inventoryCamera = Util.MyLocalPlayerObject.transform.FindChild("InventoryPreviewCamera").GetComponent<Camera>();
                }
                return _inventoryCamera;
            }
        }

        public static LinkedList<GUIElement> HitTestAll(this GUILayer guiLayer, Vector3 position)
        {
            var toReturn = new LinkedList<GUIElement>();
            var previousPositions = new LinkedList<Vector3>();
            GUIElement element;
            while ((element = guiLayer.HitTest(position)) != null)
            {
                toReturn.AddLast(element);
                previousPositions.AddLast(element.gameObject.transform.position);
                element.transform.position = new Vector3(-100, -100, -100);
            }

        }


    }
}