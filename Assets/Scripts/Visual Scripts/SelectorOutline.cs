using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EternityGUI;

public class SelectorOutline : MonoBehaviour
{
    GameObject outlineGameObject;
    int screenWidth;
    int screenHeight;
    GUITexture texture;

    void Start()
    {
        outlineGameObject = new GameObject("BoxOutline");
        outlineGameObject.transform.SetParentAndCenter(this.transform);
        texture = outlineGameObject.AddComponent<GUITexture>();
        texture.texture = Resources.Load("GUI/SelectorBox") as Texture2D;
        texture.transform.localScale = new Vector3();
    }

    void OnDestroy()
    {
        Destroy(outlineGameObject);
    }

    void LateUpdate()
    {
        var bounds = collider.bounds;
        var screenMin = Camera.main.WorldToScreenPoint(bounds.min);
        var screenMax = Camera.main.WorldToScreenPoint(bounds.max);
        screenWidth = (int)((screenMax.y - screenMin.y) * 1.4f);
        screenHeight = (int)(screenMax.x - screenMin.x) * 2;

        var screenPos = Camera.main.WorldToScreenPoint(bounds.center).ScreenToViewport();

        var rect = new Rect(-screenWidth * .5f, -screenHeight*.5f, screenWidth, screenHeight);
        texture.pixelInset = rect;

        texture.transform.position = screenPos;
    }

}