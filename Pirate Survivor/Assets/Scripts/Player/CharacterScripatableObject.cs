using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterScriptObject", menuName = "ScriptableObjects/characters")]
public class CharacterScripatableObject :  ScriptableObject
{
    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }


    [SerializeField]
    new string name;
    public string  Name { get => name; private set => name = value; }

    [SerializeField]
    GameObject startingweapon; 
    public GameObject Startingweapon { get => startingweapon; private set => startingweapon = value; }

     
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float recovery; 
    public float Recovery { get => recovery; private set => recovery = value; }

    [SerializeField]

    float movespeed;
    public float MoveSpeed { get => movespeed; private set => movespeed = value; }

    [SerializeField]
    float might; 
    public float Might { get => might; private set => might = value; }

    [SerializeField]
    float  projectilespeed; 

    public float Projectilespeed { get => projectilespeed; private set => projectilespeed = value; }

    [SerializeField]
    float magnet;
    public float Magnet { get => magnet; private set => magnet = value; }


    



}
