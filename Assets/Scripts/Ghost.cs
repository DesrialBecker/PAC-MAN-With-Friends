using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public static int pointValue { get; set; } = 200;

	public GhostState State;
	private GameManager _gm;
	public Movement movement;
	public Transform target;

	private void Awake()
	{
		State = GhostState.Waiting;
		movement = GetComponent<Movement>();
	}
	private void Start()
	{
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Pacman")
		{
			if (this.State == GhostState.Frightened)
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
		this.State = GhostState.Waiting;
	}
	    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }

	public enum GhostState
	{
		Chase,
		Frightened,
		Waiting,
		Dead,
		Scatter,
		Home,
	}
}