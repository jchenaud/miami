using System.Collections;
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

	public int room;
	public GameObject room_manager;

	public bool shoot;
	public Weapon weapon;

	AudioSource audioSource;
	Vector2 vel;
	Vector2 target_dir;
	Vector2 posi;
	void Start ()
	{
		fight = false;
		player  = GameObject.Find("player");
		room_manager  = GameObject.Find("Room_manager");

		Debug.Log(player);
		rb = GetComponent<Rigidbody2D>();
		head.sprite = listHead[Random.Range(0, listHead.Count - 1)];
		body.sprite = listBody[Random.Range(0, listBody.Count - 1)];
		audioSource = GetComponent<AudioSource>();
		Player.onShootGameEvent += CheckShoot;
	}

	public void Die()
	{
		AudioClip clip = listSoundDie[Random.Range(0, listSoundDie.Count - 1)];
		weapon.Throw(transform.position);
		weapon.ennemy = false;
		head.sprite = null;
		body.sprite = null;
		Destroy(this.gameObject, clip.length);
	}

	void CheckShoot()
	{
		if (Vector3.Distance(transform.position, Player.instance.transform.position) <= 5.0f)
			shoot = true;
	}

	public void attack()
	{
		fight = true;
	}
	// public float tmp;
	// Update is called once per frame
	void Update () {
		if (room != player.GetComponent<Player>().room && fight ==  true)
		{
				fight =  false;
				shoot =  true;
				posi = Vector2.zero;
		}
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
			if(posi == Vector2.zero)
			{
				posi = room_manager.GetComponent<Room_manager>().nexto_find_position_door(room,player.GetComponent<Player>().room);
				room = player.GetComponent<Player>().room;
			}
			print(posi);
			if (posi != Vector2.zero)
			{
				print("coucou");
				vel = Vector2.zero;
				target_dir = posi - (Vector2) transform.position;
				vel = target_dir * speed;
				rb.velocity = new Vector2( Mathf.Clamp(vel.x,-4,4),Mathf.Clamp(vel.y,-4,4));
				float angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle + 90 , Vector3.forward);
				if (Vector2.Distance(transform.position, posi) < 0.2f){
			 		target_dir = player.transform.position - transform.position;
					angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.AngleAxis(angle +90 , Vector3.forward);
					shoot = false;
					if (room == player.GetComponent<Player>().room)
						fight =  true;
					posi = Vector2.zero;
				}
			}

		}
	}
}
