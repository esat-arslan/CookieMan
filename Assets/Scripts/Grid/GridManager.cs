using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways]
public class GridManager : MonoBehaviour
{
    private static GridManager instance;
    public static GridManager Instance => instance;

    [SerializeField] private Tilemap tilemap;

    private GameGrid grid;
    private GridRenderer gridRenderer;
    private bool showGrid;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        InitializeGrid();
    }

    private void InitializeGrid()
    {
        if (tilemap == null) return;
        tilemap.CompressBounds();

        grid = new GameGrid(tilemap.size.x, tilemap.size.y);
        gridRenderer = new GridRenderer(tilemap.origin);

        foreach (GridObject gridObject in grid.GetGridObjects())
        {
            Vector3 objectWorldPos = gridRenderer.GetWorldPosition(gridObject.GetCellPosition());

            int xVal = Mathf.FloorToInt(objectWorldPos.x);
            int yVal = Mathf.FloorToInt(objectWorldPos.y);

            TileBase tile = tilemap.GetTile(new Vector3Int(xVal, yVal, 0));

            if (tile == null) continue;

            Type tileType = tile.GetType();

            if (tileType == typeof(Wall))
            {
                gridObject.Type = GridObjectType.Wall;
            }

            if (tileType == typeof(Path))
            {
                gridObject.Type = GridObjectType.Path;
            }
        }

        //ask the tilemap about the respective tile type
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (tilemap == null || grid == null || showGrid == false) return;

        Gizmos.color = Color.cyan;

        GridObject[,] gridObjects = grid.GetGridObjects();

        foreach (GridObject gridObject in gridObjects)
        {
            GridCell currentCell = gridObject.GetCellPosition();

            Vector3 cellWorldPosition = gridRenderer.GetWorldPosition(currentCell);

            int xPos = (int)cellWorldPosition.x;
            int yPos = (int)cellWorldPosition.y;

            Vector3 startVertical = new Vector3(xPos, yPos);
            Vector3 endVertical = new Vector3(xPos, yPos + 1);
            Gizmos.DrawLine(startVertical, endVertical);

            Vector3 startHorizontal = new Vector3(xPos, yPos);
            Vector3 endHorizontal = new Vector3(xPos + 1, yPos);
            Gizmos.DrawLine(startHorizontal, endHorizontal);

            Vector3 cellCenter = new Vector3((xPos + 0.5f), (yPos + 0.5f));

            if (gridObject.Type == GridObjectType.Wall)
            {
                GUIStyle wallTextStyle = new GUIStyle();
                wallTextStyle.normal.textColor = Color.orange;
                wallTextStyle.fontSize = 12;
                wallTextStyle.fontStyle = FontStyle.Bold;
                wallTextStyle.alignment = TextAnchor.MiddleCenter;
                Handles.Label(cellCenter, $"Wall", wallTextStyle);
            }
            if (gridObject.Type == GridObjectType.Path)
            {
                GUIStyle textStyle = new GUIStyle();
                textStyle.normal.textColor = Color.white;
                textStyle.alignment = TextAnchor.MiddleCenter;
                Handles.Label(cellCenter, $"({currentCell.X}, {currentCell.Y})", textStyle);

            }
        }

        int tilemapWidth = grid.Width;
        int tilemapHeight = grid.Height;

        Vector3 firstCellWorldSpace = gridRenderer.GetWorldPosition(new GridCell(0, 0));
        Vector3 lastCellWorldSpace = gridRenderer.GetWorldPosition(new GridCell(tilemapWidth, tilemapHeight));

        float originWorldSpaceX = firstCellWorldSpace.x;
        float originWorldSpaceY = firstCellWorldSpace.y;

        float widthWorldSpace = lastCellWorldSpace.x;
        float heightWorldSpace = lastCellWorldSpace.y;

        // Draw one more row/column
        Vector3 finalStartVertical = new Vector3(widthWorldSpace, originWorldSpaceY);
        Vector3 finalEndVertical = new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(finalStartVertical, finalEndVertical);

        Vector3 finalStartHorizontal = new Vector3(originWorldSpaceX, heightWorldSpace);
        Vector3 finalEndHorizontal = new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(finalStartHorizontal, finalEndHorizontal);
    }

#endif

    public void RegenerateGrid()
    {
        InitializeGrid();
    }

    public void ToggleVisibility()
    {
        showGrid = !showGrid;
    }
}
