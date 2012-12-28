using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Class for handling debug commands and displaying debug messages. 
/// Use the Debug.Log() method to print to this window.
/// </summary>
public class DebugGUI : MonoBehaviour {
	
	public ArrayList entries;
	private Rect window = new Rect(60, 60, 600, 400);
	private Vector2 scrollPosition;
	public static bool visible = false;
	private String consoleInputField = "";
	private bool consoleInputFieldFocused = false;
    private LinkedList<String> history = new LinkedList<string>();
    private LinkedListNode<string> selectedHistory;
    private Vector3 mousePosition;
    GUISkin skin;
    private string currentTooltip = "";

    private Dictionary<string, ConsoleCommand> commands;

    private const int maxHistoryCount = 40;

    private static DebugGUI instance;
    public static DebugGUI Main { get { if (instance == null) instance = GameObject.Find("DebugGUI").GetComponent<DebugGUI>(); return instance; } }

	class ConsoleCommandSubmission
	{
		public String commandName;
		public String[] parameters;
	
		public ConsoleCommandSubmission(String setName, String[] setParams)
		{
			commandName = setName;
			parameters = setParams;
		}
	}

    class DebugEntry
    {
        public string message;
        public string stackTrace;
        public LogType type;
    }

    void Awake()
    {
        entries = new ArrayList();
        LoadAllConsoleCommands();
        skin = Resources.Load("ISEGUISkin") as GUISkin;
    }

    // Use this for initialization
    void OnEnable()
    {
        UnityEngine.Application.RegisterLogCallback(new Application.LogCallback(MyCallback));
    }

    private void MyCallback(string condition, string stacktrace, UnityEngine.LogType type)
    {
        if (condition[condition.Length - 1] == '\n')
            condition = condition.Substring(0, condition.Length - 1);
        if (!condition.Contains("Sent RPC call"))
            this.AddEntry(condition, stacktrace, type);
    }
	
	void Start()
	{

	}

    private void LoadAllConsoleCommands()
    {
        commands = new Dictionary<string, ConsoleCommand>();
        foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (t.IsSubclassOf(typeof(ConsoleCommand)))
            {
                ConsoleCommand cc = Activator.CreateInstance(t) as ConsoleCommand;
                foreach (string s in cc.Names)
                    commands[s] = cc;
            }
        }
    }

	void OnGUI()
	{
		if (visible)
		{
			window = GUI.Window (3, window, ConsoleWindow, "Debug");

            if (currentTooltip != "")
            {
                GUI.Window(4, new Rect(mousePosition.x, Screen.height - mousePosition.y, Screen.width - mousePosition.x - 25, Screen.height - (Screen.height - mousePosition.y) - 25), TooltipWindow, GUIContent.none, skin.GetStyle("debugTooltipWindow"));
                GUI.BringWindowToFront(4);
            }
		}
		
		Event e = Event.current;

        if (consoleInputFieldFocused && e.type == EventType.keyDown && e.keyCode == KeyCode.UpArrow)
        {
            Debug.Log("moving history up");
            if (selectedHistory == null)
            {
                selectedHistory = history.First;
                if (selectedHistory != null)
                {
                    consoleInputField = selectedHistory.Value;
                    return;
                }
            }
            if (selectedHistory != null)
            {
                selectedHistory = selectedHistory.Next;
                if (selectedHistory != null)
                    consoleInputField = selectedHistory.Value;
            }
        }

	    if (e.type == EventType.MouseDown && !window.Contains(e.mousePosition))
	    {
			consoleInputFieldFocused = false;
	    }
	}

    private void TooltipWindow(int id)
    {
        GUILayout.Label(currentTooltip, skin.GetStyle("debugLabelBlack"));
    }
	
	void Update () 
	{
        if (visible)
        {
            if (consoleInputField.StartsWith("`"))
            {
                consoleInputField = "";
            }

            mousePosition = Input.mousePosition;
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            BackQuotePressed();
        }

        if (consoleInputFieldFocused && Input.GetKeyDown(KeyCode.DownArrow))
        {   
            if (selectedHistory!=null)
            {
                selectedHistory = selectedHistory.Previous;
                if (selectedHistory != null)
                    consoleInputField = selectedHistory.Value;
            }
        }
	}

    private void BackQuotePressed()
    {
        if (visible && consoleInputFieldFocused)
        {
            CloseDebugWindow();
        }
        else if (visible && !consoleInputFieldFocused)
        {
            consoleInputFieldFocused = true;
        }
        else
        {
            OpenDebugWindow();
        }
    }

    private void OpenDebugWindow()
    {
        visible = true;
        consoleInputFieldFocused = true;
        consoleInputField = "";
    }
		
	void AddDebugLog(string message, string stackTrace)
	{
        entries.Add(new DebugEntry() { message = message, stackTrace = stackTrace, type = LogType.Log });
        scrollPosition.y = 1000000;	
	}

    void AddDebugWarning(string message, string stackTrace)
    {
        entries.Add(new DebugEntry() { message = message, stackTrace = stackTrace, type = LogType.Warning });
        scrollPosition.y = 1000000;	
    }

    void AddDebugError(string message, string stackTrace)
    {
        entries.Add(new DebugEntry() { message = message, stackTrace = stackTrace, type = LogType.Error });
        scrollPosition.y = 1000000;	
    }

    private void AddEntry(string condition, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Log:
                AddDebugLog(condition, stackTrace);
                break;
            case LogType.Warning:
                AddDebugWarning(condition, stackTrace);
                break;
            case LogType.Error:
                AddDebugError(condition, stackTrace);
                break;
            default:
                AddDebugError(condition, stackTrace);
                break;
        }
        scrollPosition.y = 1000000;	

    }
	
	void ConsoleWindow(int id)
	{	
		// Begin a scroll view. All rects are calculated automatically - 
	    // it will use up any available screen space and make sure contents flow correctly.
	    // This is kept small with the last two parameters to force scrollbars to appear.
        GUIStyle style;
		scrollPosition = GUILayout.BeginScrollView (scrollPosition);
        if (entries == null)
            entries = new ArrayList();
		foreach (DebugEntry entry in entries)
		{
			GUILayout.BeginHorizontal();
            switch (entry.type)
            {
                case LogType.Log:
                    style = skin.GetStyle("debugLabelWhite");
                    break;
                case LogType.Warning:
                    style = skin.GetStyle("debugLabelYellow");
                    break;
                case LogType.Error:
                    style = skin.GetStyle("debugLabelRed");
                    break;
                default:
                    style = skin.GetStyle("debugLabelWhite");
                    break;

            }
			GUILayout.Label (new GUIContent(entry.message, entry.stackTrace), style);

            if (Event.current.type == EventType.Repaint)
                currentTooltip = GUI.tooltip;			
			GUILayout.EndHorizontal();
			GUILayout.Space(1);
			
		}
		// End the scrollview we began above.
	    GUILayout.EndScrollView ();
		if (Event.current.type == EventType.keyDown && Event.current.character == '\n' && consoleInputField.Length > 0)
		{
            selectedHistory = null;
			SubmitConsoleCommand();
		}
		
		if (consoleInputFieldFocused && Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.BackQuote)
		{
			CloseDebugWindow();
		}
	
	    GUI.SetNextControlName("consoleInputField");
		consoleInputField = GUILayout.TextField(consoleInputField);
		if (consoleInputFieldFocused)
		{
			GUI.FocusControl("consoleInputField");
		}
	
		GUI.DragWindow();
	
	}
	
	void CloseDebugWindow()
	{
		visible = false;
	}
		
	void SubmitConsoleCommand()
	{
		String command = consoleInputField;
		ConsoleCommandSubmission consoleCommand = ParseCommand(command);
        if (consoleCommand.commandName == "help")
        {
            ShowHelp(consoleCommand.parameters);
        }
        else
        {
            if (commands.ContainsKey(consoleCommand.commandName))
            {
                var chosenCommand = commands[consoleCommand.commandName];
                chosenCommand.Execute(consoleCommand.parameters);
            }
            else
            {
                Debug.Log("Invalid command name.");
            }
        }
		consoleInputField = "";
	}

    private void ShowHelp(string[] parameters)
    {
        if (parameters.Length == 0)
        {
            Debug.Log("The following are valid console commands. Type \"help <command>\" for more information.");
            foreach (string key in commands.Keys)
            {
                Debug.Log(key);
            }
        }
        else if (parameters.Length != 1)
        {
            Debug.Log("Enter a console command name after \"help\" to get more information about how to use that command.");
        }
        else
        {
            if (commands.ContainsKey(parameters[0]))
                Debug.Log(commands[parameters[0]].HelpMessage);
            else
            {
                Debug.Log("No such command. Type \"help\" for a list of all commands.");
            }
        }
    }

	ConsoleCommandSubmission ParseCommand(String commandString)
	{
		var commandSplit = commandString.Split(" "[0]);
		String[] argsArray = new String[commandSplit.Length-1];
		for (var g = 0; g < argsArray.Length; g++)
		{
			argsArray[g] = commandSplit[g+1];
		}
		return new ConsoleCommandSubmission(commandSplit[0].ToLower(), argsArray);
	}

    internal static void Clear()
    {
        DebugGUI.Main.entries = new ArrayList();
    }
}
