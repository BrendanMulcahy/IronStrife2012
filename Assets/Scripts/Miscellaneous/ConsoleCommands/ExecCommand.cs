using UnityEngine;

class ExecCommand : ConsoleCommand
{

    public override string[] Names
    {
        get { string[] names = {"exec"}; return names; }
    }

    public override string HelpMessage
    {
        get { return "Executes an arbitrary string as a function"; }
    }

    public override void Execute(params string[] parameters)
    {
        if (parameters.Length == 0)
            CreateExecWindow();
        var singleString = "";
        foreach (string s in parameters)
        {
            singleString += s + " ";
        }
        ExecuteCommand(singleString);
    }

    private void CreateExecWindow()
    {
        if (!GameObject.Find("ExecInputWindow"))
            new GameObject("ExecInputWindow").AddComponent<ExecInputWindow>();
    }

    public static void ExecuteCommand(string command)
    {
        GameObject executor = GameObject.Find("Executor");
        if (!executor)
        {
            executor = new GameObject("Executor");
            executor.AddComponent("StringExecutor");
        }
        executor.SendMessage("ExecuteCommand", command);
    }
}

public class ExecInputWindow : MonoBehaviour
{
    Rect windowRect;
    void Start() { windowRect = new Rect(50, 50, 400, 300); }
    string inputArea = "";

    void OnGUI()
    {
        if (DebugGUI.visible)
            windowRect = GUI.Window(int.MaxValue, windowRect, ShowInputWindow, GUIContent.none);
    }

    void ShowInputWindow(int id)
    {
        GUILayout.BeginVertical();
        inputArea = GUILayout.TextArea(inputArea);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Exit"))
            Destroy(this.gameObject);
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Execute"))
        {
            ExecCommand.ExecuteCommand(inputArea);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUI.DragWindow();
    }

}