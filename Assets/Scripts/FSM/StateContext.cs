using System.Collections.Generic;
using UnityEngine;

public class StateContext
{
    public LevelManager level;
    
    // Frightened State
    public bool frightenedTimeoutSet = false; 
    
    // Eaten State
    public List<Vector3> chamberPoints;
    public Vector3 currentChamberPos;
    public int currentPosIndex = 0;
    public int numPositions = 0;
    
    public float timer = 0f;

    public StateContext(LevelManager level)
    {
        this.level = level;
    }
}