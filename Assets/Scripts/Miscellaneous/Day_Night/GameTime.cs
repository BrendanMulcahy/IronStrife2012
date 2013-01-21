using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	public Transform[] suns;
	private Sun sunBright;
	
	public float dayCycleInMinutes = 10;
	
	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	
	private const float DEGREES_PER_SECOND = 360/DAY;
	
	private float degreeRotation;

	private float timeOfDay;
	

	// Use this for initialization
	void Start () {
		Sun temp = suns[0].GetComponent<Sun>();
		if (temp == null)
		{
			Debug.Log("SunScript not found. Adding it");
			suns[0].gameObject.AddComponent<Sun>();
			temp = suns[0].GetComponent<Sun>();
		}
		
		
		timeOfDay = 0;
		degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInMinutes * MINUTE);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < suns.Length; i++)
		{
			suns[i].Rotate(new Vector3(degreeRotation,0,0) * Time.deltaTime);	
		}
		
		timeOfDay += Time.deltaTime;
	}
}

