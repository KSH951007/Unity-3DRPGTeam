using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class TrashMob_ElfWatcher : TrashMob
{
    [SerializeField] private Transform attackPoint;

    protected override void Start()
    {
        maxHp = 700;
        damage = 13;
        transform.position = spawnedPoint.position;

        base.Start();
    }

    public void Attack()
    {
        GameObject projectile = PoolManager.Instance.Get("ElfArrow", attackPoint.position, attackPoint.rotation);
        if (projectile != null)
        {
            projectile.GetComponent<ElfArrow>().Init(attackPoint.position, damage);
        }
    }
}
