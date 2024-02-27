using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ProjectileChargeSlash : MonoBehaviour
{
    private BoxCollider hitCollider;
    private float projectileSpeed;
    private float maxDistance;
    private float duration;
    private VisualEffect effect;
    private Vector3 startPos;
    private Vector3 direction;
    private float damagePercent;
    private int computeDamage;
    private void Awake()
    {
        hitCollider = GetComponent<BoxCollider>();
        effect = GetComponentInChildren<VisualEffect>();
        projectileSpeed = 5f;
        maxDistance = 10f;
        duration = maxDistance / projectileSpeed;
        effect.SetFloat("LifeTime", duration);
        damagePercent = 15f;
    }
    public void Init(Vector3 startPos, Vector3 direction, int damage)
    {
        this.startPos = startPos;
        transform.position = startPos;
        this.direction = direction;
        transform.rotation = Quaternion.FromToRotation(transform.forward, direction);
        computeDamage = (int)(damage * damagePercent);
        effect.Play();
    }
    private void OnDisable()
    {
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(startPos, transform.position) >= maxDistance)
        {
            PoolManager.Instance.ReturnPool(this.gameObject);
        }
        transform.Translate(direction * projectileSpeed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (other.TryGetComponent(out IHitable enemy))
            {
                enemy.TakeHit(computeDamage, HitType.None);
            }
        }
    }
}
