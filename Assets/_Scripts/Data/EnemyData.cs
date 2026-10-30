using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData",menuName ="TowerDefence/EnemyData")]
public class EnemyData :ScriptableObject
{
    public string enemyName = "∆’Õ®π÷ŒÔ";
    public float maxHealth = 100f;
    public float speed = 2f;
    public int rewardMoney=10;
    public int damageToPlayer = 10;
    public Color color = Color.red;
}
