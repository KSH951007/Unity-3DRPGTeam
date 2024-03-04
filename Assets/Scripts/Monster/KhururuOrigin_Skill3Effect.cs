using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin_Skill3Effect : MonoBehaviour
{
	[SerializeField] private SphereCollider skill3Collider;
	[SerializeField] private GameObject effect;

	private void Start()
	{
		if (skill3Collider == null)
		{
			skill3Collider = GetComponentInParent<SphereCollider>();
		}
	}

	private void Update()
	{
		if (skill3Collider.enabled && !effect.activeSelf)
		{
			effect.SetActive(true);
		}
		else if (!skill3Collider.enabled)
		{
			effect.SetActive(false);
		}
	}
}
