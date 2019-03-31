using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[HideInInspector] public float speed;
	public float lifeTime;
	public bool ennemy;

	void Start()
	{
		Vector3 rot = transform.rotation.eulerAngles;
		rot.z -= 90;
		transform.rotation = Quaternion.Euler(rot);
		if (lifeTime > 0.0f)
			StartCoroutine(RoutineDestroy());
	}

	void Update ()
	{
		transform.position += transform.right * speed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "weapon")
			return ;
		if (!ennemy && other.gameObject.tag != "Player")
			Destroy(gameObject);
		if (ennemy && other.gameObject.tag != "enemi")
			Destroy(gameObject);
		if (!ennemy && other.GetComponent<Ennemi>() != null)
		{
			other.GetComponent<Ennemi>().Die();
		}
		if (ennemy && other.GetComponent<Player>() != null)
		{
			other.GetComponent<Player>().Die();
		}
	}
	

	IEnumerator RoutineDestroy()
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy(gameObject);
	}
}
