using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KhururuTrans_AnimationEvent : MonoBehaviour
{
	public NavMeshAgent pushPlayer;
	public Transform KhururuTrans;
	public SphereCollider attack1Collider;
	public SphereCollider attack2Collider;
	public GameObject skill1Alert;
	public SphereCollider skill1Collider;
	public GameObject skill2_1Alert;
	public SphereCollider skill2_1Collider;
	public GameObject skill2_2Alert;
	public MeshCollider skill2_2Collider;
	public GameObject skill3;
	public KhururuTrans_Bullet bullet;
	public GameObject skill4Alert;
	public MeshCollider skill4Collider;

	public void OnPushPlayer()
	{
		pushPlayer.radius = 5f;
	}

	public void OffPushPlayer()
	{
		pushPlayer.radius = 1.4f;
	}

	public void TranslateBoss()
	{
		// skill2 사용 후 보스를 unitsphere 범위에서 랜덤한 위치로 이동
		//Vector3 targetPos = transform.position + Random.insideUnitSphere * 2;
		//targetPos.y = 0;

		//gameObject.GetComponentInParent<Transform>().position = targetPos;

		
	}
	/******************************************************/
	public void OnAttack1()
	{
		attack1Collider.enabled = true;
	}

	public void OffAttack1()
	{
		attack1Collider.enabled = false;

	}
	/******************************************************/

	public void OnAttack2()
	{
		attack2Collider.enabled = true;
	}
	public void OffAttack2()
	{
		attack2Collider.enabled = false;

	}
	/******************************************************/

	public void OnSkill1Alert()
	{
		skill1Alert.SetActive(true);
	}
	public void OffSkill1Alert()
	{
		skill1Alert.SetActive(false);

	}
	public void OnSkill1()
	{
		skill1Collider.enabled = true;
	}
	public void OffSkill1()
	{
		skill1Collider.enabled = false;

	}
	/******************************************************/
	public void OnSkill2_1Alert()
	{
		skill2_1Alert.SetActive(true);
	}
	public void OffSkill2_1Alert()
	{
		skill2_1Alert.SetActive(false);

	}
	public void OnSkill2_1()
	{
		skill2_1Collider.enabled = true;
	}
	public void OffSkill2_1()
	{
		skill2_1Collider.enabled = false;

	}
	/******************************************************/
	public void OnSkill2_2Alert()
	{
		skill2_2Alert.SetActive(true);

	}
	public void OffSkill2_2Alert()
	{
		skill2_2Alert.SetActive(false);

	}
	public void OnSkill2_2()
	{
		skill2_2Collider.enabled = true;

	}
	public void OffSkill2_2()
	{
		skill2_2Collider.enabled = false;

	}
	/******************************************************/
	public void OnSkill3()
	{
		skill3.SetActive(true);
	}
	public void HandOff()
	{
		bullet.shot = true;
	}
	/******************************************************/

	public void OnSkill4Alert()
	{
		skill4Alert.SetActive(true);
	}
	public void OffSkill4Alert()
	{
		skill4Alert.SetActive(false);

	}
	public void OnSkill4()
	{
		skill4Collider.enabled = true;
	}
	public void OffSkill4()
	{
		skill4Collider.enabled = false;

	}
}
