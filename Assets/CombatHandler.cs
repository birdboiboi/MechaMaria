using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour, ICombatable
{

    public float health = 100;
    public float[] dmgMap = { 10, 10, 10 }; // 0 = High, 1= Mid, 2= Low
    public float[] chargeTimeMap = { 1, 1, 1 };
    public float[] coolDownMap = { 1, 1, 1 };// in seconds, may remove in favor of animation times index 0 = High, 1= Mid, 2= Low
    public int blockState = -1; // -1 to 3, -1 = nothing, 0 = High, 1= Mid, 2= Low
    public Animator animPlayer;

    public CombatHandler otherCombatHandler;

    public bool striking;

    public GameObject hitEffect = null;


    // Start is called before the first frame update
    

    void GeneralStrike(int strikeType)
    {
        if (!striking)
        {
            striking = true;
            
            StartCoroutine(CoolTimeHard(strikeType));

        }

    }

    public void GetInput()
    {
        //Todo
        //Add more button Input mapping for what 

       
    }

    IEnumerator CoolTimeHard(int strikeType)
    {
        StopBlock();

        
        yield return new WaitForSeconds(chargeTimeMap[strikeType]);
        if (otherCombatHandler != null)
        {
            if (otherCombatHandler.getBlockState() != strikeType)
            {
                Debug.Log(this.name + "strikes" + otherCombatHandler);
                otherCombatHandler.AddDmg(dmgMap[strikeType]);
                Destroy(Instantiate(hitEffect, this.transform),1);
            }
        }
        

        striking = false;
        yield return new WaitForSeconds(coolDownMap[strikeType]);
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && col.name != animPlayer.gameObject.name)
        {
            this.otherCombatHandler = col.gameObject.GetComponent<CombatHandlerLinker>().attackBox;
            Debug.Log(col.name + "entered" + this.otherCombatHandler);
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            this.otherCombatHandler = null;
            Debug.Log(col.name + "exited");
        }
    }


    //***************ICOMBATABLE INTERFACE***************I
    public void AddDmg(float dmg)
    {
        this.health -= dmg;
        Instantiate(hitEffect);
        //TODO
        //AnimPlayer trigger animation for high block
        //no animation controller boolean needed, loop to begining idle
    }

    public int getBlockState()
    {
        return blockState;
    }

    public void StopBlock()
    {
        blockState = -1;
        //TODO
        //AnimPlayer set block loop boolean to true
    }

    public void BlockHigh()
    {
        blockState = 0;
        //TODO
        //AnimPlayer trigger animation for high block
        //AnimPlayer set block loop boolean to true
    }

    public void BlockMid()
    {
        blockState = 2;
        //TODO
        //AnimPlayer trigger animation for mid block
        //AnimPlayer set block loop boolean to true
        
    }
    public void BlockLow()
    {
        blockState = 1;
        //TODO
        //AnimPlayer trigger animation for low block
        //AnimPlayer set block loop boolean to true
    }

    public void Die()
    {
        throw new System.NotImplementedException();
        //TODO
        //kill self????
    }

    public void RemoveDmg(float dmg)
    {
        this.health += dmg;
        //TODO
        //AnimPlayer trigger animation for heal
        //no animation controller boolean needed, loop to begining idle

    }

    public void StrikeHigh(ICombatable other)
    {
        int idx = 0;
        GeneralStrike(idx);
        //TODO
        //AnimPlayer trigger animation for StrikeHigh

    }
    public void StrikeMid(ICombatable other)
    {
        int idx = 1;
        GeneralStrike(idx);
        animPlayer.Play("attack");
    }

    public void StrikeLow(ICombatable other)
    {
        int idx = 2;
        GeneralStrike(idx);
        //TODO
        //AnimPlayer trigger animation for StrikeHigh
    }



}
