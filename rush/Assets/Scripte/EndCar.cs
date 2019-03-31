using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCar : MonoBehaviour {
	bool moveCar;

	void Start ()
	{
		Player.onWinGameEvent += OnWin;
	}
	
	void Update ()
	{
		if (moveCar)
			transform.position += -transform.up * Time.deltaTime * 4.25f;
	}

	void OnWin()
	{
		Debug.Log("test");
		moveCar = true;
	}
}
