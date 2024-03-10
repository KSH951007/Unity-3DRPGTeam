using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountItemBuyActionUI : MonoBehaviour
{
    [SerializeField] private Image itemRatingImage;
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Slider itemCountSlider;
    [SerializeField] private TextMeshProUGUI itemConutText;
    [SerializeField] private TextMeshProUGUI itemTotalPriceText;
    public Action<ItemSO, int> onBuyAction;
    private ItemSO buyItemData;
    private int price;

    public void SetItemInfo(Sprite ratingSprite, ItemSO buyItemData, int maxCount, Action<ItemSO, int> buyAction)
    {
        gameObject.SetActive(true);
        this.buyItemData = buyItemData;
        itemRatingImage.sprite = ratingSprite;
        itemIconImage.sprite = buyItemData.GetItemIcon();
        itemNameText.text = buyItemData.GetItemName();
        itemCountSlider.value = 1;
        itemCountSlider.maxValue = maxCount;
        itemConutText.text = itemCountSlider.value.ToString();
        this.price = buyItemData.GetItemBuyPrice();
        itemTotalPriceText.text = $"ÃÑ °¡°Ý :{itemCountSlider.value * this.price}";
        onBuyAction = buyAction;
    }
    public void ChangeCountSlider(float value)
    {
        itemConutText.text = value.ToString();
        itemTotalPriceText.text = $"ÃÑ °¡°Ý :{itemCountSlider.value * this.price}";
    }
    public void PressBuyButton()
    {
        onBuyAction?.Invoke(buyItemData, (int)itemCountSlider.value);
        gameObject.SetActive(false);
    }
    public void PressCancleButton()
    {
        gameObject.SetActive(false);
    }
}
