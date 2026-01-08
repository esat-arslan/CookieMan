public class Rozy_Controller : Monster_Controller
{
    public override void SetChaseTarget()
    {
        base.SetChaseTarget();

        FinalTarget = Player.position;
    }
}