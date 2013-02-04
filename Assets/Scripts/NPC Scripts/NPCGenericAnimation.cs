using UnityEngine;

public class NPCGenericAnimation : MonoBehaviour
{
    public NPCAnimationState state;
    private Animation ani;
    private NPC_Controller controller;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        gameObject.GetCharacterStats().Damaged += NPCGenericAnimation_Damaged;
        gameObject.GetCharacterStats().Died += NPCGenericAnimation_Died;

        controller = GetComponent<NPC_Controller>();
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
                ani.CrossFade("run", .2f);
                break;

            case NPCAnimationState.Dying:
                ani.CrossFade("die", .2f);
                break;

            case NPCAnimationState.Attacking:
                ani.CrossFade("attack", .2f);
                break;

            case NPCAnimationState.Flinching:
                ani.CrossFade("gethit", .01f);
                break;

            case NPCAnimationState.Idle:
            default:
                ani.CrossFade("idle", .2f);
                break;

        }
    }

    public void StartAttacking()
    {
        ani.CrossFade("attack", .1f);
        state = NPCAnimationState.Attacking;
    }

    public void StopAttacking()
    {
        state = NPCAnimationState.Idle;
    }

    private void StartFlinchingAnimation()
    {
        ani.CrossFade("gethit", .01f);
        state = NPCAnimationState.Flinching;
        Invoke("SetIdle", .5f);
    }

    private void StartDyingAnimation()
    {
        ani.CrossFade("die", .01f);
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