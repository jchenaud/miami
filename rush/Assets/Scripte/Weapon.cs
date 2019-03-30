﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Sprite spriteGet;
	public GameObject bullet;
	public float fireRate;
	public float bulletSpeed;
	float fireRateTime;
	Sprite initSprite;
	GameObject weaponPos;
	Vector2 posThrow;
	SpriteRenderer spriteRenderer;
	float throwSpeed;
	float rotationZ;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		initSprite = spriteRenderer.sprite;
	}

	void Update ()
	{
		if (weaponPos)
		{
			transform.position = weaponPos.transform.position;
			transform.rotation = weaponPos.transform.rotation;
			if (Input.GetMouseButton(0) && fireRateTime > fireRate)
			{
				fireRateTime = 0.0f;
				GameObject go = Instantiate(bullet);
				go.transform.position = weaponPos.transform.position;
				go.transform.rotation = weaponPos.transform.rotation;
				go.GetComponent<Bullet>().speed = bulletSpeed;
			}
			if (Input.GetMouseButton(1) && fireRateTime > fireRate)
			{
				spriteRenderer.sprite = initSprite;
				weaponPos = null;
				posThrow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				throwSpeed = 10.0f;
			}
			fireRateTime += Time.deltaTime;
		}
		else if (throwSpeed > 0.0f)
		{
			transform.position = Vector2.MoveTowards(transform.position, posThrow, throwSpeed * Time.deltaTime);
			transform.rotation = Quaternion.Euler(0, 0, rotationZ);
			rotationZ -= 20.0f;
			if (Vector2.Distance(transform.position, posThrow) < 0.1f)
				throwSpeed = 0.0f;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!weaponPos && Input.GetKeyDown(KeyCode.E))
			{
				Player player = other.gameObject.GetComponent<Player>();
				spriteRenderer.sprite = spriteGet;
				weaponPos = player.weaponPos;
			}
		}
		else
			throwSpeed = 0.0f;
	}
}