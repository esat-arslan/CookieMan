using System;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private int speed = 2;
    [SerializeField] private Conf_Monster configuration;
    
    private GridManager grid;
    private Vector2 currentTarget;
    private Vector2 currentDir;

    private Vector2 finalTarget;
    
    public Conf_Monster Configuration => configuration;
    public Transform Player => player;

    public Vector2 FinalTarget
    {
        get => finalTarget;
        set => finalTarget = grid.GetCellPosition(value);
    }
    
    public Vector2 CurrentTarget
    {
        get => currentTarget;
        set => currentTarget = value;
    }
    
    public Vector2 CurrentDir
    {
        get => currentDir;
        set => currentDir = value;
    }

    private void Start()
    {
        grid = GridManager.Instance;

        FinalTarget = grid.GetNonWalkableStartPosition();
        UpdateIntermediateTarget(transform.position, finalTarget);
    }

    void Update()
    {
        if (AI_Navigation.HasReachedTargetCellCenter(currentDir, transform.position, currentTarget))
        {
            UpdateIntermediateTarget(transform.position, finalTarget);
        }
        
        Debug.DrawRay(transform.position, (Vector3)currentTarget - transform.position, Color.cyan);
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

    private void UpdateIntermediateTarget(Vector3 currentPos, Vector3 finalTargetPos)
    {
        (Vector2 newDir, Vector3 newTarget) result = AI_Navigation.GetNextIntermediateTarget(currentDir, currentPos, finalTargetPos);
        currentDir = result.newDir;
        currentTarget = result.newTarget;
    }
}
