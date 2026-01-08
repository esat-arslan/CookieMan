using UnityEngine;

[CreateAssetMenu(fileName = "T_ChaseToScatter", menuName = "CookieMan/FSM/Transitions/ChaseToScatter")]
public class T_ChaseToScatter : Transition
{
    public override bool ShouldTransition(GameObject owner, StateContext context)
    {
        if (context.level.CurrentState == Monster_Level_State.ScatterDay)
        {
            return true;
        }

        return false;
    }
}
