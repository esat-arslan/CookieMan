using UnityEngine;

public class Cookie : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !GetComponent<Cookie>().enabled) return;

        if(gameObject.CompareTag("SuperCookie"))
        {
            GameEvents.SuperCookieEaten();
        }
        else
        {
            GameEvents.CookieEaten();
        }

        Destroy(gameObject);
    }
}
