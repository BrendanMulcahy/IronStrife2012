using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Base class for handling health, mana, stamina, regeneration
/// and also strength, movespeed, etc.
/// Handles damage taking and dying, respawning, etc.
/// Players use the PlayerStats subclass, NPCs use NPCStats.
/// </summary>
public class CharacterStats : MonoBehaviour {
	
	public int Health { get; set; }
	public int MaxHealth { get; set; }
	public int Mana { get; set; }
	public int MaxMana { get; set; }
    public void ReduceMana(int amount) { Mana -= amount; manaRegenerating = false; timeTilManaRegenerating = maxManaRegenerationTime; }

    private float _stamina;
    public int Stamina { get { return (int)_stamina; } set { _stamina = value; } }
	public int MaxStamina { get; set; }
    public void ReduceStamina(float amount) { _stamina -= amount; staminaRegenerating = false; timeTilStaminaRegenerating = maxStaminaRegenerationTime; }

    public bool healthRegenerating;
    public float timeTilHealthRegenerating;
    private float maxHealthRegenerationTime = 5.0f;
    public int healthRegenerationRate = 1;

    private bool manaRegenerating;
    private int manaRegenerationRate = 1;
    private float maxManaRegenerationTime = 5.0f;
    private float timeTilManaRegenerating;

    private bool staminaRegenerating;
    private int staminaRegenerationRate = 3;
    private float maxStaminaRegenerationTime = 1.5f;
    private float timeTilStaminaRegenerating;

    private InventoryManager inventory;
		
	public int Strength;
    /// <summary>
    /// Attack strength, including all modifiers, such as weapon damage, buffs, etc.
    /// </summary>
    public int EffectiveStrength
    {
        get
        {
            var effectiveStr = Strength;
            if (inventory != null)
                effectiveStr += inventory.currentWeapon.strength;
            return effectiveStr;
        }
    }
	
	public float MoveSpeed { get; set; }

    public int teamNumber = 0;
    public int TeamNumber { get { return teamNumber; } set { networkView.RPC("ChangeTeam", RPCMode.AllBuffered, value); } }

	public KillReward reward;

    public event UnitDiedEventHandler Died;
		
	// Use this for initialization
	public virtual void Start () {
		Health = 100; MaxHealth = 100;
		Mana = 100; MaxMana = 100;
		Stamina = 100; MaxStamina = 100;
		MoveSpeed = 10.0f;
		Strength = 5;
        inventory = gameObject.GetInventory();
	}

    public void StartMonitoringRegeneration()
    {
        StartCoroutine("MonitorRegeneration");
    }

    public void StopMonitoringRegeneration()
    {
        StopCoroutine("MonitorRegeneration");
    }

    internal void StartSyncingHMS()
    {
        StartCoroutine(NetworkSyncHMS());
    }

    private IEnumerator MonitorRegeneration()
    {
        while (true)
        {
            if (healthRegenerating)
            {
                Health = Math.Min(MaxHealth, Health + healthRegenerationRate);
            }
            else
            {
                timeTilHealthRegenerating -= .25f;
                healthRegenerating = (timeTilHealthRegenerating <= 0);
            }

            if (manaRegenerating)
            {
                Mana = Math.Min(MaxMana, Mana + manaRegenerationRate);
            }
            else
            {
                timeTilManaRegenerating -= .25f;
                manaRegenerating = (timeTilManaRegenerating <= 0);
            }

            if (staminaRegenerating)
            {
                Stamina = Math.Min(MaxStamina, Stamina + staminaRegenerationRate);
            }
            else
            {
                timeTilStaminaRegenerating -= .25f;
                staminaRegenerating = (timeTilStaminaRegenerating <= 0);
            }

            yield return new WaitForSeconds(.25f);
        }
    }

    private IEnumerator NetworkSyncHMS()
    {
        int lastSyncedHealth = -1;
        int lastSyncedMana = -1;
        int lastSyncedStamina = -1;

        while (true)
        {
            if (lastSyncedHealth != Health)
            {
                networkView.RPC("HealthChanged", RPCMode.Others, Health);
                lastSyncedHealth = Health;
            }

            if (lastSyncedMana != Mana)
            {
                networkView.RPC("ManaChanged", RPCMode.Others, Mana);
                lastSyncedMana = Mana;
            }

            if (lastSyncedStamina != Stamina)
            {
                networkView.RPC("StaminaChanged", RPCMode.Others, Stamina);
                lastSyncedStamina = Stamina;
            }

            yield return null;
        }
    }

    public void ApplyDamage(GameObject attacker, int damageAmount)
    {
        if (Network.isServer)
        {
        healthRegenerating = false;
        timeTilHealthRegenerating = maxHealthRegenerationTime;
        Health = Mathf.Max(0,Health-damageAmount);

            if (Health <= 0)
            {
                OnDeath(new UnitDiedEventArgs() { killer = attacker, deathPosition = transform.position, reward = this.reward });
            }
        }
    }

    protected void OnDeath(UnitDiedEventArgs unitDiedEventArgs)
    {
        if (Died != null)
        {
            Died(gameObject, unitDiedEventArgs);
        }
    }

    public static void RewardPlayersInArea(Vector3 location, GameObject killer, KillReward reward)
    {
        if (reward == null)
            DebugGUI.Print("REWARD IS NULL.");
        int teamNumber;
        if (killer.GetCharacterStats().teamNumber == 0)
            return;


        else teamNumber = killer.GetCharacterStats().TeamNumber;

        List<GameObject> playersToReward = new List<GameObject>();
        List<GameObject> teamPlayers;
        if (teamNumber == 1) teamPlayers = MasterGameLogic.Main.PlayerManager.goodPlayers;
        else teamPlayers = MasterGameLogic.Main.PlayerManager.evilPlayers;

        foreach (GameObject teamMate in teamPlayers)
        {
            if (Vector3.Distance(teamMate.transform.position, location) <= Util.MaxExperienceRange)
            {
                playersToReward.Add(teamMate);
            }
        }
        if (playersToReward.Count != 0)
        {
            reward /= playersToReward.Count;
            foreach (GameObject player in playersToReward)
            {
                (player.GetCharacterStats() as PlayerStats).RewardKill(reward);
            }
        }
    }

	[RPC]
	public virtual void ChangeName(String newName)
	{
		Debug.Log ("new name has been received for "+gameObject.name+". The new name is "+newName);
		gameObject.name = newName;
	}
	
	[RPC] 
	void HealthChanged(int newHealth)
	{
		Health = newHealth;
	}

    [RPC] private void ManaChanged(int newMana)
    {
        Mana = newMana;
    }

    [RPC] private void StaminaChanged(int newStamina)
    {
        Stamina = newStamina;
    }
		
    [RPC]
    void ChangeTeam(int newTeam)
    {
        if (Network.isServer && this is PlayerStats)
        {
            MasterGameLogic.Main.PlayerManager.ChangePlayerTeam(gameObject, TeamNumber, newTeam);
        }
        if (gameObject == Util.MyLocalPlayerObject)
        {
            Util.MyLocalPlayerTeam = newTeam;
        }
        teamNumber = newTeam;
        DebugGUI.Print(gameObject.name + " is now on team "+newTeam);
        ParticleSystem particles;


        Transform minimapLabelTransform = transform.FindChild("MinimapLabel");
        if (minimapLabelTransform == null)
        {
            var minimapLabel = Instantiate(Resources.Load("Particles/MinimapLabel") as GameObject) as GameObject;
            minimapLabel.name = "MinimapLabel";
            minimapLabel.transform.SetParentAndCenter(this.transform.root);
            var t = minimapLabel.transform.position;
            t.y += 400;
            minimapLabel.transform.position = t;
            particles = minimapLabel.GetComponent<ParticleSystem>();
        }
        else
        {
            particles = minimapLabelTransform.GetComponent<ParticleSystem>();
        }
        particles.startColor = teamNumber == 1 ? Color.blue : Color.red;
    }

    internal void CureHealth(int healthToAdd)
    {
        if (Network.isServer)
        {
            DebugGUI.Print("Healing here.");
            Health = Math.Min(Health + healthToAdd, MaxHealth);
        }
    }


}

public class KillReward 
{
	public int experience;
	public int iron;
	
	public KillReward(int setExperience, int setIron)
	{
		experience = setExperience;
		iron = setIron;
	}

    public static KillReward operator / (KillReward reward, int divisor)
    {
        return new KillReward(reward.experience / divisor, reward.iron / divisor);
    }

    public static KillReward operator * (KillReward reward, int factor)
    {
        return new KillReward(reward.experience * factor, reward.iron * factor);
    }
}
