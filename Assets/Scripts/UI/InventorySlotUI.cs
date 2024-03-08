using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    private int slotIndex;
    private Image RatingImage;
    private Image itemIconImage;
    private GameObject lockImage;
    private Toggle SelectToggle;
    
    private void Awake()
    {
        RatingImage = GetComponent<Image>();
        itemIconImage = transform.Find("Icon").GetComponent<Image>();
        lockImage = transform.Find("LockImage").gameObject;
        SelectToggle = itemIconImage.transform.Find("SelectToggle").GetComponent<Toggle>();
        itemIconImage.enabled = false;
        SelectToggle.gameObject.SetActive(false);
    }
    public void SetInventorySlot(int newSlotIndex, Sprite ratingSprtie, Sprite itemIcon)
    {
        this.slotIndex = newSlotIndex;
        this.RatingImage.sprite = ratingSprtie;
        this.itemIconImage.sprite = itemIcon;

    }

    public void ActiveSelectToggle()
    {
        if(SelectToggle.gameObject.activeSelf)
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
