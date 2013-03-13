using UnityEngine;

[DefaultSceneObject("MapController", "MapController")]
public class MapController : MonoBehaviour
{
    public RenderTexture mapRenderTexture;
    public Rect windowRect;
    bool visible = false;

    void Awake()
    {
        mapRenderTexture = Resources.Load("MapRenderTexture") as RenderTexture;
    }

    void Update()
    {
        windowRect = new Rect(Screen.width * .15f, Screen.height * .15f, Screen.width * .7f, Screen.height * .7f);
        if (Input.GetKeyDown(KeyCode.M))
        {
            visible = !visible;

        }
    }

    void OnGUI()
    {
        GUI.skin = Util.ISEGUISkin;
        if (visible)
        {           
            GUI.Window("map".GetHashCode(), windowRect, ShowMapWindow, GUIContent.none);
        }
    }

    void ShowMapWindow(int id)
    {
        GUILayout.BeginHorizontal(); 
        GUILayout.FlexibleSpace();
        GUILayout.Label(mapRenderTexture);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}