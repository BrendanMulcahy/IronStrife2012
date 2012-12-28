using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Class for handling debug commands and displaying debug messages. 
/// Use the DebugGUI.Print() method to print to this window.
/// </summary>
public class DebugGUI : MonoBehaviour {
	
	public ArrayList entries;
	private Rect window = new Rect(60, 60, 600, 400);
	private Vector2 scrollPosition;
	private bool visible = false;
	private ArrayList debugFlags;
	private String consoleInputField = "";
	private bool consoleInputFieldFocused = false;
    private LinkedList<String> history = new LinkedList<string>();
    private LinkedListNode<string> selectedHistory;

    private Dictionary<string, ConsoleCommand> commands;


	public static String[] DebugFlags = {"collision", "audio", "combat", "network"};
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

    void Awake()
    {
        entries = new ArrayList();
        debugFlags = new ArrayList();
        SetAllFlags();

        LoadAllConsoleCommands();
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
	
	void SetAllFlags()
	{
		foreach (String flag in DebugFlags)
		{
			debugFlags.Add(flag);
		}
	}

	void OnGUI()
	{
		if (visible)
		{
			window = GUI.Window (3, window, ConsoleWindow, "Debug");
		}
		
		Event e = Event.current;

        if (consoleInputFieldFocused && e.type == EventType.keyDown && e.keyCode == KeyCode.UpArrow)
        {
            DebugGUI.Print("moving history up");
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
	
	void Update () 
	{
        if (visible)
        {
            if (consoleInputField.StartsWith("`"))
            {
                consoleInputField = "";
            }
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
	
	/// <summary>
    /// Unconditionally Prints the message to the Debug window.
	/// </summary>
	/// <param name="message">The message to be printed</param>
    public static void Print(String message)
	{
		DebugGUI.Main.SendMessage("AddDebugEntry", message);
        Debug.Log(message);
	}

    /// <summary>
    /// Unconditionally Prints the message to the Debug window.
    /// </summary>
    /// <param name="message">The message to be printed</param>
    public static void Print(System.Object message)
    {
        DebugGUI.Main.SendMessage("AddDebugEntry", message.ToString());
        Debug.Log(message);
    }
	
	//Conditionally prints the message to the Debug window (assuming the associated debugFlag is set
    public static void Print(String message, String debugFlag)
	{
		String[] both = {message, debugFlag};
		both[0] = message; both[1] = debugFlag;
		DebugGUI.Main.SendMessage("AddDebugEntry", both);
		Debug.Log(message);
	}
	
	void AddDebugEntry(String message)
	{
		entries.Add(message);
	}
	
	void AddDebugEntry(String[] both)
	{
		var message = both[0];
		var debugFlag = both[1];
		if (debugFlags.Contains(debugFlag))
		{
			entries.Add(message);
		}
	}
	
	void ConsoleWindow(int id)
	{	
		// Begin a scroll view. All rects are calculated automatically - 
	    // it will use up any available screen space and make sure contents flow correctly.
	    // This is kept small with the last two parameters to force scrollbars to appear.
        GUIStyle style = GUIStyle.none;
        style.normal.textColor = Color.white;
        style.wordWrap = true;
		scrollPosition = GUILayout.BeginScrollView (scrollPosition);
        if (entries == null)
            entries = new ArrayList();
		foreach (String entry in entries)
		{
			GUILayout.BeginHorizontal();
			
			GUILayout.Label (entry, style);
			scrollPosition.y = 1000000;	
			
			GUILayout.EndHorizontal();
			GUILayout.Space(2);
			
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
	
	static void ToggleDebugFlag(String flag)
	{
		DebugGUI.Main.SendMessage("ToggleDebugFlagCommit", flag);
	}
	
	void ToggleDebugFlagCommit(String flag)
	{
		if (debugFlags.Contains(flag))
		{
			debugFlags.Remove(flag);
		}
		else
		{
			debugFlags.Add(flag);
		}
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
                DebugGUI.Print("Invalid command name.");
            }
        }
		consoleInputField = "";
	}

    private void ShowHelp(string[] parameters)
    {
        if (parameters.Length == 0)
        {
            DebugGUI.Print("The following are valid console commands. Type \"help <command>\" for more information.");
            foreach (string key in commands.Keys)
            {
                DebugGUI.Print(key);
            }
        }
        else if (parameters.Length != 1)
        {
            DebugGUI.Print("Enter a console command name after \"help\" to get more information about how to use that command.");
        }
        else
        {
            if (commands.ContainsKey(parameters[0]))
                DebugGUI.Print(commands[parameters[0]].HelpMessage);
            else
            {
                DebugGUI.Print("No such command. Type \"help\" for a list of all commands.");
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

	void SetFlag(String flag, String setOrUnset)
	{
		if (setOrUnset == "0")
		{
			if (debugFlags.Contains(flag))
			{
				debugFlags.Remove(flag);
				AddDebugEntry(flag + " was unset.");
			}
		}
		else
		{
			if (!debugFlags.Contains(flag))
			{
				debugFlags.Add(flag);
				AddDebugEntry(flag + " was set.");
			}
		}
	}

    internal static void Clear()
    {
        DebugGUI.Main.entries = new ArrayList();
    }
}
