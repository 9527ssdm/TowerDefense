using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 子弹移动速度
    public float speed = 10f;
    // 子弹的目标敌人
    private Enemy target;
    // 子弹造成的伤害
    private float damage;

    public void SetTarget(Enemy enemy, float dmg)
    {
        // 设置子弹的目标敌人和伤害值
        target = enemy;
        damage = dmg;
    }

    void Update()
    {
        // 目标死了/到终点了，子弹消失
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        // 子弹朝目标移动
        transform.position = Vector3.MoveTowards(transform.position,target.transform.position,speed * Time.deltaTime);
        // 如果子弹和目标距离小于0.15，说明命中，造成伤害并销毁子弹
        if (Vector3.Distance(transform.position, target.transform.position) < 0.15f)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
