using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType { None }
public interface IHitable
{
    public void TakeHit(int damage, HitType hitType, GameObject hitParticle = null);
}
