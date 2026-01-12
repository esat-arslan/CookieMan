using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    private float resetDelay = 3f;
    private List<ResettableBehavior> objectsToReset = new();

    private void OnEnable()
    {
        GameEvents.OnGameStarted += InitializeGameStart;
        GameEvents.OnGameWon += StopAll;
        GameEvents.OnGameOver += StopAll;
        GameEvents.OnPlayerDead += InitializePlayerDeadReset;
        GameEvents.OnRestart += InitializeLevelReset;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStarted -= InitializeGameStart;
        GameEvents.OnGameWon -= StopAll;
        GameEvents.OnGameOver -= StopAll;
        GameEvents.OnPlayerDead -= InitializePlayerDeadReset;
        GameEvents.OnRestart -= InitializeLevelReset;
    }

    public void RegisterObjectToReset(ResettableBehavior resettable)
    {
        objectsToReset.Add(resettable);
    }

    private void InitializeLevelReset()
    {
        StartCoroutine(WaitForLevelReset());
    }

    private void InitializePlayerDeadReset(int lives)
    {
        StopAll();
        if (lives > 0) StartCoroutine(WaitForPlayerDeadReset());
    }
    

    private void InitializeGameStart()
    {
        StartCoroutine(WaitForGameStart());
    }

    private IEnumerator WaitForLevelReset()
    {
        yield return new WaitForSeconds(resetDelay);
        ResetLevel();    
    }
    
    private IEnumerator WaitForPlayerDeadReset()
    {
        yield return new WaitForSeconds(resetDelay);
        ResetOnPlayerDead();
    }
    
    private IEnumerator WaitForGameStart()
    {
        yield return new WaitForSeconds(resetDelay);
        StartAll();
    }

    private void ResetOnPlayerDead()
    {
        foreach (ResettableBehavior resettable in objectsToReset)
        {
            if (resettable == null) continue;

            if (resettable.GetType() == typeof(PlayerMovement))
            {
                resettable.Reset();
            }
            
            resettable.enabled = true;
        }
    }
    
    private void ResetLevel()
    {
        foreach (ResettableBehavior resettable in objectsToReset.ToList())
        {
            if (resettable == null) continue;
            
            resettable.Reset();
            resettable.ResetLate();
        }
        
        foreach (ResettableBehavior resettable in objectsToReset)
        {
            if (resettable == null) continue;

            resettable.enabled = true;
        }
    }

    private void StartAll()
    {
        foreach (ResettableBehavior startable in objectsToReset)
        {
            if (startable == null) continue;

            startable.enabled = true;
        }
    }
    
    private void StopAll()
    {
        foreach (ResettableBehavior stoppable in objectsToReset)
        {
            if (stoppable == null) continue;

            stoppable.enabled = false;
        }
    }

    
    
}