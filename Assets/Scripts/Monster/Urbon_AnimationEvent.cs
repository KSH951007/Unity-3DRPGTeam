using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Urbon_AnimationEvent : MonoBehaviour
{
	public SphereCollider attackCollider;
	public SphereCollider skill3Collider;

	public GameObject skill1;
	public ParticleSystem skill1Particle;
	public GameObject[] skill2;
	public GameObject skill2Effect;

	public NavMeshAgent urbonNav;

	private bool alreadyOn;
	private float slowDownSpeed = 2f;

	public void PlaySkill1Particle()
	{
		skill1Particle.Play();
	}

	public void OnSkill2Effect()
	{
		skill2Effect.SetActive(true);
	}

	public void OffSkill2Effect()
	{
		skill2Effect.SetActive(false);
	}

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
		if (alreadyOn)
		{
			return;
		}
		alreadyOn = true;
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
		if (alreadyOn)
		{
			return;
		}
		urbonNav.isStopped = false;
		urbonNav.speed -= slowDownSpeed;
	}

	public void DeActiveNav()
	{
		urbonNav.isStopped = true;
		urbonNav.speed += slowDownSpeed;
	}

	public void ResetAlreadyOn()
	{
		alreadyOn = false;
	}
}
