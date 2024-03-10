using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class ShowClearDetail : MonoBehaviour
{

    private string ClearTime;
    [SerializeField]
    RectTransform rewardGroup;
    [SerializeField]
    RewardClearDetail rewardPrefab;
    [SerializeField]
    TextMeshProUGUI clearTimeShow;
    [SerializeField]
    TextMeshProUGUI dungeonNameShow;
    [SerializeField]
    TextMeshProUGUI damageToBoss;
    [SerializeField]
    Inventory inven;
    
    [SerializeField]
    ItemSO[] itemArray;

    [SerializeField]
    BossMonsters boss;

    string st;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void showDetail()
    {
        clearTimeShow.text = ClearTime;
        if (boss.maxHp >= 50000)
        {
            st = "바루스";
        }
        else
        {
            st = "분노한 크후루루";
        }
        dungeonNameShow.text = $"-{st}-";
        damageToBoss.text = $"{boss.maxHp}";

        StartCoroutine(GetItem());
    }
    public void getClearTime(string s)
    {
        itemArray = boss.dropItem;
        ClearTime = s ;
        showDetail();
    }
    public void gotoHome() // 온클릭 이벤트
    {
        Singleton<SceneLoader>.Instance.LoadScene(0);// 마을
    }
    IEnumerator GetItem()
    {
        foreach(var item in itemArray)
        {
            Instantiate(rewardPrefab, rewardGroup).Setup(item.CreateItem());
            inven.SetItem(item.CreateItem());
            yield return new WaitForSeconds(0.5f);
        }
    }
}
