using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }
    public Transform responConnection;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right);
        }
        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
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
