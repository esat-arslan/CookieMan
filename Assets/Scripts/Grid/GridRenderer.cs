using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridRenderer
{
    private Vector3 origin;
    private float gridSize = 1.0f;

    private GameGrid grid;
    
    public GridRenderer(Vector3 origin, GameGrid grid)
    {
        this.origin = origin;
        this.grid = grid;
    }

    public Vector3 GetWorldPosition(GridCell cell)
    {
        return new Vector3(cell.X, cell.Y, 0) + origin;
    }

    public GridCell GetCell(Vector3 worldPosition)
    {
        Vector3 originReverted = worldPosition - origin;
        var cellCandidate = new GridCell(Mathf.FloorToInt(originReverted.x), Mathf.FloorToInt(originReverted.y));

        if (!grid.IsValidCell(cellCandidate)) throw new System.Exception("Invalid cell");

        return cellCandidate;
    }

    public Direction GetDirection(Vector2 directionVector)
    {
        if (directionVector == Vector2.up) return Direction.Up;
        if (directionVector == Vector2.down) return Direction.Down;
        if (directionVector == Vector2.right) return Direction.Right;
        if (directionVector == Vector2.left) return Direction.Left;
        return Direction.Invalid;
    }

    public bool HasReachedCellCenterInDirection(Vector2 dir, Vector3 currentPos)
    {
        Direction direction = GetDirection(dir);

        Vector3 posInGridCorrdinates = GetPosInGridCoordinates(currentPos);
        float xDistanceFromCellStart = posInGridCorrdinates.x - Mathf.Floor(posInGridCorrdinates.x);
        float yDistanceFromCellStart = posInGridCorrdinates.y - Mathf.Floor(posInGridCorrdinates.y);

        float distanceToCenter = gridSize / 2;

        switch (direction)
        {
            case Direction.Up: return (yDistanceFromCellStart >= distanceToCenter);
            case Direction.Down: return (yDistanceFromCellStart <= distanceToCenter);
            case Direction.Right: return (xDistanceFromCellStart >= distanceToCenter);
            case Direction.Left: return (xDistanceFromCellStart <= distanceToCenter);
        }

        return false;
    }

    private Vector3 GetPosInGridCoordinates(Vector3 worldPosition)
    {
        return worldPosition - origin;
    }

    public Vector3 GetCellCenter(GridCell cell)
    {
        return GetWorldPosition(cell) + new Vector3(gridSize/2, gridSize/2, 0);
    }
}
