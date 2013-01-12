namespace EternityGUI
{
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
            while (t != root)
            {
                t = t.parent;
                toReturn--;
            }
            return toReturn;
        }
    } 
}