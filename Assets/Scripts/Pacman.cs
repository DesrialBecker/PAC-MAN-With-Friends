using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public Movement Movement;
    public Transform responConnection;

    private void Start()
    {
        Movement = GetComponent<Movement>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Movement.SetDirection(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Movement.SetDirection(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Movement.SetDirection(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Movement.SetDirection(Vector2.right);
        }
        float angle = Mathf.Atan2(this.Movement.direction.y, this.Movement.direction.x);
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
