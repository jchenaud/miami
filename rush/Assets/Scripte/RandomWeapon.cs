using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeapon : MonoBehaviour
{
	public List<GameObject> listWeapon;

	void Start()
	{
		Instantiate(listWeapon[Random.Range(0, listWeapon.Count)], transform.position, transform.rotation);
	}
}
