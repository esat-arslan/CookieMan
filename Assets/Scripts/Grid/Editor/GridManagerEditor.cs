using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        GridManager gridManager = (GridManager)target;

        if (GUILayout.Button("Regenerate Grid"))
        {
            gridManager.RegenerateGrid();
            SceneView.RepaintAll();     // makes the changes appear immediately
        }

        if (GUILayout.Button("Toggle Grid"))
        {
            gridManager.ToggleVisibility();
            SceneView.RepaintAll();     // makes the changes appear immediately
        }
    }
}
