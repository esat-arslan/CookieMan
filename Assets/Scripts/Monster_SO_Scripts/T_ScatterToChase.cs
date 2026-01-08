using UnityEngine;

[CreateAssetMenu(fileName = "T_ScatterToChase", menuName = "CookieMan/FSM/Transitions/ScatterToChase")]
public class T_ScatterToChase : Transition
{
    public override bool ShouldTransition(GameObject owner, StateContext context)
    {
        if (context.level.CurrentState == Monster_Level_State.ChaseNight)
        {
            return true;
        }

        return false;
    }
}
