using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        this.ghost.Scatter.Enable();
    }

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
