using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "TowerDefence/TowerData")]
public class TowerData : ScriptableObject
{
    public float slowMultiplier = 0.5f;   // 减速到 50%
    public float slowDuration = 2f;       // 持续 2 秒
    public string towerName = "单体塔";
    public GameObject prefab;
    public float damage = 20f;
    public float range = 3f;
    public float attackInterval = 1f;
    public int cost = 20;
    public Color color = Color.blue;

}
