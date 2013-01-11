namespace EternityGUI
{
    using UnityEngine;

    public static class EternityUtil
    {
        public static Vector3 ScreenToViewport(this Vector3 v)
        {
            return new Vector3(v.x / Screen.width, v.y / Screen.height);
        }
    } 
}