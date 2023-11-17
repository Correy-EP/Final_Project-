using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passiveItem : MonoBehaviour
{
    protected PlayerStats player;

    public passiveItemScriptableObject passiveItemData;


    protected virtual void ApplyModifier()
    {

    }
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }

   
}
