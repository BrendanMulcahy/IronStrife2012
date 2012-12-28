using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class NPCAnimation : MonoBehaviour {

    AnimState currentstatus = AnimState.Moving_Idle;


    void Awake()
    {
        ThirdPersonSimpleAnimation.InitializeAnimation(animation);
        // By default loop all animations
        animation.wrapMode = WrapMode.Loop;

        // We are in full control here - don't let any other animations play when we start
        animation.Stop();
        animation.Play("Idle01");

    }

    public static void InitializeAnimation(Animation ani)
    {
        var clips = Resources.LoadAll("Animations/Lightweight-Animated", typeof(AnimationClip)).Cast<AnimationClip>();
        foreach (var c in clips)
        {
            ani.AddClip(c, c.name);
        }
        foreach (AnimationState state in ani)
        {
            state.layer = -1;
        }
        ani["RunSwing"].layer = 3;
        ani["RunSwing"].normalizedSpeed = 5 / 4;
        ani["IdleSwing"].normalizedSpeed = 5 / 4;
        ani["IdleSwing"].layer = 3;
        ani["IdleBowAim"].layer = 3;
        ani["IdleBowAim"].wrapMode = WrapMode.Once; //Doesnt loop these animations

        ani["WalkBowAim"].layer = 3;
        ani["WalkBowAim"].wrapMode = WrapMode.Once;

        ani["IdleBowStartAim"].layer = 4;
        ani["IdleBowStartAim"].speed = 1.5f;
        ani["WalkBowStartAim"].layer = 4;
        ani["WalkBowStartAim"].speed = 1.5f;

        ani["IdleBowShootReload"].layer = 4;
        ani["IdleBowShootReload"].wrapMode = WrapMode.Once;
        ani["IdleBowShootReload"].speed = 1.5f;
        ani["WalkBowShootReload"].layer = 4;
        ani["WalkBowShootReload"].wrapMode = WrapMode.Once;
        ani["WalkBowShootReload"].speed = 1.5f;

        ani["IdleBowRelax"].layer = 1;
        ani["IdleBowRelax"].speed = 1.5f;
        ani["WalkBowRelax"].layer = 1;
        ani["WalkBowRelax"].speed = 1.5f;

        ani["RunJumpStart"].layer = 2;
        ani["RunJumpHover"].layer = 1;
        ani["RunJumpLand"].layer = 1;

        ani["Run"].layer = 3;
        ani["Sprint"].layer = 3;
        ani["Idle01"].layer = 3;

        //ani["RunFlinch"].layer = 5;
        //ani["IdleShieldBlockFlinch"].layer = 5;


        ani["RunJumpHover"].speed = .5f;

        ani.Stop();
        ani.Play("Idle01");
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float currentspeed = GetComponent<NPCController>().currentspeed;

        switch (currentstatus)
        {
            case AnimState.Dead:
                if (!animation.IsPlaying("Death"))
                {
                    animation.Stop();
                    CrossFadeAndSync("Death", .1f);
                }
                break;
            case AnimState.Attacking:
                if (currentspeed > 0)
                {
                    CrossFadeAndSync("RunSwing", .02f);
                }
                else
                {
                    CrossFadeAndSync("IdleSwing", .02f);
                }
                
                break;
            case AnimState.Moving_Idle:
                if (currentspeed == 0)
                {
                    CrossFadeAndSync("Idle01", .04f);
                }
                else
                {
                    if (currentspeed < 6 && currentspeed > 0)
                    {
                        CrossFadeAndSync("Run", .03f);
                    }
                    else
                    {
                        CrossFadeAndSync("Sprint", .03f);
                    }
                }
                break;
        }
        


        
	}

    private void CrossFadeAndSync(string anim, float fadeLength = 0.0f)
    {
        animation.CrossFade(anim, fadeLength);
        SendMessage("SyncAnimation", anim);
    } 


    public void Die()
    {
        currentstatus = AnimState.Dead;
    }


    public void ToggleAttackAnimation()
    {
        if (currentstatus == AnimState.Attacking)
        {
            currentstatus = AnimState.Moving_Idle;
        }
        else if (currentstatus == AnimState.Moving_Idle)
        {
            currentstatus = AnimState.Attacking;
        }
            
    }

    public enum AnimState
    {
        Attacking,
        Moving_Idle,
        Dead
    }


}
