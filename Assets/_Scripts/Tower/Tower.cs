using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public TowerData data;
    // 计时器
    private float timer = 0f;
    public GameObject bulletPrefa;

    public AudioClip shootSound;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        // 更新计时器
        timer += Time.deltaTime;
        //如果计时器小于攻击间隔，说明还没到攻击时间，直接返回
        if (timer < data.attackInterval)
        {
            return;
        }
        // 查找目标敌人
        Enemy target = FindTarget();
        //敌人为空，说明没有敌人在攻击范围内，直接返回
        if (target == null)
        {
            return;
        }
        // 重置计时器
        timer = 0f;
        // 发射子弹
        Fire(target);
    }

    Enemy FindTarget()
    {
        // 获取攻击范围内的所有敌人
        Collider2D[] hits=Physics2D.OverlapCircleAll(transform.position, data.range);
        //最近的敌人
        Enemy nearest = null;
        // 最小距离
        float minDist = float.MaxValue;
        // 遍历所有敌人，找到最近的一个
        foreach (Collider2D hit in hits)
        {
            //判断是否是敌人
            Enemy e = hit.GetComponent<Enemy>();
            //如果不是敌人，继续下一个
            if (e == null)
            {
                continue;
            }
                //计算敌人和塔的距离
                float d = Vector2.Distance(transform.position, e.transform.position);
            //如果距离小于最小距离，更新最近的敌人和最小距离
            if (d < minDist)
            {
                minDist = d;
                nearest = e;
            }
        }
        // 返回最近的敌人
        return nearest;
    }

    void Fire(Enemy target)
    {

        if(audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        // 实例化子弹预制体
        GameObject go = Instantiate(bulletPrefa, transform.position, Quaternion.identity);
        // 获取子弹脚本，设置目标和伤害
        Projectile p = go.GetComponent<Projectile>();
        //设置目标和伤害
        p.SetTarget(target, data.damage);
    }

    // 在 Scene 视图画出攻击范围，调试神器
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.range);
    }

}
