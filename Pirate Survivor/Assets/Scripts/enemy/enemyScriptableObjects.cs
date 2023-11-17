using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= " EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]

public class enemyScriptableObjects : ScriptableObject
{
    //Base stats for the enemy


    [SerializeField]
     float movespeed;
    public float MoveSpeed { get => movespeed; set { movespeed = value;} }


    [SerializeField]
    float MaxHealth;

    public float MAXHEALTH { get => MaxHealth; set {  MaxHealth = value;} }



    [SerializeField]
    float damage;

    public float Damage { get => damage; set { damage = value;} }
}
