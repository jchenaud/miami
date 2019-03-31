using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Detection : MonoBehaviour {

	// Use this for initialization

	GameObject player;
	public float D;
	public float min_d;
	public float max_d;
	public float angle;

	public float fov;
	public Action Fight;
	Ennemi ennemi;
	int layer_mask;

	void Start () {
		player  = GameObject.FindGameObjectWithTag("Player");
		ennemi = gameObject.GetComponent<Ennemi>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(ennemi.die)
			return;
		D =  Vector2.Distance((Vector2)transform.position,(Vector2)player.transform.position);
		if(D > max_d) // si le joueur et trop loin 
			return;
		if(D < min_d && ennemi.room == Player.instance.room) // si joeur tres proche
			ennemi.attack(); //Debug.Log("atack");
		else // si joeur et potentiellement visible
		{
			Vector2 targetDir = player.transform.position - transform.position;
        	Vector2 forward = transform.up;
			 angle = (Vector2.SignedAngle(targetDir, forward)) ;
			if(angle >= 180-fov || angle <= -180 + fov)
			{
				layer_mask = ~(LayerMask.GetMask("enemi","room","weapon"));
				RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position,Mathf.Infinity,layer_mask);
				// Debug.Log(hit.transform.name);
				if(hit && hit.transform.tag == "Player")
				{
					// if (ennemi.room == Player.instance.room)
						ennemi.attack(); 
					// else
					// 	ennemi.shoot = true;

				}
				// else
				//  	Debug.Log("i cant see nothing : somthing betwen  : " + hit.transform.name);
			}
			// else
			// 	Debug.Log("i cant see nothing");
		}
		
	}
}
