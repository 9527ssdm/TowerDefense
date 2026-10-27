using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;//敌人预制体
    public Transform[] waypoints; // 路点数组
    public Transform spawnPoint;//敌人出生点
    public int enemyCount = 5; // 敌人数量
    public float spawnInterval = 2f; // 敌人生成间隔

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, spawnInterval);//我也不知道这个函数的用法？？?
    }

    void Spawn()
    {
        if(enemyCount<=0)
        {
            return;
        }

        GameObject go=Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);//下面这几句代码是什么作用？？？
        Enemy enemy=go.GetComponent<Enemy>();
        enemy.waypoints=waypoints;
        enemyCount--;

        if(enemyCount<=0)
        {
            CancelInvoke(nameof(Spawn));//停止生成敌人  我不知道这个函数的用法???
        }
    }
}
