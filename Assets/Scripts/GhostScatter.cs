using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehavior
{
    //ghost behavior already handels enabling and disabling this so we just need to put what happens when they are enabled.

    //Unity looks for OnDisable every time a script is diabled in an object
    private void OnDisable()
    {
        this.ghost.Chase.Enable();
    }

    //this gets the reference to the exact node we are colliding with.
    //after that we make sure that GhostScatter is enabled (this.enable) and ghost.frightened is disabled.
    //the reason we do this is ghost.frightened overrides all other ghost behaviors


    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if(node != null && this.enabled && !this.ghost.Frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);//moves a random direction that is available

            if(node.availableDirections[index] == -this.ghost.Movement.direction && node.availableDirections.Count > 1) //do not allow returning in the direction they came from
            {
                index++;
                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }
            this.ghost.Movement.SetDirection(node.availableDirections[index]);//this is where the movement finally happens
        }
    }
}
