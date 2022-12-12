using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.ScrollRect;

public class SecondaryPlayer : PlayerMove
{

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(this + "pos polar" + Mathf.Rad2Deg * Cart2Cyl(transform.position - axisRot.position).z);

        transform.LookAt(new Vector3(cam.position.x, transform.position.y, cam.position.z));
        //this.isMoving = (movement != Vector3.zero);
        movement = new Vector3(.5f * Mathf.Sin(Time.time), 0, 0) * movementSpeed;
        oldDir = dir;




        movement.y += gravity;
        if (movement.x != 0)
        {
            dir = -(int)Mathf.Sign(Input.GetAxis("Horizontal"));
            anim.SetBool("isWalk", true);
        }
        else
        {
            dir = 0;
            anim.SetBool("isWalk", false);
        }

        if (Input.GetButton("Fire1"))
        {
            anim.Play("attack");
        }


        if (canMove)
        {
            Move(movement * Time.deltaTime);
        }
        
    }
}
