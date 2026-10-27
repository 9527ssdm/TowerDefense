using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //敌人预制体
    public GameObject enemyPrefab;
    // 路点数组
    public Transform[] waypoints;
    //敌人出生点
    public Transform spawnPoint;
    // 敌人数量
    public int enemyCount = 5;
    // 敌人生成间隔
    public float spawnInterval = 2f;

    private void Start()
    {
        //这个函数的作用是每隔spawnInterval秒调用一次Spawn函数，第一次调用在1秒后
        //游戏开始后，1秒后开始生成敌人，每隔spawnInterval秒生成一个敌人，直到enemyCount为0
        InvokeRepeating(nameof(Spawn), 1f, spawnInterval);
    }

    void Spawn()
    {
        //如果敌人数量小于等于0，直接返回，不再生成敌人
        if (enemyCount<=0)
        {
            return;
        }
        //生成敌人
        GameObject go =Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        //给生成的敌人设置路点数组
        Enemy enemy =go.GetComponent<Enemy>();
        enemy.waypoints=waypoints;
        //每次生成敌人后，敌人数量减1
        enemyCount--;
        //如果敌人数量小于等于0，停止生成敌人
        if (enemyCount<=0)
        {
            CancelInvoke(nameof(Spawn));
        }
    }
}
