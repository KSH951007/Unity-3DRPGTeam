using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    private int maxStorageHerosCount;
    private int maxPlayHeroCount;
    private Hero[] hasHeros;
    private Hero[] selectHeros;
    private int mainHeroIndex;
    private float changeCooldown;
    private float currentChangeCooldown;
    public event Action onChangeCharacter;

   
   
    private void Awake()
    {
        maxStorageHerosCount = 50;
        maxPlayHeroCount = 3;
        changeCooldown = 5f;
        mainHeroIndex = 0;
        selectHeros = new Hero[maxPlayHeroCount];
        hasHeros = new Hero[maxStorageHerosCount];

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
    public void AddStorageHero()
    {

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
    public int nextCharacter()
    {
        mainHeroIndex++;
        if(mainHeroIndex >= maxPlayHeroCount)
        {
            mainHeroIndex = 0;
        }

        return mainHeroIndex;
    }


}
