using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnCookieEaten;
    public static event Action OnSuperCookieEaten;
    public static event Action<int> OnPlayerDead;
    public static event Action OnGameOver;
    public static event Action OnGameWon;
    public static event Action OnGameStarted;
    public static event Action OnRestart;

    public static void CookieEaten() => OnCookieEaten?.Invoke();
    public static void SuperCookieEaten() => OnSuperCookieEaten?.Invoke();
    public static void PlayerDead(int lives) => OnPlayerDead?.Invoke(lives);
    public static void GameOver() => OnGameOver?.Invoke();
    public static void GameWon() => OnGameWon?.Invoke();
    public static void StartGame() => OnGameStarted?.Invoke();
    public static void RestartGame() => OnRestart?.Invoke();
}
