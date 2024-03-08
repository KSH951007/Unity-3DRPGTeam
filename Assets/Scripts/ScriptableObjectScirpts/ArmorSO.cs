using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Armor", menuName = "ScriptableObject/Item/Armor")]
public class ArmorSO : EquipmentItemSO
{
    public override Item CreateItem()
    {
        return new Armor(this);
    }

   
}
