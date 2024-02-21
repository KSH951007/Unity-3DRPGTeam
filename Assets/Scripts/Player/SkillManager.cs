using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    List<Skill> skillList;


    private void Awake()
    {
        skillList = new List<Skill>(3);
    }
    public void AddSkill(Skill skill)
    {
        skillList.Add(skill);
    }


    private void Start()
    {

    }
    private void Update()
    {

    }
}
