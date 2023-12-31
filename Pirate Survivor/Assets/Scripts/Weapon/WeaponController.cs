using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // base script for all weapon 
    [Header("Weapon stats")]

    public WeaponScriptableObject weaponData;

    float currentCooldown;

    protected PlayerScript ps;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        ps = FindObjectOfType<PlayerScript>();
        currentCooldown = weaponData.CoolDownDuration;  // at the start set the current cooldown to be the  cooldown duration 
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <=0) // once the cooldown becomes 0, attack
        {
            Attack();
        }
    }


    protected virtual void Attack()
    {
        currentCooldown = weaponData.CoolDownDuration;
    }
}
