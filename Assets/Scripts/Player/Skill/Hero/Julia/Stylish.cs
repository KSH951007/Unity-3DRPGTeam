using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stylish : Skill
{
    [SerializeField] AnimationClip clip;
    private float moveSpeed;

    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 6f;
    }
    public override void UseSkill()
    {
        base.UseSkill();
        StartCoroutine(StylishRoutin());
    }


    private IEnumerator StylishRoutin()
    {
        float currentTime = hero.HeroAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float length = hero.HeroAnimator.GetCurrentAnimatorStateInfo(0).length;

        hero.GetComponent<Health>().IsInvincibility = true;
        while (currentTime < length)
        {
            if(!hero.HeroAnimator.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
            {
                Debug.Log("end");
                yield break;
            }

            hero.transform.Translate(hero.transform.forward * moveSpeed * Time.deltaTime, Space.World);
            currentTime = hero.HeroAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            yield return null;
        }
        hero.GetComponent<Health>().IsInvincibility = false;
    }
}
