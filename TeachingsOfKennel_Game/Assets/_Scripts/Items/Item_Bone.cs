using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bone : ItemBase
{
    private int[] dogIndexs = new int[] { 0 };
    private StatType[] statTypes = new StatType[] { StatType.barkStrength };
    private DogDataStruct additionalStats = new DogDataStruct(){
        faith = 0,
        barkStrength = 100,
        barkSpeed = 0,
        dogSpeed = 0
    };

    protected override void ActivateItem(){
        print("here");
        Game_Engine.instance.GetPlayerPack().ChangeDogsStatsTemp(dogIndexs, statTypes, additionalStats);
    }
}
