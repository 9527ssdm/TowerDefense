using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveManager;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [System.Serializable]
    public class EnemySpawnEntry
    {
        public EnemyData enemyData;   // 哪种敌人
        public int count = 1;         // 来几个
    }

    [System.Serializable]
    public class Wave
    {
        public EnemySpawnEntry[] entries; // 这一波由哪些敌人组成
        public float spawnInterval=1.0f; // 敌人生成间隔
        public float nextWaveDelay = 3f; // 打完这波，等几秒下一波
    }

    public Wave[] waves; // 所有波次
    public Transform[] wayPoints;//路径点
    public Transform spawnPoint;//敌人生成点
    public int TotalWaves => waves.Length;// 总波数

    private int currentWave = 0;// 当前波次索引，从 0 开始
    public int CurrentWave => currentWave + 1;   // 从 1 开始

    void Awake() 
    { 
        Instance = this; 
    }
    private void Start()
    {
        // 开始波次生成协程
        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        for (int w = 0; w < waves.Length; w++)
        {
            currentWave = w;
            Debug.Log($"===== 第 {w + 1} 波开始 =====");
            // 生成这一波的所有敌人
            foreach (EnemySpawnEntry entry in waves[w].entries)
            {
                for (int i = 0; i < entry.count; i++)
                {
                    SpawnEnemy(entry.enemyData);
                    yield return new WaitForSeconds(waves[w].spawnInterval);
                }
            }

            // 等待这一波的敌人被消灭
            while (FindObjectsOfType<Enemy>().Length > 0)
            {
                yield return null;   // 每帧检查一次
            }
            Debug.Log($"===== 第 {w + 1} 波清空 =====");

            // 最后一波不用再等
            if (w < waves.Length - 1)
            {
                Debug.Log($"{waves[w].nextWaveDelay} 秒后下一波");
                // 等待下一波的时间
                yield return new WaitForSeconds(waves[w].nextWaveDelay);
            }
        }
        Debug.Log("===== 全部波次完成，胜利！ =====");
        GameManager.Instance.Victory();
    }

    void SpawnEnemy(EnemyData enemyData)
    {
        // 如果游戏结束了，就不再生成敌人
        if (GameManager.Instance.isGameOver)
        {
            return;
        }
        //创建敌人实例，并设置其路径点
        GameObject go = Instantiate(enemyData.prefab, spawnPoint.position, Quaternion.identity);
        // 获取敌人脚本组件，并设置路径点
        Enemy e = go.GetComponent<Enemy>();

        e.data= enemyData;
        //设置敌人的路径点
        e.waypoints = wayPoints;
    }
}
