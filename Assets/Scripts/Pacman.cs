using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{

    public Movement _movement;
    public Transform responConnection;
    public Animator _animator;
    public GameManager _gm;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _movement.SetDirection(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _movement.SetDirection(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _movement.SetDirection(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _movement.SetDirection(Vector2.right);
        }
        float angle = Mathf.Atan2(this._movement.direction.y, this._movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        this._movement.ResetState();
        this.gameObject.SetActive(true);

    }
    //public void Dying()
    //{
    //    Vector3 position = responConnection.transform.position;
    //    position.x = 0;
    //    position.y = -9.5;
    //    position.z = -5;
    //        Pacman.transform.position = position; 
    //}
    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.name == "Ghosts")
    //    {
    //        //if (this.state == "chase")
    //        UIManager.LoseLife();

    //        //Pacman.Component.transform.position = (0, -9.5, -5);
    //        // Add method to restart round with _Pellets and _PowerPellets
    //    }

    //}

}
