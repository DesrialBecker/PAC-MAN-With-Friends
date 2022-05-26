using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : MonoBehaviour
{
	public static int pointValue = 50;
	private GameManager _gm;
	private void Start()
	{
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Pacman")
		{
			_gm.PowerPelletEaten();
			Destroy(this.gameObject);
		}
	}
}
