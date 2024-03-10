using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EquipmentResult {Sucess, TribeLevel,DontSameHero }
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

    public void SetEquipment(Item item, int heroID)
    {
        if (item is EquipmentItem)
        {
            for (int i = 0; i < heroManager.MaxHeroCount; i++)
            {
                if (heroManager.GetSelectHero(i).GetHeroData().GetHeroID() == heroID)
                {
                    equipments[i].SetItem(item);
                    return;
                }
            }
        }
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
