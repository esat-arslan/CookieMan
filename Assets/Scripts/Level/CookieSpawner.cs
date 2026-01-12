using UnityEngine;

public class CookieSpawner : ResettableBehavior
{
    [SerializeField] private GameObject cookiePrefab;
    [SerializeField] private GameObject superCookiePrefab;

    private Conf_Portals portalsConfig;
    private Conf_SuperCookies superCookieConfig;
    private GridManager grid;
    
    void Start()
    {   
        grid = GridManager.Instance;
        LevelManager level = GameObject.FindWithTag("Level").GetComponent<LevelManager>();
        portalsConfig = level.PortalsConf;
        superCookieConfig = level.SuperCookiesConf;
        
        SpawnCookies();
    }

    private void SpawnCookies()
    {
        foreach (GridObject gridObj in grid.GetGridObjects())
        {
            Vector3 cellPos = grid.GetWorldPosition(gridObj.GetCellPosition());

            if (superCookieConfig.superCookiePositions.Contains(cellPos))
            {
                GameObject obj = Instantiate(superCookiePrefab, cellPos, Quaternion.identity);
                obj.transform.parent = gameObject.transform;
                continue;
            }
            
            if (gridObj.Type == GridObjectType.Path && !IsPortalPosition(cellPos))
            {
                GameObject obj = Instantiate(cookiePrefab, cellPos, Quaternion.identity);
                obj.transform.parent = gameObject.transform;
            }
        }
    }

    private bool IsPortalPosition(Vector3 pos)
    {
        return pos == portalsConfig.portal1 || pos == portalsConfig.portal2;
    }
    
    public override void Reset()
    {
                  
    }

    public override void ResetLate()
    {
        base.ResetLate();
        SpawnCookies();  
    }
}
















