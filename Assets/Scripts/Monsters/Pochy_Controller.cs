using UnityEngine;

public class Pochy_Controller : Monster_Controller
{
    public override void SetChaseTarget()
    {
        base.SetChaseTarget();
        Vector3 playerPos = Player.position;
        Vector2 playerDir = Player.GetComponent<PlayerMovement>().PlayerDirection;

        Vector3 offset = new Vector3(playerDir.x, playerDir.y, 0) * 4f;
        FinalTarget = playerPos + offset;
    }
}
