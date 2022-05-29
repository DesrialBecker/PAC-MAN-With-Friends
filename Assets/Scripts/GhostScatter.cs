using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehavior
{
    public void OnDisable(){
        ghost.State = Ghost.GhostState.Chase;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && this.ghost.State != Ghost.GhostState.Frightened)
        {
            int index = Random.Range(0, node.availableDirections.Count);


            if (node.availableDirections[index] == -ghost.movement.direction && node.availableDirections.Count > 1)
            {
                index++;

                if(index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }
            ghost.movement.SetDirection(node.availableDirections[index]);
        }

    }
}
