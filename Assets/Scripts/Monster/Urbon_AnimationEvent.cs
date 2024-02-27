using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urbon_AnimationEvent : MonoBehaviour
{
	public GameObject skill1;

	public void OnSkill1()
	{
		skill1.SetActive(true);
	}
}
