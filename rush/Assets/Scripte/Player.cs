using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 10.0f;

	void Update () {
		Vector3 newPos = transform.position;
		if (Input.GetKey(KeyCode.W))
			newPos.y += speed * Time.deltaTime;
		if (Input.GetKey(KeyCode.S))
			newPos.y -= speed * Time.deltaTime;
		if (Input.GetKey(KeyCode.D))
			newPos.x += speed * Time.deltaTime;
		if (Input.GetKey(KeyCode.A))
			newPos.x -= speed * Time.deltaTime;
		transform.position = newPos;

		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 dir = pos - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

		Vector3 posCamera = transform.position;
		posCamera.z = -10;
		Camera.main.transform.position = posCamera;
	}
}
