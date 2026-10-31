using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeTower : TowerBase
{
    protected override bool Attack()
    {
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, data.range);
        bool hitAny = false;

        foreach (Collider2D hit in hits)
        {
            Enemy e = hit.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(data.damage);
                hitAny = true;
            }
        }

        return hitAny;
    }
}
