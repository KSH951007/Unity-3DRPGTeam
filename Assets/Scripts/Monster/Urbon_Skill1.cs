using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urbon_Skill1 : MonoBehaviour
{
	private int skillDamage = 1;
	public float moveSpeed = 20f;
	private Transform player;
	public Transform startPoint;
	private Vector3 moveDir;
	private SphereCollider skill1Collider;
	public AnimationClip clip;

	public LayerMask attackTargetLayer;

	private void OnEnable()
	{
		startPoint = GameObject.Find("Skill1StartPoint").transform;
		transform.position = startPoint.position;
		skill1Collider = GetComponentInChildren<SphereCollider>();
		StartCoroutine(ResetSkill());
	}

	private void Update()
	{
		player = GameObject.Find("Player").transform;
		moveDir = (player.position - transform.localPosition).normalized;

		transform.Translate(moveDir * moveSpeed * Time.deltaTime);

		if (skill1Collider.enabled)
		{
			Vector3 collCenter = skill1Collider.transform.position + skill1Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, skill1Collider.radius, attackTargetLayer);

			if (detectedColl.Length != 0)
			{
				if(detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(skillDamage);
				}
			}
		}
	}

	private IEnumerator ResetSkill()
	{
		yield return new WaitForSeconds(clip.length * 3);
		gameObject.SetActive(false);
	}

}
