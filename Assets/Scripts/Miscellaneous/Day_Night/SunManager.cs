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
		
		//SunSet
        if (currentTime > 17 && currentTime < 17.8)
        {
            float currentPercentage = (currentTime - 17f) / .8f;
            foreach (Sun light in suns)
            {
                light.light.intensity = Mathf.Lerp(light.maxLightBrightness, light.minLightBrightness, currentPercentage);
				light.GetComponent<LensFlare>().brightness = Mathf.Lerp(light.maxFlareBrightness, light.minFlareBrightness, currentPercentage);
				//Change flare color and color of the light over time
				light.GetComponent<LensFlare>().color = Color.Lerp(Color.yellow, Color.red,currentPercentage);
				light.light.color = Color.Lerp(Color.white, Color.red, currentPercentage);
				
            }
			
        }
		//MoonRise
		else if (currentTime > 18.2 && currentTime < 19.2)
		{
			float currentPercentage = currentTime - 18.2f;
			foreach (Moon light in moons)
            {
                light.light.intensity = Mathf.Lerp(light.minLightBrightness, light.maxLightBrightness, currentPercentage);
				light.GetComponent<LensFlare>().brightness = Mathf.Lerp(light.minFlareBrightness, light.maxFlareBrightness, currentPercentage);
            }
			
		}
		//MoonSet
		else if (currentTime > 4.7 && currentTime < 5.7)
		{
			float currentPercentage = currentTime - 4.7f;
			foreach (Moon light in moons)
            {
                light.light.intensity = Mathf.Lerp(light.maxLightBrightness, light.minLightBrightness, currentPercentage);
				light.GetComponent<LensFlare>().brightness = Mathf.Lerp(light.maxFlareBrightness, light.minFlareBrightness, currentPercentage);
            }
			
		}
		//SunRise
		else if (currentTime > 6.3 && currentTime < 7.3)
		{
			float currentPercentage = currentTime - 6.3f;
			foreach (Sun light in suns)
            {
                light.light.intensity = Mathf.Lerp(light.minLightBrightness, light.maxLightBrightness, currentPercentage);
				light.GetComponent<LensFlare>().brightness = Mathf.Lerp(light.minFlareBrightness, light.maxFlareBrightness, currentPercentage);
				//Change flare color and color of the light over time
				light.GetComponent<LensFlare>().color = Color.Lerp(Color.red, Color.yellow,currentPercentage);
				light.light.color = Color.Lerp(Color.red, Color.white, currentPercentage);
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

    internal static void SetTime(float gameTime)
    {
        if (gameTime > 7.3 && gameTime < 17)
        {
            foreach (Sun light in Main.suns)
            {
                light.light.intensity = light.maxLightBrightness;
				light.GetComponent<LensFlare>().brightness = light.maxFlareBrightness;
				light.light.color = Color.white;
            }
            foreach (Moon light in Main.moons)
            {
                light.light.intensity = light.minLightBrightness;
				light.GetComponent<LensFlare>().brightness = light.minFlareBrightness;
            }
        }

        else if (gameTime > 19.2 && gameTime < 6.3)
        {
            foreach (Sun light in Main.suns)
            {
                light.light.intensity = light.minLightBrightness;
                light.GetComponent<LensFlare>().brightness = light.minFlareBrightness;
                light.light.color = Color.red;
            }
            foreach (Moon light in Main.moons)
            {
                light.light.intensity = light.maxLightBrightness;
                light.GetComponent<LensFlare>().brightness = light.maxFlareBrightness;
            }
        }

    }
}
