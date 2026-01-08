using UnityEngine;

[CreateAssetMenu(fileName = "T_ScatterToFrightened", menuName = "CookieMan/FSM/Transitions/ScatterToFrightened")]
public class ScatterToFrightened : Transition
{
    public override bool ShouldTransition(GameObject owner, StateContext context)
    {
        if (context.level.CurrentState == Monster_Level_State.Frightened)
        {
            return true;
        }

        return false;
    }
}