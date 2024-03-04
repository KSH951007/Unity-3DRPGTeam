using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour , IPointerEnterHandler
{
    private int slotIndex;
    private Image skillIconImage;
    private Image skillCooldownImage;


    private void Awake()
    {
        skillIconImage = transform.GetChild(0).GetComponent<Image>();
        skillCooldownImage = skillIconImage.transform.GetChild(0).GetComponent<Image>();

    }
    public void ChangeSkillSlotUI()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       //스킬 설명출력
    }
}
