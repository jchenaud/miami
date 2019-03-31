using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapon : MonoBehaviour
{
	public List<GameObject> listWeapon;
	public bool ennemy;

	void Start()
	{
		GameObject go = Instantiate(listWeapon[Random.Range(0, listWeapon.Count)], transform.position, transform.rotation);
		if (ennemy)
		{
			go.GetComponent<Weapon>().ennemy = true;
			go.GetComponent<Weapon>().weaponPos = gameObject;
		}
	}
}
