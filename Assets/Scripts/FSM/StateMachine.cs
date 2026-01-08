using UnityEngine;

[CreateAssetMenu(menuName = "CookieMan/FSM/State Machine")]
public class StateMachine : ScriptableObject
{
        public State initialState;
        public Transition[] transitions;
}