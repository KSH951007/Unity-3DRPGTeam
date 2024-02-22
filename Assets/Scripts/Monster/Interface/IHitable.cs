using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
	public enum HitType
	{
		None,
		Stagger,
		Trip
	}

	public void TakeHit(float damage, HitType hitType, GameObject hitParticle = null);
}
