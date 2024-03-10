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

    public Equipment.EquipmentResult SetEquipment(Item item, int heroID)
    {
        if (item is EquipmentItem)
        {
            for (int i = 0; i < heroManager.MaxHeroCount; i++)
            {
                if (heroManager.GetSelectHero(i).GetHeroData().GetHeroID() == heroID)
                {
                    return equipments[i].SetItem(item);
                }
            }
        }
        return Equipment.EquipmentResult.Success;
    }
    public void SetPortionItem(Item item)
    {

    }
    public Equipment GetEquipment(int heroIndex)
    {
        if (equipments == null)
            return null;

        return equipments[heroIndex];
    }

}
