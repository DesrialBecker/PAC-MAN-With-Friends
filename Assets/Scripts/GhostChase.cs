using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior { 
//ghost behavior already handels enabling and disabling this so we just need to put what happens when they are enabled.

//Unity looks for OnDisable every time a script is diabled in an object

    private void OnDisable()
    {
        this.ghost.scatter.Enable();

    }

    //this gets the reference to the exact node we are colliding with.
    //after that we make sure that GhostScatter is enabled (this.enable) and ghost.frightened is disabled.
    //the reason we do this is ghost.frightened overrides all other ghost behaviors


    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;//this is setting direction to zero(which is like null) to start off with

            float minDistance = float.MaxValue;//this is setting the minDistance to the highest possiable value, and then we will
                                                // work our way down to the smallest distnace

            foreach(Vector2 availableDirection in node.availableDirections)//check all available directions
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                                                                    //get each new positions cordinates

                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;
                                                                    //get the distnace of pacman(target) and our position option
                
                if(distance < minDistance) //if the distnace is less than all previous distances make it the new minDistance
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);//assigns the best direction to ghost
        }
    }
}
