using UnityEngine;

public class Popup
{
    static int popupListHash = "PopupList".GetHashCode();

    public static bool List(Rect position, ref bool showList, ref int listEntry, GUIContent buttonContent, GUIContent[] listContent,
                             GUIStyle listStyle)
    {
        return List(position, ref showList, ref listEntry, buttonContent, listContent, "button", "box", listStyle);
    }

    public static bool List(Rect position, ref bool showList, ref int listEntry, GUIContent buttonContent, GUIContent[] listContent,
                             GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
    {
        int controlID = GUIUtility.GetControlID(popupListHash, FocusType.Passive);
        bool done = false;

        GUILayout.Label(buttonContent);
        position = GUILayoutUtility.GetLastRect();
        switch (Event.current.GetTypeForControl(controlID))
        {
            case EventType.mouseDown:
                if (position.Contains(Event.current.mousePosition))
                {
                    GUIUtility.hotControl = controlID;
                    showList = true;
                }
                break;
            case EventType.mouseUp:
                if (showList)
                {
                    done = true;
                }
                break;
        }
        if (showList)
        {
            //Rect listRect = new Rect(position.x, position.y, position.width, listStyle.CalcHeight(listContent[0], 1.0f) * listContent.Length);
            GUILayout.Box("", boxStyle);
            listEntry = GUILayout.SelectionGrid(listEntry, listContent, 1, listStyle);
        }
        if (done)
        {
            showList = false;
        }
        return done;
    }
}