using UnityEngine;

public class GridRenderer
{
    private Vector3 origin;
    
    public GridRenderer(Vector3 origin)
    {
        this.origin = origin;
    }

    public Vector3 GetWorldPosition(GridCell cell)
    {
        return new Vector3(cell.X, cell.Y, 0) + origin;
    }
}
