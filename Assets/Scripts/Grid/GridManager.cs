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

        foreach(GridObject gridObject in grid.GetGridObjects())
        {
            int xPos = gridObject.GetCellPosition().x;
            int yPos = gridObject.GetCellPosition().y;
            Vector3 objectWorldPos = gridRenderer.GetWorldPosition(xPos, yPos);

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
            int cellPosX = gridObject.GetCellPosition().x;
            int cellPosY = gridObject.GetCellPosition().y;
            
            int xPos = (int)gridRenderer.GetWorldPosition(cellPosX, cellPosY).x;
            int yPos = (int)gridRenderer.GetWorldPosition(cellPosX, cellPosY).y;
            
            Vector3 startVertical =  new Vector3(xPos, yPos);
            Vector3 endVertical =  new Vector3(xPos, yPos + 1);
            Gizmos.DrawLine(startVertical, endVertical);
            
            Vector3 startHorizontal =  new Vector3(xPos, yPos);
            Vector3 endHorizontal =  new Vector3(xPos + 1, yPos);
            Gizmos.DrawLine(startHorizontal, endHorizontal);
            
            // Draw Grid Position Text
            GUIStyle textStyle = new GUIStyle();
            textStyle.normal.textColor = Color.white;
            textStyle.alignment = TextAnchor.MiddleCenter;
            
            Vector3 cellCenter =  new Vector3((xPos + 0.5f), (yPos + 0.5f));
            Handles.Label(cellCenter, $"({cellPosX}, {cellPosY})", textStyle);
        }

        int tilemapWidth = grid.Width;
        int tilemapHeight = grid.Height;
        
        Vector3 tilemapEndWorldSpace = gridRenderer.GetWorldPosition(tilemapWidth, tilemapHeight);
        
        float originWorldSpaceX = gridRenderer.GetWorldPosition(0, 0).x;
        float originWorldSpaceY = gridRenderer.GetWorldPosition(0, 0).y;
        
        float widthWorldSpace = tilemapEndWorldSpace.x;
        float heightWorldSpace = tilemapEndWorldSpace.y;
        
        // Draw one more row/column
        Vector3 finalStartVertical =  new Vector3(widthWorldSpace, originWorldSpaceY);
        Vector3 finalEndVertical =  new Vector3(widthWorldSpace, heightWorldSpace);
        Gizmos.DrawLine(finalStartVertical, finalEndVertical);

        Vector3 finalStartHorizontal =  new Vector3(originWorldSpaceX, heightWorldSpace);
        Vector3 finalEndHorizontal =  new Vector3(widthWorldSpace, heightWorldSpace);
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
