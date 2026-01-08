using UnityEngine;

[CreateAssetMenu(fileName = "T_FrightenedToScatter", menuName = "CookieMan/FSM/Transitions/FrightenedToScatter")]
public class T_FrightenedToScatter : Transition
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