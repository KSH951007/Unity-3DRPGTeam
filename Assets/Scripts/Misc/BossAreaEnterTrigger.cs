using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class BossAreaEnterTrigger : MonoBehaviour
{
    [SerializeField] private HeroManager heroManager;
    private NavMeshObstacle obstacle;
    private BoxCollider myCollider;
    private PlayableDirector director;
    private bool isStart;
    private void Awake()
    {
        myCollider = GetComponent<BoxCollider>();
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
        director = transform.GetChild(0).GetComponent<PlayableDirector>();
        isStart = false;
        director.played += OnStartCutSceen;
    }
    private void Update()
    {
        //if (isStart)
        //{
        //    if (director.time >= director.duration * 0.99)
        //    {
        //        Debug.Log("paused");

        //        Transform heroTr = heroManager.GetMainHero().transform;
        //        heroTr.localPosition = Vector3.zero;
        //        heroTr.localRotation = Quaternion.identity;
        //        //obstacle.enabled = true;
        //        isStart = false;

        //    }

        //}

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            director.gameObject.SetActive(true);
            director.Play();
            //obstacle.enabled = true;
            myCollider.enabled = false;
        }
    }
    private void OnStartCutSceen(PlayableDirector newDirector)
    {
        if (director == newDirector)
        {
            isStart = true;
            return;
        }
    }
    public void ConfirmationHeroPosition()
    {
        Transform heroTr = heroManager.GetMainHero().transform;
        heroTr.localPosition = Vector3.zero;
        heroTr.localRotation = Quaternion.identity;
        //obstacle.enabled = true;
        Debug.Log("asdsadsad");
    }
}
