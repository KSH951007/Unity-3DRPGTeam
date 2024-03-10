using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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

    bool willBeDisappear;

    private void Update()
    {
        if (!willBeDisappear)
        {
            if (Physics.OverlapSphere(transform.position, 2, Player).Length <= 1)
            {
                print("123");
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
