using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DefaultSceneObject("SunManager")]

/// <summary>
/// Sun manager.
/// This class is used to create variable brightness for the sun and moon. As each of them are rising they will increase in brightness, and as each of
/// them are setting they will decrease in brightness. Also the sun flare will change color to simulate a real sunset.
/// </summary>
public class SunManager : MonoBehaviour {
	
	
	public LinkedList<Sun> suns = new LinkedList<Sun>();
	public LinkedList<Moon> moons = new LinkedList<Moon>();

    private static SunManager _instance;
    public static SunManager Main { 
        get {
        if (_instance == null)
        {
            _instance = new GameObject("SunManager").AddComponent<SunManager>();
        }
        return _instance;
    }
    }
	
	//Update is called once per frame
	void Update () {
		
		float currentTime = GameTime.CurrentTime;

        if (currentTime > 17 && currentTime < 18)
        {
            float currentPercentage = currentTime - 17f;
            foreach (Sun light in suns)
            {
                light.light.intensity = Mathf.Lerp(light.maxLightBrightness, light.minLightBrightness, currentPercentage);
				light.GetComponent<LensFlare>().brightness = Mathf.Lerp(light.maxFlareBrightness, light.minFlareBrightness, currentPercentage);
            }
			
        }
		else if (currentTime > 18.2 && currentTime < 19.2)
		{
			float currentPercentage = currentTime - 18.2f;
			foreach (Moon light in moons)
            {
                light.light.intensity = Mathf.Lerp(light.minLightBrightness, light.maxLightBrightness, currentPercentage);
				light.GetComponent<LensFlare>().brightness = Mathf.Lerp(light.minFlareBrightness, light.maxFlareBrightness, currentPercentage);
            }
			
		}
		
		else if (currentTime > 4.7 && currentTime < 5.7)
		{
			float currentPercentage = currentTime - 4.7f;
			foreach (Moon light in moons)
            {
                light.light.intensity = Mathf.Lerp(light.maxLightBrightness, light.minLightBrightness, currentPercentage);
				light.GetComponent<LensFlare>().brightness = Mathf.Lerp(light.maxFlareBrightness, light.minFlareBrightness, currentPercentage);
            }
			
		}
		else if (currentTime > 6.3 && currentTime < 7.3)
		{
			float currentPercentage = currentTime - 6.3f;
			foreach (Sun light in suns)
            {
                light.light.intensity = Mathf.Lerp(light.minLightBrightness, light.maxLightBrightness, currentPercentage);
				light.GetComponent<LensFlare>().brightness = Mathf.Lerp(light.minFlareBrightness, light.maxFlareBrightness, currentPercentage);
            }
			
		}
		
	
	}
	
	public void AddLight(Sun s)
    {
        suns.AddLast(s);
    }
	
	public void AddLight(Moon m)
	{
		moons.AddLast(m);
	}
}
