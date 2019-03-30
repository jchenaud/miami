using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
	public static GameUi instance;
	public Weapon weaponCurrent;
	public Text textNameWeapon;
	public Text textAmmoWeapon;
	public Text textNextLevel;
	public GameObject pannelClear;

	void Awake()
	{
		instance = this;
		weaponCurrent = null;
	}

	void Start()
	{
		Player.onWinGameEvent += OnWin;
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

	public void OnWin()
	{
		pannelClear.SetActive(true);
		if (SceneManager.GetActiveScene().buildIndex > SceneManager.sceneCount)
			textNextLevel.text = "Menu";
	}

	public void NextLevel()
	{
		Scene scene = SceneManager.GetActiveScene();
		int buildIndex = scene.buildIndex + 1;
		SceneManager.LoadScene(buildIndex);
	}
}
