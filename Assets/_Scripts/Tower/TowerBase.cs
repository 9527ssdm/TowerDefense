using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    public int level = 1;
    public int maxLevel = 3;
    public int UpgradeCost => data.cost * level;   // 升级费用随等级涨

    public TowerData data;
    protected float currentDamage;   // 实例级数值，升级时改这个
    protected float currentRange;
    protected float timer;


    protected virtual void Start()
    {
        currentDamage = data.damage;
        currentRange = data.range;
    }
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

    public void TryUpgrade()
    {
        if (level >= maxLevel)
        {
            Debug.Log("已满级");
            return;
        }

        if (!GameManager.Instance.SendMoney(UpgradeCost))
        {
            Debug.Log("金币不足，升级需要 " + UpgradeCost);
            return;
        }

        level++;
        currentDamage *= 1.3f;
        currentRange *= 1.1f;
        transform.localScale *= 1.08f;   // 视觉上变大一点
        Debug.Log($"升级到 {level} 级，伤害 {currentDamage:F0}，射程 {currentRange:F1}");
    }

    void OnMouseDown()
    {
        TryUpgrade();
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
