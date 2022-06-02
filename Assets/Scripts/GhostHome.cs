using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;

    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();//this is just for safety to make sure that there is nothing that will ruin our kinematic.enable=false;
    }

    private void OnDisable()
    {
        StartCoroutine(ExitTransition());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost.Movement.SetDirection(-this.ghost.Movement.direction);
        }
    }
    private IEnumerator ExitTransition()
    {
        //the tru in this SetDirection means that the Movement is forced, not checked
        this.ghost.Movement.SetDirection(Vector2.up, true);

        //this turns off collision on the object
        this.ghost.Movement.rigidbody.isKinematic = true;
        this.ghost.Movement.enabled = false;

        //this is where the animation begins for the position of the ghost
        Vector3 position = this.transform.position;

        float duration = duration = 0.5f;

        float elapsed = 0.0f;

        //this loop is to go from this.position(will be somewhere in the box) =to inside.position
        while(elapsed < duration)
        {
                // we take the position of the ghost when inside of the box,and interpolate for elapsed/duration to get a time
                // once elapsed/duration=1 this will let us know that our position == this.inside.position, so we are ready to start exiting
                        //this is why we use the while loop
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed/duration);
            newPosition.z = position.z;// we dont want z to change
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;//we use deltatime because it takes in consideration Framerate
            yield return null;// with the deltatime being used we will recheck this while loop every frame
        }

        elapsed = 0.0f;//this lets us start the new Transform

        //this while loop is to go from inside.position => outside.position
        while (elapsed < duration)
        {
            // we take the position of the ghost when inside of the box,and interpolate for elapsed/duration to get a time
            // once elapsed/duration=1 this will let us know that our position == this.outside.position, so we are ready to start exiting
            //this is why we use the while loop
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;// we dont want z to change
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;//we use deltatime because it takes in consideration Framerate
            yield return null;// with the deltatime being used we will recheck this while loop every frame
        }





        this.ghost.Movement.enabled = false;
        // inside of Vecor2 we aare using random to check on a roll. If the value is greater than 0.5f then we set the direction
        // to left(1.0f) if the value is less than 0.5f we set the direction to right(0.0f)
        this.ghost.Movement.SetDirection(new Vector2(Random.value <= 0.5f ? 1.0f : 1.0f, 0.0f), true);
        this.ghost.Movement.rigidbody.isKinematic = false;
        this.ghost.Movement.enabled = true;
        this.ghost.Scatter.Enable();
       
    }
}
