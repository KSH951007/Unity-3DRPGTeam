using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBigShot : MonoBehaviour
{
    private int computeDamage;
    private float damagePercent;
    private float distance;
    private float moveSpeed;
    private Vector3 startPos;
    private ParticleSystem particle;
    private Rigidbody rb;

    private void Awake()
    {
        damagePercent = 5.5f;
        distance = 20f;
        moveSpeed = 10f;
        particle = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
    }
    public void Init(Vector3 startPos, Quaternion direction, int damage)
    {
        this.startPos = startPos;
        transform.position = startPos;
        transform.rotation = direction;
        computeDamage = (int)(damage * damagePercent);
        particle.Play();
    }
    private void OnEnable()
    {

    }

    private void Update()
    {
        if (Vector3.Distance(startPos, transform.position) >= distance)
        {
            particle.Stop();
            PoolManager.Instance.ReturnPool(gameObject);
            return;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (other.gameObject.TryGetComponent(out IHitable enemy))
            {
                enemy.TakeHit(computeDamage);
                PoolManager.Instance.Get("BigShotHitEffect",other.transform.position);

            }
        }

    }
}
