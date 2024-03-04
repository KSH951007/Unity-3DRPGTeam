using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroMainSlotUI : MonoBehaviour
{
    private int slotIndex;
    [SerializeField]private Image heroIconImage;
    [SerializeField] private TextMeshProUGUI heroNameText;
    [SerializeField] private Image heroHpBarImage;
    [SerializeField] private Image heroMpBarImage;

    public void ChangeSlotInfo(HeroSO heroData)
    {
        heroIconImage.sprite = heroData.GetIcon();
        heroNameText.text = heroData.GetName();


    }
    



}
