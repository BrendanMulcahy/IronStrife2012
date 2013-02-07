using UnityEngine;
using System;
using System.Collections;

[PlayerComponent(PlayerScriptType.AllDisabled, PlayerScriptType.ClientOwnerEnabled, PlayerScriptType.ServerOwnerEnabled)]
public class PlayerStats : CharacterStats
{
    public string username = "default_username";
    public int experience = 0;
    public int Level { get; set; }
    public int experienceNeeded;
    public int unusedStatPoints = 0;

    public StrengthStat Strength { get; set; }
    public AgilityStat Agility { get; set; }
    public IntelligenceStat Intelligence { get; set; }

    static int[] experiencePerLevel = { 1000, 2000, 3000, 5000, 8000, 13000 };

    public event PlayerRespawnedEventHandler Respawned;
    public bool canRespawn;

    private bool visible = false;
    private Inventory inventory;
    Rect windowRect;
    private Vector2 scrollPos = new Vector2();


    public override int PhysicalDamageModifier
    {
        get
        {
            if (inventory.currentWeapon == null) Debug.Log("weapon is null");
            if (Strength == null) Debug.Log("strength is null");
            return Strength.ModifiedValue * StrengthStat.meleeDamagePerStrength + inventory.currentWeapon.damage;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        inventory = GetComponent<Inventory>();
        SetInitialStats();

    }

    protected override void Start()
    {
        experienceNeeded = experiencePerLevel[0];
        Level = 1;
        Died += PlayerDied;
        UpdateKillReward();
        TeamNumber = 1;
        inventory = gameObject.GetInventory();

        windowRect = new Rect(Screen.width * .2f, Screen.height * .2f, Screen.width * .35f, Screen.height * .6f);
    }

    private void SetInitialStats()
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
        Strength.ChangeBaseValue(10);

        MoveSpeed = new MoveSpeedStat(10.0f);
        Agility = new AgilityStat(0);
        Agility.ModifiedValueChanged += Stamina.Agility_Changed;
        Agility.BaseValueChanged += Stamina.Agility_Changed;
        Agility.ModifiedValueChanged += MoveSpeed.Agility_Changed;
        Agility.BaseValueChanged += MoveSpeed.Agility_Changed;
        Agility.ChangeBaseValue(10);

        Intelligence = new IntelligenceStat(0);
        Intelligence.ModifiedValueChanged += Mana.Intelligence_Changed;
        Intelligence.BaseValueChanged += Mana.Intelligence_Changed;
        Intelligence.ChangeBaseValue(10);

        PhysicalDefense = new PhysicalDefense(0);
        MagicalDefense = new MagicalDefense(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            visible = !visible;
    }

    void OnGUI()
    {
        if (visible)
        {
            windowRect = GUI.Window("stats".GetHashCode(), windowRect, StatsWindow, GUIContent.none);
        }
    }

    private void StatsWindow(int id)
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUILayout.BeginVertical();
        GUILayout.Label("Level " + Level);
        GUILayout.Label("Experience: " + experience + " / " + experienceNeeded);
        GUILayout.Space(10);

        GUILayout.Label(Health.ToString());
        GUILayout.Label(Mana.ToString());
        GUILayout.Label(Stamina.ToString());
        GUILayout.Space(10);

        GUILayout.Label(MoveSpeed.ToString());
        GUILayout.Space(10);

        GUILayout.Label("Unused Attribute Points: " + unusedStatPoints);
        GUILayout.Space(6);
        if (GUILayout.Button(Strength.ToString())) ButtonPressed("strength");
        if (GUILayout.Button(Agility.ToString())) ButtonPressed("agility");
        if (GUILayout.Button(Intelligence.ToString())) ButtonPressed("intelligence");

        GUILayout.Space(10);

        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUI.DragWindow();
    }

    private void ButtonPressed(string name)
    {
        if (unusedStatPoints <= 0) PopupMessage.LocalDisplay("You don't have any points to spend!", 0.2f);
        switch (name)
        {
            case "strength":
                networkView.RPCToServer(this, "TryUpgradeStat", 0);
                break;
            case "agility":
                networkView.RPCToServer(this, "TryUpgradeStat", 1);
                break;
            case "intelligence":
                networkView.RPCToServer(this, "TryUpgradeStat", 2);
                break;

        }
    }

    [RPC]
    void TryUpgradeStat(int type)
    {
        if (unusedStatPoints <= 0) return;
        networkView.RPC("CommitUpgradeStat", RPCMode.All, type);

    }

    [RPC]
    void CommitUpgradeStat(int type)
    {
        switch (type)
        {
            case 0:
                Strength.ChangeBaseValue(1);
                break;
            case 1:
                Agility.ChangeBaseValue(1);
                break;
            case 2:
                Intelligence.ChangeBaseValue(1);
                break;
        }
        unusedStatPoints--;
    }

    void OnRespawn(Vector3 requestedRespawnLocation)
    {
        if (Respawned != null)
        {
            Respawned(new PlayerRespawnedEventArgs() { respawnPosition = requestedRespawnLocation });
        }
    }

    void PlayerDied(GameObject sender, UnitDiedEventArgs e)
    {
        CharacterStats.RewardPlayersInArea(e.deathPosition, e.killer, e.reward);
        SetRespawnTimer();
        networkView.RPC("BroadcastDeath", RPCMode.All, e.killer.networkView.viewID);

        StartCoroutine(TimedDeathAnimation());

    }

    private IEnumerator TimedDeathAnimation()
    {
        if (Network.isServer)
        {
            if (this.gameObject.IsMyLocalPlayer())
            {
                this.SendMessage("BeginDying");
            }
            else
            {
                networkView.RPC("BeginDying", this.gameObject.GetNetworkPlayer());
            }
        }
        yield return new WaitForSeconds(8.0f);
        transform.position = Vector3.zero;
    }

    private void SetRespawnTimer()
    {
        canRespawn = false;
        var respawnTime = 2 * Level + 4f;
        StartCoroutine(RespawnTimer(respawnTime));
    }

    private IEnumerator RespawnTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canRespawn = true;
    }

    private void EnableRespawnCamera()
    {
        Camera.main.GetComponent<RegularCamera>().enabled = false;
        Camera.main.GetComponent<RespawnCamera>().enabled = true;
        Camera.main.GetComponent<BlurEffect>().enabled = false;

    }

    private void DisableRespawnCamera()
    {
        Camera.main.GetComponent<RegularCamera>().enabled = true;
        Camera.main.GetComponent<RespawnCamera>().enabled = false;
    }

    /// <summary>
    /// Adds rewards for this player achieving a kill.
    /// </summary>
    public void RewardKill(KillReward reward)
    {
        if (Network.isClient) return;

        this.experience += reward.experience;
        this.inventory.Gold += reward.gold;
        if (experience >= experienceNeeded)
        {
            LevelUp();
        }
        if (gameObject != Util.MyLocalPlayerObject)
            networkView.RPC("BroadcastReward", this.gameObject.GetNetworkPlayer(), reward.experience, reward.gold);
    }

    [RPC]
    void BroadcastDeath(NetworkViewID killerID)
    {
        if (gameObject == Util.MyLocalPlayerObject)
        {
            PopupMessage.Display("You were killed by " + NetworkView.Find(killerID).gameObject.name);
            gameObject.DisableControls();
            SetRespawnTimer();
            Camera.main.gameObject.GetComponent<BlurEffect>().enabled = true;
            Invoke("EnableRespawnCamera", 6.0f);
            return;
        }
        else
        {
            transform.FindChild("Name Label").GetComponent<CharacterLabel>().DisableLabels();
            if (Util.MyLocalPlayerObject.GetCharacterStats().TeamNumber != this.teamNumber)
            {
                PopupMessage.LocalDisplay(gameObject.name + " has been killed by " + NetworkView.Find(killerID).gameObject.name + "!", 2.5f, 0, 1, 0);
            }
            else
            {
                PopupMessage.LocalDisplay(gameObject.name + " has been killed by " + NetworkView.Find(killerID).gameObject.name + "!", 2.5f, 1, 0, 0);
            }
        }
    }

    /// <summary>
    /// Received by the client when he kills an enemy and receives a reward.
    /// </summary>
    [RPC]
    void BroadcastReward(int xp, int goldReward)
    {
        Debug.Log("You have obtained "+xp + " experience and " + goldReward + " gold.");
        experience += xp;
        inventory.Gold += goldReward;
    }

    void LevelUp()
    {
        Level++;
        Strength.ChangeBaseValue(1);
        Agility.ChangeBaseValue(1);
        Intelligence.ChangeBaseValue(1);
        unusedStatPoints += 2;

        //	Debug.Log(this.gameObject.name + " has leveled up.");
        experienceNeeded += experiencePerLevel[Level - 1];
        UpdateKillReward();
        if (gameObject == Util.MyLocalPlayerObject)
            PopupMessage.Display("You have reached level " + Level);
        else
            networkView.RPC("ClientLevelUp", this.gameObject.GetNetworkPlayer(), experience);
    }

    private void UpdateKillReward()
    {
        reward = new KillReward(Level * 400, Level * 200);
    }

    [RPC]
    void ClientLevelUp(int newExperience)
    {
        Level++;
        Strength.ChangeBaseValue(5);
        Agility.ChangeBaseValue(5);
        Intelligence.ChangeBaseValue(5);
        unusedStatPoints += 2;

        experience = newExperience;
        experienceNeeded = experiencePerLevel[Level - 1];
        PopupMessage.Display("You have reached level " + Level);
    }

    /// <summary>
    /// Sent from Clients to Server when a respawn is requested at a certain location
    /// This method is run on the server.
    /// </summary>
    /// <param name="requestedRespawnLocation"></param>
    [RPC]
    public void TryRespawn(Vector3 requestedRespawnLocation)
    {
        Debug.Log("Trying to respawn.");
        if (canRespawn)
        {
            Vector3 actualRespawnLocation = Util.FindClosestTeamRespawn(requestedRespawnLocation, teamNumber);
            networkView.RPC("StopDying", RPCMode.All);
            Health.CurrentValue = Health.MaxValue;
            transform.position = actualRespawnLocation;
            OnRespawn(actualRespawnLocation);
            networkView.RPC("BroadcastRespawn", RPCMode.All, actualRespawnLocation);
        }
    }

    [RPC]
    void BroadcastRespawn(Vector3 respawnLocation)
    {
        Debug.Log(gameObject.name + " has respawned.");

        if (Util.MyLocalPlayerObject == gameObject)
        {
            gameObject.EnableControls();
            DisableRespawnCamera();

        }
        else
        {
            if (transform.FindChild("Name Label"))
            {
                transform.FindChild("Name Label").GetComponent<CharacterLabel>().EnableLabels();
            }
        }
    }
    [RPC]
    public override void ChangeName(String newName)
    {
        base.ChangeName(newName);
        username = newName;
    }

    protected override void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        base.OnSerializeNetworkView(stream, info);

        stream.SerializeBuffableStat(Strength);
        stream.SerializeBuffableStat(Agility);
        stream.SerializeBuffableStat(Intelligence);

    }

    public override string ToString()
    {
        var toReturn = "Level: " + Level;
        toReturn+= "\n"+ base.ToString();
        toReturn += Strength.ToString() + "\n";
        toReturn += Agility.ToString() + "\n";
        toReturn += Intelligence.ToString() + "\n";

        return toReturn;

    }
}