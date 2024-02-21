using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseState : IBossMonsterState
{
    enum DistanceToTarget
    {
        None,
        Long,
        Medium,
        Short
    }

    DistanceToTarget distance = DistanceToTarget.None;

    public IBossMonsterState DoState(KhururuOrigin monster)
    {
        Debug.Log("체이스스테이트 진입");
        monster.nav.SetDestination(monster.target.position);
        Debug.Log(monster.nav.remainingDistance);

        distance = GetDistanceToTarget(monster);

        Debug.Log(distance);

        switch (distance)
        {
            case DistanceToTarget.Long:
                ChaseLongDistance(monster);
                return monster.chaseState;

            case DistanceToTarget.Medium:
                ChaseMediumDistance(monster);
                return monster.attackState;

            case DistanceToTarget.Short:
                return monster.attackState;

            default:                 
                return monster.chaseState;
        }
    }

    private DistanceToTarget GetDistanceToTarget(KhururuOrigin monster)
    {
        if (monster.nav.remainingDistance >= 6f)
        {
            return DistanceToTarget.Long;
        }
        else if (monster.nav.remainingDistance < 6f && monster.nav.remainingDistance >= 1f)
        {
            return DistanceToTarget.Medium;
        }
        else { return DistanceToTarget.Short; }
    }

    private void ChaseLongDistance(KhururuOrigin monster)
    {
        monster.nav.isStopped = false;
    }

    private void ChaseMediumDistance(KhururuOrigin monster)
    {
        monster.SetChasingTime();
        while (monster.chasingTime - Time.time < 0f)
        {
            monster.nav.isStopped = false;
        }
    }
}
