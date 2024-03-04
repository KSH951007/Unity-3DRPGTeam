using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Urbon_Skill2 : MonoBehaviour
{
	private Vector3 targetPos;
	private int skillDamage;

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
	}

	private void Randomize()
	{
		float randSize = Random.Range(0.3f, 2f);
		transform.localScale = new Vector3(randSize, randSize, randSize);
		targetPos = GameObject.Find("Player").transform.position + (Random.insideUnitSphere * 4);
		targetPos.y = 6f;

		transform.position = targetPos;
		alertPos.position = new Vector3(targetPos.x, 0.01f, targetPos.z);
		particlePos.position = new Vector3(targetPos.x, 0.01f, targetPos.z);
	}

	IEnumerator RockFall()
	{
		yield return new WaitForSeconds(clip.length);
		particle.Play();
		yield return null;

		if (hitPlayer.Length != 0 && hitPlayer[0] != null)
		{
			Debug.Log("ÇÃ·¹ÀÌ¾î ºÎµúÈû");

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
}
