using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHitable
{

    [SerializeField] private int helath;
    private float defensivePercent;
    private Collider myCollider;
    public enum hitType { NONE, DOWN }

    public event Action onHit;
    public event Action onDie;
    public event Action onDown;

    private bool isinvincibility;
    public bool IsInvincibility { get { return isinvincibility; } set => isinvincibility = value; }
    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        myCollider.enabled = true;
    }
    public void SetHealth(int newHealth,float newDefensivePercent)
    {
        helath = newHealth;
        this.defensivePercent = newDefensivePercent;
    }
    public void TakeHit(int damage, HitType hitType, GameObject hitParticle = null)
    {
        if (isinvincibility)
            return;

        int compueDamage = damage - (int)(damage * defensivePercent);

        helath = Mathf.Max(helath - compueDamage, 0);
        GameObject damageUI = PoolManager.Instance.Get("DamageFontUI");
        damageUI.GetComponent<DamageUI>().GetDamageFont(transform.position, damage);

        if (helath <= 0)
        {
            myCollider.enabled = false;
            onDie?.Invoke();
            return;
        }
        if (hitParticle != null)
        {

        }

        onHit?.Invoke();
    }
}
