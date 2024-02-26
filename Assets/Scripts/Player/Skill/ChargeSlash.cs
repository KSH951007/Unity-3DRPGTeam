using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSlash : Skill, IDamagable
{
    private Vector3 center;
    private float maxDistance;
    private Vector3 direction;
    private int damagePercent;
    private Transform onwerTr;
    public ChargeSlash(Transform ownerTr)
    {
        this.onwerTr = ownerTr;
    }

    public override void EndSkill()
    {

    }

    public override void StartSkill()
    {
        direction = onwerTr.transform.forward;
        center = onwerTr.transform.position + onwerTr.transform.forward;
        maxDistance = 1f;
        damagePercent = 1231;

    }

    public void TakeDamage()
    {
        RaycastHit[] hits = Physics.BoxCastAll(center, Vector3.one, direction, Quaternion.identity, maxDistance, LayerMask.GetMask("Enemy"));
        if (hits != null)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out IHitable enemy))
                {
                    enemy.TakeHit(damagePercent, HitType.None);
                }
            }
        }
    }

    public override void UpdateSkill()
    {

    }


}
