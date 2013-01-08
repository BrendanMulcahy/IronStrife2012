using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// This class handles various user game settings, such as video options and audio options
/// All relevant information is stored in the PlayerPrefs class for these settings.
/// </summary>
public class GameSettings
{
    Terrain _terrain;				// This needs to be assigned in the editor.
    Terrain terrain
    {
        get
        {
            if (_terrain == null)
            {
                var go = GameObject.Find("Terrain") as GameObject;
                if (go)
                {
                    _terrain = go.GetComponent<Terrain>();
                }
            } return _terrain;
        }
    }
    // Max number of displayed trees
    private int _maxMeshTrees;
    private int maxMeshTrees
    {
        get { return _maxMeshTrees; }

        set
        {
            _maxMeshTrees = value;
            terrain.treeMaximumFullLODCount = value;
            PlayerPrefs.SetInt("maxMeshTrees", value);
        }
    }

    // Distance at which trees will be rendered.
    private float _treeDistance;
    private float treeDistance
    {
        get { return _treeDistance; }
        set
        {
            _treeDistance = value;
            terrain.treeDistance = value;
            PlayerPrefs.SetFloat("treeDistance", value);
        }
    }

    // Distance from the camera where trees will be rendered as billboards only.
    private float _treeBillboardDistance;
    private float treeBillboardDistance
    {
        get { return _treeBillboardDistance; }
        set
        {
            _treeBillboardDistance = value;
            terrain.treeBillboardDistance = value;
            PlayerPrefs.SetFloat("treeBillboardDistance", value);
        }
    }

    // Total distance delta that trees will use to transition from billboard orientation to mesh orientation.
    private float _treeCrossFadeLength;
    private float treeCrossFadeLength
    {
        get { return _treeCrossFadeLength; }
        set
        {
            _treeCrossFadeLength = value;
            terrain.treeCrossFadeLength = value;
            PlayerPrefs.SetFloat("treeCrossFadeLength", value);
        }
    }

    // Detail objects will be displayed up to this distance.
    private float _detailObjectDistance;
    private float detailObjectDistance
    {
        get { return _detailObjectDistance; }
        set
        {
            _detailObjectDistance = value;
            terrain.detailObjectDistance = value;
            PlayerPrefs.SetFloat("detailObjectDistance", value);
        }
    }

    // Density of detail objects.
    private float _detailObjectDensity;
    private float detailObjectDensity
    {
        get { return _detailObjectDensity; }
        set
        {
            _detailObjectDensity = value;
            terrain.detailObjectDensity = value;
            PlayerPrefs.SetFloat("detailObjectDensity", value);
        }
    }

    // An approximation of how many pixels the terrain will pop in the worst case when switching lod.
    private float _heightmapPixelError;
    private float heightmapPixelError
    {
        get { return _heightmapPixelError; }
        set
        {
            _heightmapPixelError = value;
            terrain.heightmapPixelError = value;
            PlayerPrefs.SetFloat("heightmapPixelError", value);
        }
    }

    // Lets you essentially lower the heightmap resolution used for rendering
    private int _heightmapMaximumLOD;
    private int heightmapMaximumLOD
    {
        get { return _heightmapMaximumLOD; }
        set
        {
            _heightmapMaximumLOD = value;
            terrain.heightmapMaximumLOD = value;
            PlayerPrefs.SetInt("heightmapMaximumLOD", value);
        }
    }

    // Heightmap patches beyond basemap distance will use a precomputed low res basemap.
    private float _basemapDistance;
    private float basemapDistance
    {
        get { return _basemapDistance; }
        set
        {
            _basemapDistance = value;
            terrain.basemapDistance = value;
            PlayerPrefs.SetFloat("basemapDistance", value);
        }
    }

    // Should terrain cast shadows?.
    private String _castShadows;
    private bool castShadows
    {
        get { return bool.Parse(_castShadows); }
        set
        {
            _castShadows = value.ToString();
            terrain.castShadows = value;
            PlayerPrefs.SetString("castShadows", value.ToString());
        }

    }

    private string _username;
    public string username
    {
        get { return _username; }
        set
        {
            _username = value;
            PlayerPrefs.SetString("username", value);
        }
    }

    private Rect window;		//Display window size/coordinates
    private bool windowVisible = false; //Is the window visible? F8 is the hotkey currently.

    private Resolution[] resolutions;

    private GUIContent[] list;
    private bool showList = false;
    private int listEntry = 0;
    private bool picked = false;
    private GUIStyle listStyle;
    private Vector2 scrollPosition = new Vector2();

    // Use this for initialization
    public void Start()
    {
        window = new Rect(100, 100, 500, 700);
        if (!PlayerPrefs.HasKey("maxMeshTrees"))
        {
            SetDefaultOptions();
        }

        _maxMeshTrees = PlayerPrefs.GetInt("maxMeshTrees", 250);
        _treeDistance = PlayerPrefs.GetFloat("treeDistance", 350);
        _treeBillboardDistance = PlayerPrefs.GetFloat("treeBillboardDistance", 600);
        _treeCrossFadeLength = PlayerPrefs.GetFloat("treeCrossFadeLength", 100);
        _detailObjectDistance = PlayerPrefs.GetFloat("detailObjectDistance", 100);
        _detailObjectDensity = PlayerPrefs.GetFloat("detailObjectDensity", .4f);
        _heightmapPixelError = PlayerPrefs.GetFloat("heightMapPixelError", 100);
        _heightmapMaximumLOD = PlayerPrefs.GetInt("heightmapMaximumLOD", 1);
        _basemapDistance = PlayerPrefs.GetFloat("basemapDistance", 100);
        _castShadows = PlayerPrefs.GetString("castShadows", "true");
        _username = PlayerPrefs.GetString("username", "default_username");

        resolutions = Screen.resolutions;

        list = new GUIContent[resolutions.Length];
        for (int g = 0; g < resolutions.Length; g++)
        {
            list[g] = new GUIContent(resolutions[g].width + " x " + resolutions[g].height + " @ " + resolutions[g].refreshRate + " Hz");
        }
        // Make a GUIStyle that has a solid white hover/onHover background to indicate highlighted items
        listStyle = new GUIStyle();
        listStyle.normal.textColor = Color.white;
        var tex = new Texture2D(2, 2);
        var colors = new Color[4];
        for (int g = 0; g < colors.Length; g++) colors[g] = Color.white;
        tex.SetPixels(colors);
        tex.Apply();
        listStyle.hover.background = tex;
        listStyle.onHover.background = tex;
        listStyle.padding.left = listStyle.padding.right = listStyle.padding.top = listStyle.padding.bottom = 4;


        // This section sets all of the terrain's graphics options to the user's settings.
        UpdateGraphicsSettings();

    }

    private void SetDefaultOptions()
    {
        PlayerPrefs.SetInt("maxMeshTrees", 250);
        PlayerPrefs.SetFloat("treeDistance", 350);
        PlayerPrefs.SetFloat("treeBillboardDistance", 600);
        PlayerPrefs.SetFloat("treeCrossFadeLength", 100);
        PlayerPrefs.SetFloat("detailObjectDistance", 100);
        PlayerPrefs.SetFloat("detailObjectDensity", .4f);
        PlayerPrefs.SetFloat("heightMapPixelError", 100);
        PlayerPrefs.SetInt("heightmapMaximumLOD", 1);
        PlayerPrefs.SetFloat("basemapDistance", 100);
        PlayerPrefs.SetString("castShadows", "true");
        PlayerPrefs.SetString("username", "default_username");
    }

    /// <summary>
    /// Updates the Terrain objects graphics settings
    /// </summary>
    public void UpdateGraphicsSettings()
    {
        if (terrain)
        {
            terrain.treeMaximumFullLODCount = maxMeshTrees;
            terrain.treeDistance = treeDistance;
            terrain.treeBillboardDistance = treeBillboardDistance;
            terrain.treeCrossFadeLength = treeCrossFadeLength;
            terrain.detailObjectDistance = detailObjectDistance;
            terrain.detailObjectDensity = detailObjectDensity;
            terrain.heightmapPixelError = heightmapPixelError;
            terrain.heightmapMaximumLOD = heightmapMaximumLOD;
            terrain.basemapDistance = basemapDistance;
            terrain.castShadows = castShadows;
        }
    }
    /// <summary>
    /// Handles key checks.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            if (windowVisible)
            {
                windowVisible = false;
                //BackupPlayerPrefs();
            }
            else
            {
                windowVisible = true;
            }
        }
    }

    /// <summary>
    /// Displays the graphics settings window.
    /// </summary>
    void OnGUI()
    {
        if (windowVisible)
        {
            GUI.Window("gamesettings".GetHashCode(), window, SettingsWindow, "Iron Strife Settings");
        }
    }

    public void SettingsWindow(int id)
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        //GUILayout.BeginVertical();
        //GUILayout.Label("Max Trees : " + maxMeshTrees);
        //maxMeshTrees = (int)GUILayout.HorizontalSlider(maxMeshTrees, 0, 10000);
        //GUILayout.FlexibleSpace();
        //GUILayout.Label("Tree Distance :" + treeDistance);
        //treeDistance = GUILayout.HorizontalSlider(treeDistance, 0, 2000);
        //GUILayout.FlexibleSpace();
        //GUILayout.Label("Tree Billboard Distance :" + treeBillboardDistance);
        //treeBillboardDistance = GUILayout.HorizontalSlider(treeBillboardDistance, 5, 2000);
        //GUILayout.FlexibleSpace();
        //GUILayout.Label("Tree Fade Distance :" + treeCrossFadeLength);
        //treeCrossFadeLength = GUILayout.HorizontalSlider(treeCrossFadeLength, 0, 200);
        //GUILayout.FlexibleSpace();
        //GUILayout.Label("Grass Detail Distance :" + detailObjectDistance);
        //detailObjectDistance = GUILayout.HorizontalSlider(detailObjectDistance, 0, 250);
        //GUILayout.FlexibleSpace();
        //GUILayout.Label("Grass Density :" + detailObjectDensity);
        //detailObjectDensity = GUILayout.HorizontalSlider(detailObjectDensity, 0, 1);
        //GUILayout.Space(25);
        GUILayout.BeginVertical();
        GUILayout.Space(20);

        GUILayout.Label("Video Settings:");
        GUILayout.Space(8);
        GUI.skin.button.fontSize = 24;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Low"))
        {
            ChangeToGraphicsDefault("Low");
        }
        if (GUILayout.Button("Medium"))
        {
            ChangeToGraphicsDefault("Medium");
        }
        if (GUILayout.Button("High"))
        {
            ChangeToGraphicsDefault("High");

        }
        if (GUILayout.Button("Ultra"))
        {
            ChangeToGraphicsDefault("Ultra");
        }
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();


        GUILayout.Label("Username to display in-game");
        username = GUILayout.TextField(username);
        GUILayout.FlexibleSpace();

        //ShowResolutionOption();
        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        UpdateGraphicsSettings();

    }

    private void ChangeToGraphicsDefault(string p)
    {
        switch (p)
        {
            case "Low":
                QualitySettings.SetQualityLevel(0);
                maxMeshTrees = 80;
                treeDistance = 500;
                treeBillboardDistance = 1000;
                treeCrossFadeLength = 50;
                break;

            case "Medium":
                QualitySettings.SetQualityLevel(3);
                maxMeshTrees = 150;
                treeDistance = 1000;
                treeBillboardDistance = 2000;
                treeCrossFadeLength = 100;
                break;

            case "High":
                QualitySettings.SetQualityLevel(4);
                maxMeshTrees = 500;
                treeDistance = 2000;
                treeBillboardDistance = 2000;
                treeCrossFadeLength = 200;
                break;

            case "Ultra":
                QualitySettings.SetQualityLevel(5);
                maxMeshTrees = 500;
                treeDistance = 2000;
                treeBillboardDistance = 2000;
                treeCrossFadeLength = 200;
                break;
        }
    }

    /// <summary>
    /// Does not work
    /// </summary>
    private void ShowResolutionOption()
    {
        if (Popup.List(new Rect(50, 100, 100, 20), ref showList, ref listEntry, new GUIContent("Change Resolution"), list, listStyle))
        {
            picked = true;
        }
        if (picked)
        {
            picked = false;
            Screen.SetResolution(resolutions[listEntry].width, resolutions[listEntry].height, Screen.fullScreen, resolutions[listEntry].refreshRate);

        }
    }

    /// <summary>
    /// Backs up the user's graphics settings into PlayerPrefs.
    /// </summary>
    void BackupPlayerPrefs()
    {
        PlayerPrefs.SetInt("maxMeshTrees", maxMeshTrees);
        PlayerPrefs.SetFloat("treeDistance", treeDistance);
        PlayerPrefs.SetFloat("treeBillboardDistance", treeBillboardDistance);
        PlayerPrefs.SetFloat("treeCrossFadeLength", treeCrossFadeLength);
        PlayerPrefs.SetFloat("detailObjectDistance", detailObjectDistance);
        PlayerPrefs.SetFloat("detailObjectDensity", detailObjectDensity);
        PlayerPrefs.SetFloat("heightmapPixelError", heightmapPixelError);
        PlayerPrefs.SetInt("heightmapMaximumLOD", heightmapMaximumLOD);
        PlayerPrefs.SetFloat("basemapDistance", basemapDistance);
        PlayerPrefs.SetString("castShadows", castShadows.ToString());

    }
}