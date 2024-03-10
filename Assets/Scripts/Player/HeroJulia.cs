using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJulia : Hero
{
 
    protected override void Awake()
    {
   
        base.Awake();
        attackAction = new HeroJuliaAttackAction(scheduler,animator, this);
    }
    // Start is called before the first frame update
  

}
