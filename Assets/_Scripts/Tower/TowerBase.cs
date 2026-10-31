using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    public TowerData data;
    protected float timer;

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if(timer<data.attackInterval)
        {
            return;
        }

        if(Attack())
        {
            timer = 0;
        }
    }

    //子类必须实现攻击方式
    protected abstract bool Attack();

    // 公共方法：找范围内最近的敌人
    protected Enemy FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, data.range);
        Enemy nearest = null;
        float minDist = float.MaxValue;

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

    void OnDrawGizmosSelected()
    {
        if (data == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.range);
    }
}
