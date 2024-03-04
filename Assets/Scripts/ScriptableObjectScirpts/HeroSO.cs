using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Hero", menuName = "ScriptableObject/HeroData")]
public class HeroSO : ScriptableObject
{
    [SerializeField] protected Sprite heroIcon;
    [SerializeField] protected string heroName;
    [SerializeField] protected int level;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int maxMana;
    [SerializeField] protected float defensivePercent;
    [SerializeField] protected int baseDamage;
    [SerializeField] protected int maxAttackCombo;
    [SerializeField] protected SkillSO[] skillDatas = new SkillSO[3];

    public Sprite GetIcon() { return heroIcon; }
    public string GetName() { return heroName;}
    public int GetLevel() { return level; }
    public int GetMaxHealth() { return maxHealth; }
    public int GetMaxMana() { return maxMana; }
    public float GetDefensive() { return defensivePercent; }
    public int GetDamage() { return baseDamage; }
    public int GetMaxAttackCombo() { return maxAttackCombo; }
}

