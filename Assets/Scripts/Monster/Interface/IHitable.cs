using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public enum HitType
{
    None,
    Stagger,
    Trip
}
public interface IHitable_Monster
{
    public void TakeHit(float damage, HitType hitType, GameObject hitParticle = null);
=======
public interface IHitable
{
    public enum HitType
    {
        None,
        Stagger,
        Trip
    }

    public void TakeHit(int damage, HitType hitType = HitType.None, GameObject hitParticle = null);
>>>>>>> Sample
}
