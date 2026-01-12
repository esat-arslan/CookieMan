using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : ResettableBehavior, IPortable
{
    [SerializeField] private Conf_Player configuration;
    [SerializeField] private int speed = 3;
    
    private bool shouldMove = false;
    private Vector2 previousInputDir;
    private Vector2 currentInputDir;
    
    private Vector2 moveTarget;

    private GridManager grid;
    private LevelManager level;
    
    public Vector2 PlayerDirection => currentInputDir;
    public event Action<Vector2> OnDirectionChanged;

    public Vector2 CurrentInputDir
    {
        get => currentInputDir;
        set
        {
            currentInputDir = value;
            OnDirectionChanged?.Invoke(currentInputDir);
        }
    }

    private void OnDisable()
    {
        OnDirectionChanged?.Invoke(Vector2.zero);
    }

    private void Start()
    {
        grid = GridManager.Instance;
        level = GameObject.FindWithTag("Level").GetComponent<LevelManager>();

        previousInputDir = Vector2.right;
        CurrentInputDir = Vector2.right;
        shouldMove = true;
    }

    private void Update()
    {
        if (!shouldMove) return;

        if (!grid.HasReachedCellCenterInDirection(previousInputDir, transform.position))
        {
            moveTarget = GetTarget(previousInputDir);
        }
        else
        {
            moveTarget = GetTarget(currentInputDir);
        }
        Move();
    }

    private void Move()
    {
        Debug.DrawRay(transform.position, (Vector3)moveTarget - transform.position, Color.cyan);
        transform.position = Vector2.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            return;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            return;
        }

        Vector2 newDirection = context.ReadValue<Vector2>();
        if (ShouldIgnoreDir(newDirection)) return;

        // fix for oscillating on one axis
        HandleDirSwitch(newDirection);
    }

    private void HandleDirSwitch(Vector2 newDirection)
    {
        // Direcion on perpendicular Axis
        previousInputDir = currentInputDir;
        CurrentInputDir = newDirection;
        
        // Direction on same Axis
        if (ShouldSwitchDirection())
        {
            previousInputDir = newDirection;
            CurrentInputDir = newDirection;
        }
    }

    private bool ShouldSwitchDirection()
    {
        if (previousInputDir == currentInputDir) return false;
        if (Mathf.Abs(previousInputDir.x) != Mathf.Abs(currentInputDir.x) ||
            Mathf.Abs(previousInputDir.y) != Mathf.Abs(currentInputDir.y)) return false;

        return true;
    }

    private bool ShouldIgnoreDir(Vector2 dir)
    {
        // only accept 1 or -1
        if ((Mathf.Abs(dir.x) == 1 && Mathf.Abs(dir.y) == 0)) return false;
        if ((Mathf.Abs(dir.x) == 0 && Mathf.Abs(dir.y) == 1)) return false;
        return true;
    }

    private Vector3 GetTarget(Vector2 dir)
    {
        if (dir == Vector2.zero) return transform.position;
        
        if (grid.IsNeighborCellWalkable(transform.position, dir))
        {
            return grid.GetNeighborCellPosition(transform.position, dir);
        }

        return grid.GetCellPosition(transform.position);
    }

    public void Teleport(Vector3 portalPos, Vector2Int entryDir)
    {
        Vector3 portal1 = grid.GetCellPosition(level.PortalsConf.portal1);
        Vector3 portal2 = grid.GetCellPosition(level.PortalsConf.portal2);

        portalPos = grid.GetCellPosition(portalPos);

        CurrentInputDir = entryDir;
        previousInputDir = entryDir;
        
        if (portalPos == portal1)
        {
            // teleport to point2
            Vector3 nextToPortal = grid.GetNeighborCellPosition(portal2, entryDir);
            
            transform.position = nextToPortal + new Vector3(entryDir.x, entryDir.y, 0) * 0.1f;
            moveTarget = grid.GetNeighborCellPosition(nextToPortal, entryDir);
        }

        if (portalPos == portal2)
        {
            // teleport to point1
            Vector3 nextToPortal = grid.GetNeighborCellPosition(portal1, entryDir);
            
            transform.position = nextToPortal + new Vector3(entryDir.x, entryDir.y, 0) * 0.1f;
            moveTarget = grid.GetNeighborCellPosition(nextToPortal, entryDir);
        }
    }

    public override void Reset()
    {
        transform.position = configuration.spawnPosition;
        
        previousInputDir = Vector2.right;
        CurrentInputDir = Vector2.right;
        shouldMove = true;
    }
}
