using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    Resolution currentResolution;
    const float edgeMargin = .075f;
    const float windowPercentage = 1 - 2 * edgeMargin;
    private Rect windowRect;
    private Rect imageRect;
    private Rect backButtonRect;
    private Stack<GUI.WindowFunction> windowFunctions = new Stack<GUI.WindowFunction>();

    string gameName = "Enter game name here";
    string gameDescription = "Enter game description";
    string scoreLimit = "500";
    int parsedScore = 500;

    private Vector2 scrollPosition = new Vector2();
    private string chatTextInput = "";
    private Vector2 creditsScrollLocation = new Vector2();


    LinkedList<ChatEntry> chatEntries;

    private bool visible = true;
    private bool inGame = false;

    AudioClip mainMenuSong;
    Texture2D logoImage;
    Texture2D backButtonImage;

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
        skin = Resources.Load("ISEGUISkin") as GUISkin;
        logoImage = Resources.Load("GUI/MainMenuLogo") as Texture2D;
        backButtonImage = Resources.Load("GUI/BackButton") as Texture2D;
        mainMenuSong = Resources.Load("BGM/Iron Strife Theme") as AudioClip;
    }

    void Start()
    {
        // TODO: Load all textures from Resources folder (probably) for all of the buttons, and for the window, etc.

        imageRatio = logoImage.width / logoImage.height;

        Resize();
        windowFunctions.Push(MainWindow);

        gs = new GameSettings();
        gs.Start();

        audio.clip = mainMenuSong;
        audio.loop = true;
        audio.Play();
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

        backButtonRect = new Rect(Screen.width * .03f, Screen.height * .03f, 150, 100);

        windowRect = new Rect(leftMargin, topMargin - 25, windowWidth, windowHeight);
    }

    void OnGUI()
    {
        if (visible)
        {
            GUI.skin = skin;
            GUI.Label(imageRect, logoImage);

            //if (GUI.Button(backButtonRect, backButtonImage))
            //{
            //    BackButtonPressed();
            //}

            GUI.Window(0, windowRect, MainFrame, GUIContent.none);
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
        invalidScore = !int.TryParse(scoreLimit, out parsedScore);
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
            var mgl = new GameObject("MasterGameLogic").AddComponent<MasterGameLogic>();
            Network.InitializeServer(32, 25000, false);
            MasterServer.RegisterHost("IronStrife", gameName, gameDescription);
            GameState.Reset(parsedScore);
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
        hostData = MasterServer.PollHostList();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        if (hostData.Length != 0)
        {
            string[] serverStrings = new string[hostData.Length];
            for (int g = 0; g < hostData.Length; g++)
            {

                string tmpIp = "";
                for (int i = 0; i < hostData[g].ip.Length; i++)
                {
                    tmpIp = hostData[g].ip[i];
                }

                serverStrings[g] = "Game name:" + hostData[g].gameName + "\nIP: <" + tmpIp + ":" + hostData[g].port + ">\tPlayers: [" + hostData[g].connectedPlayers + " / " + hostData[g].playerLimit + "]";
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
        HostData host = hostData[selectedServer];
        //Debug.Log("Trying to connect to "+hostIp+":"+host.port);
        Network.Connect(host);
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

