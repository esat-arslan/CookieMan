using System;
using UnityEngine;

public class GameGrid 
{
    private int width;
    private int height;
    private GridObject[,] gridObjects;
    
    public int Width => width;
    public int Height => height;
    public GridObject[,] GetGridObjects() => gridObjects;
    
    public GameGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
        
        gridObjects = new GridObject[width , height];
        InitializeGrid();
    }

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
    
    public bool IsValidCell(GridCell cellToCheck)
    {
        if (cellToCheck.X >= width || cellToCheck.Y >= height) return false;
        if (cellToCheck.X < 0 || cellToCheck.Y < 0) return false;
        
        return true;
    }

    public GridCell ConvertToValidCell(GridCell cellToConvert)
    {
        if (IsValidCell(cellToConvert)) return cellToConvert;

        int xVal = cellToConvert.X;
        int yVal = cellToConvert.Y;

        if (cellToConvert.X < 0) xVal = 0;
        if (cellToConvert.X >= width) xVal = width - 1;
        if (cellToConvert.Y < 0) yVal = 0;
        if (cellToConvert.Y >= height) xVal = height - 1;

        return new GridCell(xVal, yVal);
    }
    
    public GridCell GetNeighborCell(GridCell currentCell, Direction direction)
    {
        GridCell neighborCell;

        switch (direction)
        {
            case Direction.Up:
                neighborCell = new GridCell(currentCell.X, currentCell.Y + 1);
                break;
            case Direction.Right:
                neighborCell = new GridCell(currentCell.X + 1, currentCell.Y);
                break;
            case Direction.Down:
                neighborCell = new GridCell(currentCell.X, currentCell.Y - 1);
                break;
            case Direction.Left:
                neighborCell = new GridCell(currentCell.X - 1, currentCell.Y);
                break;
            default:
                throw new Exception("Invalid direction in GetNeighborCell");
        }
        
        return ConvertToValidCell(neighborCell);
    }
}

public readonly struct GridCell
{
    public int X { get; }
    public int Y { get; }

    public GridCell(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public enum Direction { Up, Down, Left, Right, Invalid }
