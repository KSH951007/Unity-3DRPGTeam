using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KhururuTrans_Bullet : MonoBehaviour
{
    public Transform handPosition;
    public bool shot;   // �ִϸ��̼� �̺�Ʈ�� shot�� �ٲ���
    private float shotSpeed = 20;
    SphereCollider sphereCollider;

	private void OnEnable()
	{
        handPosition = GameObject.Find("Skill3BulletPosition").transform;
        sphereCollider = GetComponentInChildren<SphereCollider>();
        shot = false;
        sphereCollider.enabled = true;
        StartCoroutine(Reset());
	}

	void Update()
    {
        if (!shot)
        {
            transform.position = handPosition.position;
		}
        else
        {
            Vector3 throwDir = Vector3.forward;

            transform.Translate(throwDir * shotSpeed * Time.deltaTime);

            // TODO : �÷��̾ ���� ������ �Ҹ�
			Vector3 collCenter = sphereCollider.transform.position + sphereCollider.center;
			//Physics.OverlapSphere(collCenter, sphereCollider.radius, );

			//if (Physics.OverlapSphere)
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(3f);
        sphereCollider.enabled = false;
        gameObject.SetActive(false);
    }
}
