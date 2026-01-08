using UnityEngine;

public class Portal : MonoBehaviour
{
    public Vector2Int EntryDirOther { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.parent.GetComponent<IPortable>().Teleport(transform.position, EntryDirOther);
    }
}
