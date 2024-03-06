using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class HeroUI : MonoBehaviour
{
    private HeroManager heroManager;
    private HeroMainSlotUI[] slots;
    private ChangeTimerUI timerUI;
    private void Awake()
    {
        timerUI = transform.GetComponentInChildren<ChangeTimerUI>();
        slots = new HeroMainSlotUI[3];
        int index = 0;
        slots[index] = transform.Find("MainHeroPanel").GetComponent<HeroMainSlotUI>();

        index++;
        Transform subHeros = transform.Find("SubHeroPanels");
        for (int i = 0; i < subHeros.childCount; i++)
        {
            slots[index + i] = subHeros.GetChild(i).GetComponent<HeroMainSlotUI>();
        }

    }
    private void Start()
    {
        heroManager = GameObject.Find("Player").transform.Find("Heros").GetComponent<HeroManager>();

        ChangeSlots();
        heroManager.onChangeCharacter += UpdateSlots;
    }
    private void ChangeSlots()
    {
        Hero hero = heroManager.GetMainHero();

        slots[0].ChangeSlotInfo(hero);

        int index = heroManager.nextHeroIndex(heroManager.GetMainHeroIndex());

        for (int i = 1; i < slots.Length; i++)
        {
            hero = heroManager.GetSelectHero(index);
            slots[i].ChangeSlotInfo(hero);
            index = heroManager.nextHeroIndex(index);
        }
    }
    public void UpdateSlots()
    {
        ChangeSlots();
        timerUI.SetTimer(heroManager.ChangeCooldown);
    }

}
