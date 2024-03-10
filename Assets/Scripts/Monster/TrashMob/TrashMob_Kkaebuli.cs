using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMob_Kkaebuli : TrashMob
{
    protected override void Start()
    {
        maxHp = 500;
		damage = 5;
		transform.position = spawnedPoint.position;

        base.Start();
    }

    protected override IEnumerator CHASE()
    {
        SoundManager.instance.PlaySound("KkaebuliStep");
        yield return StartCoroutine(base.CHASE());
        if (state != State.CHASE)
        {
            SoundManager.instance.StopSound("KkaebuliStep");
        }
    }

    protected override IEnumerator ATTACK()
    {
        SoundManager.instance.PlaySound("KkaebuliAttackVoice");
        yield return StartCoroutine(base.ATTACK());
    }

    protected override IEnumerator KILLED()
    {
        SoundManager.instance.PlaySound("KkaebuliDie");
        yield return StartCoroutine(base.KILLED());
    }

    public override void TakeHit(int damage, IHitable.HitType hitType, GameObject hitParticle = null)
    {
        base.TakeHit(damage, hitType, hitParticle);
        if (!invincible)
        {
            SoundManager.instance.PlaySound("KkaebuliDamaged");
        }
    }

    protected override void AnimHit()
    {
        SoundManager.instance.PlaySound("KkaebuliAttack");
        base.AnimHit();
    }
}
