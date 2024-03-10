using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoPageUI : CategoryPageUI
{
    public enum EquipmentSlotType { Weapon, Head, Body, Hand, Foot, Size }
    public enum HeroInpoType { Din, Julia, Aaren }

    private HeroInpoType heroInpoType;
    [SerializeField] private RenderTexModel model;
    [SerializeField] private Toggle[] heroToggles;
    [SerializeField] private EquipmentSlotUI[] equipmentSlotUI;
    [SerializeField] private PortionSlotUI[] portionSlotUI;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private HeroManager heroManager;
    [SerializeField] private HeroStatViewUI heroStatViewUI;
    private void OnEnable()
    {
        heroInpoType = HeroInpoType.Din;
        model.ActvieModel(RenderTexModel.PreviewModelType.HeroDin);
        heroToggles[(int)heroInpoType].isOn = true;
        ActiveHeroSelectToggle((int)heroInpoType);
    }
    public void UpdatePortionSlots()
    {
        for (int i = 0; i < portionSlotUI.Length; i++)
        {
            portionSlotUI[i].SetItemInfo(equipmentManager.GetPortionItems()[i]);
        }
    }
    public void UpdateEquipmentSlots(int heroIndex)
    {
        for (int i = 0; i < equipmentSlotUI.Length; i++)
        {
            equipmentSlotUI[i].SetItemInfo(equipmentManager.GetEquipment(heroIndex).GetEquipmentItem((Equipment.EquipmentSlotType)i));

        }
    }
    public void ActiveHeroSelectToggle(int index)
    {

        heroInpoType = (HeroInpoType)index;
        if (heroToggles[index].isOn)
        {
            if (heroInpoType == HeroInpoType.Din)
            {
                model.ActvieModel(RenderTexModel.PreviewModelType.HeroDin);

            }
            else if (heroInpoType == HeroInpoType.Julia)
            {
                model.ActvieModel(RenderTexModel.PreviewModelType.HeroJulia);
            }
            else
            {
                model.ActvieModel(RenderTexModel.PreviewModelType.HeroAaren);
            }
            heroStatViewUI.SetText(heroManager.GetSelectHero(index));

            UpdateEquipmentSlots(index);
            UpdatePortionSlots();
        }
    }
    public void PressReleaseButton()
    {
        Debug.Log("click");

        for (int i = 0; i < equipmentSlotUI.Length; i++)
        {
            if (equipmentSlotUI[i].IsSelect)
            {
                equipmentManager.ReleaseEquipmentItem((int)heroInpoType, i);

            }
        }
        UpdateEquipmentSlots((int)heroInpoType);
        heroStatViewUI.SetText(heroManager.GetSelectHero((int)heroInpoType));
        for (int i = 0; i < portionSlotUI.Length; i++)
        {
            if (!portionSlotUI[i].IsEmpty)
            {
                if (portionSlotUI[i].IsSelect)
                {
                    Debug.Log("selec");
                    equipmentManager.ReleasePortionItem(i);
                }
            }
        }
        UpdatePortionSlots();

    }
}
