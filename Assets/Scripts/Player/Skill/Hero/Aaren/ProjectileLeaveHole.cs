using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLeaveHole : MonoBehaviour
{

    private float bombTime;
    private float attractionPower;
    private int attractionCount;
    private ParticleSystem particle;
    private Collider[] hitCollider;
    private SphereCollider rangeCollider;
    private float attractionDamagePercent;
    private int attractionDamage;
    private float bombDamagePercent;
    private int bombDamage;
    private float attractionInterval;
    private float currentTime;
    private bool isHit;
    private float maxEffectTime;
    private void Awake()
    {
        rangeCollider = GetComponent<SphereCollider>();
        particle = GetComponent<ParticleSystem>();
        hitCollider = new Collider[50];
        bombTime = 1.4f;
        attractionCount = 5;
        attractionInterval = bombTime / attractionCount;
        attractionPower = 3f;
        attractionDamagePercent = 0.2f;
        bombDamagePercent = 5f;
        maxEffectTime = 2f;
    }
    private void OnEnable()
    {
        currentTime = 0f;
        isHit = false;
    }
    public void Init(Vector3 position, int damage)
    {
        transform.position = position;
        attractionDamage = (int)(damage * attractionDamagePercent);
        bombDamage = (int)(damage * bombDamagePercent);
        particle.Play();
        SoundManager.instance.PlaySound("HeroAarenSkill2");
    }
    public void Update()
    {

        if (particle.time < bombTime)
        {

            if (Physics.OverlapSphereNonAlloc(transform.position, rangeCollider.radius, hitCollider, LayerMask.GetMask("Enemy")) > 0)
            {
                for (int i = 0; i < hitCollider.Length; i++)
                {
                    if (hitCollider[i] != null)
                    {

                        if (hitCollider[i].TryGetComponent(out IHitable enemy))
                        {
                            if (currentTime >= attractionInterval)
                            {
                                enemy.TakeHit(attractionDamage);
                                GameObject hitEffect = PoolManager.Instance.Get("AarenAttackHitParticle", hitCollider[i].gameObject.transform.position);
                                currentTime = 0f;
                            }
                            Vector3 direciton = (transform.position - hitCollider[i].transform.position);
                            if (direciton.magnitude >= direciton.normalized.magnitude)
                                hitCollider[i].transform.position += direciton.normalized * attractionPower * Time.deltaTime;
                        }
                    }
                }
            }
     

        }
        else
        {
            if (!isHit)
            {
                if (Physics.OverlapSphereNonAlloc(transform.position, rangeCollider.radius, hitCollider, LayerMask.GetMask("Enemy")) > 0)
                {
                    for (int i = 0; i < hitCollider.Length; i++)
                    {
                        if (hitCollider[i] != null)
                        {
                            if (hitCollider[i].TryGetComponent(out IHitable enemy))
                            {
                                enemy.TakeHit(bombDamage);
                                GameObject hitEffect = PoolManager.Instance.Get("AarenAttackHitEffect", hitCollider[i].gameObject.transform.position);
                            }
                        }
                    }
                }
                isHit = true;
            }
        }
        if (particle.time >= maxEffectTime)
        {
            PoolManager.Instance.ReturnPool(gameObject);
            return;
        }

        currentTime += Time.deltaTime;
    }
}
