using UnityEngine;

public class NightLight : MonoBehaviour
{
    public float dayIntensity;
    public float nightIntensity;

    public float dayRange;
    public float nightRange;

    void Awake()
    {
        NightLightManager.Main.AddLight(this);  // Register with the night light manager.
    }
}