using UnityEngine;

[CreateAssetMenu(fileName = "T_EatenToScatter", menuName = "CookieMan/FSM/Transitions/EatenToScatter")]
public class T_EatenToScatter : Transition
{
    public override bool ShouldTransition(GameObject owner, StateContext context)
    {
        bool isEaten = owner.GetComponent<Monster_Controller>().IsEaten;
        Monster_Level_State currentLevelState = context.level.CurrentState;

        if (!isEaten && currentLevelState == Monster_Level_State.ScatterDay)
        {
            return true;
        }

        return false;
    }
}