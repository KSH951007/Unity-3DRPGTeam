using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static HeroInfoPageUI;

public class HeroInfoPageUI : CategoryPageUI
{
    public enum HeroInpoType { Din,Julia,Aaren}

    private HeroInpoType heroInpoType;
    [SerializeField] private RenderTexModel model;
    [SerializeField] private Toggle[] heroToggles;

    private void OnEnable()
    {
        heroInpoType = HeroInpoType.Din;
        model.ActvieModel(RenderTexModel.PreviewModelType.HeroDin);

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
            else if(heroInpoType == HeroInpoType.Julia)
            {
                model.ActvieModel(RenderTexModel.PreviewModelType.HeroJulia);
            }
            else
            {
                model.ActvieModel(RenderTexModel.PreviewModelType.HeroAaren);
            }
        }
    }
}
