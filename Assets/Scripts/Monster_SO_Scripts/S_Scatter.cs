using UnityEngine;

[CreateAssetMenu(fileName = "S_Scatter", menuName = "CookieMan/FSM/States/Scatter")]
public class S_Scatter : State
{
    public override void Enter(GameObject owner, StateContext context)
    {   
        owner.GetComponent<MonsterAnimator>().SetDefault(true);
        
        Monster_Controller monster = owner.GetComponent<Monster_Controller>();
        monster.FinalTarget = monster.Configuration.scatterPos;
        monster.GetNextIntermediateTarget = AI_Navigation.GetNextDefaultTarget;
    }

    public override void Tick(GameObject owner, StateContext context)
    {
        
    }

    public override void Exit(GameObject owner, StateContext context)
    {
        owner.GetComponent<MonsterAnimator>().SetDefault(false);
    }
}
