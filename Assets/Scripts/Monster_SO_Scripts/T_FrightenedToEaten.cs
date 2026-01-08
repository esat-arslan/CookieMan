using UnityEngine;

[CreateAssetMenu(fileName = "T_FrightenedToEaten", menuName = "CookieMan/FSM/Transitions/FrightenedToEaten")]
public class T_FrightenedToEaten : Transition
{
    public override bool ShouldTransition(GameObject owner, StateContext context)
    {
        if (owner.GetComponent<Monster_Controller>().IsEaten)
        {
            return true;
        }

        return false;
    }
}