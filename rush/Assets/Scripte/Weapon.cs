using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Sprite spriteGet;
	GameObject weaponPos;
	
	void Update ()
	{
		if (weaponPos)
		{
			transform.position = weaponPos.transform.position;
			transform.rotation = weaponPos.transform.rotation;
			if (Input.GetMouseButtonDown(2))
			{

			}
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
				transform.rotation = Quaternion.identity;
				GetComponent<SpriteRenderer>().sprite = spriteGet;
				weaponPos = player.weaponPos;
			}
		}
	}
}