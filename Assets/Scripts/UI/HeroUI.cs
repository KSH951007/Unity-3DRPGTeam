using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUI : MonoBehaviour
{
    private HeroMainSlot[] slots;

    private void Awake()
    {
        slots = new HeroMainSlot[3];
        int index = 0;
        slots[index] = transform.Find("MainHeroPanel").GetComponent<HeroMainSlot>();
        
        index++;
        Transform subHeros = transform.Find("SubHeroPanels");
        for (int i = 0; i < subHeros.childCount; i++)
        {
            slots[index + i] = subHeros.GetChild(i).GetComponent<HeroMainSlot>();
        }

    }
}
