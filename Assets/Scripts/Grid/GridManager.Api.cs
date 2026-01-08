using UnityEngine;

public partial class GridManager
{
    public bool HasReachedCellCenterInDirection(Vector2 dir, Vector3 currentPos)
    {
        return gridRenderer.HasReachedCellCenterInDirection(dir, currentPos);
    }       
    
    public bool IsNeighborCellWalkable(Vector3 currentPos, Vector2 direction)
    {
        GridCell neighborCell = GetNeighborCell(currentPos, direction);
        
        GridObject gridObj = grid.GetGridObjects()[neighborCell.X, neighborCell.Y];
        return gridObj.Type == GridObjectType.Path;
    }
    
    public bool IsNeighborCellAIWalkable(Vector3 currentPos, Vector2 direction)
    {
        GridCell neighborCell = GetNeighborCell(currentPos, direction);
        
        GridObject gridObj = grid.GetGridObjects()[neighborCell.X, neighborCell.Y];
        return (gridObj.Type == GridObjectType.Path) || (gridObj.Type == GridObjectType.Chamber);
    }
    
    public GridCell GetNeighborCell(Vector3 currentPos, Vector2 direction)
    {
        // Get the current Grid cell
        GridCell currentCell = gridRenderer.GetCell(currentPos);
        Direction dir = gridRenderer.GetDirection(direction);
        
        // Get the neighbor cell in the requested direction
        return grid.GetNeighborCell(currentCell, dir);
    }
    
    public Vector3 GetNeighborCellPosition(Vector3 currentPos, Vector2 direction)
    {
        GridCell neighborCell = GetNeighborCell(currentPos, direction);

        // return the position of the neighbor cell
        return gridRenderer.GetCellCenter(neighborCell);
    }
    
    public Vector3 GetCellPosition(Vector3 worldPos)
    {
        GridCell cell = gridRenderer.GetCell(worldPos);
        return gridRenderer.GetCellCenter(cell);
    }
    
    public Vector3 GetNonWalkableStartPosition()
    {
        return gridRenderer.GetWorldPosition(new GridCell(0, 0));
    }
}