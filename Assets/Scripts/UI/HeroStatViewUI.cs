using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroStatViewUI : MonoBehaviour
{
    public enum StatType { Level, Health, RegenHelth, Mana, RegenMana, Damage, Defense }
    [SerializeField] private TextMeshProUGUI[] statTexts;



    public void SetText(Hero hero)
    {
        HeroData heroData = hero.data;
        statTexts[(int)StatType.Level].text = $"���� : {heroData.level}";
        statTexts[(int)StatType.Health].text = $"ü�� : {heroData.health}";
        statTexts[(int)StatType.RegenHelth].text = $"ü�� ��� : {heroData.regenerationHealth}";
        statTexts[(int)StatType.Mana].text = $"���� : {heroData.mana}";
        statTexts[(int)StatType.RegenMana].text = $"���� ��� : {heroData.regenerationMana}";
        statTexts[(int)StatType.Damage].text = $"������ : {heroData.damage}";
        statTexts[(int)StatType.Defense].text = $"���� : {heroData.defensivePercent}";
    }
}
