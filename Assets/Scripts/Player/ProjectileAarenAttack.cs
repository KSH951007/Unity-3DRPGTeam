using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAarenAttack : MonoBehaviour
{
    private int damage;
    private float moveSpeed;
    private Vector3 direction;
    private float distance;
    private Transform target;
    private float sensorRadius;
    private Collider[] sensorCollider;
    private Vector3 startPos;
    private void Awake()
    {
        moveSpeed = 10f;
        sensorRadius = 1f;
        sensorCollider = new Collider[1];
        distance = 10f;
    }
    public void Init(int damage, Vector3 startPos, Vector3 direction)
    {
        this.startPos = startPos;
        this.damage = damage;
        this.direction = direction;
    }
    private void OnDisable()
    {
        target = null;
    }
    private void Update()
    {

        if (target == null)
        {
            if (Physics.OverlapSphereNonAlloc(transform.position, sensorRadius, sensorCollider, LayerMask.GetMask("Enemy")) > 0)
            {
                target = sensorCollider[0].transform;
                return;
            }
            else if (Vector3.Distance(startPos, transform.position) >= distance)
            {
                PoolManager.Instance.ReturnPool(gameObject);

                return;
            }
        }
        else
        {
            if (target == null)
            {
                PoolManager.Instance.ReturnPool(gameObject);
                return;
            }
            direction = target.transform.position - transform.position;
        }

        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (other.gameObject.TryGetComponent(out IHitable enemy))
            {

                enemy.TakeHit(damage, IHitable.HitType.None);
                Vector3 newPosition = other.transform.position - transform.position;
                GameObject hitEffect = PoolManager.Instance.Get("AarenAttackHitEffect", other.gameObject.transform.position - newPosition);
                PoolManager.Instance.ReturnPool(gameObject);
                return;

            }
        }
    }
}
