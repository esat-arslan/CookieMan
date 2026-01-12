using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class AI_Navigation
{
    public static (Vector2 newDir, Vector3 newTarget) GetNextDefaultTarget(Vector2 currentDir, Vector3 currentPos, Vector3 finalTargetPos = new ())
    {
        GridManager grid = GridManager.Instance;
        
        // get walkable neighbors
        List<Vector2> walkableDirs = new List<Vector2>();

        foreach (Vector2 possibleDir in GetValidDirections())
        {
            if (Is180Turn(currentDir, possibleDir)) continue;
            
            if (grid.IsNeighborCellWalkable(currentPos, possibleDir))
            {
                walkableDirs.Add(possibleDir);
            }
        }
        
        return GetClosestToTarget(walkableDirs, currentPos, finalTargetPos);
    }
    
    public static (Vector2 newDir, Vector3 newTarget) GetNextEatenTarget(Vector2 currentDir, Vector3 currentPos, Vector3 finalTargetPos = new ())
    {
        GridManager grid = GridManager.Instance;
        
        // get walkable neighbors
        List<Vector2> walkableDirs = new List<Vector2>();

        foreach (Vector2 possibleDir in GetValidDirections())
        {
            if (Is180Turn(currentDir, possibleDir)) continue;
            
            if (grid.IsNeighborCellAIWalkable(currentPos, possibleDir))
            {
                walkableDirs.Add(possibleDir);
            }
        }
        
        return GetClosestToTarget(walkableDirs, currentPos, finalTargetPos);
    }
    
    public static (Vector2 newDir, Vector3 newTarget) GetNextRandomTarget(Vector2 currentDir, Vector3 currentPos, Vector3 finalTargetPos = new ())
    {
        GridManager grid = GridManager.Instance;
        
        // get walkable neighbors
        List<Vector2> walkableDirs = new List<Vector2>();

        foreach (Vector2 possibleDir in GetValidDirections())
        {
            if (Is180Turn(currentDir, possibleDir)) continue;
            
            if (grid.IsNeighborCellWalkable(currentPos, possibleDir))
            {
                walkableDirs.Add(possibleDir);
            }
        }

        Vector3 intermediateTarget = currentPos;

        int numOfElements = walkableDirs.Count;
        Vector2 decidedDir = walkableDirs[Random.Range(0, numOfElements)];

        intermediateTarget = grid.GetNeighborCellPosition(currentPos, decidedDir);
        
        //if (intermediateTarget == currentPos) throw new Exception("Check intermediate target navigation");
        return (decidedDir, intermediateTarget);
    }

    public static bool Is180Turn(Vector2 oldDir, Vector2 newDir)
    {
        return newDir == -oldDir;
    }

    public static List<Vector2> GetValidDirections()
    {
        return new List<Vector2>()
        {
            new(1, 0),
            new(0, 1),
            new(-1, 0),
            new(0, -1)
        };
    }

    public static bool HasReachedTargetCellCenter(Vector2 dir, Vector3 currentPos, Vector3 targetPos)
    {   
        GridManager grid = GridManager.Instance;

        Vector2 currentCellPos = grid.GetCellPosition(currentPos);
        Vector2 targetCellPos = grid.GetCellPosition(targetPos);

        if (currentCellPos != targetCellPos) return false;

        return grid.HasReachedCellCenterInDirection(dir, currentPos);
    }

    private static (Vector2 newDir, Vector3 newTarget) GetClosestToTarget(List<Vector2> walkableDirs,
        Vector3 currentPos, Vector3 targetPos)
    {   
        GridManager grid = GridManager.Instance;
        
        Vector3 intermediateTarget = currentPos;
        Vector2 nextDir = default;

        float minDistance = float.MaxValue;

        foreach (Vector2 dir in walkableDirs)
        {
            Vector3 neighborPosition = grid.GetNeighborCellPosition(currentPos, dir);
            float distanceToTarget = Vector3.Distance(neighborPosition, targetPos);

            // keep track of the min distance
            if (distanceToTarget < minDistance)
            {
                minDistance = distanceToTarget;
                intermediateTarget = neighborPosition;
                nextDir = dir;
            }
        }
        
        return (nextDir, intermediateTarget);
    }
}

public delegate (Vector2 newDir, Vector3 newTarget) GetNextTarget(Vector2 currentDir, Vector3 currentPos,
    Vector3 finalTargetPos = new());

