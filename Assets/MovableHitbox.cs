using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovableHitbox : ApolarMove
{
    public PlayerMove PM;
    private Transform parent;
    public Vector3 rightPolarOffset = Vector3.zero;
    public Vector3 leftPolarOffset = Vector3.zero;



    private void Start()
    {
        parent = PM.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (PM.dir == -1)
        {
            PolarOnRight();
        }
        else
        {
            PolarOnLeft();
        }
        
    }

    void PolarOnRight()
    {
        transform.position = Cyl2Cart(Cart2Cyl(parent.position) + rightPolarOffset);
    }

    void PolarOnLeft()
    {
        transform.position = Cyl2Cart(Cart2Cyl(parent.position) + leftPolarOffset);
    }

    void PolarOffset(Vector3 parent)
    {
        transform.position = Cyl2Cart(Cart2Cyl(parent) + rightPolarOffset);
    }


}
