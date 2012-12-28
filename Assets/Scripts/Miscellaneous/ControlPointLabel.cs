using UnityEngine;
using System;

[RequireComponent(typeof(GUIText))]
[RequireComponent(typeof(GUITexture))]
public class ControlPointLabel : MonoBehaviour
{

    public Transform target;		// Object that this label should follow
    float height = 10f;	// Height above player to display label
    private Vector3 offset;	// Units in world space to offset; 1 unit above object by default
    private Camera cam;
    private Transform thisTransform;
    private Transform camTransform;
    Vector3 screenOffset;
    private float maxDisplayDistance = 70.0f;

    void Start()
    {
        thisTransform = transform;
        cam = Camera.main;

        camTransform = cam.transform;
        var inset = guiTexture.pixelInset;
        inset.y = -20;
        guiTexture.pixelInset = inset;
        offset = new Vector3(0, height, 0);
        name = this.transform.parent.gameObject.name;
    }

    void LateUpdate()
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

    void DisableLabels()
    {
        guiText.enabled = false;
        guiTexture.enabled = false;
    }

    void EnableLabels()
    {
        guiText.enabled = true;
        guiTexture.enabled = true;

    }

    void DestroyLabels()
    {
        Destroy(this.gameObject);
    }

    void TrackPlayer()
    {
        thisTransform.position = cam.WorldToViewportPoint(target.position + offset);
        guiText.material.color = new Color(230, 232, 250);
        guiText.text = name + ": " + (int)this.transform.parent.gameObject.GetComponent<ControlPoint>().influence + (int)this.transform.parent.gameObject.GetComponent<ControlPoint>().allegiance;
        double controlPercentage = (double)this.transform.parent.gameObject.GetComponent<ControlPoint>().influence / (double)this.transform.parent.gameObject.GetComponent<ControlPoint>().MAXINFLUENCE;
        var inset = guiTexture.pixelInset;
        inset.width = (float)(100 * controlPercentage) - 28;
        inset.x = 10;
        guiTexture.pixelInset = inset;

        var color = guiText.material.color;
        color.a = .7f - (Vector3.Distance(camTransform.position, transform.root.position) / maxDisplayDistance);
        guiText.material.color = color;

        color = guiTexture.color;
        color.a = .7f - (Vector3.Distance(camTransform.position, transform.root.position) / maxDisplayDistance);
        guiTexture.color = color;

    }
}
