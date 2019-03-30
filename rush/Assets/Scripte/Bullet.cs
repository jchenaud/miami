using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[HideInInspector] public float speed;
	
	void Update ()
	{
		transform.position += -transform.up * speed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.tag != "Player" && other.gameObject.tag != "weapon")
		{
			Debug.Log(other.gameObject.name);
			Destroy(gameObject);
		}
	}
}
