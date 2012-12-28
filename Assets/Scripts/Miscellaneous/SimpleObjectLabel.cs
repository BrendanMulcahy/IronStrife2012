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
    private Vector3 offset;	// Units in world space to offset; 1 unit above object by default
    private Camera cam;
    private Transform thisTransform;
    private Transform camTransform;
    Vector3 screenOffset;
    private string labelText;

    void Start()
    {
        labelText = transform.root.gameObject.name;
        thisTransform = transform;
        cam = Camera.main;
        camTransform = cam.transform;
        height = .1f;
        offset = new Vector3(0, height, 0);

    }

    void LateUpdate()
    {
        if (Vector3.Dot(camTransform.forward, target.position - camTransform.position) < 0)
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
        guiText.material.color = new Color(230, 232, 250);
        guiText.text = labelText;

    }
}
