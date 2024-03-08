using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] Sprite[] itemRatingSprite;
    [SerializeField] Sprite itemRatingDefaultSprite;
    [SerializeField] GameObject countPanel;
    [SerializeField] TextMeshProUGUI countText;
    private int slotIndex;
    private int itemIndex;
    private Image ratingImage;
    private Image itemIconImage;
    private GameObject lockImage;
    private Toggle SelectToggle;

    public int ItemIndex { get => itemIndex; }
    private void Awake()
    {
        ratingImage = GetComponent<Image>();
        itemIconImage = transform.Find("Icon").GetComponent<Image>();
        lockImage = transform.Find("LockImage").gameObject;
        SelectToggle = itemIconImage.transform.Find("SelectToggle").GetComponent<Toggle>();
        itemIconImage.enabled = false;
        SelectToggle.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        SelectToggle.isOn = false;

    }
    public void SetItemSlotIndex(int index)
    {
        this.slotIndex = index;
    }
    public void SetInventorySlot(int itemIndex, Item item)
    {
        if (item == null)
        {
            ratingImage.sprite = itemRatingDefaultSprite;
            itemIconImage.enabled = false;
            this.itemIndex = -1;

            countPanel.SetActive(false);
            return;
        }
        else
        {
            this.itemIndex = itemIndex;

            if(item is CountableItem)
            {
                countPanel.SetActive(true);
                countText.text = ((CountableItem)item).Count.ToString();
            }
            else
            {
                countPanel.SetActive(false);
            }

            itemIconImage.enabled = true;

            if (item.itemData.GetRatingType() == Item.ItemRatingType.Normal)
                ratingImage.sprite = itemRatingSprite[0];
            else if (item.itemData.GetRatingType() == Item.ItemRatingType.Rair)
                ratingImage.sprite = itemRatingSprite[1];
            else if (item.itemData.GetRatingType() == Item.ItemRatingType.Unique)
                ratingImage.sprite = itemRatingSprite[2];
            else if (item.itemData.GetRatingType() == Item.ItemRatingType.Legenery)
                ratingImage.sprite = itemRatingSprite[3];

            this.itemIconImage.sprite = item.itemData.GetItemIcon();
        }



    }

    public void ActiveSelectToggle()
    {
        if (SelectToggle.gameObject.activeSelf)
        {
            SelectToggle.gameObject.SetActive(false);
        }
        else
        {
            SelectToggle.isOn = false;
            SelectToggle.gameObject.SetActive(true);

        }
    }



}
