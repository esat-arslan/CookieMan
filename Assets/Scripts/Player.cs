using UnityEngine;

public class Player : MonoBehaviour
{
    private int lives = 3;

    private void OnEnable()
    {
        GameEvents.OnPlayerDead += HandlePlayerDead;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDead -= HandlePlayerDead;
    }

    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            Debug.Log("Live lost");
        }
    }

    private void HandlePlayerDead()
    {
        Lives--;
    }
}
