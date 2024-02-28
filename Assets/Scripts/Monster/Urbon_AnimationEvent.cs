using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Urbon_AnimationEvent : MonoBehaviour
{
	public SphereCollider attackCollider;
	public SphereCollider skill3Collider;

	public GameObject skill1;
	public GameObject[] skill2;

	public NavMeshAgent urbonNav;


	public void OnSkill1()
	{
		skill1.SetActive(true);
	}

	public void OnSkill2()
	{
		for (int i = 0; i <  skill2.Length; i++)
		{
			skill2[i].SetActive(true);
		}
	}

	public void OnAttack()
	{
		attackCollider.enabled = true;
	}

	public void OnSkill3()
	{
		skill3Collider.enabled = true;
	}

	public void OffAttack()
	{
		attackCollider.enabled = false;
	}

	public void OffSkill3()
	{
		skill3Collider.enabled = false;
	}

	public void ActiveNav()
	{
		urbonNav.isStopped = false;
	}

	public void DeActiveNav()
	{
		urbonNav.isStopped = true;

	}
}
