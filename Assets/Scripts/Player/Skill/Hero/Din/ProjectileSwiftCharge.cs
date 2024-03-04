using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSwiftCharge : MonoBehaviour
{
    private SphereCollider myCollider;
    private ParticleSystem particle;
    private float lifeTime;
    private float currentLifeTime;
    private float damagePercent;
    private int computeDamage;
    private Collider[] hitColliders;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        particle = GetComponent<ParticleSystem>();
        damagePercent = 1.8f;

        hitColliders = new Collider[20];
    }
    private void OnEnable()
    {
        particle.Play();
    }
    private void OnDisable()
    {
        currentLifeTime = 0f;
    }
    private void Update()
    {
        if(currentLifeTime >= lifeTime)
        {
            gameObject.SetActive(false);
        }


        currentLifeTime += Time.deltaTime;
    }
    public void Init(float lifeTime,float damage)
    {
        this.lifeTime = lifeTime;
        this.computeDamage = (int)(damage * damagePercent);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (other.gameObject.TryGetComponent(out IHitable enemy))
                {
                    enemy.TakeHit(computeDamage, HitType.None);
                }
            }
          
        }
    }

}
