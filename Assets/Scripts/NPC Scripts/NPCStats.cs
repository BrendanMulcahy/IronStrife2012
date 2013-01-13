using UnityEngine;
using System;

public class NPCStats : CharacterStats
{
    public void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        if (Network.isClient) return;
        int rand = (int)UnityEngine.Random.Range(0, 11);
        if (rand >= 0 && rand <= 7)
        {
            networkView.RPC("ChangeName", RPCMode.All, "Iron Guard");
            Strength = 10; Health = 100; MaxHealth = 100;
            Debug.Log("Setting new kill reward.");
            reward = new KillReward(200, 200);
            Debug.Log("kill reward is " + reward.experience + " | " + reward.iron);
        }

        else if (rand == 8 || rand == 9)
        {
            Strength = 15; Health = 150; MaxHealth = 150;
            networkView.RPC("ChangeName", RPCMode.All, "Iron Captain");
            reward = new KillReward(500, 300);
            Debug.Log("kill reward is " + reward.experience + " | " + reward.iron);


        }
        else if (rand == 10)
        {
            Strength = 35; Health = 350; MaxHealth = 350; MoveSpeed = 14f;
            networkView.RPC("ChangeName", RPCMode.All, "Iron Champion");
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            reward = new KillReward(2000, 2000);
            Debug.Log("kill reward is " + reward.experience + " | " + reward.iron);


        }
        return;
    
    }


}
