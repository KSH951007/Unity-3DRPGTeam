using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViligeUI : MonoBehaviour
{
    public enum CategoryType { Info, Shop, Inven, Quest }

    [SerializeField] private Toggle[] categoryToggles;
    [SerializeField] private Sprite enableSprite;
    [SerializeField] private Sprite disableSprite;
    private CategoryType categoryType;
    private void OnEnable()
    {
        categoryType = CategoryType.Info;
        categoryToggles[(int)categoryType].isOn = true;


    }



    private void Start()
    {

    }


    private void Update()
    {

    }
    public void ActiveCategoryToggle(int index)
    {
        if (categoryToggles[index].isOn)
        {
            categoryType = (CategoryType)index;

        }
    }
}
