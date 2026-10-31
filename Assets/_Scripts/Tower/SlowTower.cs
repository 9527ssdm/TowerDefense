using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : TowerBase
{
    protected override bool Attack()
    {
        Enemy target = FindNearestEnemy();
        if (target == null)
        {
            return false;
        }

        target.ApplySlow(data.slowMultiplier, data.slowDuration);
        target.TakeDamage(data.damage);
        return true;
    }
}
