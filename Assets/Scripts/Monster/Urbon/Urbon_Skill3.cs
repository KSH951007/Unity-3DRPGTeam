using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urbon_Skill3 : MonoBehaviour
{
    [SerializeField] private SphereCollider skill3Collider;
    [SerializeField] private int skill3Damage;
    [SerializeField] private LayerMask attackTargetLayer;
    Coroutine damageCoroutine;
    Collider[] hitPlayer = new Collider[1];

    private void Update()
    {
        if (skill3Collider.enabled)
        {
            Vector3 collCenter = skill3Collider.transform.position + skill3Collider.center;

            hitPlayer =
            Physics.OverlapSphere(collCenter, skill3Collider.radius, attackTargetLayer);


            print(hitPlayer.Length);
        }

        if (skill3Collider.enabled && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(GiveDamage());
        }
        else if (damageCoroutine != null && !skill3Collider.enabled || damageCoroutine != null && hitPlayer.Length == 0)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator GiveDamage()
    {
        if (hitPlayer.Length != 0)
        {
            while(hitPlayer.Length != 0)
            {
                if (hitPlayer[0].transform.gameObject.TryGetComponent(out IHitable health))
                {
                    health.TakeHit(skill3Damage, IHitable.HitType.None);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
    }

}
