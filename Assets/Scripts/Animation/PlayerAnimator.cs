using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement player;

    private void OnEnable()
    {
        player.OnDirectionChanged += HandleDirectionState;
    }

    private void OnDisable()
    {
        player.OnDirectionChanged -= HandleDirectionState;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }

    private void HandleDirectionState(Vector2 dir)
    {
        animator.SetInteger("MoveX", (int)dir.x);
        animator.SetInteger("MoveY", (int)dir.y);
    }
}