using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMove : ApolarMove, IPushable,IDamagable
{


    public UnityEngine.Transform cam;
    public float movementSpeed = 1f;
    //public Rigidbody body;
    public PlayerMove other;
    public int dir;
    public int oldDir;
    public float jumpSpd = 10;
    public float gravity = -9.8f;
    public float airTime = 0;
    public float maxAirTime = 10;//s
    public Animator anim;
    public CharacterController characterController;

    private Stack<GameObject> touching = new Stack<GameObject>();

    public float health = 100;

    float IDamagable.health { get => this.health; set => this.health = value; }

    // Start is called before the first frame update
    void Start()
    {
        cylStart.z *= Mathf.Deg2Rad;
        //body = GetComponent<Rigidbody>();
        transform.position = axisRot.localToWorldMatrix.GetPosition() + Cyl2Cart(cylStart);

    }
    public float MoveUp()
    {

        if (airTime != maxAirTime / 2)
        {
            airTime += Time.deltaTime * 1;

        }
        else
        {
            airTime = -maxAirTime / 2;
        }

        return (.5f * (gravity) * Mathf.Pow(airTime, 2));
    }

    public int GetDir(Vector3 axis)
    {
        return (int)Mathf.Sign(CalcDir(transform.position, axis));
    }

    public new void Move(Vector3 trans)
    {
        Vector3 thisPos = transform.position;
        float tempF = trans.x;
        trans.x = cylStart.x - Cart2Cyl(thisPos).x;// trans.z; //cylStart.x;
        trans.z = tempF;//(unit vector anglle)
        
        Vector3 newPos = Cart2Cyl(thisPos) + trans;
        Vector3 de = Cyl2Cart(newPos) - thisPos;
        //Debug.Log(trans + "to move" + Cyl2Cart(newPos) + " " + transform.position + "change" + (de));

        characterController.Move(de);

    }
   //public void OnControllerColliderHit(ControllerColliderHit hit)
   //{
   //    Debug.Log("col" + hit.gameObject.name);
   //    if (hit.gameObject.tag == "Pushable")
   //    {
   //
   //        Debug.Log("pushable");
   //        IPushable obj = hit.gameObject.GetComponent<IPushable>();
   //        obj.Push(movement * movementSpeed * Time.deltaTime);
   //        
   //    }
   //  
   //
   //}
   //
    public void UnTouch()
    {
        
        for (int i = 0; i < touching.Count; i++)
        {
            Debug.Log("untouch" + touching.Peek());
            touching.Pop().GetComponent<IPushable>().SetTouch(false);
        }
    }

    public bool GetMovable()
    {
        return true;
    }

    public void Push(Vector3 move)
    {
        Move(move);
    }

    public void Push(Vector3 move, IPushable pusher)
    {
        Push(move);
    }

    public void SetTouch(bool state)
    {
        throw new System.NotImplementedException();
    }

    public void AddDmg(float dmg)
    {
        health -= dmg;
        if(health < 0)
        {
            Die();
        }
    }

    public void RemoveDmg(float dmg)
    {
        health += dmg;
    }

    public void Die()
    {
        StartCoroutine(DieDelay());
    }

    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
