using UnityEngine;

public abstract class Transition : ScriptableObject
{
    public State fromState;
    public State toState;

    public abstract bool ShouldTransition(GameObject owner, StateContext context);
}

    
