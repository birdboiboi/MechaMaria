using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainPlayer : PlayerMove
{
    
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(this + "pos polar" + Mathf.Rad2Deg * Cart2Cyl(transform.position - axisRot.position).z);
        transform.LookAt(new Vector3(cam.position.x, transform.position.y, cam.position.z));
        movement = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, 0, 0);

        if (characterController.isGrounded && movement.y < 0)
        {
            movement.y = 0f;
        }

        if (Input.GetButton("Jump") )
        {
            movement.y += Mathf.Sqrt(-jumpSpd * gravity);
        }


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
