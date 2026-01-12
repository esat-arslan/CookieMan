using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conf_Monster", menuName = "CookieMan/Configuration/Monster")]
public class Conf_Monster : ScriptableObject
{
    public Vector3 spawnPosition;
    public List<Vector3> chamberPoints;
    public Vector3 chaseDefaultPos;
    public Vector3 scatterPos;
    public Color monsterColor;
}
