using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> availableDirections { get; private set; }

    //this tells unity that Node lives on the Obstacle Layer;
    public LayerMask obstacleLayer;
    public void Start()
    {
        this.availableDirections = new List<Vector2>();

        // this is how the node is checked when the whole map begins. Nodes assign their available direcitons
        //at the start. If we did not do it this way, we could manually assign the nodes.
        //ALSO this will allow us to potentially create more nodes in the future,during gameplay.
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.right);
        CheckAvailableDirection(Vector2.left);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position,
            Vector2.one * 0.5f, 0.0f, direction, 1.5f, this.obstacleLayer);

        //if hit.collider is null that means that that direction is available. So we add that ot our direction list
        if(hit.collider == null)
        {
            this.availableDirections.Add(direction);
        }
    }
}
