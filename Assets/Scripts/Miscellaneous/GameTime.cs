using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	public Transform[] suns;
	//Life minutes for one cycle
	public float dayCycleinMinutes = 1;
	
	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	
	private const float DEGREES_PER_SECOND = 360 / DAY;
	
	private float _degreeRotation;
	
	
	private float timeOfDay;

	// Use this for initialization
	void Awake () {
		timeOfDay = 0;
		_degreeRotation = 360 / (dayCycleinMinutes * MINUTE);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(_degreeRotation);
		Vector3 sunVector = new Vector3(_degreeRotation,0,0);
		suns[0].Rotate(sunVector * Time.deltaTime);
		
		timeOfDay += Time.deltaTime;
		Debug.Log(timeOfDay);
	}
}
