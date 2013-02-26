using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[PlayerComponent(PlayerScriptType.AllEnabled)]
public class FootstepSoundGenerator : MonoBehaviour
{
    Transform thisTransform;
    FootstepType type = FootstepType.Stone;
    RaycastHit hit;
    Collider terrainCollider;
    AudioSource thisAudio;
    Dictionary<FootstepType, List<AudioClip>> allClips = new Dictionary<FootstepType, List<AudioClip>>();
    CharacterController cc;
    MoveSpeedStat moveSpeed;

    void Start()
    {
        //StartCoroutine(UpdateGroundType());
        thisTransform = this.transform;
        terrainCollider = Terrain.activeTerrain.collider;
        thisAudio = this.audio;
        cc = GetComponent<CharacterController>();
        moveSpeed = GetComponent<CharacterStats>().MoveSpeed;
        LoadAudioClips();
    }

    private void LoadAudioClips()
    {

        allClips[FootstepType.Dirt] = Resources.LoadAll("Sounds/Footsteps/Dirt").Cast<AudioClip>().ToList();
        allClips[FootstepType.Grass] = Resources.LoadAll("Sounds/Footsteps/Grass").Cast<AudioClip>().ToList();
        allClips[FootstepType.Stone] = Resources.LoadAll("Sounds/Footsteps/Stone").Cast<AudioClip>().ToList();
        allClips[FootstepType.Wood] = Resources.LoadAll("Sounds/Footsteps/Wood").Cast<AudioClip>().ToList();

    }

    /// <summary>
    /// Plays a random footstep sound effect based on what kind of material the player is standing on.
    /// </summary>
    void PlayFootStep()
    {
        UpdateGroundType();
        var clip = GetFootstepClip(type);
        var volumePercent = cc.velocity.magnitude / moveSpeed.ModifiedValue;
        thisAudio.PlayOneShot(clip, volumePercent);
    }

    /// <summary>
    /// Gets a random footstep clip from the list of clips for the given ground type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private AudioClip GetFootstepClip(FootstepType type)
    {
        List<AudioClip> clips = allClips[type];
        var rand = Random.Range(0, clips.Count);
        return clips[rand];
    }

    void UpdateGroundType()
    {
        if (Physics.Raycast(thisTransform.position, Vector3.down, out hit, 2, ~(1 << 9)))
        {
            if (hit.collider != terrainCollider)
            {
                switch (hit.collider.material.name)
                {
                    case "Grass (Instance)":
                        type = FootstepType.Grass;
                        break;
                    case "Stone (Instance)":
                        type = FootstepType.Stone;
                        break;
                    case "Dirt (Instance)":
                        type = FootstepType.Dirt;
                        break;
                    case "Wood (Instance)":
                        type = FootstepType.Wood;
                        break;
                    default:
                        type = FootstepType.Stone;
                        break;
                }
            }

            else
            {
                var mainTex = TerrainSurface.GetMainTexture(thisTransform.position);
                switch (mainTex)
                {
                    case 0:
                        type = FootstepType.Grass;
                        break;
                    case 1:
                    case 6:
                    case 2:
                        type = FootstepType.Stone;
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 7:
                    case 8:
                    case 9:
                        type = FootstepType.Dirt;
                        break;
                    default:
                        type = FootstepType.Stone;
                        break;
                }

            }
        }
    }

    public enum FootstepType { Grass, Stone, Dirt, Wood }
}