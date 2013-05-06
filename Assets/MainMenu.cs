using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using System.Net.Sockets;
using System.Net;
using System.IO;

[DefaultSceneObject("MainMenu", "MainMenu")]
public class MainMenu : MonoBehaviour
{
    Resolution currentResolution;
    const float edgeMargin = .075f;
    const float windowPercentage = 1 - 2 * edgeMargin;
    private Rect windowRect;
    private Rect imageRect;
    private Stack<GUI.WindowFunction> windowFunctions = new Stack<GUI.WindowFunction>();
    private List<ServerInfo> servers = new List<ServerInfo>();

    string gameName = "Enter game name here";
    string gameDescription = "Enter game description";
    string scoreLimit = "500";

    private Vector2 scrollPosition = new Vector2();
    private string chatTextInput = "";
    private Vector2 creditsScrollLocation = new Vector2();


    LinkedList<ChatEntry> chatEntries;

    private bool visible = true;
    private bool inGame = false;

    AudioClip mainMenuSong;
    Texture2D logoImage;

    private const int defaultFontSize = 36;
    private int hostGameFontSize = defaultFontSize;
    private int joinGameFontSize = defaultFontSize;
    private int optionsFontSize = defaultFontSize;
    private int creditsFontSize = defaultFontSize;
    private int quitFontSize = defaultFontSize;

    float windowWidth, windowHeight;
    float imageWidth, imageHeight;
    float imageRatio;

    GameSettings gs;

    GUISkin skin;

    int selectedServer = -1;
    float lastClick;
    HostData[] hostData;

    string lastTooltip = "";
    private string newTooltip = "";
    private bool invalidScore = false;

    void Awake()
    {
        skin = Util.ISEGUISkin;
        logoImage = Resources.Load("GUI/MainMenuLogo") as Texture2D;
        mainMenuSong = Resources.Load("BGM/Iron Strife Theme") as AudioClip;
        PlayerPrefs.SetInt("teamNumber", -1);
    }

    void Start()
    {
        // TODO: Load all textures from Resources folder (probably) for all of the buttons, and for the window, etc.

        imageRatio = logoImage.width / logoImage.height;

        Resize();
        windowFunctions.Push(MainWindow);

        gs = new GameSettings();

        audio.clip = mainMenuSong;
        audio.loop = true;
        audio.Play();

        //Prefetch socket policy in web players so they can get master server list.
#if UNITY_WEBPLAYER
        try
        {
            if (!UnityEngine.Security.PrefetchSocketPolicy(StrifeMasterServer.MasterServerAddress.ToString(), 845))
            {
                Debug.LogError("Error prefetching socket policy.");
            }
            else
            {
                Debug.Log("Socket policy prefetched successfully.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error prefetching securty policy: " + e.Message);
        }
#endif
    }

    private void Resize()
    {
        imageHeight = Screen.height * .2f;
        imageWidth = imageHeight * imageRatio;
        float topMargin = imageHeight;

        float bgRatio = 1.205651491365777f;
        windowHeight = Screen.height - topMargin;
        float leftMargin = Screen.width / 2 - windowWidth / 2;
        float imageLeftMargin = Screen.width / 2 - imageWidth * .5f;

        imageRect = new Rect(imageLeftMargin, 0, imageWidth, imageHeight);

        windowWidth = windowHeight * bgRatio;


        windowRect = new Rect(leftMargin, topMargin - 25, windowWidth, windowHeight);
    }

    void OnGUI()
    {
        if (visible)
        {
            GUI.skin = skin;
            GUI.Label(imageRect, logoImage);

            GUI.Window("mainmenu".GetHashCode(), windowRect, MainFrame, GUIContent.none);
            if (Event.current.type == EventType.Repaint)
            {
                newTooltip = GUI.tooltip;
                if (newTooltip != lastTooltip)
                {
                    if (lastTooltip != "")
                        SendMessage(lastTooltip + "OnMouseOut", SendMessageOptions.DontRequireReceiver);
                    if (newTooltip != "")
                        SendMessage(newTooltip + "OnMouseOver", SendMessageOptions.DontRequireReceiver);
                    lastTooltip = newTooltip;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10) || Input.GetKeyDown(KeyCode.Escape))
        {
            visible = !visible;
            if (!inGame) visible = true;
            windowFunctions = new Stack<GUI.WindowFunction>();
            windowFunctions.Push(MainWindow);
        }

        CheckForResolutionChange();
    }

    private void CheckForResolutionChange()
    {
        if (Screen.currentResolution.width != currentResolution.width && Screen.currentResolution.height != currentResolution.height)
        {
            currentResolution = Screen.currentResolution;
            Resize();
        }
    }


    private void MainFrame(int id)
    {
        float innerMarginLeft = windowRect.width * .1f;
        float innerMarginTop = windowRect.height * .1f;
        float innerWidth = windowRect.width - 2 * innerMarginLeft;
        Rect innerArea = new Rect(innerMarginLeft, innerMarginTop, innerWidth, windowRect.height - 2*innerMarginTop);

        GUILayout.BeginArea(innerArea);
        GUILayout.BeginVertical();
        
        windowFunctions.Peek()(0);

        GUILayout.EndVertical();
        GUILayout.EndArea();

    }

    public void MainWindow(int id)
    {

        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        if (!inGame)
        {

            GUI.skin.button.fontSize = hostGameFontSize;
            if (GUILayout.Button(new GUIContent("Host a Game", "hostGameButton")))
            {
                HostGameButtonPressed();
            }
            GUILayout.FlexibleSpace();

            GUI.skin.button.fontSize = joinGameFontSize;
            if (GUILayout.Button(new GUIContent("Join a Game", "joinGameButton")))
            {
                JoinGameButtonPressed();
            }
            GUILayout.FlexibleSpace();
        }
        else
        {
            GUI.skin.button.fontSize = joinGameFontSize;
            if (GUILayout.Button(new GUIContent("Disconnect", "disconnectButton")))
            {
                DisconnectButtonPressed();
            }
            GUILayout.FlexibleSpace();
        }

        GUI.skin.button.fontSize = optionsFontSize;
        if (GUILayout.Button(new GUIContent("Options", "optionsButton")))
        {
            OptionsButtonPressed();
        }
        GUILayout.FlexibleSpace();

        GUI.skin.button.fontSize = creditsFontSize;
        if (GUILayout.Button(new GUIContent("Credits", "creditsButton")))
        {
            CreditsButtonPressed();
        }
        GUILayout.FlexibleSpace();

        GUI.skin.button.fontSize = quitFontSize;
        if (GUILayout.Button(new GUIContent("Quit", "quitButton")))
        {
            QuitButtonPressed();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        //if (Event.current.type == EventType.Repaint)
        //{
        //    newTooltip = GUI.tooltip;
        //    if (newTooltip != lastTooltip)
        //    {
        //        if (lastTooltip != "")
        //            SendMessage(lastTooltip + "OnMouseOut", SendMessageOptions.DontRequireReceiver);
        //        if (GUI.tooltip != "")
        //            SendMessage(GUI.tooltip + "OnMouseOver", SendMessageOptions.DontRequireReceiver);
        //        lastTooltip = GUI.tooltip;
        //    }
        //}
    }

    private void DisconnectButtonPressed()
    {
        Network.Disconnect();
        Application.LoadLevelAsync(0);
    }

    void joinGameButtonOnMouseOver()
    {
        Debug.Log("MOUSING OVER JOIN GAME BUTTON");
        joinGameFontSize = 64;
    }
    void joinGameButtonOnMouseOut()
    {
        Debug.Log("Mouse leaving join game button.");
        joinGameFontSize = defaultFontSize;
    }

    private void QuitButtonPressed()
    {
        Application.Quit();
    }

    private void OptionsButtonPressed()
    {

        windowFunctions.Push(gs.SettingsWindow);
    }

    private void CreditsButtonPressed()
    {
        windowFunctions.Push(CreditsWindow);
    }

    private void JoinGameButtonPressed()
    {
        MasterServer.RequestHostList("IronStrife");
        servers = StrifeMasterServer.GetMasterServerList();
        windowFunctions.Push(MasterGameListWindow);
    }

    private void HostGameButtonPressed()
    {
        windowFunctions.Push(HostGameWindow);
    }

    private void CreditsWindow(int id)
    {
        creditsScrollLocation = GUILayout.BeginScrollView(creditsScrollLocation);
        GUILayout.BeginVertical();
        GUILayout.Label("");
        GUILayout.Label("Project Leader: Brendan Mulcahy");
        GUILayout.Label("Lead Artist: Mike Mollick");
        GUILayout.Label("Lead Programmer: Eric Mellino");
        GUILayout.Label("Lead Composer: Evan Snyder");
        GUILayout.Label("");

        GUILayout.Label("Programming Team:");
        GUILayout.Label("\t\tBrendan Mulcahy");
        GUILayout.Label("\t\tEric Mellino");
        GUILayout.Label("\t\tVinai Suresh");
        GUILayout.Label("\t\tMike Robertson");
        GUILayout.Label("");

        GUILayout.Label("Art Team:");
        GUILayout.Label("\t\tMike Mollick");
        GUILayout.Label("\t\tJoe Conley");
        GUILayout.Label("\t\tZach Sabatino");
        GUILayout.Label("\t\tBrendan Herlacher");
        GUILayout.Label("\t\tSara Eckhoff");
        GUILayout.Label("\t\tKedan James");
        GUILayout.Label("\t\tEvan Snyder");

        GUILayout.EndVertical();
        GUILayout.EndScrollView();

    }

    private void HostGameWindow(int id)
    {
        GUILayout.BeginVertical();
        GUILayout.Space(50);
        gameName = GUILayout.TextField(gameName);
        gameDescription = GUILayout.TextField(gameDescription);
        GUILayout.Label("Score Limit:");
        scoreLimit = GUILayout.TextField(scoreLimit);
        if (GUILayout.Button("Host Game"))
        {
            CreateGameLobby();
        }
        if (invalidScore)
        {
            GUI.color = Color.red;
            GUI.skin.label.fontSize = 36;
            GUILayout.Label("Invalid score. Please input a whole number.");
        }
        GUILayout.EndVertical();

    }

    private void GameLobbyWindow(int id)
    {
        GUILayout.BeginVertical();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        foreach (ChatEntry entry in chatEntries)
        {
            GUILayout.Label(entry.timeStamp.ToString() + " | " + entry.sender + " : " + entry.message);
        }
        GUILayout.EndScrollView();
        chatTextInput = GUILayout.TextField(chatTextInput);
        GUILayout.EndVertical();
    }

    private IEnumerator ListenForEnterKey()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SubmitChatMessage();
            }
        }
    }

    private void SubmitChatMessage()
    {
        networkView.RPC("BroadcastChatMessage", RPCMode.All, PlayerPrefs.GetString("username"), chatTextInput);
    }

    private void CreateGameLobby()
    {
        if (!invalidScore)
        {
            Network.InitializeServer(32, 25000, false);
            MasterServer.RegisterHost("IronStrife", gameName, gameDescription);
            var server = this.gameObject.AddComponent<StrifeServer>();
            server.gameDescription = gameDescription;
            server.gameName = gameName;
            server.port = 25000;

            this.inGame = true;
            CloseMainMenu();
        }
    }

    private void BackButtonPressed()
    {
        if (windowFunctions.Count != 1)
            windowFunctions.Pop();
    }

    private void CloseMainMenu()
    {
        StartCoroutine(Util.FadeOutSoundInSeconds(audio, 4.0f));
        this.visible = false;
    }

    private void GoToLobby()
    {
        chatEntries = new LinkedList<ChatEntry>();
        windowFunctions.Push(GameLobbyWindow);

    }

    [RPC]
    void BroadcastChatMessage(string username, string message, NetworkMessageInfo nmi)
    {
        chatEntries.AddLast(
            new ChatEntry()
            {
                sender = username,
                message = message,
                timeStamp = nmi.timestamp
            });
        Debug.Log("There are now " + chatEntries.Count + " chat entries.");
    }

    /// <summary>
    /// Creates a master game logic object on the server.
    /// </summary>
    void OnServerInitialized()
    {
        // Notify our objects that the level and the network is ready
        foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
            go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
    }

    void OnConnectedToServer()
    {
        this.visible = false;
        // Notify our objects that the level and the network is ready
        foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
            go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
    }

    void OnDisconnectedFromServer()
    {
        if (this.enabled != false)
            Application.LoadLevel(Application.loadedLevel);
    }

    public void MasterGameListWindow(int id)
    {
        GUI.skin.button.fontSize = 18;
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        if (servers.Count != 0)
        {
            string[] serverStrings = new string[servers.Count];
            for (int g = 0; g < servers.Count; g++)
            {

                serverStrings[g] = "Game name:" + servers[g].gameName + "\nIP: <" + servers[g].ipAddress + ":" + servers[g].port + ">\tPlayers: [" + servers[g].numConnectedPlayers + " / " + servers[g].maxPlayers + "]";
            }
            GUILayout.BeginVertical();
            for (var q = 0; q < serverStrings.Length; q++)
            {
                if (GUILayout.Button(serverStrings[q]))
                {
                    if (Time.time - lastClick < 0.3 && selectedServer == q)
                    {
                        ConnectToSelectedServer();
                    }
                    else
                    {
                        selectedServer = q;
                        lastClick = Time.time;
                    }
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();

        }
        else
        {
            GUILayout.Label("No games were found!");
        }
        GUILayout.EndScrollView();
        GUI.skin.button.fontSize = 24;
        if (GUILayout.Button("Connect") && selectedServer != -1)
        {
            ConnectToSelectedServer();
        }
    }

    /// <summary>
    /// Attempts to connect to the selected server.
    /// </summary>
    void ConnectToSelectedServer()
    {
        var server = servers[selectedServer];
        Network.Connect(server.ipAddress, server.port);
        this.inGame = true;
        this.CloseMainMenu();
    }
}

public class ChatEntry
{
    public string sender;
    public string message;
    public double timeStamp;
}