using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestCon : MonoBehaviour
{
	void Update()
    {
		if (Input.GetKey(KeyCode.W))
		{
			transform.position -= new Vector3(0.0f, 0.0f, 1.0f) * 5 * Time.deltaTime;
		}
		// s->뒤
		if (Input.GetKey(KeyCode.S))
		{
			transform.position += new Vector3(0.0f, 0.0f, 1.0f) * 5 * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.position += new Vector3(1.0f, 0.0f, 0.0f) * 5 * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * 5 * Time.deltaTime;
		}

	}
}
