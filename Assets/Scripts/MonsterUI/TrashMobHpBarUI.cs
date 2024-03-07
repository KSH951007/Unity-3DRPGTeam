using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrashMobHpBarUI : MonoBehaviour
{
    Camera _camera;
    private Transform mobTransform;
    private Slider mobHpBar;
    TextMeshProUGUI text;
    TrashMob mob;

    private void Awake()
    {
        mob = GetComponentInParent<TrashMob>();
        _camera = Camera.main;
        mobHpBar = GetComponentInChildren<Slider>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        mobTransform = GetComponentInParent<Transform>();
    }

    private void Update()
    {
        transform.rotation = _camera.transform.rotation;
        transform.position = mobTransform.position;
        UpdateHpBar(mob.maxHp, mob.currentHp);
    }

    public void UpdateHpBar(float maxHp, float currentHp)
    {
        mobHpBar.value = currentHp / maxHp;
        int intHp = (int)currentHp;
        text.text = intHp.ToString();
    }
}
