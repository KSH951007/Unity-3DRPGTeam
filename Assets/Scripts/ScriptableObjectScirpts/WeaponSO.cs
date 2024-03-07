using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObject/Item/Weapon")]
public class WeaponSO : EquipmentItemSO
{
    [SerializeField] protected int weaponAttackPower;
    [SerializeField] protected int EquipHeroID;


    public bool isEquipHero(int heroID) { if (EquipHeroID == heroID) return true; return false; }
    public int GetAttackPower() { return weaponAttackPower; }
    public override Item CreateItem()
    {
        return new Weapon(this);
    }
}
