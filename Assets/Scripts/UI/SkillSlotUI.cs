using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour, IPointerEnterHandler
{
    private int slotIndex;
    private Image IconImage;
    private Image cooldownImage;
    private TextMeshProUGUI cooldownText;

    public void InIt(int slotIndex)
    {
        this.slotIndex = slotIndex;

    }
    private void Awake()
    {
        IconImage = transform.GetChild(0).GetComponent<Image>();
        cooldownImage = IconImage.transform.GetChild(0).GetComponentInChildren<Image>();
        cooldownText = IconImage.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();


    }

    public void ChangeSkillSlotUI(Skill skill)
    {
        IconImage.sprite = skill.SkillData.GetIcon();
        UpdateCooldown(skill.CurrentCooldown, skill.SkillData.GetCoolDown());
        skill.onStartCooldown += UpdateCooldown;
    }
    public void UpdateCooldown(float currentCooldown, float maxCooldown)
    {
        Debug.Log(currentCooldown);
        cooldownImage.fillAmount = (currentCooldown / maxCooldown);
        cooldownText.text = currentCooldown.ToString("F1");
        if (cooldownImage.fillAmount <= 0f)
        {
            cooldownImage.enabled = false;
            cooldownText.enabled = false;
        }
        else
        {
            cooldownImage.enabled = true;
            cooldownText.enabled = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //스킬 설명출력
    }
}
