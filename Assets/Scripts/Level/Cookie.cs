using System;
using UnityEngine;

public class Cookie : ResettableBehavior
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !GetComponent<Cookie>().enabled) return;

        if (gameObject.CompareTag("SuperCookie"))
        {
            GameEvents.SuperCookieEaten();
        }
        else
        {
            GameEvents.CookieEaten();
        }
        
        Destroy(gameObject);

        if (AllCookiesEaten(transform.parent))
        {
            GameEvents.GameWon();
        }
    }

    private bool AllCookiesEaten(Transform cookieParentTransform)
    {
        return cookieParentTransform.childCount == 1;
    }


    public override void Reset()
    {
        Destroy(gameObject);
    }
}
