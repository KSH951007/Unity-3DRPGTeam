using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
	[SerializeField] private GameObject doorClosed;
	[SerializeField] private GameObject boss;
	[SerializeField] private LayerMask playerLayer;
	private bool playerEntered;
	BoxCollider boxCollider;

	private void Awake()
	{
		boxCollider = GetComponent<BoxCollider>();
	}

	private void Update()
	{
		Vector3 collCenter = boxCollider.transform.position;

		Vector3 collHalfExtents = boxCollider.bounds.extents * 2;
		
		Collider[] detectedColl =
			Physics.OverlapBox(collCenter, collHalfExtents, Quaternion.identity, playerLayer);

		if (detectedColl.Length != 0)
		{
			playerEntered = true;
		}

		if (playerEntered)
		{
			playerEntered = false;

			StartCoroutine(CloseDoor());
		}
	}

	IEnumerator CloseDoor()
	{
		yield return new WaitForSeconds(0.5f);

		doorClosed.SetActive(true);
		yield return new WaitForSeconds(2f);

		boss.SetActive(true);

		gameObject.SetActive(false);
	}
}
