using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFireCross : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    private BoxCollider myCollider;
    private float duration;
    private float currentDuration;
    private float damagePercent;
    private int computeDamage;
    private float damageInterval;
    private ParticleSystem particle;
    private Collider[] hitsColliders;
    private float currentTicTime;
    private void Awake()
    {
        myCollider = GetComponent<BoxCollider>();
        particle = GetComponent<ParticleSystem>();
        duration = 2f;
        damageInterval = 0.2f;
        damagePercent = 0.2f;
        hitsColliders = new Collider[10];
        currentTicTime = 0f;
    }
    public void Init(int damage)
    {
        computeDamage = (int)(damage * damagePercent);
    }
    private void OnEnable()
    {
        currentDuration = 0f;
        particle.Play();
    }
    private void OnDisable()
    {
        for (int i = 0; i < hitsColliders.Length; i++)
        {
            hitsColliders[i] = null;
        }
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentDuration >= duration)
        {
            PoolManager.Instance.ReturnPool(this.gameObject);
        }
        currentDuration += Time.deltaTime;

    }
    private void OnDrawGizmos()
    {
        
    }
    private void FixedUpdate()
    {
        
        if(currentTicTime >= damageInterval)
        {
            if (Physics.OverlapBoxNonAlloc(transform.position + myCollider.center, myCollider.size, hitsColliders, transform.rotation, layerMask) > 0)
            {
                foreach (Collider col in hitsColliders)
                {
                    if (col != null)
                    {
                        if (col.gameObject.TryGetComponent(out IHitable enemy))
                        {
                            enemy.TakeHit(computeDamage, HitType.None);
                        }
                    }

                }
            }
            currentTicTime = 0f;
        }

        currentTicTime += Time.fixedDeltaTime;
      
    }
}
