using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
    {
        None,
        Stagger,
        Trip
    }
    public interface IHitable_Monster
    {

        public void TakeHit(float damage, HitType hitType, GameObject hitParticle = null);
    }
