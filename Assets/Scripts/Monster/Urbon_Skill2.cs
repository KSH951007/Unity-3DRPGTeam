using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using Unity.VisualScripting;
>>>>>>> Sample
using UnityEngine;

public class Urbon_Skill2 : MonoBehaviour
{
	private Vector3 targetPos;
<<<<<<< HEAD
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
=======
	private int skillDamage = 50;

	public Transform alertPos;
	public Transform particlePos;
	public ParticleSystem particle;
	public AnimationClip clip;

	public SphereCollider damageCollider;
	public LayerMask playerLayer;
	Collider[] hitPlayer = new Collider[1];

	private void OnEnable()
	{
		Randomize();
		StartCoroutine(RockFall());
		StartCoroutine(OutMap());
	}
	private void Update()
	{
		DetectPlayer();
>>>>>>> Sample
	}

	private void Randomize()
	{
<<<<<<< HEAD
		float randSize = Random.Range(0.5f, 3f);
		transform.localScale = new Vector3(randSize, randSize, randSize);
		targetPos = GameObject.Find("Player").transform.position + Random.insideUnitSphere;
		targetPos.y = 6f;

		transform.position = targetPos;
=======
		float randSize = Random.Range(0.3f, 2f);
		transform.localScale = new Vector3(randSize, randSize, randSize);
		targetPos = GameObject.Find("Player").transform.position + (Random.insideUnitSphere * 4);
		targetPos.y = 6f;

		transform.position = targetPos;
		alertPos.position = new Vector3(targetPos.x, 0.01f, targetPos.z);
		particlePos.position = new Vector3(targetPos.x, 0.01f, targetPos.z);
>>>>>>> Sample
	}

	IEnumerator RockFall()
	{
		yield return new WaitForSeconds(clip.length);
<<<<<<< HEAD
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

			if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable_Monster health))
			{
				health.TakeHit(skillDamage, HitType.None);
			}

			hitSomething = true;
			gameObject.SetActive(false);

		}
	}
=======
		particle.Play();
		yield return null;

		if (hitPlayer.Length != 0 && hitPlayer[0] != null)
		{
			if (hitPlayer[0].transform.gameObject.TryGetComponent(out IHitable health))
			{
				health.TakeHit(skillDamage, IHitable.HitType.None);
				gameObject.SetActive(false);
			}
		}
	}

	IEnumerator OutMap()
	{
		yield return new WaitForSeconds(3f);
		if (gameObject.activeSelf)
		{
			gameObject.SetActive(false);
		}
	}

	private void DetectPlayer()
	{
		damageCollider.transform.position = new Vector3(targetPos.x, 0.01f, targetPos.z);

		Vector3 collCenter = damageCollider.transform.position + damageCollider.center;

		hitPlayer =
			Physics.OverlapSphere(collCenter, damageCollider.radius, playerLayer);
	}
>>>>>>> Sample
}
