using System;
using UnityEngine;

public class Game : ResettableBehavior
{
    private int score = 0;

    public event Action<int> OnScoreUpdated;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            OnScoreUpdated?.Invoke(score);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnCookieEaten += HandleCookieEaten;
    }

    private void OnDisable()
    {
        GameEvents.OnCookieEaten -= HandleCookieEaten;
    }

    private void HandleCookieEaten()
    {
        Score += 100;
    }

    public override void Reset()
    {
        Score = 0;
    }
}
