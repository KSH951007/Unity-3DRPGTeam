using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ProjectileJuliaAttack : MonoBehaviour
{

    private int damage;
    private float moveSpeed;
    private float distance;
    private Vector3 startPos;
    private Rigidbody rb;

    private void Awake()
    {
        moveSpeed = 15f;
        distance = 10f;
        rb = GetComponent<Rigidbody>();
    }
    public void Init(Vector3 startPos, int damage)
    {
        this.startPos = startPos;
        this.damage = damage;
        StartCoroutine(FireRoutine());
    }
    private IEnumerator FireRoutine()
    {
        while (Vector3.Distance(startPos, transform.position) < distance)
        {
            rb.MovePosition(rb.position + transform.forward * moveSpeed*Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
       PoolManager.Instance.ReturnPool(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (other.gameObject.TryGetComponent(out IHitable enemy))
            {

                enemy.TakeHit(damage);
                //GameObject hitEffect = PoolManager.Instance.Get("AarenAttackHitEffect");
                //hitEffect.GetComponent<HitEffect>().Init(other.gameObject.transform.position, Vector3.one);

            }
        }
       
    }

}
