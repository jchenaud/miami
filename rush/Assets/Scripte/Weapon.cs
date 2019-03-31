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
	public AudioClip fireSound;
	public AudioClip ejectSound;
	public bool ennemy;
	AudioSource audioSource;
	float fireRateTime;
	Sprite initSprite;
	public GameObject weaponPos;
	Vector2 posThrow;
	SpriteRenderer spriteRenderer;
	float throwSpeed;
	float rotationZ;
	int defaultLayer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		initSprite = spriteRenderer.sprite;
		audioSource = GetComponent<AudioSource>();
		defaultLayer = gameObject.layer;
	}

	void Update ()
	{
		if (ennemy)
		{
			spriteRenderer.sprite = spriteGet;
		}
		if (weaponPos)
		{
			transform.position = weaponPos.transform.position;
			transform.rotation = weaponPos.transform.rotation;
			if (!ennemy)
			{
				if (Input.GetMouseButton(0))
					Shoot();
				if (Input.GetMouseButton(1))
				{
					gameObject.layer = 0;
					Throw(Camera.main.ScreenToWorldPoint(Input.mousePosition));
				}
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
		if (throwSpeed == 0.0f)
			gameObject.layer = defaultLayer;
	}

	public void Throw(Vector3 pos)
	{
		spriteRenderer.sprite = initSprite;
		weaponPos = null;
		posThrow = pos;
		throwSpeed = 10.0f;
		if (!ennemy)
		{
			player.haveWeapon = false;
			GameUi.instance.weaponCurrent = null;
			audioSource.PlayOneShot(ejectSound, 0.3f);
		}
	}

	public void Shoot()
	{
		if (!infinityAmmos)
		{
			if (!ennemy && ammos <= 0)
				return ;
		}
		if (fireRateTime <= fireRate)
			return ;
		audioSource.PlayOneShot(fireSound, 0.3f);
		fireRateTime = 0.0f;
		GameObject go = Instantiate(bullet);
		go.transform.position = weaponPos.transform.position;
		go.transform.rotation = weaponPos.transform.rotation;
		go.GetComponent<Bullet>().speed = bulletSpeed;
		go.GetComponent<Bullet>().ennemy = ennemy;
		if (!ennemy)
		{
			ammos -= 1;
			if (Player.onShootGameEvent != null && nameWeapon != "Saber")
				Player.onShootGameEvent();
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (nameWeapon == "Saber" && throwSpeed > 0.0f && other.GetComponent<Ennemi>())
		{
			other.GetComponent<Ennemi>().Die();
		}
		if (other.gameObject.tag == "Player")
		{
			if (!weaponPos && Input.GetKeyDown(KeyCode.E) && !ennemy)
			{
				audioSource.PlayOneShot(ejectSound, 0.1f);
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