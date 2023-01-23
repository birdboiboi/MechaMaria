using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    //public float health { get; set; }
    public void AddDmg(float dmg);
    public void RemoveDmg(float dmg);

    public void Die();

}

public interface IPushable
{
    public void SetTouch(bool state);
    public bool GetMovable();
    public void Push(Vector3 move);
    public void Push(Vector3 move, IPushable pusher);

}

public interface ICombatable:IDamagable
{
    public void StrikeHigh(ICombatable other);
    public void StrikeMid(ICombatable other);
    public void StrikeLow(ICombatable other);

    public int getBlockState();
    public void StopBlock();
    public void BlockHigh();
    public void BlockMid();
    public void BlockLow();
}
