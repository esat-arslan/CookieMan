using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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

    private GridCell GetNeighborCell(Vector3 currentPos, Vector2 direction)
    {
        GridCell currentCell = gridRenderer.GetCell(currentPos);
        Direction dir = gridRenderer.GetDirection(direction);

        return grid.GetNeighborCell(currentCell, dir);
    }

    public Vector3 GetNeighborCellPosition(Vector3 currentPos, Vector2 direction)
    {
        GridCell neighborCell = GetNeighborCell(currentPos, direction);

        return gridRenderer.GetCellCenter(neighborCell);
    }

    public Vector3 GetCellPosition(Vector3 worldPos)
    {
        GridCell cell = gridRenderer.GetCell(worldPos);
        return gridRenderer.GetCellCenter(cell);
    }
}
