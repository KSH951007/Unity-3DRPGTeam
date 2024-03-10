using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Item
{


    public enum ItemRatingType {Normal,Rair,Unique,Legenery }
    public int itemID;
    public string itemName;

    public string itemSummary;
    public ItemRatingType ratingType;
    public int itemLevel;

    public string itemDescription;
    public int itemBuyPrice;
    public ItemSO itemData;

   
    public Item(ItemSO itemData)
    {
        this.itemData = itemData;
        itemID = itemData.GetItemID();
        ratingType = itemData.GetRatingType();
        itemName = itemData.GetItemName();
        itemSummary = itemData.GetSummary();
        itemLevel = itemData.GetItemLevel();
        itemDescription = itemData.GetItemDescription();
        itemBuyPrice = itemData.GetItemBuyPrice();
            
    }



}
