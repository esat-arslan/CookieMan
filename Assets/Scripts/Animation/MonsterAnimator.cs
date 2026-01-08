using System;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private Animator animator;
    private Monster_Controller monster;

    private void OnEnable()
    {
        monster.OnDirectionChanged += HandleDirectionState;
    }

    private void OnDisable()
    {
        monster.OnDirectionChanged -= HandleDirectionState;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        monster = GetComponent<Monster_Controller>();
    }
    
    private void HandleDirectionState(Vector2 dir)
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
    
    public void SetFrightened(bool isFrightened)
    {
        animator.SetBool("IsFrightened", isFrightened);
    }

    public void EnterFrightened()
    {
        SetFrightened(true);
        animator.SetTrigger("FrightenedEnter");
    }
    
    public void ExitFrightened()
    {
        SetFrightened(false);
    }

    public void EnterFrightenedTimeout()
    {
        animator.SetTrigger("FrightenedTimeout");
    }
}