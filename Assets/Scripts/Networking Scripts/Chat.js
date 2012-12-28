var skin : GUISkin;
var visible = false;
private var inputField = "";
private var newEntries = ArrayList();
private var oldEntries = ArrayList();
private var scrollPosition : Vector2;
var senderName = "";
private var isInputFieldFocused : boolean = false;
var style : GUIStyle;
var chatWidth = Screen.width /1.7;
var chatHeight = 140;
var chatArea : Rect;
var numberOfNewEntries : int;
var numberOfOldEntries : int;
var playerGO : GameObject;

class ChatEntry
{
	var sender = "";
	var text = "";	
	var mine = true;
	var age;
}

function Start()
{
    skin = Resources.Load("ISEGUISkin");
}

function SetPlayer( setGO : GameObject)
{
	playerGO = setGO;
}

function Update()
{
	if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
	{
		if (!visible)
		{
			GameObject.Find("Main Camera").SendMessage("ChatOpen");
			visible = true; scrollPosition.y = 100000; isInputFieldFocused = true;
		}
	}
	numberOfOldEntries = oldEntries.Count;
	numberOfNewEntries = newEntries.Count;

	chatWidth = Screen.width /2.4; 
	chatHeight = Screen.height * .31f;
	chatArea = Rect(0.25f* Screen.width + 10, Screen.height - chatHeight - 25, chatWidth, chatHeight);

	for (var g : int = newEntries.Count-1; g >= 0 ; g--)
	{
		if (newEntries[g].age >= 7)
		{
			while (g >= 0)
			{
				oldEntries.Add(newEntries[g]);
				newEntries.RemoveAt(g);
				g--;
			}	
		}
		else
		{
			newEntries[g].age += Time.deltaTime;
		}
	}
	
}

function OnGUI ()
{	

	GUI.skin = skin;
	var e : Event = Event.current;
	if (e.type == EventType.MouseDown)
    {
    	GameObject.Find("Main Camera").SendMessage("ChatClose");
		visible = false; isInputFieldFocused = false;
	}

	GUILayout.BeginArea(chatArea);
	scrollPosition = GUILayout.BeginScrollView(scrollPosition, "box");
	GUILayout.BeginVertical();
		
	if (visible)
	{
		for (var entry : ChatEntry in oldEntries)
		{
			GUILayout.Label(entry.sender + ": " + entry.text);
		}
	}
	for (var entry : ChatEntry in newEntries)
	{
		GUILayout.Label(entry.sender + ": " + entry.text);
	}
	GUILayout.EndVertical();
	GUILayout.EndScrollView();
	if (visible)
	{
		GUI.SetNextControlName("inputField");
		inputField = GUILayout.TextField(inputField, 100);
		GUI.FocusControl("inputField");
	}
	GUILayout.EndArea();
	
	if (Event.current.type == EventType.keyDown && Event.current.character == "\n" && inputField.Length > 0 && isInputFieldFocused)
	{
		ApplyGlobalChatText(inputField, 1, GetUsername());
		networkView.RPC("ApplyGlobalChatText", RPCMode.Others, inputField, 0, GetUsername());
		inputField = "";
	}
}
	
function GetUsername() : String
{
	return PlayerPrefs.GetString("username", "default_username");
}

@RPC
function ApplyGlobalChatText (str : String, mine : int, senderName : String)
{
	var entry = new ChatEntry();
	entry.sender = senderName;
	entry.text = str;
	if (mine == 1) entry.mine = true;
	else entry.mine = false;
	entry.age = 0;

	newEntries.Add(entry);		
	scrollPosition.y = 1000000;	
}