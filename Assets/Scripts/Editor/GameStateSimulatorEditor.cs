using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameStateSimulator))]
public class GameStateSimulatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameStateSimulator simulator = (GameStateSimulator)target;

        if(GUILayout.Button("Simulate game start"))
        {
            GameEvents.StartGame();
            Debug.Log("Game Start Simulate");
        }

        if(GUILayout.Button("Simulate game won"))
        {
            GameEvents.GameWon();
            Debug.Log("Game Won Simulate");
        }

        if(GUILayout.Button("Simulate game over"))
        {
            GameEvents.GameOver();
            Debug.Log("Game Over Simulate");
        }

        if(GUILayout.Button("Simulate game restart"))
        {
            GameEvents.RestartGame();
            Debug.Log("Game Restart Simulate");
        }
    }

}
