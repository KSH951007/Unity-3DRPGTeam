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
    private int damageCount;
    private float damageInterval;
    private ParticleSystem particle;
    private Collider[] hitsColliders;
    private float currentTicTime;
    private void Awake()
    {
        myCollider = GetComponent<BoxCollider>();
        particle = GetComponent<ParticleSystem>();
        duration = 2f;
        damagePercent = 0.2f;
        hitsColliders = new Collider[10];
        currentTicTime = 0f;
        damageCount = 10;
        damageInterval = duration / damageCount;

    }
    public void Init(int damage)
    {
        computeDamage = (int)(damage * damagePercent);
    }
    private void OnEnable()
    {
        currentDuration = 0f;
        particle.Play();
        SoundManager.instance.PlaySound("HeroDinSkill2");
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

        if (currentTicTime >= damageInterval)
        {
            if (Physics.OverlapBoxNonAlloc(transform.position + myCollider.center, myCollider.size, hitsColliders, transform.rotation, layerMask) > 0)
            {
                foreach (Collider col in hitsColliders)
                {
                    if (col != null)
                    {
                        if (col.gameObject.TryGetComponent(out IHitable enemy))
                        {
                            enemy.TakeHit(computeDamage);

                            PoolManager.Instance.Get("FireCrossHitEffect", col.transform.position);
                        }
                    }

                }
            }
            currentTicTime = 0f;
        }

        currentTicTime += Time.fixedDeltaTime;

    }
}
