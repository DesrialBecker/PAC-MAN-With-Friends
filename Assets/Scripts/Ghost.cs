using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	//I changed this script to hold references to all of the other ghost behaviors. I think keeping logic outside of these base classes is a good idea. Not sure to be honest though -CM 6/1/2022

	public int OptimalDirectionThreshold { get; set; } = 2; //Putting this in ghost because it allows for ghosts of different intelligence. Sets number of optimal paths before sub-optimal path chosen
	public int TurnsSinceLastSuboptimalDirection { get; set; }
	public bool ChooseSuboptimalDirection { get; private set; }
	public Movement Movement { get; private set; }
	public GhostHome Home { get; private set; }
	public GhostScatter Scatter { get; private set; }
	public GhostChase Chase { get; private set; }
	public GhostFrightened Frightened { get; private set; }

	public GhostBehavior initialBehavior;
	public Transform target; //This is the object that the ghost will be targeting -CM 6/1/2022
	public int PointValue { get; set; } = 200;

	//public GhostState State;

	private void Awake()
	{
		this.Movement = GetComponent<Movement>();
		this.Home = GetComponent<GhostHome>();
		this.Scatter = GetComponent<GhostScatter>();
		this.Chase = GetComponent<GhostChase>();
		this.Frightened = GetComponent<GhostFrightened>();
		_gm = GetComponent<GameManager>();
		//State = GhostState.Waiting;
	}

	
	private void Start() 
	{
		ResetState();
	}

	public void ResetState()
	{
		gameObject.SetActive(true);
		initialBehavior.Enable();
		movement.ResetState();

		frightened.Disable();
		chase.Disable();
		scatter.Enable();

		TurnsSinceLastSuboptimalDirection = 0;
		ChooseSuboptimalDirection = false;

		if (home == this.initialBehavior)
		{
			this.home.Enable();
		}

		if (this.home != this.initialBehavior)
        {
			this.home.Disable();
        }

		if(this.initialBehavior != null) 
		{
			this.initialBehavior.Enable();	
		}
	}

	public void SetPosition(Vector3 position)
	{
		// Keep the z-position the same since it determines draw depth
		position.z = transform.position.z;
		transform.position = position;
	}
	private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.Frightened.enabled)
            {
				FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
				FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }

	//ghosts will choose a lesser direction every x turns as set by difficulty level
	public void TrackIfDirectionWasOptimal()
	{
		if(this.ChooseSuboptimalDirection) //should only hit this after it has taken the best path amongst other options
		{
			this.ChooseSuboptimalDirection = false;
		}

		if(TurnsSinceLastSuboptimalDirection != OptimalDirectionThreshold)
		{
			this.TurnsSinceLastSuboptimalDirection++;
		}
		else
		{
			this.ChooseSuboptimalDirection = true;
			this.TurnsSinceLastSuboptimalDirection = 0;
		}
	}

    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //	if (collider.gameObject.name == "Pacman")
    //	{
    //		if (this.frightened)
    //		{
    //			_gm.GhostEaten();
    //			//this.Reset(); //cannot use this until respawn is added, otherwise the ghost is deactivated and immediately reactivated in the same location with Waiting state and kills pacman
    //			Destroy(this.gameObject);
    //		}
    //		else
    //		{
    //			_gm.PacmanEaten();
    //		}
    //	}

    //}

    public void Reset()
	{
		this.gameObject.SetActive(false);
		//TODO: location to spawn
		this.gameObject.SetActive(true);
		//this.State = GhostState.Waiting;
	}

	//public enum GhostState
	//{
	//	Chase,
	//	Afraid,
	//	Waiting,
	//	Dead
	//}
}
