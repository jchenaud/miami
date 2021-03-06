﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour {
	public List<Sprite> listHead;
	public List<Sprite> listBody;
	public List<AudioClip> listSoundDie;
	public SpriteRenderer head;
	public SpriteRenderer body;
	GameObject player;
	public float speed;
	Rigidbody2D  rb;
	public bool fight = false;
	public GameObject weaponPos;
	public GameObject prefabBlood;

	public int room;
	public GameObject room_manager;

	public bool shoot;
	public Weapon weapon;
	public bool die;

	AudioSource audioSource;
	Vector2 vel;
	Vector2 target_dir;
	Vector2 posi;

	int player_tmp_room;
	void Start ()
	{
		fight = false;
		player  = GameObject.Find("player");
		room_manager  = GameObject.Find("Room_manager");

		rb = GetComponent<Rigidbody2D>();
		head.sprite = listHead[Random.Range(0, listHead.Count - 1)];
		body.sprite = listBody[Random.Range(0, listBody.Count - 1)];
		audioSource = GetComponent<AudioSource>();
		Player.onShootGameEvent += CheckShoot;
	}

	public void Die()
	{
		if (die)
			return ;
		GameObject go = Instantiate(prefabBlood);
		go.transform.position = transform.position;
		Player.onShootGameEvent -= CheckShoot;
		Player.instance.kill++;
		AudioClip clip = listSoundDie[Random.Range(0, listSoundDie.Count - 1)];
		audioSource.PlayOneShot(clip);
		weapon.Throw(transform.position);
		weapon.ennemy = false;
		head.sprite = null;
		body.sprite = null;
		Destroy(this.gameObject, clip.length);
		GetComponent<BoxCollider2D>().enabled = false;
		die = true;
	}

	void CheckShoot()
	{
		if ((Vector3.Distance(transform.position, Player.instance.transform.position) <= 20.0f) &&
				(room_manager.GetComponent<Room_manager>().nexto_find_position_door(room,player.GetComponent<Player>().room) != Vector2.zero || room == player.GetComponent<Player>().room))
					shoot = true;
	}

	public void attack()
	{
		fight = true;
	}
	// public float tmp;
	// Update is called once per frame
	void Update () {
		if (die || Player.instance.die || Player.instance.win)
		{
			rb.velocity = Vector2.zero;
			return ;
		}
		if (room != player.GetComponent<Player>().room && fight ==  true)
		{
				fight =  false;
				shoot =  true;
				posi = Vector2.zero;
		}
		if (room == player.GetComponent<Player>().room && shoot == true)
			{
					fight = true;
					shoot = false;
					posi = Vector2.zero;
			}
		//  if(shoot == true  && room == player.GetComponent<Player>().room)
		// {
		// 	fight =  true;
		// 	shoot =  false;
		// }
		if (fight == true && shoot == false)
		{
			shoot =  false;
			posi = Vector2.zero;
			 vel = Vector2.zero;
			 target_dir = player.transform.position - transform.position;
			vel = target_dir * 1;
				if (Vector2.Distance(transform.position, player.transform.position) < 4f)
				{
					rb.velocity = Vector2.zero;
					weapon.Shoot();
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
		else if (shoot)
		{
			// if (player_tmp_room != player.GetComponent<Player>().room)
			// {
			// 	room = player_tmp_room;
			// 	player_tmp_room = player.GetComponent<Player>().room;
			// 	posi = Vector2.zero;
				
			// }
			// 	posi = Vector2.zero;
			if(posi == Vector2.zero)
			{
				if (room == player.GetComponent<Player>().room)
				{
					fight = true;
					shoot = false;
					return;
				}
				posi = room_manager.GetComponent<Room_manager>().nexto_find_position_door(room,player.GetComponent<Player>().room);
				//room = player.GetComponent<Player>().room;
				player_tmp_room = player.GetComponent<Player>().room;
			}
			if (posi != Vector2.zero)
			{ 
				GetComponent<Patrouille>().enabled = false;

				vel = Vector2.zero;
				target_dir = posi - (Vector2) transform.position;
				vel = target_dir * speed;
				rb.velocity = new Vector2( Mathf.Clamp(vel.x,-4,4),Mathf.Clamp(vel.y,-4,4));
				float angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle + 90 , Vector3.forward);
				if (Vector2.Distance(transform.position, posi) < 0.2f){
					room = player_tmp_room;
			 		target_dir = player.transform.position - transform.position;
					angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.AngleAxis(angle +90 , Vector3.forward);
					shoot = false;
					if (room == player.GetComponent<Player>().room)
						fight =  true;
					else 
					{
						fight = false;
						shoot = true;
						player_tmp_room = player.GetComponent<Player>().room;
					}
					posi = Vector2.zero;
				}
			}
		}
	}
}
