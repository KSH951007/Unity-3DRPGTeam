using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin_AnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject khururuTransPrefab;

    public SphereCollider attack1Collider;
    public BoxCollider skill1Collider;
    public GameObject skill1Alert;
<<<<<<< HEAD
=======
    public GameObject skill2Shield;
>>>>>>> Sample
    public SphereCollider skill3Collider;

    public void OnAttack1Collider()
    {
        attack1Collider.enabled = true;
    }

    public void OnSkill1Collider()
    {
        skill1Collider.enabled = true;
    }

    public void OnSkill1Alert()
    {
        skill1Alert.SetActive(true);
    }

    public void OffSkill1Alert()
    {
        skill1Alert.SetActive(false);
    }

<<<<<<< HEAD
    public void OnSkill3Collider()
=======
    public void OnSkill2Shield()
    {
		skill2Shield.SetActive(true);
	}

	public void OnSkill3Collider()
>>>>>>> Sample
    {
        skill3Collider.enabled = true;
    }

    public void OffAttack1Collider()
    {
        attack1Collider.enabled = false;
    }

    public void OffSkill1Collider()
    {
        skill1Collider.enabled = false;
    }

    public void OffSkill3Collider()
    {
        skill3Collider.enabled = false;
    }

    public void KhururuTransActive()
    {
        khururuTransPrefab.SetActive(true);
        khururuTransPrefab.transform.position = transform.position;
    }

    public void Skill3CounterOn()
    {
        // 점프 시작하는 순간에 푸른 Material로 전환
    }

    public void Skill3CounterOff()
    {
        // 착치할 때 원래 Material로 전환
    }


}
