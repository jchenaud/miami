using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_manager : MonoBehaviour {

	// Use this for initialization

	// public GameObject depart;
	// public GameObject cible;

	public List<GameObject> all_room;
	//public int conect;
	void Start () {
		 for (int i = 0; i < transform.childCount; i++) 
         {
             if(transform.GetChild(i).gameObject.tag == "room")
             {
                 all_room.Add(transform.GetChild(i).gameObject);
             }
                 
         }
		// all_room = transform.FindChild();
		
	}
	
	// // Update is called once per frame
	// void Update () {
		
	// }

	List<int> find_connection(GameObject room)
	{
		List <int> l = new List<int>();
		for (int i = 0; i < transform.childCount; i++) 
         {
             if(transform.GetChild(i).gameObject.tag == "door_chekpoint")
             {
				 l.Add(transform.GetComponent<Room_checkpoint>().room_conect);
                //  all_room.Add(transform.GetChild(i).gameObject);
             }
                 
         }
		return l;
	}

	public Vector2  nexto_find_position_door(int depart,  int cible) // si pas a coter la fuction renvoie Vector2.positiveInfinity
	{
		GameObject child;

		List <int> l  = new List<int>();//find_connection(all_room[depart]);
		// if(l.Contains(cible) == true)
		// {
			for (int i = 0; i < transform.GetChild(depart).transform.childCount; i++) 
			{
				child = transform.GetChild(depart).transform.GetChild(i).gameObject;
					if(child.tag == "door_chekpoint" && child.GetComponent<Room_checkpoint>().room_conect == cible)
					{

						return(child.transform.position);
					}

			}
			return(Vector2.positiveInfinity);

	}

	// List<int> find_road(int depart,  int cible)
	// {
	// 	List<int> road =  new List<int>();
	// 	 List<List<int>>  all_conect = new List<List<int>>();
	// 	foreach (GameObject room in all_room)
	// 	{
	// 	 	all_conect.Add(find_connection(room));
	// 	}
	// 	road.Add(depart);
	// 	//int place = depart;
	// 	// foreach(List<List<int>> con in all_conect)
	// 	// for (int i = 0; i < all_conect.Count ; i++) 
	// 	foreach(int i in all_conect[depart])
	// 	{
			
	// 		foreach(int k in all_conect[i])
	// 		{

	// 		}
	// 	}


	// }
}
