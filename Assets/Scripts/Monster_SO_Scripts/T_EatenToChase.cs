using UnityEngine;

[CreateAssetMenu(fileName = "T_EatenToChase", menuName = "CookieMan/FSM/Transitions/EatenToChase")]
public class T_EatenToChase : Transition
{
    public override bool ShouldTransition(GameObject owner, StateContext context)
    {
        bool isEaten = owner.GetComponent<Monster_Controller>().IsEaten;
        Monster_Level_State currentLevelState = context.level.CurrentState;

        if (!isEaten && currentLevelState == Monster_Level_State.ChaseNight)
        {
            return true;
        }

        return false;
    }
}