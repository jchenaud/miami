using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour {
	public List<Sprite> listSprites;

	void Start () {
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = listSprites[Random.Range(0, listSprites.Count)];
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
	}
}
