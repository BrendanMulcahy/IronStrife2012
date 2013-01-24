using UnityEngine;
using System.Collections;


[AddComponentMenu("Environments/Sun")]
public class Sun : MonoBehaviour {
	public float maxLightBrightness;
	public float minLightBrightness;
	
	public float maxFlareBrightness;
	public float minFlareBrightness;
	
	
	void Awake()
    {
        SunManager.Main.AddLight(this);  // Register with the night light manager.
    }
	
	
}
