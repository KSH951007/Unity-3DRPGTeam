using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private Equipment[] equipments;
    private PortionItem[] portionItems;
    private int maxPortionItemSlotCount;
    private HeroManager heroManager;

    private void Awake()
    {
        maxPortionItemSlotCount = 6;
        portionItems = new PortionItem[maxPortionItemSlotCount];
        equipments = GetComponentsInChildren<Equipment>();
        heroManager = GetComponent<HeroManager>();
    }

    public void SetEquipment(int heroID, Item equipItem)
    {
        for (int i = 0; i < heroManager.MaxHeroCount; i++)
        {
            if(heroManager.GetSelectHero(i).GetHeroData().GetHeroID() == heroID)
            {
                equipments[i].SetItem(equipItem);
                return;
            }
        }
    }
    public void SetPortionItem(PortionItem portionItem)
    {

    }
}
