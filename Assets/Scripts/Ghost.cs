using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public static int pointValue { get; set; } = 200;
	public GhostState state { get; set; } = GhostState.Waiting;
	/* chase= normal mode
	 * afraid=PowerPellet mode
	 * dead = Returning to box mode
	 * waiting = waiting to get out of box mode
	*/
	private GameManager _gm;

	private void Start()
	{
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Pacman")
		{
			if (this.state == GhostState.Afraid)
			{
				_gm.GhostEaten();
				//this.Reset(); //cannot use this until respawn is added, otherwise the ghost is deactivated and immediately reactivated in the same location with Waiting state and kills pacman
				Destroy(this.gameObject);
			}
			else
			{
				_gm.PacmanEaten();
			}
		}

	}

	public void Reset()
	{
		this.gameObject.SetActive(false);
		//TODO: location to spawn
		this.gameObject.SetActive(true);
		this.state = GhostState.Waiting;
	}

	public enum GhostState
	{
		Chase,
		Afraid,
		Waiting
	}
}
