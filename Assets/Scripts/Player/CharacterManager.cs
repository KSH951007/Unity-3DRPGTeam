using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private Hero[] hasHeros;
    private Hero[] selectHeros;
    private int mainHeroIndex;
    private float changeCooldown;
    private float currentChangeCooldown;
    public event Action onChangeCharacter;

   
   
    private void Awake()
    {
        changeCooldown = 5f;
        mainHeroIndex = 0;
        selectHeros = new Hero[3];
        hasHeros = new Hero[50];

        for (int i = 0; i < transform.childCount; i++)
        {
            hasHeros[i] = transform.GetChild(i).GetComponent<Hero>();
        }
        selectHeros[mainHeroIndex] = hasHeros[0];
    }
    public Hero GetMainHero()
    {
        return selectHeros[mainHeroIndex];
    }
    public void ChangeCharacter(int index)
    {
        if (selectHeros.Length < index)
            return;
        if (selectHeros[index] == null)
            return;
        if (currentChangeCooldown > 0f)
            return;




        mainHeroIndex = index;
        onChangeCharacter?.Invoke();
        StartCoroutine(ChangeCooldownRoutin());
    }
    private IEnumerator ChangeCooldownRoutin()
    {
        currentChangeCooldown = changeCooldown;

        while (currentChangeCooldown > 0)
        {
            currentChangeCooldown -= Time.deltaTime;
            yield return null;
        }

        currentChangeCooldown = 0f;

    }


}
