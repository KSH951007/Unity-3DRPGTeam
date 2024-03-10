using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin_Skill1_Effect : MonoBehaviour
{
	[SerializeField] private BoxCollider skill1Collider;
	[SerializeField] private GameObject effect;

	private void Start()
	{
		if (skill1Collider == null)
		{
			skill1Collider = GetComponentInParent<BoxCollider>();
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
