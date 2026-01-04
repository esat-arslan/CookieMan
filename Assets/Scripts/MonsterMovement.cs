using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MonsterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private int speed = 3;
    [SerializeField] private GhostType ghostType;
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject rozy; // Only needed for freezy

    private PlayerMovement playerScript;
    private MonsterAnimator monsterAnimator;
    private GridManager grid;

    private Vector2 currentDirection;
    private Vector3 targetWorldPos;
    private Vector2 targetTile;
    private bool hasTarget;
    private float scatterDistanceSQ = 64;


    private void Start()
    {
        grid = GridManager.Instance;
        monsterAnimator = gameObject.GetComponent<MonsterAnimator>();

        currentDirection = Vector2.right;
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerMovement>();
        }

        targetWorldPos = grid.GetCellPosition(transform.position);
        targetTile = grid.GetCellPosition(transform.position);
        hasTarget = false;

        monsterAnimator.SetDefault(true);

    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!hasTarget)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 playerDir = (playerScript != null) ? playerScript.CurrentInputDir : Vector2.zero;
            Vector2 rozyPos = (rozy != null) ? (Vector2)rozy.transform.position : Vector2.zero;

            Vector2 finalTargetTile = AI_Brain.GetTargetTile(
                ghostType,
                transform.position,
                playerPos,
                playerDir,
                rozyPos,
                scatterDistanceSQ
            );

            currentDirection = AI_Brain.ChooseNextDirection(grid, transform.position, currentDirection, finalTargetTile);

            targetWorldPos = grid.GetNeighborCellPosition(transform.position, currentDirection);
            hasTarget = true;

            monsterAnimator.HandleDirectionState(currentDirection);
        }

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, step);

        // Check if we arrived
        if (Vector3.Distance(transform.position, targetWorldPos) < 0.001f)
        {
            transform.position = targetWorldPos; // Snap to grid
            hasTarget = false;
        }
    }


    private void OnDrawGizmos()
    {
        if (hasTarget)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, targetWorldPos);
        }

        if (Application.isPlaying && player != null)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 playerDir = (playerScript != null) ? playerScript.CurrentInputDir : Vector2.zero;
            Vector2 rozyPos = (rozy != null) ? (Vector2)rozy.transform.position : Vector2.zero;
            Vector2 currentGoal = AI_Brain.GetTargetTile(ghostType,
                transform.position,
                playerPos,
                playerDir,
                rozyPos,
                scatterDistanceSQ);

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