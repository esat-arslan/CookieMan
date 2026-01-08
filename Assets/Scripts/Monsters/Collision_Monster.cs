using System;
using UnityEngine;

public class Collision_Monster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GetComponent<Collision_Monster>().enabled)
        {
            transform.parent.GetComponent<Monster_Controller>().IsEaten = true;
        }
    }
}
