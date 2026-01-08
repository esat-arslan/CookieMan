using UnityEngine;

public class Freezy_Controller : Monster_Controller
{
    public override void SetChaseTarget()
    {
        base.SetChaseTarget();
        Vector3 playerPos = Player.position;
        
        if ( Vector2.Distance(transform.position, playerPos) < 6.0f)
        {
            FinalTarget = Configuration.chaseDefaultPos;    
        }
        else
        {
            FinalTarget = playerPos;
        }
    }
}
