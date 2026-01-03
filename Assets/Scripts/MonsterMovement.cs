using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private int speed = 3;
    [SerializeField] private GameObject player;
    [SerializeField] private GhostType ghostType;
    [SerializeField] private GameObject rozy;

    private Vector2 previousPosition;
    private Vector2 currentDirection;
    private GridManager grid;
    private Vector3 targetPos;
    private Vector2 targetTile;
    private bool hasTarget;
    private float scatterDistanceSQ = 64;


    private void Start()
    {
        grid = GridManager.Instance;
        previousPosition = transform.position;
        currentDirection = Vector2.zero;

        targetTile = grid.GetCellPosition(transform.position);
        hasTarget = false;

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!hasTarget)
        {
            Vector2 currentTarget = GetTargetTile();
            currentDirection = ChooseDirection(currentTarget);
            targetTile = GetTarget(currentDirection);
            hasTarget = true;
        }

        Vector2 currentPos = transform.position;
        float step = speed * Time.deltaTime;
        float distanceToTarget = Vector2.Distance(transform.position, targetTile);

        if (distanceToTarget <= step)
        {
            transform.position = new Vector3(targetTile.x, targetTile.y, transform.position.z);
            hasTarget = false;
        }
        else
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, targetTile, step);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
        /*if (Vector2.Distance(newPos, targetTile) < 0.01f)
        {
            transform.position = new Vector3
            (
                targetTile.x,
                targetTile.y,
                transform.position.z
            );

            hasTarget = false;
        }*/

    }

    private Vector2 GetTargetTile()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 playerDir = player.GetComponent<PlayerMovement>().CurrentInputDir;

        switch (ghostType)
        {
            case GhostType.Rozy:
                return playerPos;
            case GhostType.Archie:
                return playerPos + (playerDir * 4);
            case GhostType.Pochy:
                float dist = Vector2.SqrMagnitude(playerPos - (Vector2)transform.position);
                if (dist < scatterDistanceSQ) return Vector2.zero; else return playerPos;
            case GhostType.Freezy:
                return CalculateFreezysTarget();
            default:
                return playerPos;
        }
    }

    private Vector2 CalculateFreezysTarget()
    {
        Vector2 rozyPos = rozy.transform.position;
        Vector2 playerPos = player.transform.position;
        Vector2 playerDir = player.GetComponent<PlayerMovement>().CurrentInputDir;
        Vector2 pivot = playerPos + (playerDir * 2);

        return (pivot * 2) - rozyPos;
    }

    private Vector2 ChooseDirection(Vector2 target)
    {
        Vector2[] Directions = { Vector2.down, Vector2.left, Vector2.right, Vector2.up };
        Vector2 bestDirection = Vector2.zero;
        float minDistance = float.MaxValue;

        foreach (Vector2 dir in Directions)
        {
            if (!grid.IsNeighborCellWalkable(transform.position, dir)) continue;
            if (dir == -currentDirection) continue;
            Vector2 neighborPos = grid.GetNeighborCellPosition(transform.position, dir);
            float dist = Vector2.SqrMagnitude(neighborPos - target);

            if (dist < minDistance)
            {
                minDistance = dist;
                bestDirection = dir;
            }
        }
        return bestDirection;
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

    private void OnDrawGizmos()
    {
        if (hasTarget)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, (Vector3)targetTile);
        }

        if (Application.isPlaying && player != null)
        {
            Vector2 currentGoal = GetTargetTile();

            Gizmos.color = ghostType switch
            {
                GhostType.Rozy => Color.red,
                GhostType.Archie => Color.magenta, 
                GhostType.Freezy => Color.cyan,
                GhostType.Pochy => new Color(1f, 0.5f, 0f),
                _ => Color.white
            };

            Gizmos.DrawWireCube(new Vector3(currentGoal.x, currentGoal.y, 0), Vector3.one * 0.5f);

            Gizmos.DrawLine(transform.position, (Vector3)currentGoal);
        }
        if (ghostType == GhostType.Freezy)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 playerDir = player.GetComponent<PlayerMovement>().CurrentInputDir;
            Vector2 pivot = playerPos + (playerDir * 2);

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(pivot, 0.2f); 
        }
    }
}

public enum GhostType { Rozy, Archie, Freezy, Pochy }