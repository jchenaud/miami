using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
	public float speed = 10.0f;
	public GameObject weaponPos;
	public bool haveWeapon;
	Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		Vector2 vel = Vector2.zero;
		if (Input.GetKey(KeyCode.W))
			vel.y = speed;
		if (Input.GetKey(KeyCode.S))
			vel.y = -speed;
		if (Input.GetKey(KeyCode.D))
			vel.x = speed;
		if (Input.GetKey(KeyCode.A))
			vel.x = -speed;
		rb.velocity = vel;
		
	}

	void Update () {

		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 dir = pos - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

		Vector3 posCamera = transform.position;
		posCamera.z = -10;
		Camera.main.transform.position = posCamera;
	}
}