using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AleBreathBehavoir : meleeWeaponBehavior
{

    List<GameObject> markedEnemies;

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }



    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (markedEnemies != null && col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());


            markedEnemies.Add(col.gameObject); // MARK THE ENEMY  so it doesnt take more damnage 

        }
        else if (col.CompareTag("prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable) && !markedEnemies.Contains(col.gameObject))
            {
                breakable.TakeDamage(GetCurrentDamage());
                

                markedEnemies.Add(col.gameObject) ;
            }
        }
    }
}
