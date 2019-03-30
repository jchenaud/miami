using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
	public static GameUi instance;
	public Weapon weaponCurrent;
	public Text textNameWeapon;
	public Text textAmmoWeapon;

	void Awake()
	{
		instance = this;
		weaponCurrent = null;
	}
	
	void Update ()
	{
		if (weaponCurrent == null)
		{
			textNameWeapon.text = "No Weapon";
			textAmmoWeapon.text = "-";
		}
		else
		{
			textNameWeapon.text = weaponCurrent.nameWeapon;
			if (weaponCurrent.infinityAmmos)
				textAmmoWeapon.text = "∞";
			else
				textAmmoWeapon.text = weaponCurrent.ammos.ToString();
		}
	}
}
