using UnityEngine;

[CreateAssetMenu(fileName = "T_FrightenedToChase", menuName = "CookieMan/FSM/Transitions/FrightenedToChase")]
public class T_FrightenedToChase : Transition
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
