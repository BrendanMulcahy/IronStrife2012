﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInteractor : MonoBehaviour
{
    const float interactionRayDistance = 25f;
    const float maxInteractionDistance = 5f;

    public GameObject selectedGO;

    void Start()
    {
        var selectionSphere = new GameObject("Object Interactor");
        selectionSphere.transform.parent = this.transform;
        selectionSphere.transform.localPosition = transform.forward * 2f + Vector3.up * 1.4f;
        selectionSphere.AddComponent<PlayerSelectorSphere>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionRayDistance))
            {
                var io = hit.collider.gameObject.GetComponent<InteractableObject>();
                if (io && Vector3.Distance(this.transform.position, collider.transform.position) < maxInteractionDistance)
                {
                    io.InteractWith(this.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedGO)
            {
                selectedGO.GetComponent<InteractableObject>().InteractWith(this.gameObject);
            }
        }
    }

    internal void SelectObject(GameObject go)
    {
        if (selectedGO)
        {
            Destroy(selectedGO.GetComponent<GlowOutline>());
        }
        selectedGO = go;
        selectedGO.AddComponent<GlowOutline>().color = Color.green;
    }

    internal void DeselectObject()
    {
        if (selectedGO.GetComponent<GlowOutline>())
            Destroy(selectedGO.GetComponent<GlowOutline>());
        selectedGO = null;

    }
}

public class PlayerSelectorSphere : MonoBehaviour
{
    PlayerObjectInteractor poi;
    float closestDistance = 999999f;

    List<GameObject> objectsInRange = new List<GameObject>();
    void Start()
    {
        poi = transform.parent.GetComponent<PlayerObjectInteractor>();
        var collider = gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(3, 3, 3);
        collider.isTrigger = true;
        var rigid = gameObject.AddComponent<Rigidbody>();
        rigid.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            if (!objectsInRange.Contains(other.gameObject))
                objectsInRange.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            if (objectsInRange.Contains(other.gameObject))
                objectsInRange.Remove(other.gameObject);
            if (poi.selectedGO == other.gameObject)
            {
                poi.DeselectObject();
            }
        }
    }

    void Update()
    {
        if (poi.selectedGO)
        {
            closestDistance = Vector3.Distance(poi.selectedGO.transform.position, transform.root.position);
        }
        else
        {
            closestDistance = 99999f;
        }

        for (int g = 0; g < objectsInRange.Count; g++)
        {
            if (objectsInRange[g] == null)
            {
                objectsInRange.RemoveAt(g); g--; continue;
            }
            if (objectsInRange[g] == poi.selectedGO) continue;

            var goDistance = Vector3.Distance(transform.root.position, objectsInRange[g].transform.position);
            if (goDistance < closestDistance)
            {
                closestDistance = goDistance;
                poi.SelectObject(objectsInRange[g]);
            }
        }
    }


}
