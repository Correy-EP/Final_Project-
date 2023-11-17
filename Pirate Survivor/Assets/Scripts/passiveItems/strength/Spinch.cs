using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinch : passiveItem
{ 
    protected override void ApplyModifier()
    {
        player.CurrentMight *= 1 + passiveItemData.Multipler / 100f;
    }
}
