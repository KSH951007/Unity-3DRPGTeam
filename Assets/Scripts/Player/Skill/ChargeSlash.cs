using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ChargeSlash : Skill
{
    private BoxCollider hitCollider;

    [SerializeField] private Hero hero;
    private Vector3 direciton;
    private float projectileSpeed;
    private float maxDistance;
    private float duration;
    private Vector3 startPos;
    private VisualEffect effect;
    private int damage;
    private Transform parentTr;
    protected override void Awake()
    {
        base.Awake();
        parentTr = transform.parent;
        hitCollider = GetComponent<BoxCollider>();
        effect = GetComponentInChildren<VisualEffect>();
        maxDistance = 10f;
        projectileSpeed = 5f;
        damage = 12;
        duration = maxDistance / projectileSpeed;
        effect.SetFloat("LifeTime", duration);

    }
    private void OnDisable()
    {
    }


    public override void UseSkill()
    {
        this.gameObject.SetActive(true);
        transform.parent = null;
        startPos = transform.position;
        direciton = hero.transform.forward;

        StartCoroutine(CoolDownRoutine());
        effect.Play();
    }
    private void Update()
    {
        if (Vector3.Distance(startPos, transform.position) >= maxDistance)
        {
            ResetSkill();
            gameObject.SetActive(false);
        }
        transform.Translate(direciton * projectileSpeed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (other.TryGetComponent(out IHitable enemy))
            {
                enemy.TakeHit(damage, HitType.None);
            }
        }
    }
    private void ResetSkill()
    {
        transform.parent = parentTr;
        transform.position = parentTr.position;
        transform.rotation = parentTr.rotation;

    }
}
