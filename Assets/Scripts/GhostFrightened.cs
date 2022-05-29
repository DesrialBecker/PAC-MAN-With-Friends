using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    [SerializeField] public Transform pinkyRespawnPoint;
    private GameManager _gm;
    private Ghost _ghost;

    public bool eaten;
    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pinkyRespawnPoint = _gm.pinkyRespawnPoint;
        _ghost = GameObject.Find("Ghost").GetComponent<Ghost>();
    }
    public override void Enable(float duration)
    {
        base.Enable(duration);
        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;
        Invoke(nameof(Flash), duration / 2f);
    }
    public override void Disable()
    {
        base.Disable();
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
    private void Eaten()
    {
        eaten = true;
        Vector3 positionPinky = this.pinkyRespawnPoint.transform.position;
        positionPinky.x = this.pinkyRespawnPoint.position.x;
        positionPinky.y = this.pinkyRespawnPoint.position.y;
        this.pinkyRespawnPoint.transform.position = positionPinky;
        ghost.SetPosition(positionPinky);
        _ghost.State = Ghost.GhostState.Home;
        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }
    private void OnEnable()
    {
        blue.GetComponent<AnimatedSprite>().Restart();
        ghost.movement.speedMultiplier = 0.5f;
        eaten = false;
    }
    private void OnDisable()
    {
        ghost.movement.speedMultiplier = 1f;
        eaten = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;
                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
            {
                Eaten();
            }
        }
    }
}
