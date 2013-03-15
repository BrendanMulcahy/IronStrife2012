﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[PlayerComponent(PlayerScriptType.AllDisabled, PlayerScriptType.ServerOwnerEnabled, PlayerScriptType.ClientOwnerEnabled)]
public class AbilityManager : StrifeScriptBase
{
    public int[] equippedSpells = { -1, -1, -1, -1, -1 };
    public KeyCode[] spellButtons = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };
    private bool visible;
    private bool lookingForSpellKey;
    private Spell lookingToBindSpell;
    private List<Spell> knownSpells = new List<Spell>();

    private Rect abilityWindowRect = new Rect(70, 70, 600, 400);
    private Dictionary<Spell, float> cooldownTimes = new Dictionary<Spell, float>();

    void Awake()
    {
        StartCoroutine(UpdateCooldowns());  // Cooldowns need to be updated on everyone, not just the owner
    }

    void Start()
    {
        AddStartingSpells();
    }

    private void AddStartingSpells()
    {
        if (knownSpells.Count == 0)
        {
            knownSpells.Add(PlayerAbilities.GetSpell("Surge"));
            knownSpells.Add(PlayerAbilities.GetSpell("Frost Blast"));
            knownSpells.Add(PlayerAbilities.GetSpell("Flameburst"));
            knownSpells.Add(PlayerAbilities.GetSpell("Magic Platform"));
            knownSpells.Add(PlayerAbilities.GetSpell("Magic Hook"));
        }
    }

    void OnSetOwnership()
    {
        EquipStartingSpells();
    }

    private void EquipStartingSpells()
    {
        AddStartingSpells();

        equippedSpells[4] = (int)knownSpells[0];
        equippedSpells[1] = (int)knownSpells[1];
        equippedSpells[3] = (int)knownSpells[2];
        equippedSpells[2] = (int)knownSpells[3];
        equippedSpells[0] = (int)knownSpells[4];

        for (int g = 0; g < 5; g++)
        {
            GetComponent<PlayerGUI>().UpdateSpellIcons(g, (Spell)equippedSpells[g]);
        }
    }

    [RPC]
    void TryLearnSpell(int spellID)
    {
        networkView.RPC("CommitLearnSpell", RPCMode.All, spellID);
    }

    [RPC]
    public void CommitLearnSpell(int toLearn)
    {
        var spell = PlayerAbilities.GetSpell(toLearn);
        knownSpells.Add(spell);

        if (this.gameObject.IsMyLocalPlayer())
        {
            PopupMessage.LocalDisplay("You learned " + spell.Name + "!", .1f);
        }
    }


    void OnGUI()
    {
        GUI.skin = Util.ISEGUISkin;
        if (visible)
        {
            abilityWindowRect = GUI.Window("abilities".GetHashCode(), abilityWindowRect, ShowAbilityWindow, "Spells & Abilities", GUI.skin.GetStyle("smallWindow"));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            visible = true;
            gameObject.DisableControls();
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            visible = false;
            gameObject.EnableControls();
        }

        if (lookingForSpellKey)
        {
            LookForSpellKey();
        }
    }

    private IEnumerator UpdateCooldowns()
    {
        while (true)
        {
            var spells = cooldownTimes.Keys.ToArray();
            for (int g = 0; g < spells.Length; g++)
            {
                cooldownTimes[spells[g]] -= Time.deltaTime;
                if (cooldownTimes[spells[g]] <= 0)
                    cooldownTimes.Remove(spells[g]);
            }
            yield return null;
        }
    }

    public void PutOnCooldown(Spell spell, float overrideTime = -1)
    {
        if (overrideTime == -1)
        {
            cooldownTimes[spell] = spell.cooldown;
        }
    }

    public bool IsOnCooldown(Spell spell)
    {
        return cooldownTimes.ContainsKey(spell);
    }

    public float GetRemainingCooldownTime(Spell spell)
    {
        return cooldownTimes[spell];
    }

    private void ShowAbilityWindow(int id)
    {
        GUILayout.BeginVertical();
        foreach (Spell spell in knownSpells)
        {
            if (spell == null)
                continue;
            if (GUILayout.Button(spell.Name, "smallButton") && !lookingForSpellKey)
            {
                lookingForSpellKey = true;
                lookingToBindSpell = spell;
            }
        }
        GUILayout.EndVertical();
        GUI.DragWindow();
    }

    private void LookForSpellKey()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(spellButtons[i]))
            {
                Debug.Log("Bound " + lookingToBindSpell.Name + " to key " + (i + 1) + ".");
                equippedSpells[i] = (int)lookingToBindSpell;
                GetComponent<PlayerGUI>().UpdateSpellIcons(i, lookingToBindSpell); //draw the new spell 
                lookingToBindSpell = null;
                lookingForSpellKey = false;
                break;
            }
        }
    }


}
