using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {

	// Use this for initialization

	GameObject player;
	public float D;
	public float min_d;
	public float max_d;
	public float angle;

	public float fov;

	void Start () {
		player  = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Debug.Log(player);
		D =  Vector2.Distance((Vector2)transform.position,(Vector2)player.transform.position);
		if(D > max_d) // si le joueur et trop loin 
			return;
		if(D < min_d) // si joeur tres proche
			Debug.Log("atack");
		else // si joeur et potentiellement visible
		{
			Vector2 targetDir = player.transform.position - transform.position;
        	Vector2 forward = transform.up;
			 angle = (Vector2.SignedAngle(targetDir, forward)) ;
			//angle  = Vector2.SignedAngle(transform.position,  player.transform.position - transform.position);
			// Debug.Log(angle);
			if(angle >= 180-fov || angle <= -180 + fov)
			{
				RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
				if(hit && hit.transform.tag == "Player")
				{
					Debug.Log("atack");
				}
				else
					Debug.Log("i cant see nothing : somthing betwen");
			}
			else
				Debug.Log("i cant see nothing");
		}
		
	}
}
