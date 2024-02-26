using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuTrans_Bullet : MonoBehaviour
{
    public Transform handPosition;
    public Transform playerPosition;
    public bool shot;   // �ִϸ��̼� �̺�Ʈ�� shot�� �ٲ���
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

            // TODO : �÷��̾ ���� ������ �Ҹ�
			Vector3 collCenter = sphereCollider.transform.position +sphereCollider.center;
			//Physics.OverlapSphere(collCenter, sphereCollider.radius, );

			//if (Physics.OverlapSphere)
        }
    }
}
