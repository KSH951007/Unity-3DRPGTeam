using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHitable
{

    [SerializeField] private int currentHealth;
    private int maxHeath;
    private float regenerationHealth;
    private float defensivePercent;
    private Collider myCollider;

    public event Action<int, int> onChangeHealth;

    public event Action onDie;

    private bool isinvincibility;
    public int CurrentHealth { get { return currentHealth; } }
    public int MaxHealth { get { return maxHeath; } }
    public bool IsInvincibility { get { return isinvincibility; } set => isinvincibility = value; }
    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        myCollider.enabled = true;
    }
    public void SetHealth(int newHealth, float newRegenerationHealth, float newDefensivePercent)
    {
        currentHealth = newHealth;
        maxHeath = newHealth;
        regenerationHealth = newRegenerationHealth;
        defensivePercent = newDefensivePercent;
    }
    public void TakeHit(int damage, IHitable.HitType hitType = IHitable.HitType.None, GameObject hitParticle = null)
    {
        if (isinvincibility)
            return;

        int compueDamage = damage - (int)(damage * defensivePercent);

        currentHealth = Mathf.Max(currentHealth - compueDamage, 0);
        GameObject damageUI = PoolManager.Instance.Get("DamageFontUI");
        damageUI.GetComponent<DamageUI>().GetDamageFont(transform.position, damage);

        if (currentHealth <= 0)
        {
            myCollider.enabled = false;
            onDie?.Invoke();
            onChangeHealth?.Invoke(currentHealth, maxHeath);
            return;
        }
        if (hitParticle != null)
        {

        }

        onChangeHealth?.Invoke(currentHealth, maxHeath);
    }

}
