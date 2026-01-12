using System;
using UnityEngine;

public class Monster_Controller : ResettableBehavior, IPortable
{
    [SerializeField] private Transform player;
    [SerializeField] private int speed = 2;
    [SerializeField] private Conf_Monster configuration;
    
    private GridManager grid;
    private LevelManager level;
    private Vector2 currentTarget;
    private Vector2 currentDir;
    private Vector2 finalTarget;

    public GetNextTarget GetNextIntermediateTarget { get; set; }
    public event Action<Vector2> OnDirectionChanged;
    public Conf_Monster Configuration => configuration;
    public Transform Player => player;
    
    public bool IsEaten { get; set; }

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
        set
        {
            currentDir = value;
            OnDirectionChanged?.Invoke(currentDir);
        }
    }
    
    private void Start()
    {
        grid = GridManager.Instance;
        level = GameObject.FindWithTag("Level").GetComponent<LevelManager>();

        FinalTarget = grid.GetNonWalkableStartPosition();
        GetNextIntermediateTarget = AI_Navigation.GetNextDefaultTarget;
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
    
    public virtual void SetChaseTarget() {}
    

    private void UpdateIntermediateTarget(Vector3 currentPos, Vector3 finalTargetPos)
    {
        (Vector2 newDir, Vector3 newTarget) result = GetNextIntermediateTarget(currentDir, currentPos, finalTarget);
        
        CurrentDir = result.newDir;
        currentTarget = result.newTarget;
    }

    public void Teleport(Vector3 portalPos, Vector2Int entryDir)
    {
        Vector3 portal1 = grid.GetCellPosition(level.PortalsConf.portal1);
        Vector3 portal2 = grid.GetCellPosition(level.PortalsConf.portal2);

        portalPos = grid.GetCellPosition(portalPos);    
        
        if (portalPos == portal1)
        {
            // teleport to point2
            Vector3 nextToPortal = grid.GetNeighborCellPosition(portal2, entryDir);
            transform.position = nextToPortal + new Vector3(entryDir.x, entryDir.y, 0) * 0.1f;
            
            UpdateIntermediateTarget(transform.position, finalTarget);
        }

        if (portalPos == portal2)
        {
            // teleport to point1
            Vector3 nextToPortal = grid.GetNeighborCellPosition(portal1, entryDir);
            transform.position = nextToPortal + new Vector3(entryDir.x, entryDir.y, 0) * 0.1f;
            
            UpdateIntermediateTarget(transform.position, finalTarget);
        }
    }

    public override void Reset()
    {
        transform.position = configuration.spawnPosition;
        
        FinalTarget = grid.GetNonWalkableStartPosition();
        GetNextIntermediateTarget = AI_Navigation.GetNextDefaultTarget;
        UpdateIntermediateTarget(transform.position, finalTarget);
    }
}



















