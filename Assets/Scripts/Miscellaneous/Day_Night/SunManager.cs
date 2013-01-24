using UnityEngine;
using System.Collections;

[DefaultSceneObject("SunManager")]
public class SunManager : MonoBehaviour {
	
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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void AddLight(NightLight l)
    {
        lights.AddLast(l);
    }
}
