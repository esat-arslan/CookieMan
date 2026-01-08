using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract void Enter(GameObject owner, StateContext context);
    public abstract void Tick(GameObject owner, StateContext context);
    public abstract void Exit(GameObject owner, StateContext context);
}
