using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin_SkillEvent : MonoBehaviour
{
    public SphereCollider attack1Collider;
    public BoxCollider skill1Collider;
    public SphereCollider skill3Collider;

    public void OnAttack1Collider()
    {
        print("@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
    }
    public void OnSkill1Collider()
    {
        print("@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
    }
    public void OnSkill3Collider()
    {
        print("@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
    }
}
