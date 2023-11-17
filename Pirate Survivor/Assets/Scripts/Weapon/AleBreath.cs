using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AleBreath : WeaponController
{
    // Start is called before the first frame update
    protected override  void Start()
    {
        base.Start(); 
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedAleBreath = Instantiate(weaponData.Prefab);

        spawnedAleBreath.transform.position = transform.position; // assiging the same location as the player 
        spawnedAleBreath.transform.parent = transform;
    }
}
