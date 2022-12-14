using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamagable
{
    public float health { get; set; }
    public void AddDmg(float dmg);
    public void RemoveDmg(float dmg);

    public void Die();
   
}
public class MovableHitbox : ApolarMove
{
    public Transform parent;
    public Vector3 polarOffset = Vector3.zero;

    public bool canDealDmg = false;
    public float dmg = 10;
  
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IDamagable>() as IDamagable != null){
            other.gameObject.GetComponent<IDamagable>().AddDmg(this.dmg);
        }

    }

}
