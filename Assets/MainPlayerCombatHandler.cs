using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerCombatHandler : CombatHandler
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    public new void GetInput()
    {
        if (Input.GetButton("Fire1"))
        {
            StrikeMid(otherCombatHandler);
        }

}    // Start is called before the first frame update
    
}
