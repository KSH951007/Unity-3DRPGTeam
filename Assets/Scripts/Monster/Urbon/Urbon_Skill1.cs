using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urbon_Skill1 : MonoBehaviour
{
	private int skillDamage = 6;
	public float moveSpeed = 20f;
	[SerializeField] private Transform player;
	public Transform startPoint;
	private Vector3 moveDir;
	private SphereCollider skill1Collider;
	public AnimationClip clip;
	[SerializeField] private bool gaveDamage = false;
	Collider[] hitPlayer = new Collider[1];

	public LayerMask attackTargetLayer;

	private void OnEnable()
	{
		gaveDamage = false;
		startPoint = GameObject.Find("Skill1StartPoint").transform;
		transform.position = startPoint.position;
		skill1Collider = GetComponentInChildren<SphereCollider>();
		StartCoroutine(ResetSkill());
	}

	private void Update()
	{
		player = GameObject.Find("Urbon (1)").GetComponent<BossMonsters>().target;
		moveDir = (player.position - transform.position).normalized;

		transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);

		if (skill1Collider.enabled)
		{
			Vector3 collCenter = skill1Collider.transform.position + skill1Collider.center;

			hitPlayer =
			Physics.OverlapSphere(collCenter, skill1Collider.radius, attackTargetLayer);
			if(!gaveDamage)
			{
				gaveDamage = true;
				StartCoroutine(GiveDamage());
			}
		}
	}

	private IEnumerator ResetSkill()
	{
		yield return new WaitForSeconds(clip.length * 3);
		gameObject.SetActive(false);
	}

	private IEnumerator GiveDamage()
	{
		if (hitPlayer.Length != 0)
		{
			if (hitPlayer[0].transform.gameObject.TryGetComponent(out IHitable health))
			{
				health.TakeHit(skillDamage, IHitable.HitType.None);
				yield return new WaitForSeconds(0.2f);
				health.TakeHit(skillDamage, IHitable.HitType.None);
				yield return new WaitForSeconds(0.2f);
				health.TakeHit(skillDamage, IHitable.HitType.None);
				yield return new WaitForSeconds(0.1f);
				health.TakeHit(skillDamage, IHitable.HitType.None);
			}
		}
		gaveDamage = false;
	}

}
