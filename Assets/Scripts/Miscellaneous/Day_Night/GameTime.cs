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

    public Material skyboxMaterial;

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
		
        skyboxMaterial = RenderSettings.skybox;

        initialRotations = new Quaternion[2];
        initialRotations[0] = Quaternion.Euler(90, 0, 0);
        initialRotations[1] = Quaternion.Euler(270, 0, 0);
		
		timeOfDay = 7.5f; // Start at 7:30 AM
	}
	
	// Update is called once per frame
	void Update () {
        timeOfDay += (Time.deltaTime / HOUR) * (1440f / dayCycleInMinutes);
        timeOfDay = timeOfDay % 24;
        
        float rotationAmount = (CurrentTime / 24f) * 360f;
		moons[0].transform.rotation = initialRotations[0];
        moons[0].Rotate(new Vector3(1, 0, 0), rotationAmount);
		
        suns[0].transform.rotation = initialRotations[1];
        suns[0].Rotate(new Vector3(1, 0, 0), rotationAmount);
		
        UpdateSkybox();
	}

    void SynchronizePlayer(NetworkPlayer player)
    {
        networkView.RPC("InitializeGameTime", player, CurrentTime);
    }

    [RPC]
    void InitializeGameTime(float time)
    {
        timeOfDay = time;
    }

    private void UpdateSkybox()
    {
        float currentVal;

        if (CurrentTime >6 && CurrentTime < 8)
        {
            float currentPercentage = (CurrentTime - 6f) / 2f ;
            currentVal = 1 - currentPercentage;
            skyboxMaterial.SetFloat("_Blend", currentVal);

        }

        else if (CurrentTime > 18 && CurrentTime < 20)
        {
            float currentPercentage = (CurrentTime - 18f) / 2f;
            currentVal = currentPercentage;
            skyboxMaterial.SetFloat("_Blend", currentVal);

        }


    }

    internal static void SetTime(float gameTime)
    {
        Main.timeOfDay = gameTime;
    }
}

