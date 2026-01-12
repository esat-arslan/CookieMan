using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameStateSimulator))]
public class GameStateSimulatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameStateSimulator simulator = (GameStateSimulator)target;

        if (GUILayout.Button("Simulate Game Start"))
        {   
            GameEvents.StartGame();
            Debug.Log("Game Start simulated");    
        }
        
        if (GUILayout.Button("Simulate Game Won"))
        {   
            GameEvents.GameWon();
            Debug.Log("Game Won simulated");    
        }
        
        if (GUILayout.Button("Simulate Game Over"))
        {   
            GameEvents.GameOver();
            Debug.Log("Game Over simulated");    
        }
        
        if (GUILayout.Button("Simulate Restart"))
        {   
            GameEvents.RestartGame();
            Debug.Log("Restart simulated");    
        }
    }
}