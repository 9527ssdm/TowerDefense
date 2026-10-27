using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    private Enemy target;
    private float damage;

    public void SetTarget(Enemy enemy, float dmg)
    {
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

        transform.position = Vector3.MoveTowards(transform.position,target.transform.position,speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.15f)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
