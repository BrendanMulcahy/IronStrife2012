using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

    public PhysicalDefense PhysicalDefense { get; set; }
    public MagicalDefense MagicalDefense { get; set; }

    public MoveSpeedStat MoveSpeed { get; set; }

    protected int teamNumber = 0;
    public int TeamNumber { get { return teamNumber; } set { networkView.RPC("ChangeTeam", RPCMode.All, value); } }

    public KillReward reward = KillReward.None;

    public event UnitDiedEventHandler Died;
    public event DamageEventHandler Damaged;
    public event HealedEventHandler Healed;

    public virtual int PhysicalDamageModifier
    {
        get
        {
            return 0;
        }
    }

    public virtual int RangedDamageModifier
    {
        get
        {
            return 0;
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

        PhysicalDefense = new PhysicalDefense(0);
        MagicalDefense = new MagicalDefense(0);

        MoveSpeed = new MoveSpeedStat(8.0f);

    }

    protected virtual void Start() 
    {
        Died += Died_RewardPlayers;

    }

    private void Died_RewardPlayers(GameObject deadUnit, UnitDiedEventArgs e)
    {
        RewardPlayersInArea(e.deathPosition, e.killer, e.reward);
    }

    /// <summary>
    /// Causes damage to be received by this character. Is reduced by defenses and resistances.
    /// </summary>
    public void ApplyDamage(GameObject attacker, Damage damage)
    {

        var e = new DamageEventArgs() { damage = damage, attacker = attacker, damageLocation = damage.location };
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

            if (Health.CurrentValue <= 0 && Network.isServer)
            {
                OnDeath(new UnitDiedEventArgs() { killer = attacker, deathPosition = transform.position, reward = this.reward });
            }
        }
    }

    public void ApplyHealing(GameObject healer, int amount)
    {
        var e = new HealedEventArgs() { healAmount = amount, healer = healer };
        if (Healed != null)
        {
            Healed(this.gameObject, e);
        }

        if (!e.handled)
        {
            Health.CurrentValue = Mathf.Min(Health.MaxValue, Health.CurrentValue + e.healAmount);
        }
    }

    private void OnDeath(UnitDiedEventArgs unitDiedEventArgs)
    {
        if (Network.isServer)
        {
            networkView.RPC("NotifyDeath", RPCMode.All, unitDiedEventArgs.deathPosition, unitDiedEventArgs.killer.GetNetworkViewID(), unitDiedEventArgs.reward.experience, unitDiedEventArgs.reward.gold);
        }
    }

    [RPC]
    protected virtual void NotifyDeath(Vector3 deathPosition, NetworkViewID killer, int experience, int gold)
    {
        var e = new UnitDiedEventArgs() { reward = new KillReward(experience, gold), killer = killer.GetGameObject(), deathPosition = deathPosition };
        if (Died != null)
        {
            Died(gameObject, e);
        }
    }

    public static void RewardPlayersInArea(Vector3 location, GameObject killer, KillReward reward)
    {
        if (Network.isClient) return;

        int teamNumber;
        if (killer.GetCharacterStats().teamNumber == 0)
            return;


        else teamNumber = killer.GetCharacterStats().TeamNumber;

        List<GameObject> playersToReward = new List<GameObject>();
        var teamPlayers = PlayerManager.Main.GetPlayersOnTeam(teamNumber).Select(x => x.gameObject);

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
        particles.startColor = TeamNumber == 1 ? Color.blue : Color.red;
    }

    protected virtual void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        stream.SerializeRegeneratingStat(Health);
        stream.SerializeRegeneratingStat(Mana);
        stream.SerializeRegeneratingStat(Stamina);

        stream.SerializeBuffableStat(PhysicalDefense);
        stream.SerializeBuffableStat(MagicalDefense);

        stream.SerializeMoveSpeed(MoveSpeed);

    }

    public override string ToString()
    {
        var toReturn = "";
        toReturn += Health.ToString() + "\n";
        toReturn += Mana.ToString() + "\n";
        toReturn += Stamina.ToString() + "\n";

        toReturn += PhysicalDefense.ToString() + "\n";
        toReturn += MagicalDefense.ToString() + "\n";
        toReturn += MoveSpeed.ToString() + "\n";

        toReturn += "Team: " + TeamNumber + "\n";

        return toReturn;
    }
}

public struct KillReward
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

    public static KillReward None { get { return new KillReward(0, 0); } }

    public override string ToString()
    {
        return String.Format("Reward [ {0} XP, {1} Gold ]", experience, gold);
    }
}
