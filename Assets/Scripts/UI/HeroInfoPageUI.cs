using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static HeroInfoPageUI;

public class HeroInfoPageUI : CategoryPageUI
{
    public enum EquipmentSlotType { Weapon, Head, Body, Hand, Foot, Size }
    public enum HeroInpoType { Din, Julia, Aaren }

    private HeroInpoType heroInpoType;
    [SerializeField] private RenderTexModel model;
    [SerializeField] private Toggle[] heroToggles;
    [SerializeField] private EquipmentSlotUI[] equipmentSlotUI;
    [SerializeField] private EquipmentManager equipmentManager;
    private void OnEnable()
    {
        heroInpoType = HeroInpoType.Din;
        model.ActvieModel(RenderTexModel.PreviewModelType.HeroDin);
        ActiveHeroSelectToggle((int)heroInpoType);
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


            for (int i = 0; i < equipmentSlotUI.Length; i++)
            {
                Equipment equipment = equipmentManager.GetEquipment(index);
                if (equipment != null)
                    equipmentSlotUI[i].SetItemInfo(equipment.GetEquipmentItem((Equipment.EquipmentSlotType)i));
            }
        }
    }
}
