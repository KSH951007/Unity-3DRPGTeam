using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileForestAura : MonoBehaviour
{
    private int currentCount;
    private int maxCount;
    private float maxDurationTime;
    private float damagePercent;
    private int computeDamage;
    private float currentDurationTime;
    private ParticleSystem particle;
    private void Awake()
    {
        maxDurationTime = 10f;
        maxCount = 3;
        damagePercent = 2f;
        particle = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        currentCount = maxCount;
        ParticleSystem.Burst burst = particle.emission.GetBurst(0);
        burst.count = currentCount;
        particle.emission.SetBurst(0, burst);
        currentDurationTime = 0f;

    }
    private void OnDisable()
    {
        if (particle.isPlaying)
            particle.Pause();
    }
    public void Init(Transform parent, int damage)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.up;
        computeDamage = (int)(damage * damagePercent);
        particle.Play();
    }
    private void Update()
    {
        currentDurationTime += Time.deltaTime;
        if (currentDurationTime > maxDurationTime|| currentCount <= 0)
        {
            Stop();
            return;
        }
    }
    public void OnParticleCollision(GameObject other)
    {
      
        if (other.gameObject.TryGetComponent(out IHitable enemy))
        {
            enemy.TakeHit(computeDamage, HitType.None);
            currentCount--;
            ParticleSystem.Burst burst = particle.emission.GetBurst(0);
            burst.count = currentCount;
            particle.emission.SetBurst(0, burst);
            GameObject hitEffect = PoolManager.Instance.Get("AarenAttackHitEffect", other.gameObject.transform.position);
        }
    }
    public void Stop()
    {
        PoolManager.Instance.ReturnPool(gameObject);
        return;
    }
}
