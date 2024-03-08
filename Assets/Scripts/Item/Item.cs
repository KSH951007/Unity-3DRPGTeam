using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item
{
    public enum ItemRatingType {Normal,Rair,Unique,Legenery }
    public ItemSO itemData { get; private set; }

    public Item(ItemSO itemData) => this.itemData = itemData;



}
