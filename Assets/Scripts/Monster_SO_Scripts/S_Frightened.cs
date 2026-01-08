using UnityEngine;

[CreateAssetMenu(fileName = "S_Frightened", menuName = "CookieMan/FSM/States/Frightened")]
public class S_Frightened: State
{
    public override void Enter(GameObject owner, StateContext context)
    {   
        owner.GetComponent<MonsterAnimator>().EnterFrightened();
        owner.GetComponent<Monster_Controller>().GetNextIntermediateTarget = AI_Navigation.GetNextRandomTarget;

        owner.GetComponentInChildren<Collision_Monster>().enabled = true;
    }

    public override void Tick(GameObject owner, StateContext context)
    {
        if (context.level.FrightenedTimer < 3.0f && !context.frightenedTimeoutSet)
        {
            owner.GetComponent<MonsterAnimator>().EnterFrightenedTimeout();
            context.frightenedTimeoutSet = true;
        }
    }

    public override void Exit(GameObject owner, StateContext context)
    {
        context.frightenedTimeoutSet = false;
        owner.GetComponent<MonsterAnimator>().ExitFrightened();
        
        owner.GetComponentInChildren<Collision_Monster>().enabled = false;
    }
}