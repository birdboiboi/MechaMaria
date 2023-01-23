using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovableHitbox : ApolarMove
{
    public Transform parent;
    public Vector3 polarOffset = Vector3.zero;


  
    // Update is called once per frame
    void Update()
    {
        PolarOffset();
    }

    void PolarOffset()
    {
        transform.position = Cyl2Cart(Cart2Cyl(parent.position) + polarOffset);
    }

    void PolarOffset(Vector3 parent)
    {
        transform.position = Cyl2Cart(Cart2Cyl(parent) + polarOffset);
    }


}
