using UnityEngine;

public static class AI_Brain
{
    /*public static Vector2 GetTargetTile(GhostType type, Vector2 ghostPos, Vector2 playerPos, Vector2 playerDir, Vector2 rozyPos, float scatterDistSq)
    {
        switch (type)
        {
            case GhostType.Rozy: // Blinky: Direct chase
                return playerPos;

            case GhostType.Archie: // Pinky: 4 tiles ahead
                return playerPos + (playerDir * 4);

            case GhostType.Pochy: // Clyde: Chases until close, then retreats
                float distSq = Vector2.SqrMagnitude(playerPos - ghostPos);
                return (distSq < scatterDistSq) ? Vector2.zero : playerPos;

            case GhostType.Freezy: // Inky: Complex pivot logic
                Vector2 pivot = playerPos + (playerDir * 2);
                return (pivot * 2) - rozyPos;

            default:
                return playerPos;
        }
    }

    public static Vector2 ChooseNextDirection(GridManager grid, Vector2 currentPos, Vector2 currentDir, Vector2 targetTile)
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector2 bestDir = currentDir;
        float minDelta = float.MaxValue;

        foreach (Vector2 dir in directions)
        {
            // 1. Prevent 180-degree turns
            if (dir == -currentDir) continue;

            // 2. Check if walkable
            if (!grid.IsNeighborCellWalkable(currentPos, dir)) continue;

            // 3. Measure distance to target from that potential neighbor
            Vector2 neighborPos = grid.GetNeighborCellPosition(currentPos, dir);
            float distSq = Vector2.SqrMagnitude(neighborPos - targetTile);

            if (distSq < minDelta)
            {
                minDelta = distSq;
                bestDir = dir;
            }
        }
        return bestDir;
    }*/
}