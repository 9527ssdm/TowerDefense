using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : TowerBase
{
    public GameObject bulletPrefab;

    protected override bool Attack()
    {
        Enemy target = FindNearestEnemy();
        if(target == null )
        {
            return false;
        }

        Instantiate(bulletPrefab, transform.position, Quaternion.identity)
            .GetComponent<Projectile>().SetTarget(target, data.damage);

        return true;
    }
}
