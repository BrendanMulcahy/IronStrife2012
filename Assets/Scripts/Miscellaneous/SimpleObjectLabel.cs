using UnityEngine;
using System;

/// <summary>
/// Similar to ObjectLabel, but doesn't have a healthbar. Used for static objects that don't have character stats.
/// </summary>
[RequireComponent(typeof(GUIText))]
public class SimpleObjectLabel : MonoBehaviour
{

    public Transform target;		// Object that this label should follow
    float height;	// Height above player to display label
    public Vector3 offset;	// Units in world space to offset; 1 unit above object by default
    private Camera cam;
    private Transform thisTransform;
    private Transform camTransform;
    Vector3 screenOffset;
    private string labelText;
    private float maxDisplayDistance = 40.0f;


    void Start()
    {
        labelText = transform.root.gameObject.name;
        thisTransform = transform;
        cam = Camera.main;
        camTransform = cam.transform;
        guiText.material.color = new Color(230, 232, 250);

    }

    void LateUpdate()
    {
        if (Vector3.Distance(camTransform.position, transform.root.position) > maxDisplayDistance
            || Vector3.Dot(camTransform.forward, target.position - camTransform.position) < 0)
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
    }

    void EnableLabels()
    {
        guiText.enabled = true;

    }

    void DestroyLabels()
    {
        Destroy(this.gameObject);
    }

    void TrackPlayer()
    {
        thisTransform.position = cam.WorldToViewportPoint(target.position + offset);
        guiText.text = labelText;
        var color = guiText.material.color;
        color.a = 1f - (Vector3.Distance(camTransform.position, transform.root.position) / maxDisplayDistance);
        guiText.material.color = color;

    }
}
