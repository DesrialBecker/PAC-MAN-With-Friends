using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
	public static int pointValue = 10;
	private GameManager _gm;

	private void Start()
	{
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Pacman")
		{
			_gm.PelletEaten();
			Destroy(this.gameObject);
		}
	}
}
