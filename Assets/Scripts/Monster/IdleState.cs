using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleState : IBossMonsterState
{
    public IBossMonsterState DoState(KhururuOrigin monster)
    {
        DetectPlayer(monster);

        if (monster.target != null)
        {
            return monster.chaseState;
        }
        else
        {
            return monster.idleState;
        }
    }

    private void DetectPlayer(KhururuOrigin monster)
    {
        Vector3 collCenter = monster.detectColl.transform.position + monster.detectColl.center;

        Collider[] detectedColl =
            Physics.OverlapSphere(collCenter, monster.detectColl.radius, monster.attackTargetLayer);


        if (monster.target == null && detectedColl[0] != null) 
        {
            monster.target = detectedColl[0].transform;
            Debug.Log(detectedColl[0].name);
        }
        else
        {
            return;
        }
    }

}
