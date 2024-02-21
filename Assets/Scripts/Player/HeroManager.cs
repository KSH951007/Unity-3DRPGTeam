using Cinemachine;
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
    [SerializeField] private CinemachineVirtualCamera playerCamera;



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
        for (int i = 0; i < selectHeros.Length; i++)
        {
            selectHeros[i] = hasHeros[i];
        }

        playerCamera.m_Follow = selectHeros[mainHeroIndex].transform;
        playerCamera.m_LookAt = selectHeros[mainHeroIndex].transform;

    }
    public Hero GetMainHero()
    {
        return selectHeros[mainHeroIndex];
    }
    public void AddStorageHero()
    {

    }
    public void ChangeCharacter()
    {
        int nextIndex = mainHeroIndex + 1;
        if (nextIndex >= maxPlayHeroCount)
        {
            nextIndex = 0;
        }

        Debug.Log(selectHeros.Length);
        if (selectHeros[nextIndex] == null)
            return;
        if (currentChangeCooldown > 0f)
            return;

        selectHeros[mainHeroIndex].Scheduler.ResetActions();
        selectHeros[mainHeroIndex].gameObject.SetActive(false);
        Transform prevTr = selectHeros[mainHeroIndex].transform;
        mainHeroIndex = nextIndex;
        selectHeros[mainHeroIndex].transform.position = prevTr.position;
        selectHeros[mainHeroIndex].transform.rotation = prevTr.rotation;
        selectHeros[mainHeroIndex].gameObject.SetActive(true);

        playerCamera.m_Follow = selectHeros[mainHeroIndex].transform;
        playerCamera.m_LookAt = selectHeros[mainHeroIndex].transform;

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
        if (mainHeroIndex >= maxPlayHeroCount)
        {
            mainHeroIndex = 0;
        }

        return mainHeroIndex;
    }


}
