using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior { 

    //ghost behavior already handles enabling and disabling this so we just need to put what happens when they are enabled.
    //Unity looks for OnDisable every time a script is diabled in an object
    private void OnDisable()
    {
        this.ghost.Scatter.Enable();
    }

    //this gets the reference to the exact node we are colliding with.
    //after that we make sure that GhostScatter is enabled (this.enable) and ghost.frightened is disabled.
    //the reason we do this is ghost.frightened overrides all other ghost behaviors
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.Frightened.enabled)
        {
            var direction = this.ghost.Movement.CalculateNewGhostDirection(node, this.ghost);
            this.ghost.Movement.SetDirection(direction); //assigns direction to ghost
        }
    }

}
