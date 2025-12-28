using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "CookieMan/Tiles/Wall")]
public class Wall : RuleTile<Wall.Neighbor>
{
   
    public List<TileBase> walkableTiles = new();
    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int Path = 3;
    }

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        switch (neighbor)
        {
            case Neighbor.Path: return walkableTiles.Contains(other);
        }
        return base.RuleMatch(neighbor, other);
    }
}
