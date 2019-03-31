using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrouille : MonoBehaviour {

	// Use this for initialization

	public  GameObject patrouille_lst;
	Rigidbody2D  rb;

	 bool im_arrive = true;
	 bool acend = false;

	 int i = -1;

	public List <Transform> pat_point;
	Vector2 target_dir;

	Vector2 vel;

	public float speed;
	void Start () {
		// GameObject [] all = patrouille_lst.Get<GameObject>();
		pat_point = new List<Transform>();
		foreach(Transform g in patrouille_lst.transform)
		{
			pat_point.Add(g);

		}
			rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!GetComponent<Ennemi>().fight)
		{
			if (im_arrive)
			{
				im_arrive = false;
				if(acend)
				{
					acend = false;
					i--;
				}
				else
				{
					acend = true;
					i++;
				}
			}
			if(acend)
			{
				vel = Vector2.zero;
				target_dir = (Vector2)pat_point[i].position - (Vector2)(transform.position);
				vel = target_dir.normalized * 1;
				rb.velocity = vel;
				
				float angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle + 90 , Vector3.forward);
				if (Vector2.Distance(transform.position, pat_point[i].position) < 0.2f)
				{
			
					if(pat_point.Count - 1 == i)
						im_arrive = true;
					i++;

				}
			}
			else
			{
				vel = Vector2.zero;
				target_dir = (Vector2)pat_point[i].position - (Vector2)(transform.position);
				vel = target_dir.normalized * 1;
				rb.velocity = vel * speed;

				float angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle + 90 , Vector3.forward);
				if (Vector2.Distance(transform.position, pat_point[i].position) < 0.2f)
				{
					if(i <= 0)
						im_arrive = true;
					else
						i--;

				}
			}
		}

	

		
	}
}
