#pragma strict
var visible : boolean = false;
var window = Rect(50,50,600,400);
private var scrollPosition : Vector2;
var players : NetworkPlayer[];
var windowTitle : String = "Players";

function Start () {

}

function Update () 
{
	if (Input.GetKeyDown(KeyCode.Backslash))
	{
		visible = true;	
	}
	if (Input.GetKeyUp(KeyCode.Backslash))
	{
		visible = false;
	}
	players = Network.connections;
	windowTitle = "Players : "+players.Length + " others currently connected.";
	
}

function OnGUI()
{
	if (visible)
	{
		window = GUI.Window(3, window, NetworkPlayerWindow, windowTitle);
	}

}

function NetworkPlayerWindow(id : int)
{
	scrollPosition = GUILayout.BeginScrollView(scrollPosition);
		GUILayout.BeginHorizontal();
		GUILayout.Label ("IP:PORT\t\t\t\t\t\t\t\tEXTERNAL:PORT\t\t\t\t\t\t\tPLAYER NUMBER");
		GUILayout.EndHorizontal();

		for (var player : NetworkPlayer in players)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label (player.ipAddress + ":" + player.port + "\t\t\t\t\t"+player.externalIP+":"+player.externalPort+"\t\t\t\t\t\t\t\t\t"+player.ToString());
		GUILayout.EndHorizontal();
		GUILayout.Space(2);
	}
		

	
	
		GUI.DragWindow();
		GUILayout.EndScrollView();

}