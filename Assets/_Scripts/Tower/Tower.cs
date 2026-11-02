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
            .GetComponent<Projectile>().SetTarget(target, currentDamage);

        return true;
    }

    void Fire(Enemy target)
    {
        GameObject go;
        if (ProjectilePool.Instance != null)
        {
            go = ProjectilePool.Instance.Get();
        }
        else
        {
            go = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }

        go.transform.position = transform.position;   // 从池里取出的子弹要重设位置
        go.GetComponent<Projectile>().SetTarget(target, currentDamage);
    }
}
