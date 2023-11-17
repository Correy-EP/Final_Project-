using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData; 
    public float destroyAfterSeconds ; // Default value


    // current satats 

    protected float currentDamge;
    protected float currentSpeed;
    protected float currrentCoolDownDuration;
    protected int currentPierce;




    private void Awake()
    {
        currentDamge = weaponData.Damage; 
        currentSpeed = weaponData.Speed;
        currrentCoolDownDuration = weaponData.CoolDownDuration;
        currentPierce = weaponData.Pierce;

    }

    public float GetCurrentDamage()
    {
        return currentDamge *= FindObjectOfType<PlayerStats>().CurrentMight;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }


    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent <EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
        }
        else if (col.CompareTag("prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                
            }
        }
    }
}

