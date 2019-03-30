using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	Player player;
	public Sprite spriteGet;
	public GameObject bullet;
	public float fireRate;
	public float bulletSpeed;
	public int ammos;
	public bool infinityAmmos;
	public string nameWeapon;
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
			if (Input.GetMouseButton(0) && fireRateTime > fireRate && (ammos > 0 || infinityAmmos))
				Shoot();
			if (Input.GetMouseButton(1) && fireRateTime > fireRate)
				Throw();
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

	void Throw()
	{
		spriteRenderer.sprite = initSprite;
		weaponPos = null;
		posThrow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		throwSpeed = 10.0f;
		player.haveWeapon = false;
		GameUi.instance.weaponCurrent = null;
	}

	void Shoot()
	{
		fireRateTime = 0.0f;
		GameObject go = Instantiate(bullet);
		go.transform.position = weaponPos.transform.position;
		go.transform.rotation = weaponPos.transform.rotation;
		go.GetComponent<Bullet>().speed = bulletSpeed;
		ammos -= 1;
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!weaponPos && Input.GetKeyDown(KeyCode.E))
			{
				player = other.gameObject.GetComponent<Player>();
				if (player.haveWeapon)
					return ;
				spriteRenderer.sprite = spriteGet;
				weaponPos = player.weaponPos;
				player.haveWeapon = true;
				GameUi.instance.weaponCurrent = this;
			}
		}
		else
			throwSpeed = 0.0f;
	}
}