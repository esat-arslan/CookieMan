using System;
using UnityEngine;

public class StateMachineRunner : ResettableBehavior
{   
    public StateMachine stateMachine;

    private State currentState;
    private StateContext stateContext;
    private LevelManager level;

    private void Start()
    {
        level = GameObject.FindWithTag("Level").GetComponent<LevelManager>();
        if (level == null) throw new Exception("Levelmanager not found");
        
        stateContext = new StateContext(level);
        
        currentState = stateMachine.initialState;
        currentState.Enter(gameObject, stateContext);
    }

    private void Update()
    {
        foreach (Transition transition in stateMachine.transitions)
        {
            if (transition.fromState != currentState)
            {
                continue;
            }
            
            if (transition.ShouldTransition(gameObject, stateContext))
            {
                currentState.Exit(gameObject, stateContext);
                currentState = transition.toState;
                //Debug.Log($"current state: {currentState.name}");
                currentState.Enter(gameObject, stateContext);
                return;
            }
        }
        
        currentState?.Tick(gameObject, stateContext);
    }

    public override void Reset()
    {
        stateContext = new StateContext(level);
        
        currentState = stateMachine.initialState;
        currentState.Enter(gameObject, stateContext);
    }
}
