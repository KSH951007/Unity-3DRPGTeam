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
        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i].InIt(i);
        }
    }
    private void Start()
    {
        skillManager = GameObject.Find("Player").transform.Find("Heros").GetComponent<SkillManager>();
        heroManager = skillManager.GetComponent<HeroManager>();

        SetSKillSlot();
        heroManager.onChangeCharacter += SetSKillSlot;

    }
    public void SetSKillSlot()
    {
        Hero mainHero = heroManager.GetMainHero();
        for (int i = 0; i < skillSlots.Length; i++)
        {
            Skill skill = skillManager.GetSkill(mainHero, i);
            skillSlots[i].ChangeSkillSlotUI(skill);
        }
    }
}
