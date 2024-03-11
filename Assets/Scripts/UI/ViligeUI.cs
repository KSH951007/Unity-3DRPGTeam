using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViligeUI : MonoBehaviour
{
    public enum CategoryType { Info, Shop, Inven ,Size}

    [SerializeField] private Toggle[] categoryToggles;
    [SerializeField] private Sprite enableSprite;
    [SerializeField] private Sprite disableSprite;
    [SerializeField] private CategoryPageUI[] categoryPageUI;
    private CategoryType categoryType;
    private void OnEnable()
    {
        for (int i = 0; i < categoryPageUI.Length; i++)
        {
            categoryPageUI[i].gameObject.SetActive(false);
        }
        categoryType = CategoryType.Info;
        categoryToggles[(int)categoryType].isOn = true;

        categoryPageUI[(int)categoryType].gameObject.SetActive(true);
        ActiveCategoryToggle((int)categoryType);

    }
    private void Update()
    {

    }
    public void ActiveCategoryToggle(int index)
    {
        categoryType = (CategoryType)index;
        SoundManager.instance.PlaySound("UIClick");

        if (categoryToggles[index].isOn)
        {
            categoryToggles[index].transform.GetChild(0).GetComponent<Image>().sprite = enableSprite;
            categoryPageUI[index].gameObject.SetActive(true);
        }
        else
        {
            categoryToggles[index].transform.GetChild(0).GetComponent<Image>().sprite = disableSprite;
            categoryPageUI[index].gameObject.SetActive(false);
        }   
    }
    public void PressQuitButton()
    {
        SoundManager.instance.PlaySound("UIClick");
        gameObject.SetActive(false);
    }
  
}
