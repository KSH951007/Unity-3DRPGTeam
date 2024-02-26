using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuTrans_Bullet : MonoBehaviour
{
    public Transform handPosition;
    public Transform playerPosition;
    public bool shot;   // 애니메이션 이벤트로 shot을 바꿔줌
    private float shotSpeed = 6;
    SphereCollider sphereCollider;

	private void OnEnable()
	{
        handPosition = GameObject.Find("Skill3BulletPosition").transform;
        playerPosition = GameObject.Find("Player").transform;
        sphereCollider = GetComponent<SphereCollider>();
	}

	void Update()
    {
        if (!shot)
        {
            transform.position = handPosition.position;
		}
        else
        {
            Vector3 throwDir = (playerPosition.position - handPosition.position).normalized;

            transform.Translate(throwDir * shotSpeed * Time.deltaTime);

            // TODO : 플레이어나 벽에 닿으면 소멸
			Vector3 collCenter = sphereCollider.transform.position +sphereCollider.center;
			//Physics.OverlapSphere(collCenter, sphereCollider.radius, );

			//if (Physics.OverlapSphere)
        }
    }
}
