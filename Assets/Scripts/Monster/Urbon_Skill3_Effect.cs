using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urbon_Skill3_Effect : MonoBehaviour
{
    [SerializeField] private SphereCollider skill3Collider;
	[SerializeField] private ParticleSystem[] skill3Particles;
	private bool played = false;

	private void Start()
	{
		if (skill3Collider == null)
		{
			skill3Collider = GameObject.Find("Skill3Collider").GetComponent<SphereCollider>();
		}
	}

	private void Update()
	{
		if (skill3Collider.enabled && !played)
		{
			for (int i = 0; i < skill3Particles.Length; i++)
			{
				skill3Particles[i].Play();
			}

			played = true;
		}
		else if (!skill3Collider.enabled && played)
		{
			for (int i = 0; i < skill3Particles.Length; i++)
			{
				skill3Particles[i].Stop();
			}
			played = false;
		}
	}
}
