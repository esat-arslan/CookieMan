using UnityEngine;

[CreateAssetMenu(fileName = "T_ChaseToFrightened", menuName = "CookieMan/FSM/Transitions/ChaseToFrightened")]
public class T_ChaseToFrightened : Transition
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