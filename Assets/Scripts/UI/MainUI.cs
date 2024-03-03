using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    private HeroManager heroManager;

    private void Start()
    {
        Transform hero = GameObject.Find("Player").transform.Find("Heros");
        heroManager = hero.GetComponent<HeroManager>();

        heroManager.onChangeCharacter += ChangeHero;
    }


    public void ChangeHero()
    {

    }
}
