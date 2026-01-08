using UnityEngine;

[CreateAssetMenu(fileName = "S_Chase", menuName = "CookieMan/FSM/States/Chase")]
public class S_Chase : State
{
    public override void Enter(GameObject owner, StateContext context)
    {
        owner.GetComponent<MonsterAnimator>().SetDefault(true);
        owner.GetComponent<Monster_Controller>().GetNextIntermediateTarget = AI_Navigation.GetNextDefaultTarget;
    }

    public override void Tick(GameObject owner, StateContext context)
    {
        owner.GetComponent<Monster_Controller>().SetChaseTarget();
    }

    public override void Exit(GameObject owner, StateContext context)
    {
        owner.GetComponent<MonsterAnimator>().SetDefault(false);
    }
}
