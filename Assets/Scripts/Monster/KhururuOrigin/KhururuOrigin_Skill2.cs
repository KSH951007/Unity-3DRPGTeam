using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KhururuOrigin_Skill2 : MonoBehaviour
{
	Renderer _renderer;
	Camera _camera;
	[SerializeField] private KhururuOrigin shieldState;

    private void Start()
	{
		_camera = Camera.main;
		_renderer = GetComponent<Renderer>();
	}

	private void Update()
	{
		if (!gameObject.activeSelf && shieldState.curShieldAmount > 0)
		{
			gameObject.SetActive(true);
		}

		Vector3 screenPoint = _camera.WorldToScreenPoint(transform.position);
		screenPoint.x = screenPoint.x / Screen.width;
		screenPoint.y = screenPoint.y / Screen.height;
		_renderer.material.SetVector("_skillScreenPosition", screenPoint);

		if (gameObject.activeSelf && shieldState.shieldBroken)
		{
			gameObject.SetActive(false);
		}
	}

	private void OnDisable()
	{
		//TODO : 실드 깨지는 이펙트 추가;
	}
}
