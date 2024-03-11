using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractKeyObject : MonoBehaviour,IInteractable
{
    [SerializeField]
    private UnityEvent playerHere;
    [SerializeField]
    private ParticleSystem disableParticle;
    [SerializeField]
    private LayerMask Player;

    private BoxCollider coll;


    bool willBeDisappear;


    private void Awake()
    {
        coll = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        Vector3 vec = transform.position + coll.center;
        if (!willBeDisappear)
        {   
            if (Physics.OverlapBox(vec, coll.bounds.extents, Quaternion.identity, Player).Length >= 1)
            {
                Interact();
            }
        }
    }
    public void Interact()
    {
        willBeDisappear = true;
        playerHere.Invoke();
        StartCoroutine(CountSec());
    }

    IEnumerator CountSec()
    {
        ParticleSystem ther = Instantiate(disableParticle,transform.position,Quaternion.identity);
        yield return new WaitForSeconds(2);
        ther.Stop();
        gameObject.SetActive(false);
    }

}
