using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown heroDropdown;

    public event Action<Item,int> onHeroSelect;
    private Item item;

    private void Start()
    {
        
    }

    public void ActiveHeroSelectUI(Item item, Action<Item,int> equipAction)
    {
        gameObject.SetActive(true);
        this.item= item;
        onHeroSelect = equipAction;

    }
    public void PressSelectButton()
    {
        onHeroSelect?.Invoke(item,heroDropdown.value);
        gameObject.SetActive(false);
    }
}
