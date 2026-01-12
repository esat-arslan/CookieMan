using System;
using UnityEngine;

public class LevelManager : ResettableBehavior
{   
    [SerializeField]
    private Conf_Portals portalsConf;
    [SerializeField] private Conf_SuperCookies superCookiesConf;
    [SerializeField] private GameObject portalsPrefab;
    
    [SerializeField]
    private Monster_Level_State currentState = Monster_Level_State.ScatterDay;
    private Monster_Level_State lastState = Monster_Level_State.ScatterDay;
    
    private float scatterTimer = 10f;
    private float chaseTimer = 20f;
    private float frightenedTimer = 10f;

    public float FrightenedTimer => frightenedTimer;
    public Monster_Level_State CurrentState => currentState;
    public Conf_Portals PortalsConf => portalsConf;
    public Conf_SuperCookies SuperCookiesConf => superCookiesConf;

    private void OnEnable()
    {
        GameEvents.OnSuperCookieEaten += SetToFrightened;
    }

    private void OnDisable()
    {
        GameEvents.OnSuperCookieEaten -= SetToFrightened;
    }

    private void Start()
    {
        CreatePortals();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStateChange();
        
        if (currentState == Monster_Level_State.ScatterDay)
        {
            DecreaseScatterTimer();
        }

        if (currentState == Monster_Level_State.ChaseNight)
        {
            DecreaseChaseTimer();
        }

        if (currentState == Monster_Level_State.Frightened)
        {
            DecreaseFrightenedTimer();
        }
    }

    private void SetToFrightened()
    {
        currentState = Monster_Level_State.Frightened;
    }

    private void CheckStateChange()
    {
        if (currentState == Monster_Level_State.ScatterDay && scatterTimer <= 0)
        {
            currentState = Monster_Level_State.ChaseNight;
            lastState = Monster_Level_State.ChaseNight;
            ResetScatterTime();
            Debug.Log(currentState);
            return;
        }
        
        if (currentState == Monster_Level_State.ChaseNight && chaseTimer <= 0)
        {
            currentState = Monster_Level_State.ScatterDay;
            lastState = Monster_Level_State.ScatterDay;
            Debug.Log(currentState);
            ResetChaseTime();
            return;
        }

        if (currentState == Monster_Level_State.Frightened && frightenedTimer <= 0)
        {
            currentState = lastState;
            ResetFrightenedTime();
            return;
        }
    }

    private void ResetScatterTime()
    {
        scatterTimer = 10.0f;
    }
    
    private void ResetChaseTime()
    {
        chaseTimer = 20.0f;
    }
    
    private void ResetFrightenedTime()
    {
        frightenedTimer = 10.0f;
    }

    private void DecreaseScatterTimer()
    {
        scatterTimer -= Time.deltaTime;
    }
    
    private void DecreaseChaseTimer()
    {
        chaseTimer -= Time.deltaTime;
    }
    
    private void DecreaseFrightenedTimer()
    {
        frightenedTimer -= Time.deltaTime;
    }

    private void CreatePortals()
    {
        GameObject portal1GO = Instantiate(portalsPrefab, portalsConf.portal1, Quaternion.identity);
        portal1GO.GetComponent<Portal>().EntryDirOther = portalsConf.portal2_entryDir;
        portal1GO.transform.parent = this.transform;
        
        GameObject portal2GO = Instantiate(portalsPrefab, portalsConf.portal2, Quaternion.identity);
        portal2GO.GetComponent<Portal>().EntryDirOther = portalsConf.portal1_entryDir;
        portal2GO.transform.parent = this.transform;
    }

    public override void Reset()
    {
        currentState = Monster_Level_State.ScatterDay;
        lastState = Monster_Level_State.ScatterDay;
        
        ResetScatterTime();
        ResetChaseTime();
        ResetFrightenedTime();
    }
}

public enum Monster_Level_State { ScatterDay, ChaseNight, Frightened}
























