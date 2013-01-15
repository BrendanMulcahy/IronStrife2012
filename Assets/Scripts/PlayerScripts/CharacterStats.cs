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
public class CharacterStats : MonoBehaviour
{

    #region Deprecated Stats Code
    //public int Health { get; set; }
    //public int MaxHealth { get; set; }
    //public int Mana { get; set; }
    //public int MaxMana { get; set; }
    //public void ReduceMana(int amount) { Mana -= amount; manaRegenerating = false; timeTilManaRegenerating = maxManaRegenerationTime; }

    //private float _stamina;
    //public int Stamina { get { return (int)_stamina; } set { _stamina = value; } }
    //public int MaxStamina { get; set; }
    //public void ReduceStamina(float amount) { _stamina -= amount; staminaRegenerating = false; timeTilStaminaRegenerating = maxStaminaRegenerationTime; }

    ////public bool healthRegenerating;
    ////public float timeTilHealthRegenerating;
    ////private float maxHealthRegenerationTime = 5.0f;
    ////public int healthRegenerationRate = 1;

    ////private bool manaRegenerating;
    ////private int manaRegenerationRate = 1;
    ////private float maxManaRegenerationTime = 5.0f;
    ////private float timeTilManaRegenerating;

    ////private bool staminaRegenerating;
    ////private int staminaRegenerationRate = 3;
    ////private float maxStaminaRegenerationTime = 1.5f;
    ////private float timeTilStaminaRegenerating;

    //public int Strength;
    ///// <summary>
    ///// Attack strength, including all modifiers, such as weapon damage, buffs, etc.
    ///// </summary>
    //public int EffectiveStrength
    //{
    //    get
    //    {
    //        var effectiveStr = Strength;
    //        if (inventory != null)
    //            effectiveStr += inventory.currentWeapon.damage;
    //        return effectiveStr;
    //    }
    //}
    #endregion

    private Inventory inventory;

    public Health Health { get; set; }
    public Mana Mana { get; set; }
    public Stamina Stamina { get; set; }

    public StrengthStat Strength { get; set; }
    public AgilityStat Agility { get; set; }
    public IntelligenceStat Intelligence { get; set; }

    public PhysicalDefense PhysicalDefense { get; set; }

    public MoveSpeedStat MoveSpeed { get; set; }

    public int teamNumber = 0;
    public int TeamNumber { get { return teamNumber; } set { networkView.RPC("ChangeTeam", RPCMode.AllBuffered, value); } }

    public KillReward reward;

    public event UnitDiedEventHandler Died;
    public event DamageEventHandler Damaged;

    protected virtual void Awake()
    {
        if ((Health = GetComponent<Health>()) == null)
            Health = gameObject.AddComponent<Health>();
        Health.SetInitialValues(50, 50);

        if ((Mana = GetComponent<Mana>()) == null)
            Mana = gameObject.AddComponent<Mana>();
        Mana.SetInitialValues(50, 50);

        if ((Stamina = GetComponent<Stamina>()) == null)
            Stamina = gameObject.AddComponent<Stamina>();
        Stamina.SetInitialValues(50, 50);

        Strength = new StrengthStat(0);
        Strength.Changed += Health.Strength_Changed;
        Strength.BaseChanged += Health.Strength_Changed;
        Strength.ChangeBaseValue(5);

        MoveSpeed = new MoveSpeedStat(10.0f);
        Agility = new AgilityStat(0);
        Agility.Changed += MoveSpeed.Agility_Changed;
        Agility.BaseChanged += MoveSpeed.Agility_Changed;
        Agility.ChangeBaseValue(5);

        Intelligence = new IntelligenceStat(0);
        Intelligence.Changed += Mana.Intelligence_Changed;
        Intelligence.BaseChanged += Mana.Intelligence_Changed;
        Intelligence.ChangeBaseValue(5);
    }

    // Use this for initialization
    public virtual void Start()
    {
        inventory = gameObject.GetInventory();
    }

    public void ApplyDamage(GameObject attacker, Damage damage)
    {
        if (Network.isServer)
        {
            Health.CurrentValue = Mathf.Max(0, Health.CurrentValue - damage.amount);

            if (Health.CurrentValue <= 0)
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
            Debug.Log("REWARD IS NULL.");
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
        Debug.Log("new name has been received for " + gameObject.name + ". The new name is " + newName);
        gameObject.name = newName;
    }

    [RPC]
    void HealthChanged(int newHealth)
    {
        Health.CurrentValue = newHealth;
    }

    [RPC]
    private void ManaChanged(int newMana)
    {
        Mana.CurrentValue = newMana;
    }

    [RPC]
    private void StaminaChanged(int newStamina)
    {
        Stamina.CurrentValue = newStamina;
    }

    [RPC]
    void ChangeTeam(int newTeam)
    {
        if (Network.isServer && this is PlayerStats)
        {
            if (MasterGameLogic.Main.PlayerManager == null) Debug.Log("NULL!");
            MasterGameLogic.Main.PlayerManager.ChangePlayerTeam(gameObject, TeamNumber, newTeam);
        }
        if (gameObject == Util.MyLocalPlayerObject)
        {
            Util.MyLocalPlayerTeam = newTeam;
        }
        teamNumber = newTeam;
        Debug.Log(gameObject.name + " is now on team " + newTeam);
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
            Debug.Log("Healing here.");
            Health.CurrentValue = Math.Min(Health.CurrentValue + healthToAdd, Health.MaxValue);
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

    public static KillReward operator /(KillReward reward, int divisor)
    {
        return new KillReward(reward.experience / divisor, reward.iron / divisor);
    }

    public static KillReward operator *(KillReward reward, int factor)
    {
        return new KillReward(reward.experience * factor, reward.iron * factor);
    }
}
