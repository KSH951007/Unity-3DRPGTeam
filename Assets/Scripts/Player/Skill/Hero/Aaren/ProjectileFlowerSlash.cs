using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlowerSlash : MonoBehaviour
{
    private SphereCollider myCollider;
    private int damage;
    private Collider[] hitCollider;
    private ParticleSystem particle;
    private bool isHit;
    private float maxPlayTime;
    private void Awake()
    {
        hitCollider = new Collider[50];
        myCollider = GetComponent<SphereCollider>();
        particle = GetComponent<ParticleSystem>();
        maxPlayTime = 3f;
    }
    private void OnEnable()
    {
        isHit = false;
    }
    public void Init(Vector3 position, int damage)
    {
        transform.position = position;
        this.damage = damage;
        particle.Play();
    }
    private void Update()
    {
        if (!isHit)
        {
            if (particle.time >= 1f)
            {
                if (Physics.OverlapSphereNonAlloc(transform.position, myCollider.radius, hitCollider, LayerMask.GetMask("Enemy")) > 0)
                {

                    for (int i = 0; i < hitCollider.Length; i++)
                    {
                        if (hitCollider[i] != null)
                        {
                            if (hitCollider[i].gameObject.layer == LayerMask.NameToLayer("Enemy"))
                            {
                                if (hitCollider[i].TryGetComponent(out IHitable enemy))
                                {
                                    enemy.TakeHit(damage);
                                    GameObject hitEffect = PoolManager.Instance.Get("AarenAttackHitEffect", hitCollider[i].gameObject.transform.position);

                                }
                            }
                        }

                    }

                }
                isHit = true;
            }
        }
        if (particle.time >= maxPlayTime)
        {
            PoolManager.Instance.ReturnPool(gameObject);
            return;
        }

    }
}
