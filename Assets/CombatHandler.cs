using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour, ICombatable
{

    public float health = 100;
    public float[] dmgMap = { 10, 10, 10 }; // 0 = High, 1= Mid, 2= Low
    public float[] coolDownMap = { 1, 1, 1 };// in seconds, may remove in favor of animation times index 0 = High, 1= Mid, 2= Low
    public int blockState = -1; // -1 to 3, -1 = nothing, 0 = High, 1= Mid, 2= Low
    public Animator animPlayer;

    ICombatable otherCombatHandler = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GeneralStrike(int strikeType)
    {
        StopBlock();
        if (otherCombatHandler != null)
        {
            if (otherCombatHandler.getBlockState() != strikeType)
            {
                StartCoroutine(CoolTimeHard(strikeType));
            }
        }
    }

    void GetInput()
    {
        //Todo
        //Add more button Input mapping for what 

        if (Input.GetButton("fire1"))
        {
            StrikeMid(otherCombatHandler);
        }
    }

    IEnumerator CoolTimeHard(int strikeType)
    {
        otherCombatHandler.AddDmg(dmgMap[strikeType]);
        yield return new WaitForSeconds(coolDownMap[strikeType]);
    }

    void OnTriggerEnter(Collider col)
    {
        this.otherCombatHandler = col.gameObject.GetComponent<ICombatable>();
    }

    void OnTriggerExit(Collider other)
    {
        this.otherCombatHandler = null;
    }


    //***************ICOMBATABLE INTERFACE***************I
    public void AddDmg(float dmg)
    {
        this.health -= dmg;
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
        //TODO
        //AnimPlayer trigger animation for StrikeHigh
    }

    public void StrikeLow(ICombatable other)
    {
        int idx = 2;
        GeneralStrike(idx);
        //TODO
        //AnimPlayer trigger animation for StrikeHigh
    }



}
