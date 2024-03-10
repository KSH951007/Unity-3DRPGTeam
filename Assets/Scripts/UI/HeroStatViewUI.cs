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
        statTexts[(int)StatType.Level].text = $"레벨 : {heroData.level}";
        statTexts[(int)StatType.Health].text = $"체력 : {heroData.health}";
        statTexts[(int)StatType.RegenHelth].text = $"체력 재생 : {heroData.regenerationHealth}";
        statTexts[(int)StatType.Mana].text = $"마나 : {heroData.mana}";
        statTexts[(int)StatType.RegenMana].text = $"마나 재생 : {heroData.regenerationMana}";
        statTexts[(int)StatType.Damage].text = $"데미지 : {heroData.damage}";
        statTexts[(int)StatType.Defense].text = $"방어력 : {heroData.defensivePercent}";
    }
}
