using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
	//I changed this script to hold references to all of the other ghost behaviors. I think keeping logic outside of these base classes is a good idea. Not sure to be honest though -CM 6/1/2022

	public Movement movement { get; private set; }
	public GhostHome home { get; private set; }
	public GhostScatter scatter { get; private set; }
	public GhostChase chase { get; private set; }
	public GhostFrightened frightened { get; private set; }

	public GhostBehavior initialBehavior;
	//This is the object that the ghost will be targeting -CM 6/1/2022
	public Transform target;
	public int pointValue { get; set; } = 200;


	//public GhostState State;
	private GameManager _gm;

	private void Awake()
	{
		this.movement = GetComponent<Movement>();
		this.home = GetComponent<GhostHome>();
		this.scatter = GetComponent<GhostScatter>();
		this.chase = GetComponent<GhostChase>();
		this.frightened = GetComponent<GhostFrightened>();
		_gm = GetComponent<GameManager>();

		//State = GhostState.Waiting;
	}

	
	private void Start() {
		ResetState();
	}
	public void ResetState()
	{
		this.gameObject.SetActive(true);
		this.movement.ResetState();

		this.frightened.Disable();
		this.chase.Disable();
		this.scatter.Enable();
		this.home.Disable();
		if(this.home != this.initialBehavior)
        {
			this.home.Disable();
        }
		if(this.initialBehavior != null) 
		{
			this.initialBehavior.Enable();	
		}
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
				FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
				FindObjectOfType<GameManager>().PacmanEaten();
            }
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
