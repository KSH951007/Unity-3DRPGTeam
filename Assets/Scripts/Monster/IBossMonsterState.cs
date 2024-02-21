using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossMonsterState
{
    IBossMonsterState DoState(KhururuOrigin monster);
}
