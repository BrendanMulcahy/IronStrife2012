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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float currentspeed = GetComponent<NPC_Controller>().MoveSpeed;

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
        SendMessage("SyncAnimation", anim, SendMessageOptions.DontRequireReceiver);
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
