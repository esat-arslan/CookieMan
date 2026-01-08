using UnityEngine;

public class CookieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cookiePrefab;
    [SerializeField] private GameObject superCookiePrefab;


    private Conf_Portals portals;
    private Conf_SuperCookies conf_SuperCookies;
    private GridManager grid;
    private LevelManager level;

    void Start()
    {
        grid = GridManager.Instance;
        level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
        portals = level.PortalsConf;
        conf_SuperCookies = level.SuperCookieConf;
        SpawnCookies();
    }

    private void SpawnCookies()
    {
        foreach (GridObject gridObject in grid.GetGridObjects())
        {
            Vector3 cellPos = grid.GetWorldPosition(gridObject.GetCellPosition());

            if (conf_SuperCookies.superCookiePositions.Contains(cellPos))
            {
                GameObject obj = Instantiate(superCookiePrefab, cellPos, Quaternion.identity);
                obj.transform.parent = gameObject.transform;
                continue;
            }

            if (gridObject.Type == GridObjectType.Path && !isPortal(cellPos))
            {
                GameObject obj = Instantiate(cookiePrefab, cellPos, Quaternion.identity);
                obj.transform.parent = gameObject.transform;
            }
        }
    }

    private bool isPortal(Vector3 cellPos)
    {
        return cellPos == portals.portal1 || cellPos == portals.portal2;
    }
}
