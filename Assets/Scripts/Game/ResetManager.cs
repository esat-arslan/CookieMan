using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    private List<ResettableBehavior> objectsToReset = new();

    public void RegisterObjectsToReset(ResettableBehavior resettable)
    {
        objectsToReset.Add(resettable);
    }
}
