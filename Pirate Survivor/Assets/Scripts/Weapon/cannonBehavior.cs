using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonBehavior : ProjectileWeaponBehavior
{
    cannonController cc;
    protected override void Start()
    {
        base.Start();
        cc =  FindObjectOfType<cannonController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * cc.speed * Time.deltaTime; // setting the movement of the cannon 
    }
}
