using Assets.Scripts.Monster.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urbon_Skill2 : MonoBehaviour
{
	private Vector3 targetPos;
	private int skillDamage;
	private float fallSpeed = 8f;
	public AnimationClip clip;
	private SphereCollider skill2Collider;
	private bool hitSomething = false;

	public LayerMask attackTargetLayer;


	private void OnEnable()
	{
		skill2Collider = GetComponentInChildren<SphereCollider>();

		Randomize();
		StartCoroutine(RockFall());
	}
	private void Update()
	{
		HitSomething();
	}

	private void Randomize()
	{
		float randSize = Random.Range(0.5f, 3f);
		transform.localScale = new Vector3(randSize, randSize, randSize);
		targetPos = GameObject.Find("Player").transform.position + Random.insideUnitSphere;
		targetPos.y = 6f;

		transform.position = targetPos;
	}

	IEnumerator RockFall()
	{
		yield return new WaitForSeconds(clip.length);
		while (!hitSomething)
		{
			transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
		}
	}

	private void HitSomething()
	{
		Vector3 collCenter = skill2Collider.transform.position + skill2Collider.center;

		Collider[] detectedColl =
		Physics.OverlapSphere(collCenter, skill2Collider.radius, attackTargetLayer);

		if (detectedColl.Length != 0)
		{
			Debug.Log("ºÎµúÈû");

			if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
			{
				health.TakeHit(skillDamage, HitType.None);
			}

			hitSomething = true;
			gameObject.SetActive(false);

		}
	}
}
