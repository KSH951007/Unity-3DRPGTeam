using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class HeroManager : MonoBehaviour
{

    [SerializeField] private ChangeEffect changeEffect;



    private int maxStorageHerosCount;
    private int maxPlayHeroCount;
    private Hero[] hasHeros;
    private Hero[] selectHeros;
    private int mainHeroIndex;
    private float changeCooldown;
    private float currentChangeCooldown;
    public event Action onChangeCharacter;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    private float currentRegenerationTime;
    private float regenerationTime;

    public float ChangeCooldown { get { return changeCooldown; } }

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
        regenerationTime = 1f;
        currentRegenerationTime = 0f;
    }
    private void Start()
    {
        SceneChangeHeroSetting();
        //SceneLoader.Instance.onSceneChanged += SceneChangeHeroSetting;

    }
    private void Update()
    {
        currentRegenerationTime += Time.deltaTime;
        if (currentRegenerationTime >= regenerationTime)
        {
            for (int i = 0; i < selectHeros.Length; i++)
            {
                if (i == mainHeroIndex)
                    continue;

                selectHeros[i].NoneActiveHero();
            }
            currentRegenerationTime = 0f;
        }

    }
    public Hero GetMainHero()
    {
        return selectHeros[mainHeroIndex];
    }
    public int GetMainHeroIndex() { return mainHeroIndex; }
    public Hero GetSelectHero(int index) { return selectHeros[index]; }

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

        if (selectHeros[nextIndex] == null)
            return;
        if (currentChangeCooldown > 0f)
            return;
        selectHeros[mainHeroIndex].Scheduler.ResetActions();

        //changeEffect.ChangeMaterial(ChangeEffect.ChangeType.Appearance, selectHeros[mainHeroIndex].MeshRenderers[1], selectHeros[mainHeroIndex].transform.position,changeTime);

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
    public int nextHeroIndex(int index)
    {
        index++;
        if (index >= maxPlayHeroCount)
        {
            index = 0;
        }

        return index;
    }
    private void SceneChangeHeroSetting()
    {
        for (int i = 0; i < selectHeros.Length; i++)
        {
            if (SceneLoader.Instance.GetSceneType() == SceneLoader.SceneType.Dungeon)
                selectHeros[i].ChangeAnimatorController(EnumType.HeroAnimType.Battle);
            else
                selectHeros[i].ChangeAnimatorController(EnumType.HeroAnimType.Base);

            selectHeros[i].gameObject.SetActive(false);
        }
        selectHeros[mainHeroIndex].gameObject.SetActive(true);

    }

}
