using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Class that handles Network connection and utilization. 
/// For more info check out the documentation about the Network class on Unity's website.
/// This class should be replaced when we make the main menu system.
/// </summary>
public class ConnectGUI : MonoBehaviour {

    int listenPort = 25000;
    bool useNat = false;
    string username;
    bool hidden = false;
    GameObject playerGO;
    bool showMasterGameList = true;
    private Rect window = new Rect(10, 100, 850, 400);
    int selectedServer;
    Vector2 scrollPosition;
    string serverGameName= "Enter the Server's Name Here";
    float lastClick;
    HostData[] hostData;

    void Start() 
    {
	    username = PlayerPrefs.GetString("username", "default_username");
	    showMasterGameList = true;
	    serverGameName = "Enter the Server's Name Here";
	    MasterServer.RequestHostList("IronStrifeEternity");
       // StartServer();
    }

    void SetPlayer(GameObject setGO)
    {
        playerGO = setGO;
    }

    void Update()
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
		    //Debug.Log("Refreshing the Master Server List", "network");
		    MasterServer.RequestHostList("IronStrifeEternity");
	    }
    }

    void OnGUI ()
    {   
	    if (showMasterGameList && !hidden)
	    {
		    window = GUI.Window("connectgui".GetHashCode(),window,MasterGameListWindow,"List of Available Games");
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
                StartServer();
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
		    //Debug.Log("username is now " + username);
	    }
	    GUILayout.EndHorizontal();
    }
    }

    /// <summary>
    /// Starts a server and registers it with the Master Host list.
    /// </summary>
    private void StartServer()
    {
        Network.InitializeServer(32, listenPort, useNat);
        MasterServer.RegisterHost("IronStrifeEternity", serverGameName, "ISE_GAME_COMMENT");

        hidden = true;
    }
    /// <summary>
    /// Changes the user's username, and sends an RPC to everyone with the newly changed name.
    /// </summary>
    void ChangeUserName()
    {
	    PlayerPrefs.SetString("username", username);
        playerGO.networkView.RPC("ChangeName", RPCMode.AllBuffered, username);
    }

    /// <summary>
    /// Creates a master game logic object on the server.
    /// </summary>
    void OnServerInitialized()
    {
	    GameObject logic = new GameObject("MasterGameLogic");
	    logic.AddComponent<MasterGameLogic>();
        // Notify our objects that the level and the network is ready
        foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
            go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
    }

    void OnConnectedToServer() 
    {
	    // Notify our objects that the level and the network is ready
	    foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
		    go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);		
    }

    void OnDisconnectedFromServer () {
	    if (this.enabled != false)
		    Application.LoadLevel(Application.loadedLevel);
    }

    public void MasterGameListWindow(int id)
    {	
	    hostData = MasterServer.PollHostList();
	    scrollPosition = GUILayout.BeginScrollView(scrollPosition);
	    if (hostData.Length != 0)
	    {
		    String[] serverStrings = new String[hostData.Length];
            for (int g = 0; g < hostData.Length; g++)
	         {
	     
	            String tmpIp = "";
	            for(int i = 0; i < hostData[g].ip.Length; i++) 
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

    /// <summary>
    /// Attempts to connect to the selected server.
    /// </summary>
    void ConnectToSelectedServer()
    {
	    HostData host = hostData[selectedServer];
	    //Debug.Log("Trying to connect to "+hostIp+":"+host.port);
	    Network.Connect(host);
	    showMasterGameList = false;
	    hidden = true;

    }
}
