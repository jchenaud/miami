using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	// GameObject player
	// // // Use this for initialization
	// void Start () {
	// 	player  = GameObject.FindGameObjectWithTag[]
	// }
	public int room_number;
	
	void OnTriggerEnter2D(Collider2D colider)
	{
		if (colider.transform.tag == "Player")
		{
			colider.GetComponent<Player>().room =  room_number;
		}
		else if(colider.transform.tag == "enemi")
		{
			colider.GetComponent<Ennemi>().room =  room_number;

		}
	}
	// // Update is called once per frame
	// void Update () {
		
	// }

}
