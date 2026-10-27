using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 3f; // 攻击范围
    public float attackInterval = 1f;// 攻击间隔

    public float damage = 20f;// 攻击力

    public GameObject projectilePrefab;// 子弹预制体

    private float timer = 0f;// 计时器

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < attackInterval)
        {
            return;
        }

        Enemy target = FindTarget();// 查找目标敌人
        if(target == null)
        {
            return;
        }

        timer=0f;

        Fire(target);// 发射子弹
    }

    Enemy FindTarget()
    {
        Collider2D[] hits=Physics2D.OverlapCircleAll(transform.position, range);
        Enemy nearest = null;// 最近的敌人
        float minDist = float.MaxValue;// 最小距离

        foreach (Collider2D hit in hits)
        {
            Enemy e = hit.GetComponent<Enemy>();
            if (e == null) continue;

            float d = Vector2.Distance(transform.position, e.transform.position);
            if (d < minDist)
            {
                minDist = d;
                nearest = e;
            }
        }
        return nearest;
    }

    void Fire(Enemy target)
    {
        GameObject go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile p = go.GetComponent<Projectile>();
        p.SetTarget(target, damage);
    }

    // 在 Scene 视图画出攻击范围，调试神器
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
