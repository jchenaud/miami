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
	public static Player instance;
	public static Action onWinGameEvent;
	public bool win;

	void Awake()
	{
		onWinGameEvent = null;
		instance = this;
	}

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if (win)
		{
			rb.velocity = Vector2.zero;
			return ;
		}
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

	void Update ()
	{
		if (win)
			return ;
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 dir = pos - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

		Vector3 posCamera = transform.position;
		posCamera.z = -10;
		Camera.main.transform.position = posCamera;
	}

	void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.tag == "end car")
		{
			Debug.Log("win");
			if (onWinGameEvent != null)
				onWinGameEvent();
			win = true;
		}
	}
}