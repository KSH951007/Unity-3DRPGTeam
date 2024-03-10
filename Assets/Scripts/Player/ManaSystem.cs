using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    [SerializeField] private int mana;
    private int maxMana;
    private float regenerationMana;
    public event Action<int, int> onChangeMana;


    public int CurrentMana { get { return mana; } }
    public int MaxMana { get { return maxMana; } }



    public void Init(int newMana, float newRegenerationMana)
    {
        mana = newMana;
        maxMana = newMana;
        regenerationMana = newRegenerationMana;
    }

    public void Regenerat()
    {
        int addMana = (int)(maxMana * regenerationMana);

        if (addMana + mana > maxMana)
        {
            mana = maxMana;
            onChangeMana?.Invoke(mana, maxMana);
            return;
        }

        mana += addMana;
        onChangeMana?.Invoke(mana, maxMana);
    }
    public bool CanUseCost(int cost)
    {
        int tempMana = mana - cost;

        if (tempMana < 0)
            return false;


        return true;
    }
    public void PaymentCost(int cost)
    {
        mana -= cost;
        onChangeMana?.Invoke(mana, maxMana);
    }

}
