using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroMainSlotUI : MonoBehaviour
{
    private int slotIndex;
    [SerializeField] private Image heroIconImage;
    [SerializeField] private TextMeshProUGUI heroNameText;
    [SerializeField] private Image heroHpBarImage;
    [SerializeField] private TextMeshProUGUI heroHPBarText;
    [SerializeField] private Image heroMpBarImage;
    [SerializeField] private TextMeshProUGUI heroMPBarText;

    private Hero hero;
    public void ChangeSlotInfo(Hero newHero)
    {
     

        if (hero != null)
        {
            hero.GetComponent<ManaSystem>().onChangeMana -= ChangeManaUI;
            hero.GetComponent<Health>().onChangeHealth -= ChangeHpUI;

        }
        hero = newHero;
        Health health = hero.GetComponent<Health>();
        health.onChangeHealth += ChangeHpUI;
        ChangeHpUI(health.CurrentHealth, health.MaxHealth);
        ManaSystem manaSystem = hero.GetComponent<ManaSystem>();
        manaSystem.onChangeMana += ChangeManaUI;
        ChangeManaUI(manaSystem.CurrentMana, manaSystem.MaxMana);
        heroIconImage.sprite = hero.GetHeroData().GetIcon();
        heroNameText.text = hero.GetHeroData().GetName();

    }
    public void ChangeHpUI(int newCurrentHealth, int newMaxHealth)
    {
        heroHpBarImage.fillAmount = newCurrentHealth / (float)newMaxHealth;
        heroHPBarText.text = $"{newCurrentHealth} / {newMaxHealth}";
    }
    public void ChangeManaUI(int newCurrentMana, int newMaxMana)
    {
        heroMpBarImage.fillAmount = newCurrentMana / (float)newMaxMana;
        heroMPBarText.text = $"{newCurrentMana} / {newMaxMana}";

    }





}
