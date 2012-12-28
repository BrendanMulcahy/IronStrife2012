//DontDestroyOnLoad(this);
var remoteIP = "127.0.0.1";
var remotePort = 25000;
var listenPort = 25000;
var remoteGUID = "";
var useNat = false;
private var connectionInfo = "";
var username : String;
var hidden : boolean = false;
var playerGO : GameObject;
var showMasterGameList : boolean = true;
private var window = Rect(10, 100, 850, 400);
var selectedServer : int;
var scrollPosition : Vector2;
var serverGameName : String = "Enter the Server's Name Here";
var lastClick : float;
var hostData : HostData[];

function Start() 
{
	username = PlayerPrefs.GetString("username", "default_username");
	showMasterGameList = true;
	serverGameName = "Enter the Server's Name Here";
	MasterServer.RequestHostList("IronStrifeEternity");
}

function SetPlayer( setGO : GameObject)
{
	playerGO = setGO;
}

function Awake() 
{

}

function Update()
{
	if (Input.GetKeyDown(KeyCode.F1))
	{
		hidden = !hidden;
	}
	if (Input.GetKeyDown(KeyCode.F4))
	{
		showMasterGameList = !showMasterGameList;
		MasterServer.RequestHostList("IronStrifeEternity");
	}
	if (Input.GetKeyDown(KeyCode.F5))
	{
		//DebugGUI.Print("Refreshing the Master Server List", "network");
		MasterServer.RequestHostList("IronStrifeEternity");
	}
}

function OnGUI ()
{   
	if (showMasterGameList && !hidden)
	{
		window = GUI.Window(3,window,MasterGameListWindow,"List of Available Games");
	}

	if (!hidden)
	{
	GUILayout.Space(10);
	GUILayout.BeginHorizontal();
	GUILayout.Space(10);
	if (Network.peerType == NetworkPeerType.Disconnected)
	{
		GUILayout.EndHorizontal();
		serverGameName = GUILayout.TextField(serverGameName, 100);

		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		if (GUILayout.Button ("Start Server") && serverGameName!="")
		{
			Network.InitializeServer(32, listenPort, useNat);
			MasterServer.RegisterHost("IronStrifeEternity", serverGameName,"ISE_GAME_COMMENT");
			// Notify our objects that the level and the network is ready
			for (var go in FindObjectsOfType(GameObject))
				go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);		
			hidden = true;
		}
		if (GUILayout.Button("Get Server List"))
		{
			MasterServer.RequestHostList("IronStrifeEternity");
			showMasterGameList = true;
		}
	}
	else
	{
		if (useNat)
			GUILayout.Label("GUID: " + Network.player.guid + " - ");
		GUILayout.Label("Local IP/port: " + Network.player.ipAddress + "/" + Network.player.port);
		GUILayout.Label(" - External IP/port: " + Network.player.externalIP + "/" + Network.player.externalPort);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button ("Disconnect"))
			Network.Disconnect(200);
	}
	GUILayout.FlexibleSpace();
	GUILayout.EndHorizontal();
	GUILayout.BeginHorizontal();
	username = GUILayout.TextField(username,25);	
	if (GUILayout.Button("Change username"))
	{
		ChangeUserName();
		//DebugGUI.Print("username is now " + username);
	}
	GUILayout.EndHorizontal();
}
}
function ChangeUserName()
{
	PlayerPrefs.SetString("username", username);
	playerGO.SendMessage("ChangeUsername", username);
}


function OnServerInitialized()
{
	var logic : GameObject = new GameObject("MasterGameLogic");
	logic.AddComponent("MasterGameLogic");
	logic.name = "MasterGameLogic";
}

function OnConnectedToServer() 
{
	// Notify our objects that the level and the network is ready
	for (var go in FindObjectsOfType(GameObject))
		go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);		
}

function OnDisconnectedFromServer () {
	if (this.enabled != false)
		Application.LoadLevel(Application.loadedLevel);
	else
		FindObjectOfType(NetworkLevelLoad).OnDisconnectedFromServer();
}

function MasterGameListWindow()
{	
	hostData = MasterServer.PollHostList();
	scrollPosition = GUILayout.BeginScrollView(scrollPosition);
	if (hostData.Length != 0)
	{
		var serverStrings : String[] = new String[hostData.length];
        for (var g : int = 0; g < hostData.Length; g++)
	     {
	     
	        var tmpIp : String = "";
	        for(var i : int = 0; i < hostData[g].ip.Length; i++) 
	        {
	            tmpIp = hostData[g].ip[i];
	        }
	        
            serverStrings[g] = "Game name:" + hostData[g].gameName + "\t\t\t\t\tIP: <" +tmpIp + ":"+hostData[g].port+ ">\t\t\t\t\tPlayers: ["+hostData[g].connectedPlayers+" / "+hostData[g].playerLimit+"]";
        }   
        
       	for (var q = 0; q < serverStrings.Length; q++)
       	{
       		if (GUILayout.Button(serverStrings[q]))
       		{
       			if(Time.time-lastClick<0.3 && selectedServer == q)
       			{
       				ConnectToSelectedServer();
  				}
  				else
  				{
					selectedServer = q;
					lastClick = Time.time;
				}
       		}
       	}
	}
	GUILayout.EndScrollView();
		if (GUILayout.Button ("Connect"))
		{
			ConnectToSelectedServer();
		}	
	GUI.DragWindow();
}

function ConnectToSelectedServer()
{
	var host : HostData = hostData[selectedServer];
	//DebugGUI.Print("Trying to connect to "+hostIp+":"+host.port);
	Network.Connect(host);
	showMasterGameList = false;
	hidden = true;

}