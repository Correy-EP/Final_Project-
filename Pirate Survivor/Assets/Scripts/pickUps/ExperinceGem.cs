using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperinceGem : pickUp
{


    public int experinceGranted;
    public override void  Collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
        PlayerStats player =  FindObjectOfType<PlayerStats>();
        player. IncreaseExperience(experinceGranted);
      
    }


}
