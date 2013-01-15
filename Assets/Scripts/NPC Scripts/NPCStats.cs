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
            Strength.ChangeBaseValue(10); Health.CurrentValue = 100; Health.MaxValue = 100;
            Debug.Log("Setting new kill reward.");
            reward = new KillReward(200, 200);
            Debug.Log("kill reward is " + reward.experience + " | " + reward.iron);
        }

        else if (rand == 8 || rand == 9)
        {
            Strength.ChangeBaseValue(15); Health.CurrentValue = 150; Health.MaxValue = 150;
            networkView.RPC("ChangeName", RPCMode.All, "Iron Captain");
            reward = new KillReward(500, 300);
            Debug.Log("kill reward is " + reward.experience + " | " + reward.iron);


        }
        else if (rand == 10)
        {
            Strength.ChangeBaseValue(36); Health.CurrentValue = 350; Health.MaxValue = 350;
            networkView.RPC("ChangeName", RPCMode.All, "Iron Champion");
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            reward = new KillReward(2000, 2000);
            Debug.Log("kill reward is " + reward.experience + " | " + reward.iron);


        }
        return;
    
    }


}
