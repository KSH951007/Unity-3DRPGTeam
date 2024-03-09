using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Armor", menuName = "ScriptableObject/Item/Armor")]
public class ArmorSO : EquipmentItemSO
{
    [SerializeField] private float defensivePercent;
    [SerializeField] private DefensiveItemType defensiveType;

    public DefensiveItemType GetDefensiveItemType() { return defensiveType; }
    public override Item CreateItem()
    {
        return new Armor(this);
    }


}
