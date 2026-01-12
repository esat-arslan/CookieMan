using UnityEngine;

public interface IPortable
{
    public void Teleport(Vector3 portalPos, Vector2Int entryDir);
}