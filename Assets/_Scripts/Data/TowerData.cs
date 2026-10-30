using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "TowerDefence/TowerData")]
public class TowerData : ScriptableObject
{
    public string towerName = "µ¥ÌåËþ";
    public GameObject prefab;
    public float damage = 20f;
    public float range = 3f;
    public float attackInterval = 1f;
    public int cost = 20;
    public Color color = Color.blue;

}
