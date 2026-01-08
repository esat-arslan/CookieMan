using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Debugger : MonoBehaviour
{
    public GameObject pathPrefab;
    
    private Monster_Controller npc;
    private List<(Vector2 dir, Vector3 pos)> pathPositions = new();
    private List<GameObject> pathVisualizers = new();
    private Color pathColor;
    private Transform debugParent;

    private void Start()
    {
        npc = GetComponent<Monster_Controller>();
        pathColor = npc.Configuration.monsterColor;

        debugParent = GameObject.FindGameObjectWithTag("Debug").transform;
    }

    void Update()
    {
        CleanUp();
        UpdatePathProjection();
        UpdateVisualization();
    }

    private void UpdatePathProjection()
    {
        Vector2 finalTarget = npc.FinalTarget;
        (Vector2 newDir, Vector2 newTarget) nextResult = (npc.CurrentDir, npc.CurrentTarget);

        int counter = 100;
        
        while (finalTarget != nextResult.newTarget && counter > 0)
        {
            nextResult = AI_Navigation.GetNextDefaultTarget(nextResult.newDir, nextResult.newTarget, finalTarget);
            pathPositions.Add(nextResult);

            counter--;
        }
    }

    private void UpdateVisualization()
    {
        foreach ((Vector2 dir, Vector3 pos) pathPos in pathPositions)
        {
            GameObject tempGo = Instantiate(pathPrefab, pathPos.pos, Quaternion.identity);
            tempGo.transform.parent = debugParent;
            pathVisualizers.Add(tempGo);
            
            tempGo.GetComponent<PathArrow>().Setup(pathColor, pathPos.dir);
        }
    }

    private void CleanUp()
    {
        pathPositions.Clear();
        foreach (GameObject go in pathVisualizers)
        {
            Destroy(go);
        }
        pathVisualizers.Clear();
    }
}
