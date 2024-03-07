using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KhururuTrans_Bullet : MonoBehaviour
{
    public Transform handPosition;
    public bool shot;   // �ִϸ��̼� �̺�Ʈ�� shot�� �ٲ���
    private float shotSpeed = 20;
    private int skillDamage = 30;
    SphereCollider sphereCollider;
    public LayerMask attackTargetLayer;

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
			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, sphereCollider.radius, attackTargetLayer);
			if (detectedColl.Length != 0)
			{
				if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(skillDamage);
                    gameObject.SetActive(false);
				}
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
		}
    }
}
