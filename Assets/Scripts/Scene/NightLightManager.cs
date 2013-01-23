using System.Collections.Generic;
using UnityEngine;

[DefaultSceneObject("NightLightManager")]
public class NightLightManager : MonoBehaviour
{
    public LinkedList<NightLight> lights = new LinkedList<NightLight>();

    private static NightLightManager _instance;
    public static NightLightManager Main { 
        get {
        if (_instance == null)
        {
            _instance = new GameObject("NightLightManager").AddComponent<NightLightManager>();
        }
        return _instance;
    }
    }

    void Update()
    {

        float currentTime = GameTime.CurrentTime;

        if (currentTime > 8 && currentTime < 9)
        {
            float currentPercentage = currentTime - 8f;
            foreach (NightLight light in lights)
            {
                light.light.intensity = Mathf.Lerp(light.nightIntensity, light.dayIntensity, currentPercentage);
                light.light.range = Mathf.Lerp(light.nightRange, light.dayRange, currentPercentage);

            }
        }

        else if (currentTime > 20 && currentTime < 21)
        {
            float currentPercentage = currentTime - 20f;
            foreach (NightLight light in lights)
            {
                light.light.intensity = Mathf.Lerp(light.dayIntensity, light.nightIntensity, currentPercentage);
                light.light.range = Mathf.Lerp(light.dayRange, light.nightRange, currentPercentage);

            }
        }


    }
    

    public void AddLight(NightLight l)
    {
        lights.AddLast(l);
    }

    public void RemoveLight(NightLight l)
    { 
    
    }    
}