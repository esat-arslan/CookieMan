using System;
using UnityEngine;

public class Player : ResettableBehavior
{
    private int lives = 3;

    public event Action<int> OnLivesChanged;

    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            if (lives == 0)
            {
                GameEvents.GameOver();
            }
            OnLivesChanged?.Invoke(lives);
        }
    }

    public void HandlePlayerDead()
    {
        Lives--;
    }

    public override void Reset()
    {
        Lives = 3;
    }
}
