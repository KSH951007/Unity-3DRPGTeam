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
	public SphereCollider skill1Collider;
	public GameObject skill1Alert;
	public GameObject skill2_1Alert;
	public GameObject skill2_2Alert;
	public GameObject skill3;
	public MeshCollider skill4Collider;
	public GameObject skill4Alert;

	public void OnPushPlayer()
	{
		pushPlayer.radius = 5f;
	}

	public void OffPushPlayer()
	{
		pushPlayer.radius = 1.4f;

	}
}
