using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ApolarMove : MonoBehaviour 
{
    public bool canMove = true;
    public Vector3 cylStart = new Vector3(1, 20, 2);
    public Quaternion rotOffset;
    public Transform axisRot;
    public Vector3 movement = new Vector3(0, 0, 0);

    public bool isMoving { get; set; }
    public bool addedToStack { get; set; }
    public static Vector3 Cyl2Cart(Vector3 polarIn)
    {
        // rad, z, phi(radians)
        return new Vector3(polarIn.x * Mathf.Cos(polarIn.z),
                              polarIn.y,
                              polarIn.x * Mathf.Sin(polarIn.z ));
    }
    public static Vector3 Cart2Cyl(Vector3 CartIn)
    {
        // x,y,z
        Vector2 xyVect = new Vector2(CartIn.x, CartIn.z);
        //Debug.Log("angle" + Mathf.Atan2(xyVect.y, xyVect.x));
        return new Vector3(xyVect.magnitude,
                            CartIn.y,
                            Mathf.Atan2(xyVect.y, xyVect.x)  // rad out
                            );


    }

    public  float CalcDir(Vector3 playerToCheck, Vector3 midPoint)
    {
        Vector3 diff1 = (midPoint - axisRot.position);
        Vector3 diff2 = (playerToCheck - axisRot.position);
        return ((Cart2Cyl(diff1).z - Cart2Cyl(diff2).z) * Mathf.Rad2Deg) % 90;
      
    }

  


    //trans = <horizonatl,vertical,radius depth>
    public  void   Move(Vector3 trans)
    {
        
        isMoving = trans.magnitude==0;
        Vector3 curPos = Cart2Cyl(transform.position - axisRot.position);
        // Debug.Log("polar Pos" + curPos);
        //Debug.Log("new cart" + Cyl2Cart(curPos));
        curPos.z += trans.x;
        curPos.x = cylStart.x;
        curPos.y += trans.y;
        transform.position = Cyl2Cart(curPos) + axisRot.position;
        Debug.Log(this + isMoving.ToString() + "moves" + trans);

    }



}
