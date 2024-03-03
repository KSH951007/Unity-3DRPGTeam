using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBarUI : MonoBehaviour
{
    BossMonsters boss;
    [SerializeField] Image fillArea;
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        boss = GetComponentInParent<BossMonsters>();
    }

    private void Update()
    {
        UpdateHpBar(boss.maxHp, boss.currentHp);
    }

    public void UpdateHpBar(float maxHp, float currentHp)
    {
        fillArea.transform.localScale = new Vector3(currentHp / maxHp, 1, 1);
        int intPercent = (int)((currentHp / maxHp) * 100);
        text.text = $"{(int)currentHp} / {(int)maxHp}  ({intPercent} %)";
    }

}
