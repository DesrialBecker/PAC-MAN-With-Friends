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

        List<KeyValuePair<Vector2, float>> availableDirectionsWithDistance = new List<KeyValuePair<Vector2, float>>(); //stores the potential directions for AI decision making
        if(node.availableDirections.Count() > 1)
		{
            var previousDirection = -ghost.Movement.direction;
            node.availableDirections.Remove(previousDirection);
		}

        //find optimal direction and populate available direction list
        float minDistance = float.MaxValue; //set minDistance to highest possible value since all comparisons will be of smaller numbers
        float maxDistance = float.MinValue;
        Vector2 optimalDirectionToTarget = Vector2.zero;
        foreach(Vector2 direction in node.availableDirections)
        {
            Vector3 newPosition = this.transform.position + new Vector3(direction.x, direction.y, 0.0f); //get each new positions coordinates

            float distance = (ghost.target.position - newPosition).sqrMagnitude; //get the distnace of pacman (target) and our position option

            if(ghost.Chase.enabled && distance < minDistance)
			{
                optimalDirectionToTarget = direction;
                minDistance = distance;
			}
            if (ghost.Frightened.enabled && distance > maxDistance)
            {
                optimalDirectionToTarget = direction;
                maxDistance = distance;
            }

            availableDirectionsWithDistance.Add(new KeyValuePair<Vector2, float>(newPosition, distance));
        }
        
        //choose direction based on difficulty settings
        if(node.availableDirections.Count() == 1 || !ghost.ChooseSuboptimalDirection) //if there is only 1 direction or the int-your-team flag isn't set
		{
            directionToTravel = optimalDirectionToTarget;
        }
		else
		{
            List<KeyValuePair<Vector2, float>> suboptimalDirections = new List<KeyValuePair<Vector2, float>>();
            if (ghost.Chase.enabled)
            {
                suboptimalDirections = availableDirectionsWithDistance.Where(x => x.Value != minDistance).ToList();
            }
            if (ghost.Frightened.enabled)
            {
                suboptimalDirections = availableDirectionsWithDistance.Where(x => x.Value != maxDistance).ToList();
            }

            if (suboptimalDirections.Any())
            {
                directionToTravel = suboptimalDirections.ElementAt(Random.Range(0, suboptimalDirections.Count() - 1)).Key;
            }
        }

        ghost.TrackIfDirectionWasOptimal(); //adjust direction decision flag for difficulty

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
