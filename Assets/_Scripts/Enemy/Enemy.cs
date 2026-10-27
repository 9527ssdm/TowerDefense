using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 把路点拖进来
    public Transform[] waypoints;
    //敌人移动速度
    public float speed = 2f;
    // 敌人与路点的距离小于这个值就算到达路点
    public float arriveThreshold = 0.1f;
    // 敌人造成的伤害值
    public int damageToPlayer = 10;
    // 敌人的血量
    public float enemyMaxHealth = 100.0f;
    // 敌人的当前血量
    private float enemyCurrentHealth;
    // 敌人是否到达终点
    private bool hasReachedEnd = false; 
    // 敌人被击杀后奖励的金钱
    public int rewardMoney = 10;
    //敌人死亡标记
    private bool isDead = false;
    // 当前要去的路点索引
    private int index = 0;

    private void Start()
    {
        // 初始化敌人的血量
        enemyCurrentHealth = enemyMaxHealth; 
    }
    void Update()
    {
        if (hasReachedEnd)
        { 
            return;
        }

        // 所有路点都走完了，停住
        if (index >= waypoints.Length)
        {
            ReachEnd();
            return;
        }

        // 当前要去的路点
        Vector3 target = waypoints[index].position;

        // 朝目标路点移动：速度 × 帧间隔，保证帧率无关
        transform.position = Vector3.MoveTowards(transform.position,target,speed * Time.deltaTime);

        // 到了一个路点就换下一个
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
        // 标记已经到达终点，防止重复扣血
        hasReachedEnd = true; 
        //如果到达终点对玩家造成伤害
        GameManager.Instance.PlayerTakeDamage(damageToPlayer);

        Destroy(gameObject);

    }
}
