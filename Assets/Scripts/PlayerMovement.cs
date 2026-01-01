using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private int speed = 3;
    
    private bool shouldMove = false;
    private Vector2 previousInputDir;
    private Vector2 currentInputDir;
    private Vector2 moveTarget;

    private GridManager grid;

    public event Action<Vector2> OnDirectionChanged;

    private void Start()
    {
        grid = GridManager.Instance;

        previousInputDir = Vector2.right;
        currentInputDir = Vector2.right;

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

        OnDirectionChanged?.Invoke(newDirection);
    }

    private void HandleDirSwitch(Vector2 newDirection)
    {
        // Direcion on perpendicular Axis
        previousInputDir = currentInputDir;
        currentInputDir = newDirection;
        
        // Direction on same Axis
        if (ShouldSwitchDirection())
        {
            previousInputDir = newDirection;
            currentInputDir = newDirection;
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
}