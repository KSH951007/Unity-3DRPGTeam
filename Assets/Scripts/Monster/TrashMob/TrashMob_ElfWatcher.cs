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

    protected override IEnumerator CHASE()
    {
        SoundManager.instance.PlaySound("ElfStep");
        yield return StartCoroutine(base.CHASE());
        if (state != State.CHASE)
        {
            SoundManager.instance.StopSound("ElfStep");
        }
    }

    protected override IEnumerator ATTACK()
    {
        SoundManager.instance.PlaySound("ElfAttack");

        yield return StartCoroutine(base.ATTACK());
    }

    protected override IEnumerator KILLED()
    {
        SoundManager.instance.PlaySound("ElfDie");
        yield return StartCoroutine(base.KILLED());
    }

    public override void TakeHit(int damage, IHitable.HitType hitType, GameObject hitParticle = null)
    {
        base.TakeHit(damage, hitType, hitParticle);
        if (!invincible)
        {
            SoundManager.instance.PlaySound("ElfDamaged");
        }
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
