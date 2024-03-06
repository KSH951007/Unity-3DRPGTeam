using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuTrans_Skill2_2 : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			print("dd");
		}
	}
}
