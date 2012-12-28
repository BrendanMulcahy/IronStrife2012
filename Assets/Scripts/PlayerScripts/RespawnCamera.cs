﻿using System;
using UnityEngine;

public class RespawnCamera : MonoBehaviour
{
    Vector3 targetLocation;
    bool CanRespawn { get { return ((PlayerStats)Util.MyLocalPlayerObject.GetCharacterStats()).canRespawn; } }
    void Start()
    {
        
    }

    void OnEnable()
    {
        targetLocation = new Vector3(400, 200, 200);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, 75.0f * Time.deltaTime);
        transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

        if (Input.GetButtonDown("Fire1"))
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Debug.Log("You clicked on " + hit.point.ToString());
                if (CanRespawn)
                {
                    if (Network.isServer)
                    {
                        ((PlayerStats)Util.MyLocalPlayerObject.GetCharacterStats()).TryRespawn(hit.point);
                        Util.MyLocalPlayerObject.GetComponent<PlayerStats>().TryRespawn(hit.point);
                    }
                    else
                    {
                        Util.MyLocalPlayerObject.networkView.RPC("TryRespawn", RPCMode.Server, hit.point);
                    }
                }
            }
        }
    }
}
