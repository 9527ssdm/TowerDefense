using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;   // 把 5 个路点拖进来
    public float speed = 2f;
    public float arriveThreshold = 0.1f; // 到达判定距离

    public int damageToPlayer = 10; // 敌人造成的伤害值
    public float enemyMaxHealth = 100.0f; // 敌人的血量
    private float enemyCurrentHealth; // 敌人的当前血量
    private bool hasReachedEnd = false; // 防止重复扣血

    public int rewardMoney = 10;
    private bool isDead = false;

    private int index = 0;

    private void Start()
    {
        enemyCurrentHealth = enemyMaxHealth; // 初始化敌人的血量
    }
    void Update()
    {
        if (hasReachedEnd)
        { 
            return;
        }

        // 1. 所有路点都走完了，停住
        if (index >= waypoints.Length)
        {
            ReachEnd();
            return;
        }

        // 2. 当前要去的路点
        Vector3 target = waypoints[index].position;

        // 3. 朝目标移动：速度 × 帧间隔，保证帧率无关
        transform.position = Vector3.MoveTowards(transform.position,target,speed * Time.deltaTime);

        // 4. 到了就换下一个
        if (Vector3.Distance(transform.position, target) < arriveThreshold)
        {
            index++;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;          // 防止多个子弹同时打死时重复加钱
        enemyCurrentHealth -= damage;
        Debug.Log("敌人血量: " + enemyCurrentHealth);
        if (enemyCurrentHealth <= 0)
        {
            isDead = true;
            GameManager.Instance.AddMoney(rewardMoney);
            Destroy(gameObject);
        }
    }

    void ReachEnd()
    {
        if(hasReachedEnd)
        {
            return;
        }

        hasReachedEnd = true; // 标记已经到达终点，防止重复扣血

        GameManager.Instance.PlayerTakeDamage(damageToPlayer);

        Destroy(gameObject);

    }
}
