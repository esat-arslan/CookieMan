using UnityEngine;

public class Collision_Player : MonoBehaviour
{
    private LevelManager level;

    private void Start()
    {
        level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ShouldConsiderCollision(other) && GetComponent<Collision_Player>().enabled)
        {
            GameEvents.PlyerDead();
        }
    }

    private bool ShouldConsiderCollision(Collider2D other)
    {
        if (!other.CompareTag("Monster")) return false;

        bool isEaten = other.transform.parent.GetComponent<Monster_Controller>().IsEaten;
        bool isFrightened = level.CurrentState == Monster_Level_State.Frightened;

        return !isEaten && !isFrightened;
    }
}
