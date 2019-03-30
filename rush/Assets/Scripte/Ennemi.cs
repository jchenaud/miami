using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour {

	// // Use this for initialization
	// void Start () {
		
	// }
	
	// // Update is called once per frame
	// void Update () {
		
	// }
	GameObject player;
	Rigidbody2D  rb;
	public bool fight = false;
	void Start () {
		fight = false;
		player  = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
	}

	public void attack()
	{
		fight = true;
	}
	// public float tmp;
	// Update is called once per frame
	void Update () {
		if (fight == true)
		{
			Vector2 vel = Vector2.zero;
			Vector2 target_dir = player.transform.position - transform.position;
			vel = target_dir * 1;
			rb.velocity = vel;
			Vector3 dir = player.transform.position - transform.position;
			//transform.rotation;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle + 90 , Vector3.forward);
		}
	}
}
