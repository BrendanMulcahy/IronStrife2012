using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CloudZone : MonoBehaviour {

    public int numClouds = 5;
    public Vector3 windDirection;
    public float windSpeed = 3.0f;

    private List<GameObject> clouds = new List<GameObject>();
    private List<GameObject> cloudPrefabs = new List<GameObject>();
    private Transform sunTransform;
    private Transform moonTransform;
    private Transform currentLightTransform;

	void Start () {
        this.collider.isTrigger = true;

        var cloudprefs = Resources.LoadAll("Clouds", typeof(GameObject));
        foreach (Object o in cloudprefs)
        {
            cloudPrefabs.Add(o as GameObject);
        }

        for (int g = numClouds; g-- > 0; )
        {
            MakeNewCloud(randomizeZ:true);
        }

        windDirection = new Vector3(0,0,1);
        windSpeed = Random.Range(3.0f, 8.0f);

        sunTransform = GameObject.Find("Sun").transform;
        moonTransform = GameObject.Find("Moon").transform;

        currentLightTransform = sunTransform;
	}

    void Update()
    {
        foreach (GameObject cloud in clouds)
        {
            cloud.transform.position += windDirection.normalized * windSpeed * Time.deltaTime;
        }

        if (GameTime.CurrentTime == 6)
        {
            if (currentLightTransform != sunTransform)
            {
                currentLightTransform = sunTransform;
                SetSuns();
            }
        }

        if (GameTime.CurrentTime == 18)
        {
            if (currentLightTransform != moonTransform)
            {
                currentLightTransform = moonTransform;
                SetSuns();
            }
        }
	}

    private void SetSuns()
    {
        foreach (GameObject cloud in clouds)
        {
            cloud.GetComponent<CS_Cloud>().Sun = currentLightTransform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject + "has triggered the cloudzone");
        if (clouds.Contains(other.gameObject))
        {
            Debug.Log("A cloud has exited the cloud zone. Making a new one.");
            StartCoroutine(RemoveCloud(other.gameObject));
            MakeNewCloud();
        }
    }

    IEnumerator RemoveCloud(GameObject cloud)
    {
        yield return new WaitForSeconds(10f);
        clouds.Remove(cloud);
        Destroy(cloud);
    }

    private void MakeNewCloud(bool randomizeZ = false)
    {
        var xPos = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        var yPos = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        float zPos;
        if (randomizeZ)
            zPos = Random.Range(collider.bounds.min.z, collider.bounds.max.z);
        else
            zPos = collider.bounds.min.z;
        var position = new Vector3(xPos, yPos , zPos);

        var cloudIndex = Random.Range(0, cloudPrefabs.Count);
        var newCloud = Instantiate(cloudPrefabs[cloudIndex], position, Quaternion.identity) as GameObject;
        newCloud.GetComponent<CS_Cloud>().Sun = GameObject.Find("Sun").transform;
        clouds.Add(newCloud);
    }   
}
