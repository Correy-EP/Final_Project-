using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }



    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedCannon = Instantiate(prefab);
        spawnedCannon.transform.position  = transform.position;  // assign the postion to be the same as this object which is parented to the player 
        spawnedCannon.GetComponent<cannonBehavior>().DirectionChecker(ps.moveDir); //refrence the player direction
    }
   
}
