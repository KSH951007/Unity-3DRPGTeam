using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin_AttackEffect : MonoBehaviour
{
	[SerializeField] private SphereCollider attack1Collider;
	[SerializeField] private GameObject effect;

	private void Update()
	{
		if (attack1Collider.enabled && !effect.activeSelf)
		{
			effect.SetActive(true);
		}
		else if (!attack1Collider.enabled)
		{
			effect.SetActive(false);
		}
	}
}
