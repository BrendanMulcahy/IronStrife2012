using UnityEngine;
using System;

/// <summary>
/// Used to display a label over the GameObject while in the game.
/// </summary>
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(GUIText))]
[RequireComponent(typeof(GUITexture))]
public class CharacterLabel : MonoBehaviour {

	public Transform target;		// Object that this label should follow
	private Vector3 offset;	// Units in world space to offset; 1 unit above object by default
	private Camera cam;
	private Transform thisTransform;
	private Transform camTransform;
	Vector3 screenOffset;
	private int maxHealthWidth = 75;
    private float maxDisplayDistance = 40.0f;
    CharacterStats stats;
 
	void Start () 
	{
        if (!target)
        {
            target = this.transform.root;
        }
		thisTransform = transform;
		cam = Camera.main;
		camTransform = cam.transform;
		var inset = guiTexture.pixelInset;
		inset.y = -35;
        inset.height = 15;
		guiTexture.pixelInset = inset;
        var targetHeight = ((CharacterController)target.collider).center.y + ((CharacterController)target.collider).height *.7f;
        Debug.Log("Target height is " + targetHeight);
        offset = new Vector3(0, targetHeight, 0); 
        stats = transform.root.gameObject.GetCharacterStats();
        guiTexture.texture = Resources.Load("WhiteSquare") as Texture2D;
        thisTransform.localScale = new Vector3();
	}

    void OnSetOwnership() { Destroy(this); }
	 
	void LateUpdate () 
	{	
		if (Vector3.Distance(camTransform.position, transform.root.position) > maxDisplayDistance 
            || Vector3.Dot(camTransform.forward, target.position-camTransform.position) < 0)
        {
			DisableLabels();
			return;
		}
		EnableLabels();
		enabled = true;
		
		TrackPlayer();
	
	}
	
	public void DisableLabels()
	{
		guiText.enabled = false;
		guiTexture.enabled = false;
	}

    public void EnableLabels()
	{	guiText.enabled = true;
		guiTexture.enabled = true;
	
	}
	
	void DestroyLabels()
	{
		Destroy(this.gameObject);
	}
	
	void TrackPlayer()
	{
        SetTeamColor();

		thisTransform.position = cam.WorldToViewportPoint(target.position + offset);
        guiText.text = transform.root.gameObject.name;
        var color = guiText.material.color;
        color.a = 1f - (Vector3.Distance(camTransform.position, transform.root.position) / maxDisplayDistance);
        guiText.material.color = color;

        double healthPercentage = stats.Health.CurrentPercentage;
		var inset = guiTexture.pixelInset;
		inset.width = (float ) (maxHealthWidth * healthPercentage);
		inset.x = -(maxHealthWidth / 2);
		guiTexture.pixelInset = inset;
        color = guiTexture.color;
        color.a = .7f - (Vector3.Distance(camTransform.position, transform.root.position) / maxDisplayDistance);
        guiTexture.color = color;
	}

    private void SetTeamColor()
    {
        if (Util.MyLocalPlayerTeam == stats.TeamNumber)
        {
            var color = Color.green;
            guiText.material.color = color;
            guiTexture.color = color;

        }
        else
        {
            var color = Color.red;
            guiText.material.color = color;
            guiTexture.color = color;

        }
    }
}
