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
    private Skill skill;

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
    public void ChangeSkillSlotUI(Skill newskill)
    {
        if(skill != null)
        {
            skill.onCooldownAction -= UpdateCooldown;
        }
        skill = newskill;
        IconImage.sprite = skill.SkillData.GetIcon();
        UpdateCooldown(skill.CurrentCooldown, skill.SkillData.GetCoolDown());
        skill.onCooldownAction += UpdateCooldown;
    }
    public void UpdateCooldown(float currentCooldown, float maxCooldown)
    {
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
