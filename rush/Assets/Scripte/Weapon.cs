using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Sprite spriteGet;
	public GameObject bullet;
	public float fireRate;
	public float bulletSpeed;
	float fireRateTime;
	GameObject weaponPos;
	Vector2 posThrow;

	void Start()
	{

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
			fireRateTime += Time.deltaTime;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!weaponPos && Input.GetKeyDown(KeyCode.E))
			{
				Player player = other.gameObject.GetComponent<Player>();
				// gameObject.transform.parent = player.weaponPos.transform;
				// transform.rotation = Quaternion.identity;
				GetComponent<SpriteRenderer>().sprite = spriteGet;
				weaponPos = player.weaponPos;
			}
		}
	}
}