using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AbilityManager : MonoBehaviour
{
    public int[] equippedSpells = { -1, -1, -1, -1, -1 };
    public KeyCode[] spellButtons = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };
    private bool visible;
    private bool lookingForSpellKey;
    private Spell lookingToBindSpell;

    private Rect abilityWindowRect = new Rect(70, 70, 600, 400);

    void Start()
    {
        EquipStartingSpells();
    }

    private void EquipStartingSpells()
    {
        equippedSpells[4] = (int)PlayerAbilities.GetSpell("Surge");
        equippedSpells[1] = (int)PlayerAbilities.GetSpell("Burning Blade");
        equippedSpells[3] = (int)PlayerAbilities.GetSpell("Flameburst");
        equippedSpells[2] = (int)PlayerAbilities.GetSpell("Fireball");
        equippedSpells[0] = (int)PlayerAbilities.GetSpell("Magic Hook");
        for (int g = 0; g < 5; g++)
        {
            GetComponent<PlayerGUI>().UpdateSpellIcons(g, (Spell)equippedSpells[g]);

        }



    }


    void OnGUI()
    {
        if (visible)
        {
            GUI.Window("abilities".GetHashCode(), abilityWindowRect, ShowAbilityWindow, "Spells & Abilities");
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Debug.Log("E pressed");
        //    visible = true;
        //    gameObject.DisableControls();
        //}
        //else if (Input.GetKeyUp(KeyCode.E))
        //{
        //    visible = false;
        //    gameObject.EnableControls();
        //}

        if (lookingForSpellKey)
        {
            LookForSpellKey();
        }
    }

    private void ShowAbilityWindow(int id)
    {
        GUILayout.BeginVertical();
        foreach (Spell spell in PlayerAbilities.AllSpells())
        {
            if (spell == null)
                continue;
            if (GUILayout.Button(spell.name) && !lookingForSpellKey)
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
                Debug.Log("Bound " + lookingToBindSpell.name + " to key " + (i + 1) + ".");
                equippedSpells[i] = (int)lookingToBindSpell;
                GetComponent<PlayerGUI>().UpdateSpellIcons(i, lookingToBindSpell); //draw the new spell 
                lookingToBindSpell = null;
                lookingForSpellKey = false;
                break;
            }
        }
    }


}
