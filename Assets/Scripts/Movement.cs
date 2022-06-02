using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; }

    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private GameManager _gm;


    private void Awake()
    {
        startingPosition = this.transform.position;
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ResetState();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody.isKinematic = false;
        this.enabled = true;
    }
    private void Update()
    {
        if(this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }
    }

    

    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rigidbody.MovePosition(position + translation);
    }

    public Vector2 CalculateNewGhostDirection(Node node, Ghost ghost)
	{
        Vector2 directionToTravel = Vector2.zero; //set direction to zero (which is like null)
        Vector2 shortestDirectionToTarget = Vector2.zero;

        Dictionary<Vector2, float> availableDirectionsWithDistance = new Dictionary<Vector2, float>(); //stores the potential directions for AI decision making
        float minDistance = float.MaxValue; //set minDistance to highest possible value

        if(node.availableDirections.Count() > 1)
		{
            var previousDirection = -ghost.Movement.direction;
            node.availableDirections.Remove(previousDirection);
		}

        foreach(Vector2 direction in node.availableDirections) //check all available directions
        {
            Vector3 newPosition = this.transform.position + new Vector3(direction.x, direction.y, 0.0f); //get each new positions coordinates

            float distance = (ghost.target.position - newPosition).sqrMagnitude; //get the distnace of pacman (target) and our position option

            if (distance < minDistance) //assign new shortest distance
            {
                shortestDirectionToTarget = direction;
                minDistance = distance;
            }

            availableDirectionsWithDistance.Add(direction, distance);
        }
        
        if(node.availableDirections.Count() > 1)
		{
            if (ghost.ChooseSuboptimalDirection)
			{
                var suboptimalDirections = availableDirectionsWithDistance.Where(x => x.Value != minDistance).ToList();
                if (suboptimalDirections.Any())
				{
                    directionToTravel = suboptimalDirections.ElementAt(Random.Range(0, suboptimalDirections.Count() - 1)).Key;
                }
			}
			else
			{
                directionToTravel = shortestDirectionToTarget;
            }
		}
		else
		{
            directionToTravel = shortestDirectionToTarget;
		}

        ghost.TrackIfDirectionWasOptimal();

        return directionToTravel;
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one 
                * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }
}
