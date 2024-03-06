using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMob_Kkaebuli : TrashMob
{
    protected override void Start()
    {
        maxHp = 500;
        transform.position = spawnedPoint.position;

        base.Start();
    }
}
