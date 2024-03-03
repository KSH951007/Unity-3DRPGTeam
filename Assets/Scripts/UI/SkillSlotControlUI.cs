using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotControlUI : MonoBehaviour
{

    private SkillSlotUI[] skillSlots;
    private SkillManager skillManager;
    private HeroManager heroManager;

    private void Awake()
    {
        skillSlots = transform.Find("Slots").GetComponentsInChildren<SkillSlotUI>();
    }
    private void Start()
    {
        skillManager = GameObject.Find("Player").transform.Find("Heros").GetComponent<SkillManager>();
        heroManager = skillManager.GetComponent<HeroManager>();

        //skillManager.GetSkill(heroManager.GetMainHero(), 0).SkillData.GetIcon();


    }
}
