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
    public Health Health { get; set; }
    public Mana Mana { get; set; }
    public Stamina Stamina { get; set; }

    public StrengthStat Strength { get; set; }
    public AgilityStat Agility { get; set; }
    public IntelligenceStat Intelligence { get; set; }

    public PhysicalDefense PhysicalDefense { get; set; }
    public MagicalDefense MagicalDefense { get; set; }

    public MoveSpeedStat MoveSpeed { get; set; }

    public Inventory inventory;
    public int teamNumber = 0;
    public int TeamNumber { get { return teamNumber; } set { networkView.RPC("ChangeTeam", RPCMode.All, value); } }

    public KillReward reward;

    public event UnitDiedEventHandler Died;
    public event DamageEventHandler Damaged;

    public virtual int PhysicalDamageModifier
    {
        get
        {
            return Strength.ModifiedValue * StrengthStat.meleeDamagePerStrength + ((inventory) ? inventory.currentWeapon.damage : 0);
        }
    }

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
        Strength.ModifiedValueChanged += Health.Strength_Changed;
        Strength.BaseValueChanged += Health.Strength_Changed;
        Strength.ChangeBaseValue(5);

        MoveSpeed = new MoveSpeedStat(10.0f);
        Agility = new AgilityStat(0);
        Agility.ModifiedValueChanged += Stamina.Agility_Changed;
        Agility.BaseValueChanged += Stamina.Agility_Changed;
        Agility.ModifiedValueChanged += MoveSpeed.Agility_Changed;
        Agility.BaseValueChanged += MoveSpeed.Agility_Changed;
        Agility.ChangeBaseValue(5);

        Intelligence = new IntelligenceStat(0);
        Intelligence.ModifiedValueChanged += Mana.Intelligence_Changed;
        Intelligence.BaseValueChanged += Mana.Intelligence_Changed;
        Intelligence.ChangeBaseValue(5);

        PhysicalDefense = new PhysicalDefense(0);
        MagicalDefense = new MagicalDefense(0);
    }

    protected virtual void Start() { inventory = gameObject.GetInventory(); }

    /// <summary>
    /// Causes damage to be received by this character. Is reduced by defenses and resistances.
    /// </summary>
    public void ApplyDamage(GameObject attacker, Damage damage)
    {

        var e = new DamageEventArgs() { damage = damage };
        if (Damaged != null)
        {
            Damaged(this.gameObject, e);
        }

        if (!e.handled)
        {
            switch (damage.damageType)
            {
                case DamageType.Pure:
                    break;
                case DamageType.Physical:
                    damage.amount = (int)(damage.amount - damage.amount * PhysicalDefense.PercentageReduced);
                    break;
                case DamageType.Magical:
                    damage.amount = (int)(damage.amount - damage.amount * MagicalDefense.PercentageReduced);
                    break;
                case DamageType.Composite:
                    damage.amount = (int)(damage.amount - damage.amount * PhysicalDefense.PercentageReduced);
                    damage.amount = (int)(damage.amount - damage.amount * MagicalDefense.PercentageReduced);
                    break;

                default:
                    Debug.LogError("Invalid damage type from attacker " + attacker.name);
                    break;
            }

            Health.CurrentValue = Mathf.Max(0, Health.CurrentValue - damage.amount);
            Debug.Log(this.gameObject.name + " took " + damage.amount + " damage after modifications. He now has " + Health.CurrentValue);


            if (Health.CurrentValue <= 0)
            {
                OnDeath(new UnitDiedEventArgs() { killer = attacker, deathPosition = transform.position, reward = this.reward });
            }
        }
    }

    protected void OnDeath(UnitDiedEventArgs unitDiedEventArgs)
    {
        Debug.Log("Calling OnDeath : " + name);
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
    public int gold;

    public KillReward(int setExperience, int setGold)
    {
        experience = setExperience;
        gold = setGold;
    }

    public static KillReward operator /(KillReward reward, int divisor)
    {
        return new KillReward(reward.experience / divisor, reward.gold / divisor);
    }

    public static KillReward operator *(KillReward reward, int factor)
    {
        return new KillReward(reward.experience * factor, reward.gold * factor);
    }
}
