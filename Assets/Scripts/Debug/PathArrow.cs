using System;
using UnityEngine;

public class PathArrow : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Setup(Color color, Vector2 direction)
    {
        sprite.color = color;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
