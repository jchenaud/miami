﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour {
	public List<Sprite> listHead;
	public List<Sprite> listBody;
	public SpriteRenderer head;
	public SpriteRenderer body;
	GameObject player;
	public float speed;
	Rigidbody2D  rb;
	public bool fight = false;

	public int room;
	public GameObject room_manager;

	public bool shoot;

	Vector2 vel;
	Vector2 target_dir;
	void Start ()
	{
		fight = false;
		player  = GameObject.Find("player");
		room_manager  = GameObject.Find("Room_manager");

		Debug.Log(player);
		rb = GetComponent<Rigidbody2D>();
		head.sprite = listHead[Random.Range(0, listHead.Count - 1)];
		body.sprite = listBody[Random.Range(0, listBody.Count - 1)];
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
			 vel = Vector2.zero;
			 target_dir = player.transform.position - transform.position;
			vel = target_dir * 1;
				if (Vector2.Distance(transform.position, player.transform.position) < 4f)
				{
					rb.velocity = Vector2.zero;
				}
				else
				{
					rb.velocity = vel * speed;
					
				}
			//Vector3 dir = player.transform.position - transform.position;
			//transform.rotation;
			float angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle + 90 , Vector3.forward);
		}
		// Debug.Log(room_manager.GetComponent<Room_manager>().all_room);
		// Debug.Log(room);
		// Debug.Log(player.name);
		// Debug.Log(player.GetComponent<Player>().room);
		else if (shoot)
		{
			Vector2 posi = new Vector2();
			posi = room_manager.GetComponent<Room_manager>().nexto_find_position_door(room,player.GetComponent<Player>().room);
			if (posi != Vector2.positiveInfinity)
			{
				vel = Vector2.zero;
				target_dir = posi - (Vector2) transform.position;
				vel = target_dir * speed;
				rb.velocity = new Vector2( Mathf.Clamp(vel.x,-2,2),Mathf.Clamp(vel.y,-2,2));
				float angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle + 90 , Vector3.forward);
				if (Vector2.Distance(transform.position, posi) < 0.2f){
					room = player.GetComponent<Player>().room;
					
					 print("loock player");
			 		target_dir = player.transform.position - transform.position;
					angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.AngleAxis(angle +90 , Vector3.forward);
					Debug.Break();
					shoot = false;
					fight =  true;
				}
			}

		}
	}
}
