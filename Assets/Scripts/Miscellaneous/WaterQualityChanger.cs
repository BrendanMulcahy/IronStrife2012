using System.Collections;
using UnityEngine;

public class WaterQualityChanger : QualityChanger
{
    Water water;
    Transform camTransform;
    Transform thisTransform;
    public float distanceThreshold = 150;

    void Start() { 
        water = this.GetComponent<Water>(); 
        camTransform = Camera.main.transform;
        thisTransform = this.transform;
        StartCoroutine(TryDisable());
    }

    public override void ChangeToQualityLevel(int level)
    {
        if (!water) return;
        switch (level)
        {
            case 0:
            case 1:
                water.m_WaterMode = Water.WaterMode.Simple;
                break;
            case 2:
            case 3:
                water.m_WaterMode = Water.WaterMode.Reflective;

                break;
            case 4:
            case 5:
                water.m_WaterMode = Water.WaterMode.Reflective;
                break;
        }
    }


    private IEnumerator TryDisable()
    {
        while (true)
        {
            if (Vector3.Distance(thisTransform.position, camTransform.position) > distanceThreshold)
            {
                water.enabled = false;
                StartCoroutine(TryEnable());
                yield break;
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    private IEnumerator TryEnable()
    {
        while (true)
        {
            if (Vector3.Distance(thisTransform.position, camTransform.position) <= distanceThreshold)
            {
                water.enabled = true;
                StartCoroutine(TryDisable());
                yield break;
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    void Update()
    {

    }
}