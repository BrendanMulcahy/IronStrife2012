using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

[PlayerComponent(PlayerScriptType.ServerEnabled, PlayerScriptType.ClientEnabled, PlayerScriptType.ClientOwnerDisabled)]
/// <summary>
/// Handles syncing animation information over the network. 
/// The server will send continuous info that clients read and use to animate the player.
/// </summary>
public class NetworkSyncAnimation : MonoBehaviour {

    private List<string> animationList;
    bool isFirstLandFrame = false;
    private int currentAnimation = 0;
    private int lastAnimation = 0;
    public string curAni;

    void Start()
    {
        if (this.animation.GetClipCount() == 0 )
            ThirdPersonSimpleAnimation.InitializeAnimation(this.animation);
        animationList = Util.GetAnimationList(GetComponent<Animation>().animation);
        
    }
	
	public void SyncAnimation(String animationValue)
	{
        if (animationList == null)
            animationList = Util.GetAnimationList(GetComponent<Animation>().animation);
		currentAnimation = animationList.IndexOf(animationValue);
	}
	
	// Update is called once per frame
	void Update () {
        if (animationList == null)
            animationList = Util.GetAnimationList(GetComponent<Animation>().animation);

        if (isFirstLandFrame)
        {
            isFirstLandFrame = false;
            animation.CrossFade("RunJumpLand", .05f);
            return;
        }

        curAni = (String)animationList[currentAnimation];
		if (lastAnimation != currentAnimation)
		{
			lastAnimation = currentAnimation;
			animation.CrossFade((String)animationList[currentAnimation]);

		}
	}

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{
			char ani = (char)currentAnimation;
			stream.Serialize(ref ani);
		}
		else
		{
            char ani = (char)0;
			stream.Serialize(ref ani);
            currentAnimation = ani;

		}	
	}

    [RPC]
    void SyncDidLand()
    {
        isFirstLandFrame = true;
    }

    [RPC]
    void StopConjure()
    {
        animation.Stop("IdleConjure");
    }

}
