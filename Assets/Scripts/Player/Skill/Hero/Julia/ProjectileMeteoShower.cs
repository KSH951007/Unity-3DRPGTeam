using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMeteoShower : MonoBehaviour
{
    private ParticleSystem particle;
    private float damagePercent;
    private int computeDamage;
    private HitTrigger hitTrigger;
    private int damageCount;
    private int currentDamageCount;
    private float damageInterval;
    private float currentInterval;
    private float durationTime;

    private void Awake()
    {
        damagePercent = 1.2f;
        particle = GetComponentInChildren<ParticleSystem>();
        hitTrigger = GetComponentInChildren<HitTrigger>();
        hitTrigger.onTrigger += TargetDamage;
        damageCount = 8;


    }
    public void Init(Vector3 position, Quaternion diretion, int damage, Animator animator)
    {
        transform.position = position;
        transform.rotation = diretion;
        computeDamage = (int)(damage * damagePercent);
        currentDamageCount = 0;
        currentInterval = 0;
        float length = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        durationTime = length - (length * animator.GetCurrentAnimatorStateInfo(0).normalizedTime) - (length * 0.1f);
        damageInterval = durationTime / damageCount;
        var main = particle.main;
        main.duration = durationTime;
        ParticleSystem.Burst burst = particle.emission.GetBurst(0);
        burst.cycleCount = damageCount;
        burst.repeatInterval = damageInterval;
        particle.emission.SetBurst(0, burst);

        particle.Play();
        SoundManager.instance.PlaySound("HeroJuliaSkill1");
    }
    private void OnDisable()
    {
        currentDamageCount = 0;
        currentInterval = 0;
    }
    private void Update()
    {
        if (particle.time >= durationTime)
        {
            if (particle.isPlaying)
            {
                particle.Stop();

            }
            PoolManager.Instance.ReturnPool(gameObject);
            return;
        }
        currentInterval += Time.deltaTime;

    }
    private void TargetDamage(Collider target)
    {
        if (currentInterval >= damageInterval)
        {
            if (target.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("3"+target.name);
                if (target.gameObject.TryGetComponent(out IHitable enemy))
                {
                    Debug.Log(LayerMask.LayerToName(target.gameObject.layer));
                    enemy.TakeHit(computeDamage);
                    PoolManager.Instance.Get("RandomMeteoShowerHitEffect", target.transform.position);
                }
                currentInterval = 0f;
            }
        }

    }

}