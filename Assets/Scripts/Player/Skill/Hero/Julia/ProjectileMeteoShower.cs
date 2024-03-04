using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMeteoShower : MonoBehaviour
{
    private ParticleSystem particle;
    private float damagePercent;
    private int computeDamage;
    private HitTrigger hitTrigger;

    private void Awake()
    {
        damagePercent = 1.2f;
        particle = GetComponentInChildren<ParticleSystem>();
        hitTrigger = GetComponentInChildren<HitTrigger>();
        hitTrigger.onTrigger += TargetDamage;

    }
    public void Init(Vector3 position, Quaternion diretion, int damage)
    {
        transform.position = position;
        transform.rotation = diretion;
        computeDamage = (int)(damage * damagePercent);
        particle.Play();
    }
    private void Update()
    {
        if (particle.time >= particle.main.startLifetime.constant)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
            }
            PoolManager.Instance.ReturnPool(gameObject);
            return;
        }


    }
    private void TargetDamage(Collider target)
    {
        if (target.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (target.gameObject.TryGetComponent(out IHitable enemy))
            {
                enemy.TakeHit(computeDamage, HitType.None);
                PoolManager.Instance.Get("RandomMeteoShowerHitEffect", target.transform.position);
            }

        }
    }

}