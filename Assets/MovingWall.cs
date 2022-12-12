using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class MovingWall : ApolarMove, IPushable
{
    public bool isLeader = false;
    public MovingWall other;
    public float angleOffset = 160;
    public bool isTouch = false;


    // Start is called before the first frame update
    void Start()
    {
        cylStart.z *= Mathf.Deg2Rad;
        transform.position = axisRot.localToWorldMatrix.GetPosition() + Cyl2Cart(cylStart);
        canMove = true;
        //angleOffset = Cart2Cyl(transform.position).z - Cart2Cyl(other.transform.position).z;
    }



    // Update is called once per frame
    void Update()
    {
        transform.LookAt(axisRot);

        //if (!other.isLeader)
        //{
        //    isLeader = isMoving;
        //    //isTouch = isMoving;
        //}



    }

    public void Push(Vector3 move)
    {
        Debug.Log(this + "is pushed");
        if (isTouch && other.isTouch)
        {
            canMove = false;
        }
        else
        {
            if (!other.isLeader)
            {
                isTouch = true;
                Debug.Log("playa stay");
                isLeader = true;
                Move(move);
                other.Move(move);
                other.GetComponent<Collider>().isTrigger = true;
                canMove = false;
            }
            else
            {
                //isTouch = true;
                Move(move);
            }
        }



    }

    public void Push(Vector3 move, IPushable pusher)
    {
        if (!isLeader)
        {
            pusher.Push(move);
        }
        else
        {
            Move(move);
            other.Push(move);
        }
    }

    public bool GetMovable()
    {
        return canMove;
    }

    public void SetTouch(bool state)
    {
        this.isTouch = state;
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMove pm = other.gameObject.GetComponent<PlayerMove>();
            Debug.Log("other" + other);
            isTouch = true;
            if (!this.other.isLeader && !this.other.isTouch)
            {
                this.isLeader = true;
                
                Push(Vector3.Scale(pm.movement ,Vector3.right)* pm.movementSpeed * Time.deltaTime);
               
            }
            else 
            {

                pm.Push(-Vector3.Scale(pm.movement, Vector3.right) * pm.movementSpeed * Time.deltaTime * 1.1f);
            }
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMove pm = other.gameObject.GetComponent<PlayerMove>();
            Debug.Log("other" + other);
            isTouch = false;
            if (!this.other.isLeader)
            {
                this.isLeader = false;
                
            }

        }
    }
}
