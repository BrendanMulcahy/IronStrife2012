using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	public Transform[] suns;
	private Sun sunBright;
    public Quaternion[] initialRotations;
	
	public float dayCycleInMinutes = 1;
	
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
		Sun temp = suns[0].GetComponent<Sun>();
		if (temp == null)
		{
			Debug.Log("SunScript not found. Adding it");
			suns[0].gameObject.AddComponent<Sun>();
			temp = suns[0].GetComponent<Sun>();
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
        
		for (int i = 0; i < suns.Length; i++)
		{
            float rotationAmount = (CurrentTime / 24f) * 360f;
            suns[i].transform.rotation = initialRotations[i];
            suns[i].Rotate(new Vector3(1, 0, 0), rotationAmount);

		}

        UpdateSkybox();
	}

    private void UpdateSkybox()
    {
        float currentVal;

        if (CurrentTime >6 && CurrentTime < 9)
        {
            float currentPercentage = (CurrentTime - 6f) / 3f ;
            currentVal = 1 - currentPercentage;
            skyboxMaterial.SetFloat("_Blend", currentVal);

        }

        else if (CurrentTime > 18 && CurrentTime < 21)
        {
            float currentPercentage = (CurrentTime - 18f) / 3f;
            currentVal = currentPercentage;
            skyboxMaterial.SetFloat("_Blend", currentVal);

        }


    }
}

