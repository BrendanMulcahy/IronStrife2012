using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

class ControlPoint : MonoBehaviour
{
    private int controlledBy;
    public Team allegiance;
    private int[] teams;
    public float influence;
    private bool locked;
    public float MAXINFLUENCE = 30;
    private float radius;
    private LinkedList<GameObject> players = new LinkedList<GameObject>();
    public enum Team { Neutral, Good, Evil };
    private ParticleSystem _particleSys;
    private ParticleSystem ParticleSys
    {
        get
        {
            if (GetComponentInChildren<ParticleSystem>() != null)
            {
                return GetComponentInChildren<ParticleSystem>();
            }
            return null;
        }

    }

    private float lastInfluence;

    private void Awake()
    {
        //make sure the collider is turned on at the start of the game
        InvokeRepeating("AwardPoints", 0f, 1.0f);
    }

    void Start()
    {
        //particleSys = GetComponentInChildren<ParticleSystem>();
    }

    public void ServerInitialize()
    {
        GetComponent<SphereCollider>().enabled = true;
        StartCoroutine(MonitorInfluenceChanges());

    }

    private void Update()
    {
        controlledBy = FindControllingTeam();
        influence = UpdateInfluence(controlledBy, influence);
        UpdateAllegiance();
        UpdateColor(influence);
    }

    private void AwardPoints()
    {
        if (allegiance == Team.Good)
        {
            GameState.goodScore += 1;
        }
        else if (allegiance == Team.Evil)
        {
            GameState.evilScore += 1;
        }
    }

    private IEnumerator MonitorInfluenceChanges()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            if (lastInfluence != influence)
            {
                networkView.RPC("NetworkSyncInfluence", RPCMode.OthersBuffered, influence);
                lastInfluence = influence;
            }
        }
    }

    [RPC]
    void NetworkSyncInfluence(float newInfluence)
    {
        influence = newInfluence;
        UpdateColor(influence);
        UpdateAllegiance();
    }
    //checks if a lockable object enter the control point
    //might be useful to check players and NPCs so npcs need to be killed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            players.AddLast(other.gameObject);
            Debug.Log(other.gameObject.name + " has entered " + this.gameObject.name);
        }
    }

    //checks if a player leaves the control point
    private void OnTriggerExit(Collider other)
    {
        if (players.Contains(other.gameObject))
        {
            players.Remove(other.gameObject);
            Debug.Log(other.gameObject.name + " has left " + this.gameObject.name);
        }
    }

    /* Returns which team has the most players in this control point
     * Returns Neutral if any team has an equal number of players
     */
    private int FindControllingTeam()
    {
        this.teams = new int[3];
        int controllingTeam = (int)Team.Neutral;

        //count all the players for each team
        foreach (GameObject player in players)
        {
            teams[player.GetComponent<CharacterStats>().TeamNumber]++;
        }

        //check if good or evil has more players
        if (teams[(int)Team.Good] > teams[(int)Team.Evil] && teams[(int)Team.Good] > teams[(int)Team.Neutral])
        {
            controllingTeam = (int)Team.Good;
        }
        else if (teams[(int)Team.Evil] > teams[(int)Team.Good] && teams[(int)Team.Evil] > teams[(int)Team.Neutral])
        {
            controllingTeam = (int)Team.Evil;
        }

        return controllingTeam;
    }

    //Updates the value of influence based on the current controlledBy
    //Influence changes faster depending on the number of players
    private float UpdateInfluence(int controlledBy, float influence)
    {
        switch (controlledBy)
        {
            case (int)Team.Neutral:
                if (!locked)
                {
                    if (influence > 0)
                    {
                        influence -= Time.deltaTime;
                        influence = Mathf.Max(influence, 0);
                    }
                    else if (influence < 0)
                    {
                        influence += Time.deltaTime;
                        influence = Mathf.Min(influence, 0);
                    }
                }
                break;
            case (int)Team.Good:
                influence += this.teams[(int)Team.Good] * Time.deltaTime;
                break;
            case (int)Team.Evil:
                influence -= this.teams[(int)Team.Evil] * Time.deltaTime;
                break;
            default:
                Debug.Log("Cannot update influence.  Improper controlledBy.");
                break;
        }

        if ((influence < MAXINFLUENCE && influence > -MAXINFLUENCE) && locked)
        {
            locked = false;
            PopupMessage.NetworkDisplay("Team " + (int)allegiance + " has lost control of " + gameObject.name);
        }

        if (influence >= MAXINFLUENCE)
        {
            influence = MAXINFLUENCE;
            if (!locked)
            {
                locked = true;
                PopupMessage.NetworkDisplay("Team 1 has captured " + gameObject.name);
            }
        }
        else if (influence <= -MAXINFLUENCE)
        {
            influence = -MAXINFLUENCE;
            if (!locked)
            {
                locked = true;
                PopupMessage.NetworkDisplay("Team 2 has captured " + gameObject.name);
            }
        }

        return influence;
    }

    private void UpdateColor(float influence)
    {
        ParticleSystem p = null;
        if (ParticleSys != null) p = ParticleSys;
        if (p != null)
        {
            if (influence > 0)
            {
                p.startColor = new Color((MAXINFLUENCE - influence) / MAXINFLUENCE, (MAXINFLUENCE - influence) / MAXINFLUENCE, 1, 0.5f);

            }
            else if (influence < 0)
            {
                p.startColor = new Color(1, (MAXINFLUENCE + influence) / MAXINFLUENCE, (MAXINFLUENCE + influence) / MAXINFLUENCE, 0.5f);
            }
            else
            {
                p.startColor = new Color(1, 1, 1, 0.5f);
            }
        }
    }

    private void UpdateAllegiance()
    {
        if (influence > 0)
        {
            if (allegiance != Team.Good)
            {
                if (allegiance == Team.Evil)
                    GameState.numEvilControlPoints--;
                GameState.numGoodControlPoints++;
                allegiance = Team.Good;

            }
        }
        else if (influence < 0)
        {
            if (allegiance != Team.Evil)
            {
                if (allegiance == Team.Good)
                    GameState.numGoodControlPoints--;
                GameState.numEvilControlPoints++;
                allegiance = Team.Evil;
            }
        }
        else
        {
            if (allegiance == Team.Good)
                GameState.numGoodControlPoints--;
            else if (allegiance == Team.Evil)
                GameState.numEvilControlPoints--;

            allegiance = Team.Neutral;
        }
    }

    [RPC]
    private void ChangeName(string setName)
    {
        gameObject.name = setName;
    }
}
