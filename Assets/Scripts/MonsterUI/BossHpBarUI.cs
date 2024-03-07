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
        UpdateHpBar(boss.maxHp, boss.currentHp, boss.curShieldAmount);
    }

    public void UpdateHpBar(float maxHp, float currentHp, float currentShieldAmount)
    {
        if (currentShieldAmount > 0)
        {
            fillArea.transform.localScale = new Vector3((currentHp + currentShieldAmount) / (maxHp + currentShieldAmount), 1, 1);
            int percent = (int)((currentHp / maxHp) * 100);
            text.text = $"{(int)currentHp} (+{(int)currentShieldAmount}) / {(int)maxHp}  ({percent} %)";
		}
        else
        {
			fillArea.transform.localScale = new Vector3(currentHp / maxHp, 1, 1);
			int intPercent = (int)((currentHp / maxHp) * 100);
			text.text = $"{(int)currentHp} / {(int)maxHp}  ({intPercent} %)";
		}
    }

}
