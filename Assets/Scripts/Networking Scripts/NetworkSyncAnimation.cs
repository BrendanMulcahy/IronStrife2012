using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

/// <summary>
/// Handles syncing animation information over the network. 
/// The server will send continuous info that clients read and use to animate the player.
/// </summary>
public class NetworkSyncAnimation : MonoBehaviour {

    private ArrayList animationList;

    void Awake()
    {
        //ThirdPersonSimpleAnimation.InitializeAnimation(animation);
    }

    void Start()
    {
        animationList = Util.GetAnimationList(GetComponent<Animation>().animation);
    }

	private int currentAnimation = 0;
	private int lastAnimation = 0;

    public string curAni;
	
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

}
