using UnityEngine;

[CreateAssetMenu(fileName = "Conf_Portals", menuName = "CookieMan/Configuration/Conf_Portals")]
public class Conf_Portals : ScriptableObject
{
    public Vector3 portal1;
    public Vector2Int portal1_entryDir;
    public Vector3 portal2;
    public Vector2Int portal2_entryDir;
}
