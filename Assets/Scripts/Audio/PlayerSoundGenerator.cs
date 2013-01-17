using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[PlayerComponent(PlayerScriptType.AllEnabled)]
public class PlayerSoundGenerator : MonoBehaviour {
    AudioClip[] footStepClip = new AudioClip[1];
    AudioClip swordSwingClip = new AudioClip();
    AudioClip arrowFireClip = new AudioClip();
    AudioClip arrowDrawClip;
    AudioClip swordAttackHitClip;

    Dictionary<string, AudioClip> elements;

    /// <summary>
    /// Initializes each animation clip that is used for player sounds.
    /// </summary>
    void Start()
    {
        elements = new Dictionary<string, AudioClip>();
        footStepClip[0] = Resources.Load("Sounds/footstep_grass_01") as AudioClip;
        arrowFireClip = Resources.Load("Sounds/bow_fire") as AudioClip;
        swordSwingClip = Resources.Load("Sounds/sword_swoosh") as AudioClip;
        arrowDrawClip = Resources.Load("Sounds/arrow_draw_fast") as AudioClip;
        swordAttackHitClip = Resources.Load("Sounds/sword_hit") as AudioClip;
    
    }

    AudioClip GetAudioClip(string key)
    {
        if (!elements.ContainsKey(key))
        {
            elements[key] = Resources.Load("Sounds/" + key) as AudioClip;
        }
        return elements[key];
    }

    /// <summary>
    /// Footstep sound effect. Should eventually be able to detect which material the player 
    /// is standing on and play an appropriate sound
    /// </summary>
    void PlayFootStep()
    {
        PlayRandomPitch(footStepClip[Random.Range(0, footStepClip.Length)], .9f, 1.1f, 2.0f);
    }

    /// <summary>
    /// Plays a sound effect when the player swings his weapon.
    /// </summary>
    void PlaySwordSwing()
    {
      PlayRandomPitch(swordSwingClip,.75f, 1.25f);

    }

    /// <summary>
    /// Plays an audio clip at a randomize pitch.
    /// </summary>
    /// <param name="clip">The clip to play</param>
    /// <param name="min">The minimum pitch</param>
    /// <param name="max">The maximum pitch</param>
    /// <param name="volume">The volume to play the clip at</param>
    void PlayRandomPitch(AudioClip clip, float min, float max, float volume = 1.0f)
    {
        float pitch = Random.Range(min, max);
        if (audio)
        {
            audio.pitch = pitch;
            audio.PlayOneShot(clip, volume);
        }
    }

    /// <summary>
    /// Played when the player releases his drawn arrow
    /// </summary>
    internal void PlayArrowFire()
    {
        PlayRandomPitch(arrowFireClip, .94f, 1.15f);
    }

    /// <summary>
    /// Plays when the player draws an arrow
    /// </summary>
    internal void PlayArrowDraw()
    {
        PlayRandomPitch(arrowDrawClip, .94f, 1.15f, .4f);
    }

    internal void PlaySwingAttackHitSound(Collider other)
    {
        if (other.material)
        {
            if (other.material.name == "Metal (Instance)" || other.material.name == "Metal")
            {
                PlayRandomPitch(GetAudioClip("sword on metal"), .9f, 1.1f);
            }
            else
            {
                PlayRandomPitch(swordAttackHitClip, .94f, 1.15f);
            }
        }
        else
        {
            PlayRandomPitch(swordAttackHitClip, .94f, 1.15f);
        }
    }
}
