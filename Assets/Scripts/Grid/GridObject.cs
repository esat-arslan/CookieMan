using UnityEngine;

public class GridObject
{
    private Vector2Int cellPosition;
    
    public GridObject(Vector2Int cellPosition)
    {
        this.cellPosition = cellPosition;
    }
    
    public override string ToString()
    {
        return "GridObject: " + cellPosition;
    }
    
    public Vector2Int GetCellPosition() => cellPosition;
}
