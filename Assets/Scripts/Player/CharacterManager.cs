using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    private Hero[] heroes;
    private int mainHeroIndex;
    private float changeCooldown;
    private float currentChangeCooldown;
    public event Action onChangeCharacter;

   
   
    private void Awake()
    {
        changeCooldown = 5f;
        mainHeroIndex = 0;
        heroes = new Hero[3];

        for (int i = 0; i < transform.childCount; i++)
        {
            heroes[i] = transform.GetChild(i).GetComponent<Hero>();
        }
    }
    public Hero GetMainHero()
    {
        return heroes[mainHeroIndex];
    }
    public void ChangeCharacter(int index)
    {
        if (heroes.Length < index)
            return;
        if (heroes[index] == null)
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
