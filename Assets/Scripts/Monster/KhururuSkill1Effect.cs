using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuSkill1Effect : MonoBehaviour
{
	[SerializeField] private SphereCollider skill1Collider;
	[SerializeField] private GameObject effect;

	private void Start()
	{
		if (skill1Collider == null)
		{
			skill1Collider = GetComponentInParent<SphereCollider>();
		}
	}

	private void Update()
	{
		if (skill1Collider.enabled && !effect.activeSelf)
		{
			effect.SetActive(true);
		}
		else if (!skill1Collider.enabled)
		{
			effect.SetActive(false);
		}
	}
}
