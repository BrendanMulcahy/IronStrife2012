using UnityEngine;

/// <summary>
/// Effect that creates a glowing outline around a gameObject and cleans it up when this component is removed.
/// </summary>
public class GlowOutline : MonoBehaviour
{
    GameObject clone;
    Material outlineMat;
    float outlineWidth = 0f;
    float cycleTime = .66f;

    public Color color = Color.red;
    
    void Start()
    {
        if (this.gameObject.GetComponents<GlowOutline>().Length > 1)
            Destroy(this);
        clone = Instantiate((Object)this.gameObject) as GameObject;
        Destroy(clone.GetComponent<GlowOutline>());

        var lights = clone.GetComponentsInChildren<Light>();
        Util.Destroy(lights);
        var audioSources = clone.GetComponentsInChildren<AudioSource>();
        Util.Destroy(audioSources);
        var interactableObjects = clone.GetComponentsInChildren<InteractableObject>();
        Util.Destroy(interactableObjects);
        var searchers = clone.GetComponentsInChildren<EnemySearcher>();
        Util.Destroy(searchers);

        clone.transform.SetParentAndCenter(this.transform);
        clone.SetLayerRecursively(14);
        clone.AddComponent<GlowCloneScript>();
        outlineMat = Resources.Load("Materials/Outlined Only") as Material;

        Renderer[] renderers = clone.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.material = outlineMat;
            r.material.SetColor("_OutlineColor", color);
        }
    }

    void Update()
    {
        outlineWidth = (outlineWidth + (Time.deltaTime / cycleTime) * .009f) % .009f;
        outlineMat.SetFloat("_Outline", outlineWidth);

        // ----Experimental scale cycle
        //scale = (scale + Time.deltaTime * cycleTime) % cloneMaxScaleIncrease;
        //clone.transform.localScale = new Vector3(1+scale, 1+scale, 1+scale);
    }

    void OnDestroy()
    {
        Destroy(clone);
    }

}

public class GlowCloneScript : MonoBehaviour
{
    void LateUpdate()
    {
        this.transform.localPosition = new Vector3();
    }
}
