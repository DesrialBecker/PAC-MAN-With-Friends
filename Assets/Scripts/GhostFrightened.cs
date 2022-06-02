using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public bool isEaten { get; private set;}

    public override void Enable(float duration)
    {
        base.Enable(duration);
        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = true;

        Invoke(nameof(Flash), duration / 2.0f);

        
    }

    public override void Disable()
    {
        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }
    private void Flash()
    {
        {
            if (!this.isEaten)
            {
                this.blue.enabled = false;
                this.white.enabled = true;
                this.white.GetComponent<AnimatedSprite>().Restart();
            }
        }

    }

    private void Eaten()
    {
        this.isEaten = true;

        Vector3 position = this.ghost.Home.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;

        
        this.ghost.Home.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void OnEnable()
    {
        this.ghost.Movement.speedMultiplier = 0.5f;
        this.Enable();
    }
    private void OnDisable()
    {
        this.ghost.Movement.speedMultiplier = 1.0f;
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


        if(other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.ghost.Frightened.enabled)
            {
                Eaten();
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }

}
