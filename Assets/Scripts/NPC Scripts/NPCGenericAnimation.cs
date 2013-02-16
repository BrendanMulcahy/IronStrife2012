using System.Collections.Generic;
using UnityEngine;

public class NPCGenericAnimation : MonoBehaviour
{
    public NPCAnimationState state;
    private Animation ani;
    private NavMeshAgent navMeshAgent;

    private Dictionary<AnimationName, string> _aniNames = new Dictionary<AnimationName, string>();
    public Dictionary<AnimationName, string> aniNames { get { return _aniNames; } }

    public enum AnimationName
    {
        Run,
        Die,
        Attack,
        GetHit,
        Idle,
    }

    void Start()
    {
        gameObject.GetCharacterStats().Damaged += NPCGenericAnimation_Damaged;
        gameObject.GetCharacterStats().Died += NPCGenericAnimation_Died;

        ani = this.GetComponent<Animation>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    void NPCGenericAnimation_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        StartDyingAnimation();
    }

    void NPCGenericAnimation_Damaged(GameObject sender, DamageEventArgs e)
    {
        StartFlinchingAnimation();
    }

    void Update()
    {
        if (state == NPCAnimationState.Idle || state == NPCAnimationState.Moving)
        {
            if (navMeshAgent.velocity.magnitude >= .4f)
            {
                state = NPCAnimationState.Moving;
            }
            else
                state = NPCAnimationState.Idle;
        }
        switch (this.state)
        {

            case NPCAnimationState.Moving:
                ani.CrossFade(aniNames[AnimationName.Run], .2f);
                break;

            case NPCAnimationState.Dying:
                ani.CrossFade(aniNames[AnimationName.Die], .2f);
                break;

            case NPCAnimationState.Attacking:
                ani.CrossFade(aniNames[AnimationName.Attack], .2f);
                break;

            case NPCAnimationState.Flinching:
                ani.CrossFade(aniNames[AnimationName.GetHit], .01f);
                break;

            case NPCAnimationState.Idle:
            default:
                ani.CrossFade(aniNames[AnimationName.Idle], .2f);
                break;

        }
    }

    public void StartAttacking()
    {
        ani.CrossFade(aniNames[AnimationName.Attack], .1f);
        state = NPCAnimationState.Attacking;
    }

    public void StopAttacking()
    {
        state = NPCAnimationState.Idle;
    }

    private void StartFlinchingAnimation()
    {
        ani.CrossFade(aniNames[AnimationName.GetHit], .01f);
        state = NPCAnimationState.Flinching;
        Invoke("SetIdle", .5f);
    }

    private void StartDyingAnimation()
    {
        ani.CrossFade(aniNames[AnimationName.Die], .2f);
        state = NPCAnimationState.Dying;
    }

    void SetIdle()
    {
        if (state != NPCAnimationState.Dying)
            state = NPCAnimationState.Idle;
    }

}

public enum NPCAnimationState
{
    Idle,
    Moving,
    Attacking,
    Flinching,
    Dying,

}