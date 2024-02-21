using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AppearState : IBossMonsterState
{
    public IBossMonsterState DoState(KhururuOrigin monster)
    {
        monster.nav.isStopped = true;

        if (monster.patience != 0)
        {
            return monster.appearState;
        }
        else
        {
            //if (monster.animator.parameters.Any(p => p.name == "FirstHit"))
            //{
                monster.animator.SetTrigger("FirstHit");
                monster.animator.SetTrigger("Trip");
            //}

            return monster.idleState;
        }
    }
}
