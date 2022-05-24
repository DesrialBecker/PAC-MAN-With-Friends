using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : MonoBehaviour
{
	public static int pointValue = 50;
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Pacman")
		{
			Destroy(this.gameObject);
			UIManager.score += PowerPellet.pointValue;
		}
	}
}
