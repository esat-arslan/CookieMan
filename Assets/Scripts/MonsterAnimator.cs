using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleDirectionState(Vector2 dir)
    {
        animator.SetInteger("MoveX", (int)dir.x);
        animator.SetInteger("MoveY", (int)dir.y);
    }

    public void SetDefault(bool isDefault)
    {
        animator.SetBool("IsDefault", isDefault);
    }

    public void SetEaten(bool isEaten)
    {
        animator.SetBool("IsEaten", isEaten);
    }

    public void SetFrighten(bool isFrighten)
    {
        animator.SetBool("IsFrighten", isFrighten);
    }
}
