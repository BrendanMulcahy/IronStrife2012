using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGUI : MonoBehaviour {
    private bool visible = true;

    private const int MAX_SPELL_NUMBER = 5;
	private float health;
	private float maxHealth;
	private float mana;
	private float maxMana;
	private float stamina;
	private float maxStamina;
	private float experience;
	private float experienceNeeded;
	private float level;
	private float team;
    private PlayerStats stats;
    private Texture2D[] spellIcons = new Texture2D[MAX_SPELL_NUMBER];
	public Texture healthBar;
    private Rect scoreboardRect = new Rect(Screen.width * .5f, 5, 200, 200);

    private ControlPoint blueBaseControlPoint;
    private ControlPoint fortressControlPoint;
    private ControlPoint farmControlPoint;
    private ControlPoint swampControlPoint;
    private ControlPoint redBaseControlPoint;

    private Font regularFont;
    private GUISkin skin;

    private Texture2D blueBaseLabel, redBaselabel, fortressLabel, farmLabel, swampLabel;

    private Texture bezelTexture;

    private Dictionary<string, Texture2D> elements = new Dictionary<string, Texture2D>();

    private bool loaded = false;
	
	private void Start()
	{
		healthBar = Resources.Load ("WhiteSquare") as Texture;
        stats = GetComponent<PlayerStats>();
        regularFont = Resources.Load("OFLGoudyStMTT") as Font;

        elements["CaptureTentNeutral"] = Resources.Load("GUI/CaptureTentNeutral") as Texture2D;
        elements["CaptureTentBlue"] = Resources.Load("GUI/CaptureTentBlue") as Texture2D;
        elements["CaptureTentRed"] = Resources.Load("GUI/CaptureTentRed") as Texture2D;

        elements["CaptureFortNeutral"] = Resources.Load("GUI/CaptureFortNeutral") as Texture2D;
        elements["CaptureFortBlue"] = Resources.Load("GUI/CaptureFortBlue") as Texture2D;
        elements["CaptureFortRed"] = Resources.Load("GUI/CaptureFortRed") as Texture2D;

        elements["CaptureFarmNeutral"] = Resources.Load("GUI/CaptureFarmNeutral") as Texture2D;
        elements["CaptureFarmBlue"] = Resources.Load("GUI/CaptureFarmBlue") as Texture2D;
        elements["CaptureFarmRed"] = Resources.Load("GUI/CaptureFarmRed") as Texture2D;

        elements["CaptureSwampNeutral"] = Resources.Load("GUI/CaptureSwampNeutral") as Texture2D;
        elements["CaptureSwampBlue"] = Resources.Load("GUI/CaptureSwampBlue") as Texture2D;
        elements["CaptureSwampRed"] = Resources.Load("GUI/CaptureSwampRed") as Texture2D;

        elements["HealthBackground"] = Resources.Load("GUI/HealthBackground") as Texture2D;
        elements["HealthForeground"] = Resources.Load("GUI/HealthActive") as Texture2D;
        elements["ManaBackground"] = Resources.Load("GUI/ManaBackground") as Texture2D;
        elements["ManaForeground"] = Resources.Load("GUI/ManaActive") as Texture2D;
        elements["StaminaBackground"] = Resources.Load("GUI/StamBackground") as Texture2D;
        elements["StaminaForeground"] = Resources.Load("GUI/StamActive") as Texture2D;

        elements["ActionSocketBezel"] = Resources.Load("GUI/ActionSocketBezel") as Texture2D;
        elements["ActionBackground"] = Resources.Load("GUI/ActionBackground") as Texture2D;

        elements["CastRed"] = Resources.Load("SpellIcons/CastRed") as Texture2D;
        elements["OneHandedRed"] = Resources.Load("GUI/OneHandedRed") as Texture2D;


        elements["Button"] = Resources.Load("GUI/Button") as Texture2D;

        StartCoroutine(LoadNetworkRequiredComponents());

        skin = Resources.Load("ISEGUISkin") as GUISkin;

	}

    private IEnumerator LoadNetworkRequiredComponents()
    {
        while (true)
        {
            if (GameObject.Find("Blue Base Control Point") != null)
            {
                blueBaseControlPoint = GameObject.Find("Blue Base Control Point").GetComponent<ControlPoint>();
                fortressControlPoint = GameObject.Find("Fortress Control Point").GetComponent<ControlPoint>();
                farmControlPoint = GameObject.Find("Farm Control Point").GetComponent<ControlPoint>();
                swampControlPoint = GameObject.Find("Swamp Control Point").GetComponent<ControlPoint>();
                redBaseControlPoint = GameObject.Find("Red Base Control Point").GetComponent<ControlPoint>();

                if (Util.MyLocalPlayerObject != null)
                {
                    string teamColor = Util.MyLocalPlayerTeam == 1 ? "Blue" : "Red";
                    elements["bezel"] = Resources.Load("GUI/Bezel" + teamColor) as Texture2D;
                    loaded = true;
                    yield break;
                }
            }
            yield return null;
        }
    }
	
	private void Update()
	{
        health = stats.Health;
        maxHealth = stats.MaxHealth;
        mana = stats.Mana;
        maxMana = stats.MaxMana;
        stamina = stats.Stamina;
        maxStamina = stats.MaxStamina;
        experience = stats.experience;
        experienceNeeded = stats.experienceNeeded;
        level = stats.Level;
        team = stats.TeamNumber;
        scoreboardRect = new Rect(Screen.width * .5f - 100, 5, 200, 200);
	}

    public void UpdateSpellIcons(int index, Spell newSpell)
    {
        spellIcons[index] = newSpell.spellImage;

    }

    private void OnGUI()
    {
        if (visible && loaded)
        {
            GUI.skin = skin;
            DrawWholeScreenBezel();
            DrawHealth();
            DrawMana();
            DrawStamina();
            DrawSpellIcons();
            DrawScoreboard();
            DrawControlPoints();
            //DrawHotkeyHelperIcons();
        }
    }

    private void DrawHotkeyHelperIcons()
    {
        float topMargin = Screen.width * .8f;
        float leftMargin = .07f;

        GUI.Label(new Rect(leftMargin, topMargin, 100, 100), elements["CastRed"]);
        GUI.Label(new Rect(leftMargin + 110, topMargin, 100, 100), elements["OneHandedRed"]);

    }

    private void DrawWholeScreenBezel()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), elements["bezel"]);
    }

    private void DrawControlPoints()
    {
        float bezelHeight = 0.395f * Screen.height;
        float labelWidth = .75f * (105f * Screen.width / 2560f);
        float interMargin = (bezelHeight - (labelWidth *5f)) / 5f;
        float leftMargin = Screen.width - Screen.width * 0.055f;

        float totalSpace = labelWidth * 5 + interMargin * 4;
        float topMargin = Screen.height * 0.203f;

        blueBaseLabel = GetControlPointLabel(blueBaseControlPoint);
        swampLabel = GetControlPointLabel(swampControlPoint);
        farmLabel = GetControlPointLabel(farmControlPoint);
        redBaselabel = GetControlPointLabel(redBaseControlPoint);
        fortressLabel = GetControlPointLabel(fortressControlPoint);

        GUI.DrawTexture(new Rect(leftMargin, topMargin + interMargin / 2, labelWidth, labelWidth), blueBaseLabel);
        GUI.DrawTexture(new Rect(leftMargin, topMargin + 3*interMargin / 2 + labelWidth, labelWidth, labelWidth), swampLabel);
        GUI.DrawTexture(new Rect(leftMargin, topMargin + 5*interMargin/2 + labelWidth * 2, labelWidth, labelWidth), fortressLabel);
        GUI.DrawTexture(new Rect(leftMargin, topMargin + 7*interMargin/2 + labelWidth * 3, labelWidth, labelWidth), farmLabel);
        GUI.DrawTexture(new Rect(leftMargin, topMargin + 9*interMargin/2 + labelWidth * 4, labelWidth, labelWidth), redBaselabel);

    }

    private Texture2D GetControlPointLabel(ControlPoint controlPoint)
    {
        string shortName="";
        if (controlPoint.name == "Red Base Control Point" || controlPoint.name == "Blue Base Control Point")
            shortName = "Tent";
        else if (controlPoint.name == "Fortress Control Point") shortName = "Fort";
        else if (controlPoint.name == "Swamp Control Point") shortName = "Swamp";
        else if (controlPoint.name == "Farm Control Point") shortName = "Farm";

        if (controlPoint.allegiance == ControlPoint.Team.Neutral)
            return elements["Capture"+shortName + "Neutral"];
        else if ((int)controlPoint.allegiance == 2)
            return elements["Capture" + shortName + "Red"];
        else
            return elements["Capture" + shortName + "Blue"];

    }

    private void DrawHealth()
	{
        float leftMargin = 0.0557291666666667f * Screen.width;
        float topmargin = 0.6476190476190476f * Screen.height;
        float width = Screen.width * 0.1901140684410646f;
        float height = (width / elements["HealthBackground"].width) * elements["HealthBackground"].height;

        GUI.DrawTexture(new Rect(leftMargin, topmargin, width, height), elements["HealthBackground"]);
		GUI.DrawTexture(new Rect(leftMargin, topmargin, (health / maxHealth)* width, height), elements["HealthForeground"]);
	}

    private void DrawMana()
	{
        float leftMargin = 0.0557291666666667f * Screen.width;
        float topmargin = 0.6978835978835979f * Screen.height;
        float width = Screen.width * 0.1901140684410646f;
        float height = (width / elements["ManaBackground"].width) * elements["ManaBackground"].height;
        GUI.DrawTexture(new Rect(leftMargin, topmargin, width, height), elements["ManaBackground"]);
        GUI.DrawTexture(new Rect(leftMargin, topmargin, (mana / maxMana) * width, height), elements["ManaForeground"]);
	}

    private void DrawStamina()
	{
        float leftMargin = 0.0557291666666667f * Screen.width;
        float topmargin = 0.7481481481481481f * Screen.height;
        float width = Screen.width * 0.1901140684410646f;
        float height = (width / elements["StaminaBackground"].width) * elements["StaminaBackground"].height;

        GUI.DrawTexture(new Rect(leftMargin, topmargin, width, height), elements["StaminaBackground"]);
        GUI.DrawTexture(new Rect(leftMargin, topmargin, (stamina / maxStamina) * width, height), elements["StaminaForeground"]);
	}

    private void DrawXPBar()
	{
		GUI.color = Color.grey;
		GUI.DrawTexture(new Rect(Screen.width-240, Screen.height - 35, 230, 30), healthBar);
		GUI.color = new Color(.19f, .19f, .19f);	
		GUI.DrawTexture(new Rect(Screen.width-215, Screen.height - 30, 200, 20), healthBar);
		GUI.color = new Color(	153f/255,50f/255,205f/255);
		GUI.DrawTexture(new Rect(Screen.width-215, Screen.height - 30, (experience / experienceNeeded)* 200, 20), healthBar);
        GUI.color = Color.white;
        GUI.Label(new Rect(Screen.width - 240, Screen.height - 30, 230, 30), "XP:");
	}

    private void DrawSpellIcons()
    {
        float leftMargin = 0.047f * Screen.width;
        float topMargin = 0.8240740740740741f * Screen.height;
        float width = Screen.width * 0.2f;
        float height = (width / elements["ActionSocketBezel"].width) * elements["ActionSocketBezel"].height;
        float bezelScale = width / elements["ActionSocketBezel"].width;
        float buttonWidth = elements["Button"].width * bezelScale;
        float buttonHeight = elements["Button"].height * bezelScale;
        float buttonTopOffset = height * .291f;

        float buttonMargin = width * 0.145f;
        float buttonStartLeftMargin = width * .165f;


        GUI.DrawTexture(new Rect(leftMargin, topMargin, width, height), elements["ActionBackground"]);

        for (int i = 0; i < MAX_SPELL_NUMBER; i++)
        {
            if (spellIcons[i] != null)
            {
                GUI.DrawTexture(new Rect(buttonStartLeftMargin + leftMargin + buttonMargin * i, topMargin + buttonTopOffset, buttonWidth, buttonHeight), spellIcons[i]);
                GUI.Label(new Rect(buttonStartLeftMargin + leftMargin + buttonMargin * i, topMargin + buttonTopOffset, 54, 54), i + 1 + "", "label");
            }
        }
        GUI.DrawTexture(new Rect(leftMargin, topMargin, width, height), elements["ActionSocketBezel"]);

    }

    private void DrawLevel()
    {
        GUI.color = Color.grey;
        GUI.DrawTexture(new Rect(Screen.width - 460, Screen.height - 35, 210, 30), healthBar);
        GUI.color = Color.white;
        GUI.Label(new Rect(Screen.width - 455, Screen.height - 30, 230, 30), "Level: " + level);
		
	}

    private void DrawTeam()
    {
        GUI.color = Color.white;
        GUI.Label(new Rect(Screen.width - 400, Screen.height - 30, 230, 30), "Team: " + team);
	}

    private void DrawScoreboard()
    {
        //var centeredStyle = GUI.skin.customStyles[0];
        //centeredStyle.alignment = TextAnchor.UpperCenter;
        //centeredStyle.contentOffset = new Vector2(0, 0);
        //centeredStyle.font = regularFont;
        GUI.Label(scoreboardRect, "Good\t\t\tEvil\n" + "(+" + GameState.numGoodControlPoints + ") "
            + GameState.goodScore + "\t\t" + "(+" + GameState.numEvilControlPoints + ") " + GameState.evilScore, GUI.skin.customStyles[0]);
    }

    internal void ToggleVisibility()
    {
        visible = !visible;
    }
}
