using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Hero", menuName = "ScriptableObject/HeroData")]
public class HeroSO : ScriptableObject
{
    [SerializeField] protected Sprite heroIcon;
    [SerializeField] protected int level;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int maxMana;
    [SerializeField] protected int defensive;
    [SerializeField] protected int damage;
    [SerializeField] protected int maxAttackCombo;


    public Sprite GetIcon() { return heroIcon; }
    public int GetLevel() { return level; }
    public int GetMaxHealth() { return maxHealth; }
    public int GetMaxMana() { return maxMana; }
    public int GetDefensive() { return defensive; }
    public int GetDamage() { return damage; }
    public int GetMaxAttackCombo() { return maxAttackCombo; }
}
