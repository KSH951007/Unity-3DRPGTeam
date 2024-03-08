using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Urbon_Skill2 : MonoBehaviour
{
	[SerializeField] private Transform targetPos;
	private Vector3 fallPos;
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
		targetPos = GameObject.Find("Heros").transform;

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
		fallPos = targetPos.position + (Random.insideUnitSphere * 3);
		fallPos.y = 6f;

		transform.position = fallPos;
		alertPos.position = new Vector3(fallPos.x, 0.8f, fallPos.z);
		particlePos.position = new Vector3(fallPos.x, 0.8f, fallPos.z);
	}

	IEnumerator RockFall()
	{
		yield return new WaitForSeconds(clip.length);
		particle.Play();
		yield return new WaitForSeconds(0.7f);

		if (hitPlayer.Length != 0 && hitPlayer[0] != null)
		{
			if (hitPlayer[0].transform.gameObject.TryGetComponent(out IHitable health))
			{
				health.TakeHit(skillDamage, IHitable.HitType.None);
				yield return new WaitForSeconds(0.1f);
				health.TakeHit(skillDamage, IHitable.HitType.None);
				yield return new WaitForSeconds(0.1f);
				health.TakeHit(skillDamage, IHitable.HitType.None);
				yield return new WaitForSeconds(0.1f);
				health.TakeHit(skillDamage, IHitable.HitType.None);
			}
		}
	}

	IEnumerator OutMap()
	{
		yield return new WaitForSeconds(2f);
		if (gameObject.activeSelf)
		{
			gameObject.SetActive(false);
		}
	}

	private void DetectPlayer()
	{
		damageCollider.transform.position = new Vector3(fallPos.x, 0.8f, fallPos.z);
		
		Vector3 collCenter = damageCollider.transform.position + damageCollider.center;

		hitPlayer =
			Physics.OverlapSphere(collCenter, damageCollider.radius * transform.localScale.x, playerLayer);
	}
}
