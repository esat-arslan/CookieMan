using UnityEngine;

/// <summary>
/// Create a Grid object with properties,
/// has a constructor that accepts width and height.
/// </summary>
public class GameGrid
{
    private int height;
    private int width;

    // whole grid to access
    private GridObject[,] gridObjects;
    public int Width => width;
    public int Height => height;

    // returns the whole grid
    public GridObject[,] GetGridObjects() => gridObjects;

    // constructor; creates a grid and calls InitializeGrid()
    public GameGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

        gridObjects = new GridObject[width, height];
        InitializeGrid();
    }

    // Create a new GridCell object inside the gridMap
    // store it in gridObjects.
    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cellPosition = new GridCell(x, y);
                gridObjects[x, y] = new GridObject(cellPosition);
            }
        }
    }

    // Accepts a GridCell as parameter
    // Checks whether the cell's coordinates are in bound.1
    public bool IsValidCell(GridCell cellToCheck)
    {
        if (cellToCheck.X > width || cellToCheck.Y > height) return false;
        if (cellToCheck.X< 0 || cellToCheck.Y < 0) return false;

        return true; 
    }

    public GridCell GetNeighborCell(GridCell currentCell, Direction direction)
    {
        GridCell neighborCell;

        switch (direction)
        {
            case Direction.Up:
                neighborCell = new GridCell(currentCell.X, currentCell.Y + 1);
                break;
            case Direction.Down:
                neighborCell = new GridCell(currentCell.X, currentCell.Y - 1);
                break;
            case Direction.Right:
                neighborCell = new GridCell(currentCell.X + 1, currentCell.Y);
                break;
            case Direction.Left:
                neighborCell = new GridCell(currentCell.X -1, currentCell.Y);
                break;
            default:
                throw new System.Exception("Invalid direction in GetNeighborCell");
        }

        if (!IsValidCell(neighborCell)) throw new System.Exception("Neighborcell is outside of the grid");

        return neighborCell;
    }

}

// struct to store each gridcell
// Immutable value type for grid coordinates
public readonly struct GridCell
{
    public int X {get;}
    public int Y {get;}
    
    public GridCell(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public enum Direction {Up, Down, Left, Right, Invalid}
