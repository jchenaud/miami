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
	public static Action onLooseGameEvent;
	public static Action onShootGameEvent;
	public bool win;
	public AudioClip clipWin;
	public AudioClip clipLoose;
	AudioSource audioSource;
	public int room;
	public bool die;
	public int kill;

	void Awake()
	{
		onWinGameEvent = null;
		onLooseGameEvent = null;
		onShootGameEvent = null;
		instance = this;
	}

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}

	void FixedUpdate()
	{
		if (win || die)
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
		if (win || die)
			return ;
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 dir = pos - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

		Vector3 posCamera = transform.position;
		posCamera.z = -10;
		Camera.main.transform.position = posCamera;
	}

	public void Die()
	{
		if (win || die)
			return ;
		audioSource.PlayOneShot(clipLoose);
		Debug.Log("loose");
		if (onLooseGameEvent != null)
			onLooseGameEvent();
		GetComponent<SpriteRenderer>().sprite = null;
		die = true;
	}

	void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.tag == "end car")
		{
			if (win)
				return ;
			audioSource.PlayOneShot(clipWin);
			Debug.Log("win");
			if (onWinGameEvent != null)
				onWinGameEvent();
			win = true;
		}
	}
}