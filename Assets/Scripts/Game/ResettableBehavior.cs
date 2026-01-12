using System;
using UnityEngine;

public abstract class ResettableBehavior : MonoBehaviour
{
    private ResetManager resetManager;

    private void Awake()
    {
        resetManager = GameObject.FindWithTag("Level").GetComponent<ResetManager>();
        RegisterReset();
    }

    public abstract void Reset();
    public virtual void ResetLate(){}

    private void RegisterReset()
    {
        resetManager.RegisterObjectToReset(this);
    }
}