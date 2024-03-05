using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMob_ElfWatcher : TrashMob
{
    protected override void Start()
    {
        maxHp = 600;
        transform.position = spawnedPoint.position;

        base.Start();
    }
}
