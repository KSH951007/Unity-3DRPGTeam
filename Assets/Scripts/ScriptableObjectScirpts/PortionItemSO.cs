using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HealthPortionType {HpPortion,MpPortion }
[CreateAssetMenu(fileName = "New HealthItem", menuName = "ScriptableObject/Item/Portion/Health")]
public class PortionItemSO : CountableItemSO
{
    [SerializeField] protected int health;
    [SerializeField] protected HealthPortionType portionType;

   

    public int GetHealth() { return health; }
    public HealthPortionType GetPortionType() {  return portionType; }
    public override Item CreateItem()
    {
        return new PortionItem(this);
    }
}
