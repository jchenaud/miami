﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = Vector3.zero;
	}
	
	void Update () {
		
	}
}
