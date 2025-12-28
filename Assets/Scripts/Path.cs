using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "CookieMan/Tiles/Path")]
public class Path : RuleTile<Path.Neighbor>
{
    public List<TileBase> wallTiles = new();
    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int Wall = 4;
    }

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        switch (neighbor)
        {
            case Neighbor.Wall: return wallTiles.Contains(other);
        }
        return base.RuleMatch(neighbor, other);
    }
}
