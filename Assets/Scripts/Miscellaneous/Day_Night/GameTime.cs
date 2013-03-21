using UnityEngine;
using System.Collections;



/// <summary>
/// Game time.
/// Class Handles the movement of the sun and moon in the sky, as well as the changing of the skybox over time.
/// </summary>
public class GameTime : MonoBehaviour {
	public Transform[] suns;
	
	public Transform[] moons;
	
	private Sun sunBright;
    public Quaternion[] initialRotations;
	
	//Full day cycle 
	public float dayCycleInMinutes = 6;
	
	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	
	private const float DEGREES_PER_SECOND = 360/DAY;
	
	public float timeOfDay;

    public Skybox skybox;

    public event DawnEventHandler Dawn;
    public event DuskEventHandler Dusk;
    public event NewDayEventHandler NewDay;

    private bool isDay = false;
    public bool IsDay { get { return isDay; } }
    public bool IsNight { get { return !isDay; } }

    public int currentDayNumber = 0;
    public delegate void DawnEventHandler();
    public delegate void DuskEventHandler();
    public delegate void NewDayEventHandler();

    private static GameTime _instance;
    public static GameTime Main
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.Find("GameTime").GetComponent<GameTime>();
            }
            return _instance;
        }
    }

    public static float CurrentTime { get { return Main.timeOfDay; } }

	// Use this for initialization
	void Start () {
        if (suns.Length == 0)
        {
            suns = new Transform[1];
            var sunPrefab = Resources.Load("Sun") as GameObject;
            var newSun = Instantiate(sunPrefab) as GameObject;
            suns[0] = newSun.transform;
        }
        if (moons.Length == 0)
        {
            moons = new Transform[1];
            var moonPrefab = Resources.Load("Moon") as GameObject;
            var newMoon = Instantiate(moonPrefab) as GameObject;
            moons[0] = newMoon.transform;
        }

		for (int i = 0; i < suns.Length; i++)
		{
			Sun temp = suns[i].GetComponent<Sun>();
			if (temp == null)
			{
				Debug.Log("SunScript not found. Adding it");
				suns[i].gameObject.AddComponent<Sun>();
				temp = suns[i].GetComponent<Sun>();
			}
		}
		
		for (int i = 0; i < moons.Length; i++)
		{
			Moon temp = moons[i].GetComponent<Moon>();
			if (temp == null)
			{
				Debug.Log("MoonScript not found. Adding it");
				moons[i].gameObject.AddComponent<Moon>();
				temp = moons[i].GetComponent<Moon>();
			}
		}

        skybox = Camera.main.GetComponent<Skybox>();

        initialRotations = new Quaternion[2];
        initialRotations[0] = Quaternion.Euler(90, 0, 0);
        initialRotations[1] = Quaternion.Euler(270, 0, 0);
		
		timeOfDay = 7.5f; // Start at 7:30 AM

        Dawn += GameTime_Dawn;
        Dusk += GameTime_Dusk;
	}

    void GameTime_Dusk()
    {
        PopupMessage.LocalDisplay("Night of Day <b>" + currentDayNumber + "</b>", 5.0f, 0.15f, .3f, 1f, 48);
    }

    void GameTime_Dawn()
    {
        PopupMessage.LocalDisplay("Dawn of Day <b>" + currentDayNumber + "</b>", 5.0f, .98f, .1f, 0, 48);
    }
	
    [ExecuteInEditMode]
	// Update is called once per frame
	void Update () {
        timeOfDay += (Time.deltaTime / HOUR) * (1440f / dayCycleInMinutes);
        if (timeOfDay >= 24)
        {
            OnHourZero();
        }

        if (!isDay && timeOfDay >= 6 && timeOfDay <18)
        {
            currentDayNumber++;
            OnDawn();
        }
        else if (isDay &&  (timeOfDay >= 18 || timeOfDay < 6))
        {
            OnDusk();
        }
        
        float rotationAmount = (CurrentTime / 24f) * 360f;
		moons[0].transform.rotation = initialRotations[0];
        moons[0].Rotate(new Vector3(1, 0, 0), rotationAmount);
		
        suns[0].transform.rotation = initialRotations[1];
        suns[0].Rotate(new Vector3(1, 0, 0), rotationAmount);
		
        UpdateSkybox();
	}

    private void OnHourZero()
    {
        timeOfDay -= 24;
        if (NewDay != null)
            NewDay();
    }

    private void OnDusk()
    {
        if (Dusk!= null)
            Dusk();
        isDay = false;
    }

    private void OnDawn()
    {
        if (Dawn != null)
            Dawn();
        isDay = true;
    }

    void SynchronizePlayer(NetworkPlayer player)
    {
        networkView.RPC("InitializeGameTime", player, CurrentTime);
    }

    [RPC]
    void InitializeGameTime(float time)
    {
        SetTime(time);
    }

    private void UpdateSkybox()
    {
        float currentVal;

        if (CurrentTime >6 && CurrentTime < 8)
        {
            float currentPercentage = (CurrentTime - 6f) / 2f ;
            currentVal = 1 - currentPercentage;
            skybox.material.SetFloat("_Blend", currentVal);

        }

        else if (CurrentTime > 18 && CurrentTime < 20)
        {
            float currentPercentage = (CurrentTime - 18f) / 2f;
            currentVal = currentPercentage;
            skybox.material.SetFloat("_Blend", currentVal);

        }


    }


    internal static void SetDayCycleInMinutes(float dayCycle)
    {
        Main.dayCycleInMinutes = dayCycle;
    }

    internal static void SetTime(float gameTime)
    {
        Main.timeOfDay = gameTime;

        if (gameTime > 8 && gameTime < 18)
            Main.skybox.material.SetFloat("_Blend", 0);
        else if (gameTime > 20 && gameTime < 6)
            Main.skybox.material.SetFloat("_Blend", 1);

        SunManager.SetTime(gameTime);
    }

    internal static void Reset()
    {
        SetTime(7.0f);  //Set time to the start of dawn.
    }
}

