using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public interface IPushable
{
    public void SetTouch(bool state);
    public bool GetMovable();
    public void Push(Vector3 move);
    public void Push(Vector3 move,IPushable pusher);

}
