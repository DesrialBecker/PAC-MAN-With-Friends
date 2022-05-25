using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public int points = 200;
    public string state = "chase";
    /* chase= normal mode
	 * afraid=PowerPellet mode
	 * dead = Returning to box mode
	 * waiting = waiting to get out of box mode
	*/

    void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Pacman" && state == "chase")
		{
				UIManager.lives -= 1;
		}
	}


}

//HELLO I LOVE COMMENTS