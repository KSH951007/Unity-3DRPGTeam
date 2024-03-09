using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown heroDropdown;

    public event Action<int> onHeroSelect;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    public void ActiveHeroSelectUI(Action<int> equipAction)
    {
        gameObject.SetActive(true);
        onHeroSelect = equipAction;
    }
    public void PressSelectButton()
    {
        onHeroSelect?.Invoke(heroDropdown.value);
        gameObject.SetActive(false);
    }
}
