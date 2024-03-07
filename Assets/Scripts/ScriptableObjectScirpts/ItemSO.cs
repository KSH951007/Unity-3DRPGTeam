using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class ItemSO : ScriptableObject
{

    [SerializeField] protected int itemID;
    [SerializeField] protected Sprite itemIcon;
    [SerializeField] protected string itemName;
    [SerializeField] protected Item.ItemRatingType ratingType;
    [SerializeField] protected int itemLevel;
    [TextArea(8,8)]
    [SerializeField] protected string itemDescription;
    [SerializeField] protected int itemBuyPrice;


    public int GetItemID() { return itemID; }
    public int GetItemLevel() {  return itemLevel; }
    public Sprite GetItemIcon() { return itemIcon; }
    public string GetItemName() {  return itemName; }
    public int GetItemBuyPrice() { return itemBuyPrice; }
    public string GetItemDescription() {  return itemDescription; }

    public abstract Item CreateItem();
}
