using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "S_Eaten", menuName = "CookieMan/FSM/States/Eaten")]
public class S_Eaten : State
{
    public override void Enter(GameObject owner, StateContext context)
    {
        owner.GetComponent<MonsterAnimator>().SetEaten(true);

        Monster_Controller monster = owner.GetComponent<Monster_Controller>();
        List<Vector3> configuredPositions = monster.Configuration.chamberPoints;

        if (configuredPositions.Count > 0)
        {
            context.chamberPoints = configuredPositions;
            context.currentPosIndex = 0;
            context.numPositions = context.chamberPoints.Count;
            context.currentChamberPos = context.chamberPoints[context.currentPosIndex];    
        }
        else
        {
            throw new Exception("no chamber positions configured");
        }
        
        monster.FinalTarget = context.currentChamberPos;
        monster.GetNextIntermediateTarget = AI_Navigation.GetNextEatenTarget;
    }

    public override void Tick(GameObject owner, StateContext context)
    {
        Monster_Controller monster = owner.GetComponent<Monster_Controller>();
        Vector3 monsterPos = owner.transform.position;
        
        
        if ( Vector2.Distance(monsterPos, context.currentChamberPos) < 0.1f)
        {
            context.currentPosIndex++;
            
            if (HasReachedFinalPosition(context))
            {
                monster.IsEaten = false;
                return;
            }
            
            context.currentChamberPos = context.chamberPoints[context.currentPosIndex];
            monster.FinalTarget = context.currentChamberPos;
        }
    }

    public override void Exit(GameObject owner, StateContext context)
    {
        owner.GetComponent<MonsterAnimator>().SetEaten(false);
    }

    private bool HasReachedFinalPosition(StateContext context)
    {
        return context.currentPosIndex == context.numPositions;
    }
}