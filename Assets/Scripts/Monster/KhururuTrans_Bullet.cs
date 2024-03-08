using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KhururuTrans_Bullet : MonoBehaviour
{
    public Transform handPosition;
    [SerializeField] private Transform attackTarget;
    public bool shot;   // 애니메이션 이벤트로 shot을 바꿔줌
    private float shotSpeed = 10;
    private int skillDamage = 30;
    SphereCollider sphereCollider;
    public LayerMask attackTargetLayer;
    Vector3 throwDir;
	private bool following = false;

	private void OnEnable()
	{
        handPosition = GameObject.Find("Skill3BulletPosition").transform;
        sphereCollider = GetComponentInChildren<SphereCollider>();
        shot = false;
		StartCoroutine(Reset());
	}

	void Update()
    {
		attackTarget = GetComponentInParent<BossMonsters>().target;

		if (!shot)
        {
            transform.position = handPosition.position;
		}
        else
        {
			throwDir = (attackTarget.position - transform.position).normalized;

			if (!following)
			{
				StartCoroutine(ChaseTarget());
				following = true;
			}
		}
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(3f);
        if (gameObject.activeSelf)
        {
			sphereCollider.enabled = false;
			gameObject.SetActive(false);
			following = false;
		}
	}

    private IEnumerator ChaseTarget()
    {
		while (gameObject.activeSelf)
		{

			transform.Translate(throwDir * shotSpeed * Time.deltaTime, Space.World);

			// TODO : 플레이어나 벽에 닿으면 소멸
			Vector3 collCenter = sphereCollider.transform.position + sphereCollider.center;
			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, sphereCollider.radius, attackTargetLayer);

			if (detectedColl.Length != 0)
			{
				if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(skillDamage);
					gameObject.SetActive(false);
					following = false;

				}
			}

			yield return null;
		}
	}
}
